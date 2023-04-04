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
using System.Collections.Generic;
using umi3d.cdk;
using umi3dVRBrowsersBase.connection;
using umi3dVRBrowsersBase.interactions;
using umi3dVRBrowsersBase.navigation;
using Unity.VisualScripting;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    public interface IUmi3dPlayer
    {
        void OnValidate()
        {
            OnPlayerFieldUpdate();
            OnMainCameraFieldUpdate();
            OnAnimatorFieldUpdate();
            OnAvatarFieldUpdate();
            OnJoinMeshFieldUpdate();
            OnSurfaceMeshFieldUpdate();
            OnLeftHandFieldUpdate();
            OnRightHandFieldUpdate();
            OnPrefabArcImpactNotPossibleFieldUpdate();
            OnPrefabArcImpactFieldUpdate();
            OnPrefabArcStepDisplayerFieldUpdate();
            OnPrefabSelectorFieldUpdate();
        }
        void OnPlayerFieldUpdate();
        void OnMainCameraFieldUpdate();
        void OnLeftHandFieldUpdate();
        void OnRightHandFieldUpdate();

        void OnPrefabArcImpactNotPossibleFieldUpdate();
        void OnPrefabArcImpactFieldUpdate();
        void OnPrefabArcStepDisplayerFieldUpdate();
        void OnPrefabSelectorFieldUpdate();

        void OnAnimatorFieldUpdate();
        void OnAvatarFieldUpdate();
        void OnJoinMeshFieldUpdate();
        void OnSurfaceMeshFieldUpdate();
    }

    public interface IUmi3dPlayerLife
    {
        void Create();
        void AddComponents();
        void SetComponents();
        void SetHierarchy();
        void Clear();
    }

    public class Umi3dPlayerManager : PersistentSingleBehaviour<Umi3dPlayerManager>, IUmi3dPlayer, IUmi3dPlayerLife
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
        [Tooltip("Prefab for the YBot")]
        public GameObject PrefabUnityYBot;
        [Tooltip("Prefab for the invisible unit skeleton")]
        public GameObject PrefabInvisibleUnitSkeleton;
        [Tooltip("Prefab for the arc impact not possible")]
        public GameObject PrefabArcImpactNotPossible;
        [Tooltip("Prefab for the arc impact")]
        public GameObject PrefabArcImpact;
        [Tooltip("Prefab for the arc step displayer")]
        public GameObject PrefabArcStepDisplayer;
        [Tooltip("Prefab for the selector")]
        public GameObject PrefabSelector;

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

        //[HideInInspector]
        public Umi3dIkManager IkManager;
        [HideInInspector]
        public Umi3dHandManager HandManager;

        #endregion

        protected bool HasBeenSetUp = false;

        protected override void Awake()
        {
            base.Awake();
            HasBeenSetUp = true;
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (UnityEditor.BuildPipeline.isBuildingPlayer) return;
#endif
            if (!HasBeenSetUp) return;

            var umi3dPlayer = this as IUmi3dPlayer;
            umi3dPlayer.OnValidate();

            if (PrefabInvisibleUnitSkeleton != null)
            {
                IkManager.CollaborationTracking.UnitSkeleton = PrefabInvisibleUnitSkeleton;
            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [ContextMenu("Create Player")]
        void IUmi3dPlayerLife.Create()
        {
            if (IkManager == null) IkManager = new Umi3dIkManager();
            if (HandManager == null) HandManager = new Umi3dHandManager();

            (IkManager as IUmi3dPlayerLife).Create();
            (HandManager as IUmi3dPlayerLife).Create();

            (this as IUmi3dPlayerLife).AddComponents();
            (this as IUmi3dPlayerLife).SetComponents();
            (this as IUmi3dPlayerLife).SetHierarchy();

            OnValidate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.AddComponents()
        {
            this.GetOrAddComponent<umi3dVRBrowsersBase.navigation.UMI3DNavigation>(out VRNavigation);
            this.GetOrAddComponent<umi3d.cdk.UMI3DNavigation>(out Navigation);
            this.GetOrAddComponent<WaitForServer>(out WaitForServer);
            this.GetOrAddComponent<VRInteractionMapper>(out InteractionMapper);
            this.GetOrAddComponent<SnapTurn>(out SnapTurn);

            (IkManager as IUmi3dPlayerLife).AddComponents();
            (HandManager as IUmi3dPlayerLife).AddComponents();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.SetComponents()
        {
            if (Navigation.navigations == null) Navigation.navigations = new List<AbstractNavigation>();
            if (!Navigation.navigations.Contains(VRNavigation)) Navigation.navigations.Add(VRNavigation);

            (IkManager as IUmi3dPlayerLife).SetComponents();
            (HandManager as IUmi3dPlayerLife).SetComponents();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.SetHierarchy() 
        {
            this.gameObject.Add(IkManager.Avatar);

            (IkManager as IUmi3dPlayerLife).SetHierarchy();
            (HandManager as IUmi3dPlayerLife).SetHierarchy();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        [ContextMenu("Clear Player")]
        void IUmi3dPlayerLife.Clear()
        {
            (IkManager as IUmi3dPlayerLife).Clear();
            (HandManager as IUmi3dPlayerLife).Clear();

            IkManager = null;
            HandManager = null;
        }

        /// <summary>
        /// Teleports player.
        /// </summary>
        protected void Teleport(TeleportArc arc)
        {
            Vector3? position = arc.GetPointedPoint();

            if (position.HasValue)
            {
                Vector3 offset = this.transform.rotation * MainCamera.transform.localPosition;
                this.transform.position = new Vector3
                (
                    position.Value.x - offset.x,
                    position.Value.y,
                    position.Value.z - offset.z
                );
            }
        }

        [ContextMenu("Teleport left")]
        public void TeleportLeft() => Teleport(HandManager.LeftHand.ArcController);
        [ContextMenu("Teleport right")]
        public void TeleportRight() => Teleport(HandManager.RightHand.ArcController);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnAnimatorFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnAnimatorFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnAnimatorFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnAvatarFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnAvatarFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnAvatarFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnJoinMeshFieldUpdate()
        {
            (IkManager as IUmi3dPlayer)?.OnJoinMeshFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnJoinMeshFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnLeftHandFieldUpdate()
        {
            if (LeftHand == null) return;

            (IkManager as IUmi3dPlayer)?.OnLeftHandFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnLeftHandFieldUpdate();

            LeftHand.Add(HandManager.LeftHand.RootHand);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnMainCameraFieldUpdate()
        {
            if (MainCamera == null) return;

            (IkManager as IUmi3dPlayer)?.OnMainCameraFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnMainCameraFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPlayerFieldUpdate()
        {
            if (Player == null) return;

            this.gameObject.Add(Player);

            (IkManager as IUmi3dPlayer)?.OnPlayerFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnPlayerFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnRightHandFieldUpdate()
        {
            if (RightHand == null) return;

            (IkManager as IUmi3dPlayer)?.OnRightHandFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnRightHandFieldUpdate();

            RightHand.Add(HandManager.RightHand.RootHand);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPrefabArcImpactFieldUpdate()
        {
            if (PrefabArcImpact == null) return;

            (IkManager as IUmi3dPlayer)?.OnPrefabArcImpactFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnPrefabArcImpactFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPrefabArcImpactNotPossibleFieldUpdate()
        {
            if (PrefabArcImpactNotPossible == null) return;

            (IkManager as IUmi3dPlayer)?.OnPrefabArcImpactNotPossibleFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnPrefabArcImpactNotPossibleFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPrefabArcStepDisplayerFieldUpdate()
        {
            if (PrefabArcStepDisplayer == null) return;

            (IkManager as IUmi3dPlayer)?.OnPrefabArcStepDisplayerFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnPrefabArcStepDisplayerFieldUpdate();
        }

        void IUmi3dPlayer.OnPrefabSelectorFieldUpdate()
        {
            if (PrefabSelector == null) return;

            (IkManager as IUmi3dPlayer)?.OnPrefabSelectorFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnPrefabSelectorFieldUpdate();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnSurfaceMeshFieldUpdate()
        {
            if (RightHand == null) return;

            (IkManager as IUmi3dPlayer)?.OnSurfaceMeshFieldUpdate();
            (HandManager as IUmi3dPlayer)?.OnSurfaceMeshFieldUpdate();
        }

        
    }

    public static class ObjectExtension
    {
        public static void GetOrAddComponent<T>(this Object go, out T component)
            where T : UnityEngine.Component
        {
            component = go.GetComponent<T>();
            if (component == null) component = go.AddComponent<T>();
        }
    }
}
