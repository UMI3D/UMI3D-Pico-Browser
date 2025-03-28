﻿/*
Copyright 2019 - 2021 Inetum
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
    http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System.Collections;
using System.Collections.Generic;
using umi3d.cdk;
using umi3d.cdk.collaboration;
using umi3d.cdk.userCapture;
using umi3d.common;
using umi3d.common.userCapture;
using UnityEngine;
using UnityEngine.Events;

namespace umi3d.baseBrowser.emotes
{
    public interface IEmoteManager
    {
        public void PlayEmote(Emote emote);

        public void StopEmote(Emote emote);
    }

    /// <summary>
    /// Describes an emote from the client side
    /// </summary>
    [System.Serializable]
    public class Emote
    {
        /// <summary>
        /// Emote's label
        /// </summary>
        public string Label => dto.label;
        /// <summary>
        /// Icon of the emote in the UI
        /// </summary>
        public Sprite icon;
        /// <summary>
        /// Should the emote be available or not
        /// </summary>
        public bool available;
        /// <summary>
        /// Emote order in UI
        /// </summary>
        public int uiOrder;
        /// <summary>
        /// Emote dto
        /// </summary>
        public UMI3DEmoteDto dto;
    }

    /// <summary>
    /// Manager that handles emotes
    /// </summary>
    public class EmoteManager : inetum.unityUtils.SingleBehaviour<EmoteManager>, IEmoteManager
    {
        #region Fields
        #region Events
        public event System.Action<List<Emote>> EmotesLoaded;
        public event System.Action NoEmotesLoaded;
        public event System.Action<Emote> EmoteUpdated;
        #endregion

        #region AnimatorManagement
        ///// <summary>
        ///// AnimatorController that manages emotes from a bundle
        ///// </summary>
        //[HideInInspector]
        //public RuntimeAnimatorController emoteAnimatorController;
        private UserAvatar avatar;
        /// <summary>
        /// Cache to keep previous animator controller during emote animation
        /// </summary>
        private RuntimeAnimatorController cachedAnimatorController;
        /// <summary>
        /// Reference to the skeleton animator
        /// </summary>
        private Animator skeletonAnimator;
        /// <summary>
        /// Reference to the avatar animator
        /// </summary>
        private Animator avatarAnimator;
        /// <summary>
        /// Default idle animation
        /// </summary>
        public AnimationClip idleAnimation;
        #endregion AnimatorManagement

        #region EmotesConfigManagement

        public Dictionary<Emote, Coroutine> runningCoroutines = new();

        /// <summary>
        /// Available emotes from bundle
        /// </summary>
        [HideInInspector]
        public List<Emote> Emotes = new List<Emote>();
        /// <summary>
        /// Last received dto reference
        /// </summary>
        private UMI3DEmotesConfigDto emoteConfigDto;
        /// <summary>
        /// Default icon used when no corresponding emote is found
        /// </summary>
        public Sprite defaultIcon;
        /// <summary>
        /// True when a bundle with emotes has been loaded
        /// </summary>
        [HideInInspector]
        private bool hasReceivedEmotes = false;
        #endregion EmotesConfigManagement

        #endregion Fields

        private void Start()
        {
            UMI3DClientUserTracking.Instance.EmotesLoadedEvent.AddListener(ConfigEmotes);
        }

        #region Emote Config
        /// <summary>
        /// Load and configure emotes from an <see cref="UMI3DEmotesConfigDto"/>
        /// and try to get the animations.
        /// </summary>
        /// <param name="dto"></param>
        private void ConfigEmotes(UMI3DEmotesConfigDto dto)
        {
            emoteConfigDto = dto;
            if (!UMI3DEnvironmentLoader.Instance.isEnvironmentLoaded)
                UMI3DEnvironmentLoader.Instance.onEnvironmentLoaded.AddListener(StartGetEmotes);
            else
                StartGetEmotes();
            UMI3DCollaborationClientServer.Instance.OnRedirection.AddListener(OnRedirection);
        }

        private void StartGetEmotes()
        {
            StartCoroutine(GetEmotes());
        }

        private void OnRedirection()
        {
            StopAllCoroutines();
            ResetEmoteSystem();
            UMI3DEnvironmentLoader.Instance.onEnvironmentLoaded.RemoveListener(StartGetEmotes);
            UMI3DCollaborationClientServer.Instance.OnRedirection.RemoveListener(OnRedirection);
        }

        /// <summary>
        /// Change the availability of an emote based on the received <see cref="UMI3DEmoteDto"/>
        /// </summary>
        /// <param name="dto"></param>
        private void UpdateEmote(UMI3DEmoteDto dto)
        {
            if (!hasReceivedEmotes) return;
            var emote = Emotes.Find(x => x.dto.stateName == dto.stateName);
            emote.available = dto.available;
            emote.dto = dto;
            EmoteUpdated?.Invoke(emote);
        }

        /// <summary>
        /// True if emote are supported without any animation. To remove.
        /// </summary>
        bool dirtyMode = true;

        /// <summary>
        /// Waits bundle loading attached to avatar, retreives emotes and their icon
        /// </summary>
        /// <returns></returns>
        private IEnumerator GetEmotes()
        {
            Debug.Log("Get emotes");
            var id = UMI3DClientServer.Instance.GetUserId();
            float failTime = Time.time + 30f;
            while (!UMI3DClientUserTracking.Instance.embodimentDict.ContainsKey(id))
            {
                if (Time.time > failTime)
                {
                    UMI3DLogger.LogError("Embodiment loading took too long. Impossible to load emotes.", DebugScope.Loading | DebugScope.Collaboration);
                    yield break;
                }
                yield return new WaitForSeconds(0.5f);
            }
            Debug.Log("Ended waiting");
            if (!dirtyMode)
            {
                // todo : adapt for VR
                //avatar = UMI3DClientUserTracking.Instance.embodimentDict[id];
                //while (avatar.transform.childCount == 0
                //    || (avatar.transform.childCount == 1 && avatar.transform.GetChild(0).transform.childCount == 0)) //wait for bundle loading
                //{
                //    yield return null;
                //}
                //Debug.Log("Got bundle");
            }


            var i = 0;
            foreach (UMI3DEmoteDto emoteRefInConfig in emoteConfigDto.emotes)
            {
                if (emoteRefInConfig != null)
                {
                    var emote = new Emote()
                    {
                        available = emoteConfigDto.allAvailableByDefault ? true : emoteRefInConfig.available,
                        icon = defaultIcon,
                        uiOrder = i,
                        dto = emoteRefInConfig
                    };
                    if (emoteRefInConfig.iconResource != null) LoadFile(emoteRefInConfig, emote);
                    Emotes.Add(emote);
                }
                i++;
            }

            if (Emotes.Count == 0)
            {
                NoEmotesLoaded?.Invoke();
                yield break;
            }

            hasReceivedEmotes = true;
            EmotesLoaded?.Invoke(Emotes); //Display the Emote Button and add emotes in windows.
            UMI3DClientUserTracking.Instance.EmoteChangedEvent.AddListener(UpdateEmote);

            if (dirtyMode)
                yield break;

            // todo : adapt for VR
            //skeletonAnimator = UMI3DCollaborationClientUserTracking.Instance.GetComponentInChildren<Animator>();

            //avatarAnimator = avatar.GetComponentInChildren<Animator>();
            //if (avatarAnimator == null && transform.parent != null)
            //    avatarAnimator = transform.GetComponentInParent<Animator>();
            //if (avatarAnimator != null)
            //{
            //    avatarAnimator.enabled = false; //disabled because it causes interferences with avatar bindings
            //    if (avatarAnimator.runtimeAnimatorController == null)
            //    {
            //        NoEmotesLoaded?.Invoke(); //no emotes support in the scene
            //        yield break;
            //    }
            //}

            dirtyMode = skeletonAnimator == null || avatarAnimator == null;
        }

        async void LoadFile(UMI3DEmoteDto emoteRefInConfig, Emote emote)
        {
            IResourcesLoader loader = UMI3DEnvironmentLoader.Parameters.SelectLoader(emoteRefInConfig.iconResource.extension);
            var image = await UMI3DResourcesManager.LoadFile(
                emoteConfigDto.id,
                emoteRefInConfig.iconResource,
                loader);
            var tex = (Texture2D)image;
            emote.icon = Sprite.Create((Texture2D)image, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
            return;
        }
        #endregion Emote Config

        #region UI-related
        /// <summary>
        /// Reset all variables and disable the system
        /// </summary>
        public void ResetEmoteSystem()
        {
            if (!hasReceivedEmotes) return;

            NoEmotesLoaded?.Invoke();
            Emotes.Clear();
            emoteConfigDto = null;
            //emoteAnimatorController = null;
            hasReceivedEmotes = false;
        }
        #endregion UI-related

        #region Emote Playing
        /// <summary>
        /// Loads the emotes in the animator
        /// </summary>
        private void LoadEmotes()
        {
            // todo : adapt for VR
            //skeletonAnimator.enabled = false;
            //avatarAnimator.enabled = true;
            //skeletonAnimator.Update(0);
            //avatarAnimator.Update(0);
            //avatar.ForceDisablingBinding = true;
        }

        /// <summary>
        /// Put back the normal animator of the avatar
        /// </summary>
        private void UnloadEmotes()
        {
            // todo : adapt for VR
            //skeletonAnimator.enabled = true;
            //skeletonAnimator.Play("Movement", layer: 0, 0f);
            //avatarAnimator.enabled = false;
            //skeletonAnimator.Update(0);
            //avatarAnimator.Update(0);
            //avatar.ForceDisablingBinding = false;
        }

        private UnityAction currentInterruptionAction;

        public void PlayEmote(Emote emote)
        {
            Debug.Log($"Playing emote {emote.Label}");
            runningCoroutines[emote] = StartCoroutine(PlayEmoteAnimation(emote));
        }

        /// <summary>
        /// Play the emote
        /// </summary>
        /// <param name="emote"></param>
        /// <returns></returns>
        public IEnumerator PlayEmoteAnimation(Emote emote)
        {
            UMI3DClientUserTracking.Instance.EmotePlayedSelfEvent.Invoke();
            // send the emote triggerring text to other browsers through the server
            var emoteRequest = new EmoteRequest()
            {
                emoteId = emote.dto.id,
                shouldTrigger = true,
                sendingUserId = UMI3DClientServer.Instance.GetUserId()
            };
            UMI3DClientServer.SendData(emoteRequest, true);


            if (!dirtyMode)
                LoadEmotes();

            currentInterruptionAction = new UnityAction(delegate { InterruptEmote(emote); });
            UMI3DClientUserTracking.Instance.EmotePlayedSelfEvent.AddListener(currentInterruptionAction); //used if another emote is played in the meanwhile

            if (dirtyMode)
            {
                yield return new WaitForSecondsRealtime(3);
            }
            else
            {
                // todo : adapt for VR
                ////BaseFPSNavigation.PlayerMoved.AddListener(currentInterruptionAction);
                //avatarAnimator.Play(emote.dto.stateName, layer: 0, 0f);

                //float startTime = Time.time;
                //float expectedLength = default;
                //yield return new WaitUntil(() =>
                //{
                //    var stateInfo = avatarAnimator.GetCurrentAnimatorStateInfo(0);
                //    if (stateInfo.IsName(emote.dto.stateName)) // the animation state is not transitionned to the played one directly
                //    {
                //        if (stateInfo.normalizedTime >= 1)
                //            return true;
                //        else if (expectedLength == default)
                //            expectedLength = stateInfo.length;
                //    }
                //    else if (expectedLength != default && Time.time - startTime > expectedLength) //we are in another state but we went through the one we intended to
                //        return true;
                //    return false;
                // }); //wait for emote end of animation
            }

            //? Possible to improve using a StateMachineBehaviour attached to the EmoteController & trigger events on OnStateExit on anim/OnStateEnter on AnyState
            StopEmote(emote);

            yield break;
        }

        /// <summary>
        /// Stop the emote playing process.
        /// </summary>
        /// <param name="emote"></param>
        public void StopEmote(Emote emote)
        {
            if (runningCoroutines.ContainsKey(emote))
            {
                StopCoroutine(runningCoroutines[emote]);
                runningCoroutines.Remove(emote);
            }

            if (currentInterruptionAction is not null)
            {
                // todo : adapt for VR
                //BaseFPSNavigation.PlayerMoved.RemoveListener(currentInterruptionAction);
                UMI3DClientUserTracking.Instance.EmotePlayedSelfEvent.RemoveListener(currentInterruptionAction);
                currentInterruptionAction = null;
            }
            if (!dirtyMode)
            {
                UnloadEmotes();
            }
            UMI3DClientUserTracking.Instance.EmoteEndedSelfEvent.Invoke();

            // send the emote interruption text to other browsers through the server
            var emoteRequest = new EmoteRequest()
            {
                emoteId = emote.dto.id,
                shouldTrigger = false,
                sendingUserId = UMI3DClientServer.Instance.GetUserId()
            };
            UMI3DClientServer.SendData(emoteRequest, true);
        }

        /// <summary>
        /// Interrupts an emote animation
        /// </summary>
        /// <param name="emote"></param>
        private void InterruptEmote(Emote emote)
        {
            StopEmote(emote);
        }

        #endregion Emote Playing
    }
}