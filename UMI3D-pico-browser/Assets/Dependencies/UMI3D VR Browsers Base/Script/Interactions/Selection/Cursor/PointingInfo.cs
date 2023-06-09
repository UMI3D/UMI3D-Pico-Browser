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

using umi3d.cdk.interaction;
using UnityEngine;

namespace umi3dBrowsers.interaction.selection.cursor
{
    /// <summary>
    /// Info on the interactable currently pointed at.
    /// Previously known as "Hover info".
    /// </summary>
    public class PointingInfo
    {
        /// <summary>
        /// True if an interactable is hit by the ray
        /// </summary>
        public bool isHitting;

        /// <summary>
        /// Controller tracked
        /// </summary>
        public AbstractController controller;

        /// <summary>
        /// Interactable currently pointed at
        /// </summary>
        public Interactable target;

        /// <summary>
        /// Interactable container of the interactable target
        /// </summary>
        public InteractableContainer targetContainer;

        /// <summary>
        /// Raycast associated
        /// </summary>
        public RaycastHit raycastHit;

        /// <summary>
        /// Ray direction in local coordinates
        /// </summary>
        public Vector3 direction;

        /// <summary>
        /// Ray direction in world coordinates
        /// </summary>
        public Vector3 directionWorld;
    }
}