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
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    public class Umi3dIkManager : MonoBehaviour
    {
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
        public GameObject Ybot;
        [HideInInspector]
        public GameObject AlphaJoints;
        [HideInInspector]
        public GameObject AlphaSurface;
        [HideInInspector]
        public Umi3dMixamorigManager Mixamorig;

        [ContextMenu("Create Ybot Hierarchy")]
        protected void CreateYbotHierarchy()
        {
            if (Ybot == null) Ybot = new GameObject($"Ybot");
            if (AlphaJoints == null) AlphaJoints = new GameObject($"AlphaJoints");
            if (AlphaSurface == null) AlphaSurface = new GameObject($"AlphaSurface");
            if (Mixamorig == null) Mixamorig = new Umi3dMixamorigManager();

            this.gameObject.Add(Ybot);
            Ybot.Add(AlphaJoints);
            Ybot.Add(AlphaSurface);
            Mixamorig.Ybot = Ybot;
            Mixamorig.CreateMixamorigHierarchy();
            Ybot.Add(Mixamorig.Hips);

            SetYbot();
        }

        protected void SetYbot()
        {
            if (Ybot.GetComponent<Animator>() == null) Ybot.AddComponent<Animator>();
            if (Ybot.GetComponent<PlayerMovement>() == null) Ybot.AddComponent<PlayerMovement>();
            if (AlphaJoints.GetComponent<SkinnedMeshRenderer>() == null) AlphaJoints.AddComponent<SkinnedMeshRenderer>();
            if (AlphaSurface.GetComponent<SkinnedMeshRenderer>() == null) AlphaSurface.AddComponent<SkinnedMeshRenderer>();
        }

        private void Reset()
        {
            CreateYbotHierarchy();
        }
    }
}
