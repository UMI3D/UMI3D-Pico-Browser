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
using umi3d.common.userCapture;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dBasicHand: IUmi3dPlayerLife
    {
        [HideInInspector]
        public AvatarIKGoal Goal;

        #region GameObjects

        [HideInInspector]
        public GameObject BasicHand;

        [HideInInspector]
        public GameObject Sphere;

        #region Thumb

        [HideInInspector]
        public GameObject Thumb1;
        [HideInInspector]
        public GameObject Thumb2;
        [HideInInspector]
        public GameObject Thumb3;
        [HideInInspector]
        public GameObject Thumb4;

        #endregion

        #region Index

        [HideInInspector]
        public GameObject Index1;
        [HideInInspector]
        public GameObject Index2;
        [HideInInspector]
        public GameObject Index3;
        [HideInInspector]
        public GameObject Index4;

        #endregion

        #region Middle

        [HideInInspector]
        public GameObject Middle1;
        [HideInInspector]
        public GameObject Middle2;
        [HideInInspector]
        public GameObject Middle3;
        [HideInInspector]
        public GameObject Middle4;

        #endregion

        #region Ring

        [HideInInspector]
        public GameObject Ring1;
        [HideInInspector]
        public GameObject Ring2;
        [HideInInspector]
        public GameObject Ring3;
        [HideInInspector]
        public GameObject Ring4;

        #endregion

        #region Pinky

        [HideInInspector]
        public GameObject Pinky1;
        [HideInInspector]
        public GameObject Pinky2;
        [HideInInspector]
        public GameObject Pinky3;
        [HideInInspector]
        public GameObject Pinky4;

        #endregion

        #endregion

        #region Components

        [HideInInspector]
        public VirtualObjectBodyInteraction HandBodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionEndBone SphereBodyInteraction;

        #region Thumb

        [HideInInspector]
        public VirtualObjectBodyInteractionBone Thumb1BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Thumb2BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Thumb3BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionEndBone Thumb4BodyInteraction;

        #endregion

        #region Index

        [HideInInspector]
        public VirtualObjectBodyInteractionBone Index1BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Index2BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Index3BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionEndBone Index4BodyInteraction;

        #endregion

        #region Middle

        [HideInInspector]
        public VirtualObjectBodyInteractionBone Middle1BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Middle2BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Middle3BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionEndBone Middle4BodyInteraction;

        #endregion

        #region Ring

        [HideInInspector]
        public VirtualObjectBodyInteractionBone Ring1BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Ring2BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Ring3BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionEndBone Ring4BodyInteraction;

        #endregion

        #region Pinky

        [HideInInspector]
        public VirtualObjectBodyInteractionBone Pinky1BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Pinky2BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionBone Pinky3BodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteractionEndBone Pinky4BodyInteraction;

        #endregion

        #endregion


        void IUmi3dPlayerLife.Create()
        {
            if (BasicHand == null) BasicHand = new GameObject($"Basic {Goal}");
            if (Sphere == null) Sphere = new GameObject("Sphere");
            {
                if (Thumb1 == null) Thumb1 = new GameObject("Thumb1");
                if (Thumb2 == null) Thumb2 = new GameObject("Thumb2");
                if (Thumb3 == null) Thumb3 = new GameObject("Thumb3");
                if (Thumb4 == null) Thumb4 = new GameObject("Thumb4");
            }
            {
                if (Index1 == null) Index1 = new GameObject("Index1");
                if (Index2 == null) Index2 = new GameObject("Index2");
                if (Index3 == null) Index3 = new GameObject("Index3");
                if (Index4 == null) Index4 = new GameObject("Index4");
            }
            {
                if (Middle1 == null) Middle1 = new GameObject("Middle1");
                if (Middle2 == null) Middle2 = new GameObject("Middle2");
                if (Middle3 == null) Middle3 = new GameObject("Middle3");
                if (Middle4 == null) Middle4 = new GameObject("Middle4");
            }
            {
                if (Ring1 == null) Ring1 = new GameObject("Ring1");
                if (Ring2 == null) Ring2 = new GameObject("Ring2");
                if (Ring3 == null) Ring3 = new GameObject("Ring3");
                if (Ring4 == null) Ring4 = new GameObject("Ring4");
            }
            {
                if (Pinky1 == null) Pinky1 = new GameObject("Pinky1");
                if (Pinky2 == null) Pinky2 = new GameObject("Pinky2");
                if (Pinky3 == null) Pinky3 = new GameObject("Pinky3");
                if (Pinky4 == null) Pinky4 = new GameObject("Pinky4");
            }
        }

        void IUmi3dPlayerLife.AddComponents()
        {
            if (HandBodyInteraction == null) HandBodyInteraction = BasicHand.AddComponent<VirtualObjectBodyInteraction>();
            if (SphereBodyInteraction == null) SphereBodyInteraction = Sphere.AddComponent<VirtualObjectBodyInteractionEndBone>();
            {
                if (Thumb1BodyInteraction == null) Thumb1BodyInteraction = Thumb1.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Thumb2BodyInteraction == null) Thumb2BodyInteraction = Thumb2.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Thumb3BodyInteraction == null) Thumb3BodyInteraction = Thumb3.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Thumb4BodyInteraction == null) Thumb4BodyInteraction = Thumb4.AddComponent<VirtualObjectBodyInteractionEndBone>();
            }
            {
                if (Index1BodyInteraction == null) Index1BodyInteraction = Index1.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Index2BodyInteraction == null) Index2BodyInteraction = Index2.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Index3BodyInteraction == null) Index3BodyInteraction = Index3.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Index4BodyInteraction == null) Index4BodyInteraction = Index4.AddComponent<VirtualObjectBodyInteractionEndBone>();
            }
            {
                if (Middle1BodyInteraction == null) Middle1BodyInteraction = Middle1.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Middle2BodyInteraction == null) Middle2BodyInteraction = Middle2.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Middle3BodyInteraction == null) Middle3BodyInteraction = Middle3.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Middle4BodyInteraction == null) Middle4BodyInteraction = Middle4.AddComponent<VirtualObjectBodyInteractionEndBone>();
            }
            {
                if (Ring1BodyInteraction == null) Ring1BodyInteraction = Ring1.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Ring2BodyInteraction == null) Ring2BodyInteraction = Ring2.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Ring3BodyInteraction == null) Ring3BodyInteraction = Ring3.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Ring4BodyInteraction == null) Ring4BodyInteraction = Ring4.AddComponent<VirtualObjectBodyInteractionEndBone>();
            }
            {
                if (Pinky1BodyInteraction == null) Pinky1BodyInteraction = Pinky1.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Pinky2BodyInteraction == null) Pinky2BodyInteraction = Pinky2.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Pinky3BodyInteraction == null) Pinky3BodyInteraction = Pinky3.AddComponent<VirtualObjectBodyInteractionBone>();
                if (Pinky4BodyInteraction == null) Pinky4BodyInteraction = Pinky4.AddComponent<VirtualObjectBodyInteractionEndBone>();
            }
        }

        void IUmi3dPlayerLife.SetComponents()
        {
            HandBodyInteraction.goal = Goal;
            {
                Thumb1BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftThumbProximal : HumanBodyBones.RightThumbProximal;
                Thumb2BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftThumbIntermediate : HumanBodyBones.RightThumbIntermediate;
                Thumb3BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftThumbDistal : HumanBodyBones.RightThumbDistal;
            }
            {
                Index1BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftIndexProximal : HumanBodyBones.RightIndexProximal;
                Index2BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftIndexIntermediate : HumanBodyBones.RightIndexIntermediate;
                Index3BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftIndexDistal : HumanBodyBones.RightIndexDistal;
            }
            {
                Middle1BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftMiddleProximal : HumanBodyBones.RightMiddleProximal;
                Middle2BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftMiddleIntermediate : HumanBodyBones.RightMiddleIntermediate;
                Middle3BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftMiddleDistal : HumanBodyBones.RightMiddleDistal;
            }
            {
                Ring1BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftRingProximal : HumanBodyBones.RightRingProximal;
                Ring2BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftRingIntermediate : HumanBodyBones.RightRingIntermediate;
                Ring3BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftRingDistal : HumanBodyBones.RightRingDistal;
            }
            {
                Pinky1BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftLittleProximal : HumanBodyBones.RightLittleProximal;
                Pinky2BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftLittleIntermediate : HumanBodyBones.RightLittleIntermediate;
                Pinky3BodyInteraction.bone = Goal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftLittleDistal : HumanBodyBones.RightLittleDistal;
            }
        }

        void IUmi3dPlayerLife.SetHierarchy()
        {
            BasicHand.Add(Sphere);
            Sphere.Add(Thumb1);
            Sphere.Add(Index1);
            Sphere.Add(Middle1);
            Sphere.Add(Ring1);
            Sphere.Add(Pinky1);
            {
                Thumb1.Add(Thumb2);
                Thumb2.Add(Thumb3);
                Thumb3.Add(Thumb4);
            }
            {
                Index1.Add(Index2);
                Index2.Add(Index3);
                Index3.Add(Index4);
            }
            {
                Middle1.Add(Middle2);
                Middle2.Add(Middle3);
                Middle3.Add(Middle4);
            }
            {
                Ring1.Add(Ring2);
                Ring2.Add(Ring3);
                Ring3.Add(Ring4);
            }
            {
                Pinky1.Add(Pinky2);
                Pinky2.Add(Pinky3);
                Pinky3.Add(Pinky4);
            }
        }

        void IUmi3dPlayerLife.Clear()
        {

        }
    }
}
