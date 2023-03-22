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
using System.Linq;
using umi3d.cdk.interaction;
using umi3d.cdk.userCapture;
using umi3d.common.userCapture;
using umi3dVRBrowsersBase.interactions;
using umi3dVRBrowsersBase.interactions.input;
using umi3dVRBrowsersBase.interactions.selection;
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
        public GameObject Selector;
        [HideInInspector]
        public GameObject ManipulationPoint;

        #region Interactions

        [HideInInspector]
        public GameObject IndexTrigger;
        [HideInInspector]
        public GameObject HandTrigger;
        [HideInInspector]
        public GameObject AButton;
        [HideInInspector]
        public GameObject BButton;

        [HideInInspector]
        public VRInputObserver IndexTriggerInputObserver;
        [HideInInspector]
        public VRInputObserver HandTriggerInputObserver;
        [HideInInspector]
        public VRInputObserver AButtonInputObserver;
        [HideInInspector]
        public VRInputObserver BButtonInputObserver;

        [HideInInspector]
        public BooleanInput IndexTriggerBooleanInput;
        [HideInInspector]
        public BooleanInput HandTriggerBooleanInput;
        [HideInInspector]
        public BooleanInput AButtonBooleanInput;
        [HideInInspector]
        public BooleanInput BButtonBooleanInput;

        [HideInInspector]
        public ManipulationInput IndexTriggerManipulationInput;
        [HideInInspector]
        public ManipulationInput HandTriggerManipulationInput;
        [HideInInspector]
        public ManipulationInput AButtonManipulationInput;

        #endregion

        [HideInInspector]
        public ProjectionMemory Projection;
        [HideInInspector]
        public VRController VrController;
        [HideInInspector]
        public VRSelectionManager SelectionManager;

        #region IUmi3dPlayerLife

        void IUmi3dPlayerLife.AddComponents()
        {
            if (Projection == null) Projection = Controller.AddComponent<ProjectionMemory>();
            if (VrController == null) VrController = Controller.AddComponent<VRController>();

            if (IndexTriggerInputObserver == null) IndexTriggerInputObserver = IndexTrigger.AddComponent<VRInputObserver>();
            if (HandTriggerInputObserver == null) HandTriggerInputObserver = HandTrigger.AddComponent<VRInputObserver>();
            if (AButtonInputObserver == null) AButtonInputObserver = AButton.AddComponent<VRInputObserver>();
            if (BButtonInputObserver == null) BButtonInputObserver = BButton.AddComponent<VRInputObserver>();

            if (IndexTriggerBooleanInput == null) IndexTriggerBooleanInput = IndexTrigger.AddComponent<BooleanInput>();
            if (HandTriggerBooleanInput == null) HandTriggerBooleanInput = HandTrigger.AddComponent<BooleanInput>();
            if (AButtonBooleanInput == null) AButtonBooleanInput = AButton.AddComponent<BooleanInput>();
            if (BButtonBooleanInput == null) BButtonBooleanInput = BButton.AddComponent<BooleanInput>();

            if (IndexTriggerManipulationInput == null) IndexTriggerManipulationInput = IndexTrigger.AddComponent<ManipulationInput>();
            if (HandTriggerManipulationInput == null) HandTriggerManipulationInput = HandTrigger.AddComponent<ManipulationInput>();
            if (AButtonManipulationInput == null) AButtonManipulationInput = AButton.AddComponent<ManipulationInput>();
        }

        void IUmi3dPlayerLife.Clear()
        {
        }

        void IUmi3dPlayerLife.Create()
        {
            if (Controller == null) Controller = new GameObject($"{Goal} Input Controller");
            if (Interactions == null) Interactions = new GameObject($"Interactions");
            if (ManipulationPoint == null) ManipulationPoint = new GameObject("Manipulation Point");

            if (IndexTrigger == null) IndexTrigger = new GameObject($"IndexTrigger");
            if (HandTrigger == null) HandTrigger = new GameObject($"HandGrab");
            if (AButton == null) AButton = new GameObject($"AButton");
            if (BButton == null) BButton = new GameObject("BButton");
        }

        void IUmi3dPlayerLife.SetComponents()
        {
            VrController.projectionMemory = Projection;
            VrController.type = Goal == AvatarIKGoal.LeftHand ? ControllerType.LeftHandController : ControllerType.RightHandController;
            VrController.bone = Goal == AvatarIKGoal.LeftHand ? Umi3dPlayerManager.Instance.IkManager.Mixamorig.LeftHand.GetComponent<UMI3DClientUserTrackingBone>() : Umi3dPlayerManager.Instance.IkManager.Mixamorig.RightHand.GetComponent<UMI3DClientUserTrackingBone>();
            VrController.manipulationInputs = new List<ManipulationInput>
            {
                IndexTriggerManipulationInput,
                HandTriggerManipulationInput,
                AButtonManipulationInput
            };
            VrController.booleanInputs = new List<BooleanInput>
            {
                IndexTriggerBooleanInput,
                HandTriggerBooleanInput,
                AButtonBooleanInput,
                BButtonBooleanInput
            };
            VrController.HoldInput = HandTriggerBooleanInput;

            IndexTriggerBooleanInput.vrInput = IndexTriggerInputObserver;
            HandTriggerBooleanInput.vrInput = HandTriggerInputObserver;
            AButtonBooleanInput.vrInput = AButtonInputObserver;
            BButtonBooleanInput.vrInput = BButtonInputObserver;

            IndexTriggerInputObserver.action = ActionType.Trigger;
            HandTriggerInputObserver.action = ActionType.Grab;
            AButtonInputObserver.action = ActionType.PrimaryButton;
            BButtonInputObserver.action = ActionType.SecondaryButton;

            IndexTriggerInputObserver.controller = Goal == AvatarIKGoal.LeftHand ? ControllerType.LeftHandController : ControllerType.RightHandController;
            HandTriggerInputObserver.controller = Goal == AvatarIKGoal.LeftHand ? ControllerType.LeftHandController : ControllerType.RightHandController;
            AButtonInputObserver.controller = Goal == AvatarIKGoal.LeftHand ? ControllerType.LeftHandController : ControllerType.RightHandController;
            BButtonInputObserver.controller = Goal == AvatarIKGoal.LeftHand ? ControllerType.LeftHandController : ControllerType.RightHandController;

            umi3d.common.interaction.DofGroupEnum[] dofs =
            {
                umi3d.common.interaction.DofGroupEnum.X,
                umi3d.common.interaction.DofGroupEnum.Y,
                umi3d.common.interaction.DofGroupEnum.Z,
                umi3d.common.interaction.DofGroupEnum.XY,
                umi3d.common.interaction.DofGroupEnum.XZ,
                umi3d.common.interaction.DofGroupEnum.XYZ,
                umi3d.common.interaction.DofGroupEnum.RX,
                umi3d.common.interaction.DofGroupEnum.RY,
                umi3d.common.interaction.DofGroupEnum.RZ,
                umi3d.common.interaction.DofGroupEnum.RX_RY,
                umi3d.common.interaction.DofGroupEnum.RX_RZ,
                umi3d.common.interaction.DofGroupEnum.RY_RZ,
                umi3d.common.interaction.DofGroupEnum.RX_RY_RZ,
                umi3d.common.interaction.DofGroupEnum.X_RX,
                umi3d.common.interaction.DofGroupEnum.Y_RY,
                umi3d.common.interaction.DofGroupEnum.Z_RZ,
                umi3d.common.interaction.DofGroupEnum.YZ,
                umi3d.common.interaction.DofGroupEnum.ALL,
            };

            IndexTriggerManipulationInput.activationButton = IndexTriggerInputObserver;
            HandTriggerManipulationInput.activationButton = HandTriggerInputObserver;
            AButtonManipulationInput.activationButton = AButtonInputObserver;
            IndexTriggerManipulationInput.cursor = ManipulationPoint.transform;
            HandTriggerManipulationInput.cursor = ManipulationPoint.transform;
            AButtonManipulationInput.cursor = ManipulationPoint.transform;
            IndexTriggerManipulationInput.implementedDofs = dofs.ToList();
            HandTriggerManipulationInput.implementedDofs = dofs.ToList();
            AButtonManipulationInput.implementedDofs = dofs.ToList();
        }

        void IUmi3dPlayerLife.SetHierarchy()
        {
            Controller.Add(Interactions);
            Controller.Add(ManipulationPoint);

            Interactions.Add(IndexTrigger);
            Interactions.Add(HandTrigger);
            Interactions.Add(AButton);
            Interactions.Add(BButton);
        }

        #endregion
    }
}
