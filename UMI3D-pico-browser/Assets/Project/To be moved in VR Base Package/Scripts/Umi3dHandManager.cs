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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dHandManager: IUmi3dPlayer
    {
        [HideInInspector]
        public Umi3dHandController LeftHand;
        [HideInInspector]
        public Umi3dHandController RightHand;

        public void CreateHands()
        {
            if (LeftHand == null) LeftHand = new Umi3dHandController { Goal = AvatarIKGoal.LeftHand };
            if (RightHand == null) RightHand = new Umi3dHandController { Goal = AvatarIKGoal.RightHand };

            LeftHand.CreateHand();
            RightHand.CreateHand();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnAnimatorFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.AnimatorController == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnAvatarFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.Avatar == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnJoinMeshFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.MeshJoints == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnLeftArcFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.LeftArc == null) return;
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
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPlayerFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.Player == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnRightArcFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.RightArc == null) return;
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
        void IUmi3dPlayer.OnSurfaceMeshFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.MeshSurface == null) return;
        }
    }
}
