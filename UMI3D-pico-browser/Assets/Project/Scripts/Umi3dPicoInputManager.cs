/*
Copyright 2019 - 2022 Inetum

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

namespace umi3d.picoBrowser
{
    public class Umi3dPicoInputManager : AbstractControllerInputManager
    {
        public Dictionary<ControllerType, bool> isTeleportDown = new Dictionary<ControllerType, bool>();

        [Header("Left Controller")]
        public InputAction LeftGrab;
        public InputAction LeftJoystick;
        public InputAction LeftPrimaryButton;
        public InputAction LeftSecondaryButton;
        public InputAction LeftTrigger;

        [Header("Right Controller")]
        public InputAction RightGrab;
        public InputAction RightJoystick;
        public InputAction RightPrimaryButton;
        public InputAction RightSecondaryButton;
        public InputAction RightTrigger;

        #region Grab

        public override bool GetGrab(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftGrab.IsPressed();
                case ControllerType.RightHandController:
                    return RightGrab.IsPressed();
                default:
                    return false;
            }
        }

        public override bool GetGrabDown(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftGrab.WasPressedThisFrame();
                case ControllerType.RightHandController:
                    return RightGrab.WasPressedThisFrame();
                default:
                    return false;
            }
        }

        public override bool GetGrabUp(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftGrab.WasReleasedThisFrame();
                case ControllerType.RightHandController:
                    return RightGrab.WasReleasedThisFrame();
                default:
                    return false;
            }
        }

        #endregion

        #region Joystick

        public override Vector2 GetJoystickAxis(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftJoystick.ReadValue<Vector2>();
                case ControllerType.RightHandController:
                    return RightJoystick.ReadValue<Vector2>();
                default:
                    return Vector2.zero;
            }
        }

        public override bool GetJoystickButton(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftJoystick.IsPressed();
                case ControllerType.RightHandController:
                    return RightJoystick.IsPressed();
                default:
                    return false;
            }
        }

        public override bool GetJoystickButtonDown(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftJoystick.WasPressedThisFrame();
                case ControllerType.RightHandController:
                    return RightJoystick.WasPressedThisFrame();
                default:
                    return false;
            }
        }

        public override bool GetJoystickButtonUp(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftJoystick.WasReleasedThisFrame();
                case ControllerType.RightHandController:
                    return RightJoystick.WasReleasedThisFrame();
                default:
                    return false;
            }
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
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftPrimaryButton.IsPressed();
                case ControllerType.RightHandController:
                    return RightPrimaryButton.IsPressed();
                default:
                    return false;
            }
        }

        public override bool GetPrimaryButtonDown(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftPrimaryButton.WasPressedThisFrame();
                case ControllerType.RightHandController:
                    return RightPrimaryButton.WasPressedThisFrame();
                default:
                    return false;
            }
        }

        public override bool GetPrimaryButtonUp(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftPrimaryButton.WasReleasedThisFrame();
                case ControllerType.RightHandController:
                    return RightPrimaryButton.WasReleasedThisFrame();
                default:
                    return false;
            }
        }

        #endregion

        #region Secondary Button

        public override bool GetSecondaryButton(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftSecondaryButton.IsPressed();
                case ControllerType.RightHandController:
                    return RightSecondaryButton.IsPressed();
                default:
                    return false;
            }
        }

        public override bool GetSecondaryButtonDown(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftSecondaryButton.WasPressedThisFrame();
                case ControllerType.RightHandController:
                    return RightSecondaryButton.WasPressedThisFrame();
                default:
                    return false;
            }
        }

        public override bool GetSecondaryButtonUp(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftSecondaryButton.WasReleasedThisFrame();
                case ControllerType.RightHandController:
                    return RightSecondaryButton.WasReleasedThisFrame();
                default:
                    return false;
            }
        }

        #endregion

        #region Trigger

        public override bool GetTrigger(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftTrigger.IsPressed();
                case ControllerType.RightHandController:
                    return RightTrigger.IsPressed();
                default:
                    return false;
            }
        }

        public override bool GetTriggerDown(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftTrigger.WasPressedThisFrame();
                case ControllerType.RightHandController:
                    return RightTrigger.WasPressedThisFrame();
                default:
                    return false;
            }
        }

        public override bool GetTriggerUp(ControllerType controller)
        {
            switch (controller)
            {
                case ControllerType.LeftHandController:
                    return LeftTrigger.WasReleasedThisFrame();
                case ControllerType.RightHandController:
                    return RightTrigger.WasReleasedThisFrame();
                default:
                    return false;
            }
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
