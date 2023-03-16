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
    public class Umi3dPlayerManager : PersistentSingleBehaviour<Umi3dPlayerManager>
    {
        [Header("Player")]
        [Tooltip("Player, the XR root")]
        public GameObject Player;
        [Tooltip("Main camera")]
        public Camera MainCamera;
        [Tooltip("Left teleporting arc")]
        public TeleportArc LeftArc;
        [Tooltip("Right teleporting arc")]
        public TeleportArc RightArc;
        // TODO snap turn 

        [Header("Avatar")]
        [Tooltip("The animator controller.")]
        public RuntimeAnimatorController Controller;
        [Tooltip("The avatar")]
        public Avatar Avatar;
        [Tooltip("Mesh for the join")]
        public Mesh Join;
        [Tooltip("Mesh for the surface")]
        public Mesh Surface;

        [HideInInspector]
        public umi3dVRBrowsersBase.navigation.UMI3DNavigation VRNavigation;
        [HideInInspector]
        public umi3d.cdk.UMI3DNavigation Navigation;
        [HideInInspector]
        public VRInteractionMapper InteractionMapper;
        [HideInInspector]
        public Umi3dIkManager IkManager;


        private void OnValidate()
        {
            if (Player != null) Player.Add(IkManager.Avatar);
            else this.gameObject.Add(IkManager.Avatar);
            if (IkManager != null && MainCamera != null) IkManager.Tracking.viewpoint = MainCamera.transform;
        }

        [ContextMenu("Create Player")]
        protected void CreatePlayer()
        {
            if (this.GetComponent<umi3dVRBrowsersBase.navigation.UMI3DNavigation>() == null) VRNavigation = this.AddComponent<umi3dVRBrowsersBase.navigation.UMI3DNavigation>();
            if (this.GetComponent<umi3d.cdk.UMI3DNavigation>() == null) Navigation = this.AddComponent<umi3d.cdk.UMI3DNavigation>();
            if (this.GetComponent<WaitForServer>() == null) this.AddComponent<WaitForServer>();
            if (this.GetComponent<VRInteractionMapper>() == null) InteractionMapper = this.AddComponent<VRInteractionMapper>();

            if (Navigation.navigations == null) Navigation.navigations = new List<AbstractNavigation>();
            if (!Navigation.navigations.Contains(VRNavigation)) Navigation.navigations.Add(VRNavigation);

            if (IkManager == null) IkManager = new Umi3dIkManager();

            IkManager.CreateYbotHierarchy();
            if (Player != null) Player.Add(IkManager.Avatar);
            else this.gameObject.Add(IkManager.Avatar);
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
    }
}
