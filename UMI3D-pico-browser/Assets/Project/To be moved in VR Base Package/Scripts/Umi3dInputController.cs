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
using umi3d.cdk.interaction;
using umi3d.cdk.userCapture;
using umi3d.common.userCapture;
using umi3dVRBrowsersBase.interactions;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dInputController : IUmi3dPlayerLife
    {
        [HideInInspector]
        public AvatarIKGoal Goal;

        [HideInInspector]
        public GameObject Controller;
        [HideInInspector]
        public GameObject Interactions;
        [HideInInspector]
        public GameObject Manipulations;
        [HideInInspector]
        public GameObject Selector;
        [HideInInspector]
        public GameObject ManipulationPoint;

        [HideInInspector]
        public ProjectionMemory Projection;
        [HideInInspector]
        public VRController VrController;

        #region IUmi3dPlayerLife

        void IUmi3dPlayerLife.AddComponents()
        {
            if (Projection == null) Projection = Controller.AddComponent<ProjectionMemory>();
            if (VrController == null) VrController = Controller.AddComponent<VRController>();
        }

        void IUmi3dPlayerLife.Clear()
        {
        }

        void IUmi3dPlayerLife.Create()
        {
            if (Controller == null) Controller = new GameObject($"{Goal} Input Controller");
            if (Interactions == null) Interactions = new GameObject($"Interactions");
            if (Manipulations == null) Manipulations = new GameObject($"Manipulations");
            if (ManipulationPoint == null) ManipulationPoint = new GameObject("Manipulation Point");
        }

        void IUmi3dPlayerLife.SetComponents()
        {
            VrController.projectionMemory = Projection;
            VrController.type = Goal == AvatarIKGoal.LeftHand ? ControllerType.LeftHandController : ControllerType.RightHandController;
            VrController.bone = Goal == AvatarIKGoal.LeftHand ? Umi3dPlayerManager.Instance.IkManager.Mixamorig.LeftHand.GetComponent<UMI3DClientUserTrackingBone>() : Umi3dPlayerManager.Instance.IkManager.Mixamorig.RightHand.GetComponent<UMI3DClientUserTrackingBone>();
        }

        void IUmi3dPlayerLife.SetHierarchy()
        {
            Controller.Add(Interactions);
            Controller.Add(Manipulations);
            Controller.Add(ManipulationPoint);
        }

        #endregion
    }
}
