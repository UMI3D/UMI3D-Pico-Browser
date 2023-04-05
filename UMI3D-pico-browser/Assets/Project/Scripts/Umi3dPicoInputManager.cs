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
using umi3dVRBrowsersBase.interactions;
using Unity.XR.PXR;
using Unity.XR.PXR.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using static UnityEngine.Rendering.DebugUI;

namespace umi3d.picoBrowser
{
    public class Umi3dPicoInputManager : AbstractControllerInputManager
    {
        public Dictionary<ControllerType, bool> isTeleportDown = new Dictionary<ControllerType, bool>();

        [HideInInspector]
        public UnityEngine.XR.InputDevice LeftController;
        [HideInInspector]
        public UnityEngine.XR.InputDevice RightController;

        private void Start()
        {
            LeftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            RightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }

        #region Grab

        public override bool GetGrab(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out value);
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out value);
                    break;
                default:
                    return false;
            }

            return value;
        }

        [HideInInspector]
        public bool GrabLeftIsDown = false;
        [HideInInspector]
        public bool GrabRightIsDown = false;
        public override bool GetGrabDown(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out value);
                    value = value && !GrabLeftIsDown;
                    if (value) GrabLeftIsDown = true;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out value);
                    value = value && !GrabRightIsDown;
                    if (value) GrabRightIsDown = true;
                    break;
                default:
                    return false;
            }

            return value;
        }

        public override bool GetGrabUp(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out value);
                    value = !value && GrabLeftIsDown;
                    if (value) GrabLeftIsDown = false;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out value);
                    value = !value && GrabRightIsDown;
                    if (value) GrabRightIsDown = false;
                    break;
                default:
                    return false;
            }

            return value;
        }

        #endregion

        #region Joystick

        public override Vector2 GetJoystickAxis(ControllerType controller)
        {
            Vector2 value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out value);
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out value);
                    break;
                default:
                    return Vector2.zero;
            }

            return value;
        }

        public override bool GetJoystickButton(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out value);
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out value);
                    break;
                default:
                    return false;
            }

            return value;
        }

        [HideInInspector]
        public bool JoystickLeftIsDown = false;
        [HideInInspector]
        public bool JoystickRightIsDown = false;
        public override bool GetJoystickButtonDown(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out value);
                    value = value && !JoystickLeftIsDown;
                    if (value) JoystickLeftIsDown = true;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out value);
                    value = value && !JoystickRightIsDown;
                    if (value) JoystickRightIsDown = true;
                    break;
                default:
                    return false;
            }

            return value;

        }

        public override bool GetJoystickButtonUp(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out value);
                    value = !value && JoystickLeftIsDown;
                    if (value) JoystickLeftIsDown = false;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out value);
                    value = !value && JoystickRightIsDown;
                    if (value) JoystickRightIsDown = false;
                    break;
                default:
                    return false;
            }

            return value;
        }

        public override bool GetRightSnapTurn(ControllerType controller)
        {
            var res = GetJoystick(controller);

            if (res)
            {
                (float pole, float magnitude) = GetJoystickPoleAndMagnitude(controller);

                if ((pole >= 0 && pole < 20) || (pole > 340 && pole <= 360))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return res;
        }

        public override bool GetLeftSnapTurn(ControllerType controller)
        {
            var res = GetJoystick(controller);

            if (res)
            {
                (float pole, float magnitude) = GetJoystickPoleAndMagnitude(controller);

                if (pole > 160 && pole <= 200)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return res;
        }

        private (float, float) GetJoystickPoleAndMagnitude(ControllerType controller)
        {
            var getAxis = GetJoystickAxis(controller);

            Vector2 axis = getAxis.normalized;
            float pole = 0.0f;

            if (axis.x != 0)
                pole = Mathf.Atan(axis.y / axis.x);
            else
                if (axis.y == 0)
                pole = 0;
            else if (axis.y > 0)
                pole = Mathf.PI / 2;
            else
                pole = -Mathf.PI / 2;

            pole *= Mathf.Rad2Deg;

            if (axis.x < 0)
                if (axis.y >= 0)
                    pole = 180 - Mathf.Abs(pole);
                else
                    pole = 180 + Mathf.Abs(pole);
            else if (axis.y < 0)
                pole = 360 + pole;

            return (pole, getAxis.magnitude);
        }

        #endregion

        #region Primary Button

        public override bool GetPrimaryButton(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value);
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value);
                    break;
                default:
                    return false;
            }

            return value;
        }

        [HideInInspector]
        public bool PrimaryLeftIsDown = false;
        [HideInInspector]
        public bool PrimaryRightIsDown = false;
        public override bool GetPrimaryButtonDown(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value);
                    value = value && !PrimaryLeftIsDown;
                    if (value) PrimaryLeftIsDown = true;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value);
                    value = value && !PrimaryRightIsDown;
                    if (value) PrimaryRightIsDown = true;
                    break;
                default:
                    return false;
            }

            return value;
        }

        public override bool GetPrimaryButtonUp(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value);
                    value = !value && PrimaryLeftIsDown;
                    if (value) PrimaryLeftIsDown = false;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out value);
                    value = !value && PrimaryRightIsDown;
                    if (value) PrimaryRightIsDown = false;
                    break;
                default:
                    return false;
            }

            return value;
        }

        #endregion

        #region Secondary Button

        public override bool GetSecondaryButton(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value);
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value);
                    break;
                default:
                    return false;
            }

            return value;
        }

        [HideInInspector]
        public bool SecondaryLeftIsDown = false;
        [HideInInspector]
        public bool SecondaryRightIsDown = false;
        public override bool GetSecondaryButtonDown(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value);
                    value = value && !SecondaryLeftIsDown;
                    if (value) SecondaryLeftIsDown = true;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value);
                    value = value && !SecondaryRightIsDown;
                    if (value) SecondaryRightIsDown = true;
                    break;
                default:
                    return false;
            }
            
            return value;
        }

        public override bool GetSecondaryButtonUp(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value);
                    value = !value && SecondaryLeftIsDown;
                    if (value) SecondaryLeftIsDown = false;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out value);
                    value = !value && SecondaryRightIsDown;
                    if (value) SecondaryRightIsDown = false;
                    break;
                default:
                    return false;
            }

            return value;
        }

        #endregion

        #region Trigger

        public override bool GetTrigger(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value);
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value);
                    break;
                default:
                    return false;
            }

            return value;
        }

        [HideInInspector]
        public bool TriggerLeftIsDown = false;
        [HideInInspector]
        public bool TriggerRightIsDown = false;
        public override bool GetTriggerDown(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value);
                    value = value && !TriggerLeftIsDown;
                    if (value) TriggerLeftIsDown = true;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value);
                    value = value && !TriggerRightIsDown;
                    if (value) TriggerRightIsDown = true;
                    break;
                default:
                    return false;
            }

            return value;
        }

        public override bool GetTriggerUp(ControllerType controller)
        {
            bool value;
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    LeftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value);
                    value = !value && TriggerLeftIsDown;
                    if (value) TriggerLeftIsDown = false;
                    break;
                case ControllerType.RightHandController:
                    RightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out value);
                    value = !value && TriggerRightIsDown;
                    if (value) TriggerRightIsDown = false;
                    break;
                default:
                    return false;
            }

            return value;
        }

        #endregion

        public override bool GetTeleportDown(ControllerType controller)
        {
            var res = GetJoystickDown(controller);

            if (res)
            {

                (float pole, float magnitude) = GetJoystickPoleAndMagnitude(controller);

                if ((pole > 20 && pole < 160))
                {
                    isTeleportDown[controller] = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return res;
        }

        public override bool GetTeleportUp(ControllerType controller)
        {
            var res = GetJoystickUp(controller) && isTeleportDown[controller];

            if (res)
            {
                isTeleportDown[controller] = false;
            }

            return res;
        }

        public override void VibrateController(ControllerType controller, float vibrationDuration, float vibrationFrequency, float vibrationAmplitude)
        {
            PXR_Input.SendHapticImpulse(controller == ControllerType.LeftHandController ? PXR_Input.VibrateType.LeftController : PXR_Input.VibrateType.RightController, vibrationAmplitude, (int)vibrationDuration, (int)vibrationFrequency);
        }
    }
}
