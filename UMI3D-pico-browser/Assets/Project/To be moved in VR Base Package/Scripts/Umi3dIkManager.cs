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
using MathNet.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using umi3d.cdk.collaboration;
using umi3d.cdk.userCapture;
using umi3d.cdk.volumes;
using umi3d.common.userCapture;
using umi3dVRBrowsersBase.connection;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dIkManager: IUmi3dPlayer, IUmi3dPlayerLife
    {
        #region Children

        [HideInInspector]
        public GameObject Avatar;
        [HideInInspector]
        public GameObject Skeleton;
        [HideInInspector]
        public GameObject Ybot;
        [HideInInspector]
        public GameObject AlphaJoints;
        [HideInInspector]
        public GameObject AlphaSurface;
        [HideInInspector]
        public Umi3dMixamorigManager Mixamorig;
        [HideInInspector]
        public GameObject Feet;
        [HideInInspector]
        public GameObject LeftFoot;
        [HideInInspector]
        public GameObject RightFoot;

        #endregion

        #region Components

        [HideInInspector]
        public UMI3DClientUserTrackingBone CameraTracking;
        [HideInInspector]
        public SetUpAvatarHeight AvatarHeight;
        [HideInInspector]
        public UMI3DCollaborationClientUserTracking CollaborationTracking;
        [HideInInspector]
        public UMI3DClientUserTrackingBone SkeletonTracking;
        [HideInInspector]
        public Animator Animator;
        [HideInInspector]
        public IKControl IkControl;
        [HideInInspector]
        public PlayerMovement Movement;
        [HideInInspector]
        public SkinnedMeshRenderer MeshJoints;
        [HideInInspector]
        public SkinnedMeshRenderer MeshSurface;
        [HideInInspector]
        public FootTargetBehavior FootBehaviour;
        [HideInInspector]
        public VirtualObjectBodyInteraction LeftFootBodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteraction RightFootBodyInteraction;

        #endregion

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.Create()
        {
            CreateChildren();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        protected void CreateChildren()
        {
            if (Avatar == null) Avatar = new GameObject($"Avatar");
            if (Skeleton == null) Skeleton = new GameObject($"Skeleton");
            if (Ybot == null) Ybot = new GameObject($"Ybot");
            if (AlphaJoints == null) AlphaJoints = new GameObject($"AlphaJoints");
            if (AlphaSurface == null) AlphaSurface = new GameObject($"AlphaSurface");
            if (Mixamorig == null) Mixamorig = new Umi3dMixamorigManager();
            if (Feet == null) Feet = new GameObject($"Feet");
            if (LeftFoot == null) LeftFoot = new GameObject($"LeftFoot");
            if (RightFoot == null) RightFoot = new GameObject($"RightFoot");

            Mixamorig.CreateMixamorigHierarchy();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.SetHierarchy()
        {
            Avatar.Add(Skeleton);
            Skeleton.Add(Ybot);
            Ybot.Add(AlphaJoints);
            Ybot.Add(AlphaSurface);
            Ybot.Add(Mixamorig.Hips);
            Avatar.Add(Feet);
            Feet.Add(LeftFoot);
            Feet.Add(RightFoot);

            Ybot.transform.localScale = new Vector3(0.5555556f, 0.5555556f, 0.5555556f);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.AddComponents()
        {
            if (AvatarHeight == null) AvatarHeight = Avatar.AddComponent<SetUpAvatarHeight>();
            if (CollaborationTracking == null) CollaborationTracking = Avatar.AddComponent<UMI3DCollaborationClientUserTracking>();
            if (SkeletonTracking == null) SkeletonTracking = Skeleton.AddComponent<UMI3DClientUserTrackingBone>();
            if (Animator == null) Animator = Ybot.AddComponent<Animator>();
            if (IkControl == null) IkControl = Ybot.AddComponent<IKControl>();
            if (Movement == null) Movement = Ybot.AddComponent<PlayerMovement>();
            if (MeshJoints == null) MeshJoints = AlphaJoints.AddComponent<SkinnedMeshRenderer>();
            if (MeshSurface == null) MeshSurface = AlphaSurface.AddComponent<SkinnedMeshRenderer>();
            if (FootBehaviour == null) FootBehaviour = Feet.AddComponent<FootTargetBehavior>();
            if (LeftFootBodyInteraction == null) LeftFootBodyInteraction = LeftFoot.AddComponent<VirtualObjectBodyInteraction>();
            if (RightFootBodyInteraction == null) RightFootBodyInteraction = RightFoot.AddComponent<VirtualObjectBodyInteraction>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.SetComponents()
        {
            AvatarHeight.IKControl = IkControl;
            AvatarHeight.Neck = Mixamorig.Neck.transform;

            SkeletonTracking.boneType = BoneType.CenterFeet;

            Animator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;

            IkControl.LeftBodyRestPose = Mixamorig.LeftHandBodyInteraction;
            IkControl.RightBodyRestPose = Mixamorig.RightHandBodyInteraction;
            IkControl.LeftFoot = LeftFootBodyInteraction;
            IkControl.RightFoot = RightFootBodyInteraction;
            IkControl.LeftBodyInteraction = Umi3dPlayerManager.Instance.HandManager.LeftHand.BasicHand.HandBodyInteraction;
            IkControl.RightBodyInteraction = Umi3dPlayerManager.Instance.HandManager.RightHand.BasicHand.HandBodyInteraction;
            IkControl.LeftHand = Umi3dPlayerManager.Instance.HandManager.LeftHand.IkTargetBodyInteraction;
            IkControl.RightHand = Umi3dPlayerManager.Instance.HandManager.RightHand.IkTargetBodyInteraction;

            MeshJoints.rootBone = Mixamorig.Hips.transform;
            MeshSurface.rootBone = Mixamorig.Hips.transform;
            MeshJoints.localBounds = new Bounds
            {
                center = new Vector3(9.536743e-07f, -0.1948793f, 0.001876384f),
                extents = new Vector3(0.9452735f, 0.7953514f, 0.1378017f)
            };
            MeshSurface.localBounds = new Bounds
            {
                center = new Vector3(9.536743e-07f, -0.09565458f, 0.02162284f),
                extents = new Vector3(0.9734249f, 0.9023672f, 0.1821343f)
            };

            LeftFootBodyInteraction.goal = AvatarIKGoal.LeftFoot;
            RightFootBodyInteraction.goal = AvatarIKGoal.RightFoot;

            AvatarHeight.skeletonContainer = Skeleton.transform;
            AvatarHeight.FootTargetBehavior = FootBehaviour;

            CollaborationTracking.skeletonContainer = Skeleton.transform;
            CollaborationTracking.viewpointBonetype = BoneType.Head;

            FootBehaviour.FollowedAvatarNode = Avatar.transform;
            FootBehaviour.SkeletonAnimator = Animator;
            FootBehaviour.LeftTarget = LeftFootBodyInteraction;
            FootBehaviour.RightTarget = RightFootBodyInteraction;

            LeftFoot.transform.localScale = new Vector3(.01f, .01f, .01f);
            RightFoot.transform.localScale = new Vector3(.01f, .01f, .01f);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.Clear()
        {

        }

        #region IUmi3dPlayer

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnAnimatorFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.AnimatorController == null) return;
            Animator.runtimeAnimatorController = Umi3dPlayerManager.Instance.AnimatorController;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnAvatarFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.Avatar == null) return;
            Animator.avatar = Umi3dPlayerManager.Instance.Avatar;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnJoinMeshFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.MeshJoints == null) return;
            MeshJoints.sharedMesh = Umi3dPlayerManager.Instance.MeshJoints;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnLeftHandFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.LeftHand == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnMainCameraFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.MainCamera == null) return;

            if (AvatarHeight != null) AvatarHeight.OVRAnchor = Umi3dPlayerManager.Instance.MainCamera.transform;
            if (CollaborationTracking != null) CollaborationTracking.viewpoint = Umi3dPlayerManager.Instance.MainCamera.transform;
            if (CameraTracking == null) CameraTracking = Umi3dPlayerManager.Instance.MainCamera.gameObject.AddComponent<UMI3DClientUserTrackingBone>();
            CameraTracking.boneType = BoneType.Viewpoint;
            CameraTracking.isTracked = true;
            if (Umi3dPlayerManager.Instance.MainCamera.gameObject.GetComponent<BasicAllVolumesTracker>() == null) Umi3dPlayerManager.Instance.MainCamera.gameObject.AddComponent<BasicAllVolumesTracker>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPlayerFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.Player == null) return;

            if (FootBehaviour != null) FootBehaviour.OVRRig = Umi3dPlayerManager.Instance.Player.transform;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnRightHandFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.RightHand == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        void IUmi3dPlayer.OnPrefabArcImpactFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabArcImpact == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        void IUmi3dPlayer.OnPrefabArcImpactNotPossibleFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabArcImpactNotPossible == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPrefabArcStepDisplayerFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabArcStepDisplayer == null) return;
        }

        void IUmi3dPlayer.OnPrefabSelectorFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabSelector == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnSurfaceMeshFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.MeshSurface == null) return;
            MeshSurface.sharedMesh = Umi3dPlayerManager.Instance.MeshSurface;
        }

        #endregion
    }
}
