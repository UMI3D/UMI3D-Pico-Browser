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

using UnityEngine;

namespace umi3dBrowsers.interaction.selection.cursor
{
    /// <summary>
    /// Base class for cursor associated with selection
    /// </summary>
    public abstract class AbstractCursor : MonoBehaviour
    {
        /// <summary>
        /// Display the cursor
        /// </summary>
        public abstract void Display();

        /// <summary>
        /// Hide the cursor
        /// </summary>
        public abstract void Hide();

        /// <summary>
        /// Change properties of the cursor according to the selected object passed
        /// </summary>scal
        /// <param name="selectedObjectInfo">Type InteractableContainer or Selectable</param>
        public abstract void ChangeAccordingToSelection(AbstractSelectionData selectedObjectInfo);
    }
}