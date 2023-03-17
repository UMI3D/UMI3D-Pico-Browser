/*
Copyright 2019 - 2023 Inetum

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
using inetum.unityUtils;
using System.Collections;
using System.Collections.Generic;
using umi3d.cdk;
using umi3dVRBrowsersBase.connection;
using umi3dVRBrowsersBase.interactions;
using umi3dVRBrowsersBase.navigation;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace umi3dVRBrowsersBase.ikManagement
{
    public interface IUmi3dPlayer
    {
        void OnValidate()
        {
            OnPlayerFieldUpdate();
            OnMainCameraFieldUpdate();
            OnLeftArcFieldUpdate();
            OnRightArcFieldUpdate();
            OnAnimatorFieldUpdate();
            OnAvatarFieldUpdate();
            OnJoinMeshFieldUpdate();
            OnSurfaceMeshFieldUpdate();
        }
        void OnPlayerFieldUpdate();
        void OnMainCameraFieldUpdate();
        void OnLeftHandFieldUpdate();
        void OnRightHandFieldUpdate();

        void OnLeftArcFieldUpdate();
        void OnRightArcFieldUpdate();

        void OnAnimatorFieldUpdate();
        void OnAvatarFieldUpdate();
        void OnJoinMeshFieldUpdate();
        void OnSurfaceMeshFieldUpdate();
    }

    public class Umi3dPlayerManager : PersistentSingleBehaviour<Umi3dPlayerManager>, IUmi3dPlayer
    {
        [Header("Player Extern SDK")]
        [Tooltip("Player, the XR root")]
        public GameObject Player;
        [Tooltip("Main camera")]
        public Camera MainCamera;
        [Tooltip("Player left hand")]
        public GameObject LeftHand;
        [Tooltip("player right hand")]
        public GameObject RightHand;

        [Header("Player Umi3d SDK")]
        [Tooltip("Left teleporting arc")]
        public TeleportArc LeftArc;
        [Tooltip("Right teleporting arc")]
        public TeleportArc RightArc;

        [Header("Avatar")]
        [Tooltip("The animator controller.")]
        public RuntimeAnimatorController AnimatorController;
        [Tooltip("The avatar")]
        public Avatar Avatar;
        [Tooltip("Mesh for the joints")]
        public Mesh MeshJoints;
        [Tooltip("Mesh for the surface")]
        public Mesh MeshSurface;

        #region Components

        [HideInInspector]
        public umi3dVRBrowsersBase.navigation.UMI3DNavigation VRNavigation;
        [HideInInspector]
        public umi3d.cdk.UMI3DNavigation Navigation;
        [HideInInspector]
        public WaitForServer WaitForServer;
        [HideInInspector]
        public VRInteractionMapper InteractionMapper;
        [HideInInspector]
        public SnapTurn SnapTurn;

        #endregion

        #region Sub manager class

        [HideInInspector]
        public Umi3dIkManager IkManager;

        #endregion

        private void OnValidate()
        {
            var umi3dPlayer = this as IUmi3dPlayer;
            umi3dPlayer.OnValidate();
        }

        [ContextMenu("Create Player")]
        protected void CreatePlayer()
        {
            AddComponents();
            SetComponents();

            if (IkManager == null) IkManager = new Umi3dIkManager();
            IkManager.CreateYbotHierarchy();

            OnValidate();
        }

        protected void AddComponents()
        {
            if (VRNavigation == null) VRNavigation = this.AddComponent<umi3dVRBrowsersBase.navigation.UMI3DNavigation>();
            if (Navigation == null) Navigation = this.AddComponent<umi3d.cdk.UMI3DNavigation>();
            if (WaitForServer == null) WaitForServer = this.AddComponent<WaitForServer>();
            if (InteractionMapper == null) InteractionMapper = this.AddComponent<VRInteractionMapper>();
            if (SnapTurn == null) SnapTurn = this.AddComponent<SnapTurn>();
        }

        protected void SetComponents()
        {
            if (Navigation.navigations == null) Navigation.navigations = new List<AbstractNavigation>();
            if (!Navigation.navigations.Contains(VRNavigation)) Navigation.navigations.Add(VRNavigation);
        }

        /// <summary>
        /// Teleports player.
        /// </summary>
        public void Teleport(TeleportArc arc)
        {
            Vector3? position = arc.GetPointedPoint();

            //if (position.HasValue)
            //{
            //    Vector3 offset = this.transform.rotation * centerEyeAnchor.transform.localPosition;
            //    this.transform.position = new Vector3
            //    (
            //        position.Value.x - offset.x,
            //        position.Value.y,
            //        position.Value.z - offset.z
            //    );
            //}
        }

        [ContextMenu("Teleport left")]
        protected void TeleportLeft() => Teleport(LeftArc);
        [ContextMenu("Teleport right")]
        protected void TeleportRight() => Teleport(RightArc);

        private void Reset()
        {
            CreatePlayer();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPlayerFieldUpdate()
        {
            //if (Player != null) Player.Add(IkManager.Avatar);
            //else this.gameObject.Add(IkManager.Avatar);
            if (Player == null) return;

            this.gameObject.Add(Player);

            (IkManager as IUmi3dPlayer)?.OnPlayerFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnMainCameraFieldUpdate()
        {
            if (MainCamera == null) return;

            (IkManager as IUmi3dPlayer)?.OnMainCameraFieldUpdate();
        }

        void IUmi3dPlayer.OnLeftHandFieldUpdate()
        {
            if (LeftHand == null) return;

            (IkManager as IUmi3dPlayer)?.OnLeftHandFieldUpdate();
        }

        void IUmi3dPlayer.OnRightHandFieldUpdate()
        {
            if (RightHand == null) return;

            (IkManager as IUmi3dPlayer)?.OnRightHandFieldUpdate();
        }

        void IUmi3dPlayer.OnLeftArcFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnLeftArcFieldUpdate();
        }

        void IUmi3dPlayer.OnRightArcFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnRightArcFieldUpdate();
        }

        void IUmi3dPlayer.OnAvatarFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnAvatarFieldUpdate();
        }

        void IUmi3dPlayer.OnJoinMeshFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnJoinMeshFieldUpdate();
        }

        void IUmi3dPlayer.OnSurfaceMeshFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnSurfaceMeshFieldUpdate();
        }

        void IUmi3dPlayer.OnAnimatorFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnAnimatorFieldUpdate();
        }
    }
}
