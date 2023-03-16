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
using umi3d.cdk.collaboration;
using umi3d.cdk.userCapture;
using umi3d.common.userCapture;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dIkManager
    {
        [HideInInspector]
        public GameObject Avatar;
        [HideInInspector]
        public GameObject Skeleton;
        [HideInInspector]
        public GameObject Ybot;
        [HideInInspector]
        public GameObject AlphaJoints;
        [HideInInspector]
        public GameObject AlphaSurface;
        [HideInInspector]
        public Umi3dMixamorigManager Mixamorig;
        [HideInInspector]
        public GameObject Feet;
        [HideInInspector]
        public GameObject LeftFoot;
        [HideInInspector]
        public GameObject RightFoot;

        [HideInInspector]
        public UMI3DCollaborationClientUserTracking Tracking;

        public void CreateYbotHierarchy()
        {
            if (Avatar == null) Avatar = new GameObject($"Avatar");
            if (Skeleton == null) Skeleton = new GameObject($"Skeleton");
            if (Ybot == null) Ybot = new GameObject($"Ybot");
            if (AlphaJoints == null) AlphaJoints = new GameObject($"AlphaJoints");
            if (AlphaSurface == null) AlphaSurface = new GameObject($"AlphaSurface");
            if (Mixamorig == null) Mixamorig = new Umi3dMixamorigManager();
            if (Feet == null) Feet = new GameObject($"Feet");
            if (LeftFoot == null) LeftFoot = new GameObject($"LeftFoot");
            if (RightFoot == null) RightFoot = new GameObject($"RightFoot");

            Mixamorig.CreateMixamorigHierarchy();

            Avatar.Add(Skeleton);
            Skeleton.Add(Ybot);
            Ybot.Add(AlphaJoints);
            Ybot.Add(AlphaSurface);
            Ybot.Add(Mixamorig.Hips);
            Avatar.Add(Feet);
            Feet.Add(LeftFoot);
            Feet.Add(RightFoot);

            SetAvatar();
        }

        protected void SetAvatar()
        {
            if (Avatar.GetComponent<UMI3DCollaborationClientUserTracking>() == null) Tracking = Avatar.AddComponent<UMI3DCollaborationClientUserTracking>();
            if (Skeleton.GetComponent<UMI3DClientUserTrackingBone>() == null) Skeleton.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.CenterFeet;
            if (Ybot.GetComponent<Animator>() == null) Ybot.AddComponent<Animator>();
            if (Ybot.GetComponent<PlayerMovement>() == null) Ybot.AddComponent<PlayerMovement>();
            if (AlphaJoints.GetComponent<SkinnedMeshRenderer>() == null) AlphaJoints.AddComponent<SkinnedMeshRenderer>();
            if (AlphaSurface.GetComponent<SkinnedMeshRenderer>() == null) AlphaSurface.AddComponent<SkinnedMeshRenderer>();

            Tracking.skeletonContainer = Skeleton.transform;
            Tracking.viewpoint = Umi3dPlayerManager.Instance.MainCamera?.transform;
            Tracking.viewpointBonetype = BoneType.Head;
        }
    }
}
