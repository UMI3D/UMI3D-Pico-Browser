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
using umi3d.cdk.userCapture;
using umi3d.common.userCapture;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dMixamorigManager
    {
        [HideInInspector]
        public GameObject Hips;

        #region Left Leg

        [HideInInspector]
        public GameObject LeftUpLeg;
        [HideInInspector]
        public GameObject LeftLeg;
        [HideInInspector]
        public GameObject LeftFoot;
        [HideInInspector]
        public GameObject LeftToeBase;
        [HideInInspector]
        public GameObject LeftToeEnd;

        #endregion

        #region Right Leg

        [HideInInspector]
        public GameObject RightUpLeg;
        [HideInInspector]
        public GameObject RightLeg;
        [HideInInspector]
        public GameObject RightFoot;
        [HideInInspector]
        public GameObject RightToeBase;
        [HideInInspector]
        public GameObject RightToeEnd;

        #endregion

        #region Spine

        [HideInInspector]
        public GameObject Spine0;
        [HideInInspector]
        public GameObject Spine1;
        [HideInInspector]
        public GameObject Spine2;

        #region Left Shoulder

        [HideInInspector]
        public GameObject LeftShoulder;
        [HideInInspector]
        public GameObject LeftArm;
        [HideInInspector]
        public GameObject LeftForearm;
        [HideInInspector]
        public GameObject LeftHand;

        #region Index

        [HideInInspector]
        public GameObject LeftHandIndex1;
        [HideInInspector]
        public GameObject LeftHandIndex2;
        [HideInInspector]
        public GameObject LeftHandIndex3;
        [HideInInspector]
        public GameObject LeftHandIndex4;

        #endregion

        #region Middle

        [HideInInspector]
        public GameObject LeftHandMiddle1;
        [HideInInspector]
        public GameObject LeftHandMiddle2;
        [HideInInspector]
        public GameObject LeftHandMiddle3;
        [HideInInspector]
        public GameObject LeftHandMiddle4;

        #endregion

        #region Pinky

        [HideInInspector]
        public GameObject LeftHandPinky1;
        [HideInInspector]
        public GameObject LeftHandPinky2;
        [HideInInspector]
        public GameObject LeftHandPinky3;
        [HideInInspector]
        public GameObject LeftHandPinky4;

        #endregion

        #region Ring

        [HideInInspector]
        public GameObject LeftHandRing1;
        [HideInInspector]
        public GameObject LeftHandRing2;
        [HideInInspector]
        public GameObject LeftHandRing3;
        [HideInInspector]
        public GameObject LeftHandRing4;

        #endregion

        #region Thumb

        [HideInInspector]
        public GameObject LeftHandThumb1;
        [HideInInspector]
        public GameObject LeftHandThumb2;
        [HideInInspector]
        public GameObject LeftHandThumb3;
        [HideInInspector]
        public GameObject LeftHandThumb4;

        #endregion

        #endregion

        #region Neck

        [HideInInspector]
        public GameObject Neck;
        [HideInInspector]
        public GameObject Head;
        [HideInInspector]
        public GameObject HeadTop;
        [HideInInspector]
        public GameObject LeftEye;
        [HideInInspector]
        public GameObject RightEye;

        #endregion

        #region Right Shoulder

        [HideInInspector]
        public GameObject RightShoulder;
        [HideInInspector]
        public GameObject RightArm;
        [HideInInspector]
        public GameObject RightForearm;
        [HideInInspector]
        public GameObject RightHand;

        #region Index

        [HideInInspector]
        public GameObject RightHandIndex1;
        [HideInInspector]
        public GameObject RightHandIndex2;
        [HideInInspector]
        public GameObject RightHandIndex3;
        [HideInInspector]
        public GameObject RightHandIndex4;

        #endregion

        #region Middle

        [HideInInspector]
        public GameObject RightHandMiddle1;
        [HideInInspector]
        public GameObject RightHandMiddle2;
        [HideInInspector]
        public GameObject RightHandMiddle3;
        [HideInInspector]
        public GameObject RightHandMiddle4;

        #endregion

        #region Pinky

        [HideInInspector]
        public GameObject RightHandPinky1;
        [HideInInspector]
        public GameObject RightHandPinky2;
        [HideInInspector]
        public GameObject RightHandPinky3;
        [HideInInspector]
        public GameObject RightHandPinky4;

        #endregion

        #region Ring

        [HideInInspector]
        public GameObject RightHandRing1;
        [HideInInspector]
        public GameObject RightHandRing2;
        [HideInInspector]
        public GameObject RightHandRing3;
        [HideInInspector]
        public GameObject RightHandRing4;

        #endregion

        #region Thumb

        [HideInInspector]
        public GameObject RightHandThumb1;
        [HideInInspector]
        public GameObject RightHandThumb2;
        [HideInInspector]
        public GameObject RightHandThumb3;
        [HideInInspector]
        public GameObject RightHandThumb4;

        #endregion

        #endregion

        #endregion

        #region Components

        [HideInInspector]
        public VirtualObjectBodyInteraction LeftHandBodyInteraction;
        [HideInInspector]
        public VirtualObjectBodyInteraction RightHandBodyInteraction;

        #endregion

        const string RIG = "mixamorig:";

        public void CreateMixamorigHierarchy()
        {
            #region Create GO

            if (Hips == null) Hips = new GameObject($"{RIG}Hips");
            {
                if (LeftUpLeg == null) LeftUpLeg = new GameObject($"{RIG}LeftUpLeg");
                if (LeftLeg == null) LeftLeg = new GameObject($"{RIG}LeftLeg");
                if (LeftFoot == null) LeftFoot = new GameObject($"{RIG}LeftFoot");
                if (LeftToeBase == null) LeftToeBase = new GameObject($"{RIG}LeftToeBase");
                if (LeftToeEnd == null) LeftToeEnd = new GameObject($"{RIG}LeftToeEnd");
            }
            {
                if (RightUpLeg == null) RightUpLeg = new GameObject($"{RIG}RightUpLeg");
                if (RightLeg == null) RightLeg = new GameObject($"{RIG}RightLeg");
                if (RightFoot == null) RightFoot = new GameObject($"{RIG}RightFoot");
                if (RightToeBase == null) RightToeBase = new GameObject($"{RIG}RightToeBase");
                if (RightToeEnd == null) RightToeEnd = new GameObject($"{RIG}RightToeEnd");
            }
            {
                if (Spine0 == null) Spine0 = new GameObject($"{RIG}Spine0");
                if (Spine1 == null) Spine1 = new GameObject($"{RIG}Spine1");
                if (Spine2 == null) Spine2 = new GameObject($"{RIG}Spine2");
                {
                    if (LeftShoulder == null) LeftShoulder = new GameObject($"{RIG}LeftShoulder");
                    if (LeftArm == null) LeftArm = new GameObject($"{RIG}LeftArm");
                    if (LeftForearm == null) LeftForearm = new GameObject($"{RIG}LeftForearm");
                    if (LeftHand == null) LeftHand = new GameObject($"{RIG}LeftHand");
                    {
                        if (LeftHandIndex1 == null) LeftHandIndex1 = new GameObject($"{RIG}LeftHandIndex1");
                        if (LeftHandIndex2 == null) LeftHandIndex2 = new GameObject($"{RIG}LeftHandIndex2");
                        if (LeftHandIndex3 == null) LeftHandIndex3 = new GameObject($"{RIG}LeftHandIndex3");
                        if (LeftHandIndex4 == null) LeftHandIndex4 = new GameObject($"{RIG}LeftHandIndex4");
                    }
                    {
                        if (LeftHandMiddle1 == null) LeftHandMiddle1 = new GameObject($"{RIG}LeftHandMiddle1");
                        if (LeftHandMiddle2 == null) LeftHandMiddle2 = new GameObject($"{RIG}LeftHandMiddle2");
                        if (LeftHandMiddle3 == null) LeftHandMiddle3 = new GameObject($"{RIG}LeftHandMiddle3");
                        if (LeftHandMiddle4 == null) LeftHandMiddle4 = new GameObject($"{RIG}LeftHandMiddle4");
                    }
                    {
                        if (LeftHandPinky1 == null) LeftHandPinky1 = new GameObject($"{RIG}LeftHandPinky1");
                        if (LeftHandPinky2 == null) LeftHandPinky2 = new GameObject($"{RIG}LeftHandPinky2");
                        if (LeftHandPinky3 == null) LeftHandPinky3 = new GameObject($"{RIG}LeftHandPinky3");
                        if (LeftHandPinky4 == null) LeftHandPinky4 = new GameObject($"{RIG}LeftHandPinky4");
                    }
                    {
                        if (LeftHandRing1 == null) LeftHandRing1 = new GameObject($"{RIG}LeftHandRing1");
                        if (LeftHandRing2 == null) LeftHandRing2 = new GameObject($"{RIG}LeftHandRing2");
                        if (LeftHandRing3 == null) LeftHandRing3 = new GameObject($"{RIG}LeftHandRing3");
                        if (LeftHandRing4 == null) LeftHandRing4 = new GameObject($"{RIG}LeftHandRing4");
                    }
                    {
                        if (LeftHandThumb1 == null) LeftHandThumb1 = new GameObject($"{RIG}LeftHandThumb1");
                        if (LeftHandThumb2 == null) LeftHandThumb2 = new GameObject($"{RIG}LeftHandThumb2");
                        if (LeftHandThumb3 == null) LeftHandThumb3 = new GameObject($"{RIG}LeftHandThumb3");
                        if (LeftHandThumb4 == null) LeftHandThumb4 = new GameObject($"{RIG}LeftHandThumb4");
                    }
                }
                {
                    if (Neck == null) Neck = new GameObject($"{RIG}Neck");
                    if (Head == null) Head = new GameObject($"{RIG}Head");
                    if (HeadTop == null) HeadTop = new GameObject($"{RIG}HeadTop");
                    if (LeftEye == null) LeftEye = new GameObject($"{RIG}LeftEye");
                    if (RightEye == null) RightEye = new GameObject($"{RIG}RightEye");
                }
                {
                    if (RightShoulder == null) RightShoulder = new GameObject($"{RIG}RightShoulder");
                    if (RightArm == null) RightArm = new GameObject($"{RIG}RightArm");
                    if (RightForearm == null) RightForearm = new GameObject($"{RIG}RightForearm");
                    if (RightHand == null) RightHand = new GameObject($"{RIG}RightHand");
                    {
                        if (RightHandIndex1 == null) RightHandIndex1 = new GameObject($"{RIG}RightHandIndex1");
                        if (RightHandIndex2 == null) RightHandIndex2 = new GameObject($"{RIG}RightHandIndex2");
                        if (RightHandIndex3 == null) RightHandIndex3 = new GameObject($"{RIG}RightHandIndex3");
                        if (RightHandIndex4 == null) RightHandIndex4 = new GameObject($"{RIG}RightHandIndex4");
                    }
                    {
                        if (RightHandMiddle1 == null) RightHandMiddle1 = new GameObject($"{RIG}RightHandMiddle1");
                        if (RightHandMiddle2 == null) RightHandMiddle2 = new GameObject($"{RIG}RightHandMiddle2");
                        if (RightHandMiddle3 == null) RightHandMiddle3 = new GameObject($"{RIG}RightHandMiddle3");
                        if (RightHandMiddle4 == null) RightHandMiddle4 = new GameObject($"{RIG}RightHandMiddle4");
                    }
                    {
                        if (RightHandPinky1 == null) RightHandPinky1 = new GameObject($"{RIG}RightHandPinky1");
                        if (RightHandPinky2 == null) RightHandPinky2 = new GameObject($"{RIG}RightHandPinky2");
                        if (RightHandPinky3 == null) RightHandPinky3 = new GameObject($"{RIG}RightHandPinky3");
                        if (RightHandPinky4 == null) RightHandPinky4 = new GameObject($"{RIG}RightHandPinky4");
                    }
                    {
                        if (RightHandRing1 == null) RightHandRing1 = new GameObject($"{RIG}RightHandRing1");
                        if (RightHandRing2 == null) RightHandRing2 = new GameObject($"{RIG}RightHandRing2");
                        if (RightHandRing3 == null) RightHandRing3 = new GameObject($"{RIG}RightHandRing3");
                        if (RightHandRing4 == null) RightHandRing4 = new GameObject($"{RIG}RightHandRing4");
                    }
                    {
                        if (RightHandThumb1 == null) RightHandThumb1 = new GameObject($"{RIG}RightHandThumb1");
                        if (RightHandThumb2 == null) RightHandThumb2 = new GameObject($"{RIG}RightHandThumb2");
                        if (RightHandThumb3 == null) RightHandThumb3 = new GameObject($"{RIG}RightHandThumb3");
                        if (RightHandThumb4 == null) RightHandThumb4 = new GameObject($"{RIG}RightHandThumb4");
                    }
                }
            }

            #endregion
            AddComponents();
            SetHierarchy();
        }

        protected void AddComponents()
        {
            if (Hips.GetComponent<UMI3DClientUserTrackingBone>() == null) Hips.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
            {
                if (LeftUpLeg.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftUpLeg.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftHip;
                if (LeftLeg.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftLeg.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftKnee;
                if (LeftFoot.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftFoot.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftAnkle;
                if (LeftToeBase.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftToeBase.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftToeBase;
                if (LeftToeEnd.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftToeEnd.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.None;
            }
            {
                if (RightUpLeg.GetComponent<UMI3DClientUserTrackingBone>() == null) RightUpLeg.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightHip;
                if (RightLeg.GetComponent<UMI3DClientUserTrackingBone>() == null) RightLeg.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightKnee;
                if (RightFoot.GetComponent<UMI3DClientUserTrackingBone>() == null) RightFoot.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightAnkle;
                if (RightToeBase.GetComponent<UMI3DClientUserTrackingBone>() == null) RightToeBase.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightToeBase;
                if (RightToeEnd.GetComponent<UMI3DClientUserTrackingBone>() == null) RightToeEnd.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.None;
            }
            {
                if (Spine0.GetComponent<UMI3DClientUserTrackingBone>() == null) Spine0.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Spine;
                if (Spine1.GetComponent<UMI3DClientUserTrackingBone>() == null) Spine1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Chest;
                if (Spine2.GetComponent<UMI3DClientUserTrackingBone>() == null) Spine2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.UpperChest;
                {
                    if (LeftShoulder.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftShoulder.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftShoulder;
                    if (LeftArm.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftArm.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftUpperArm;
                    if (LeftForearm.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftForearm.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftForearm;
                    if (LeftHand.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHand.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftHand;
                    if (LeftHandBodyInteraction == null) LeftHandBodyInteraction = LeftHand.AddComponent<VirtualObjectBodyInteraction>();
                    {
                        if (LeftHandIndex1.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandIndex1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftIndexProximal;
                        if (LeftHandIndex2.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandIndex2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftIndexIntermediate;
                        if (LeftHandIndex3.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandIndex3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftIndexDistal;
                        //if (LeftHandIndex4.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandIndex4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.None;
                    }
                    {
                        if (LeftHandMiddle1.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandMiddle1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftMiddleProximal;
                        if (LeftHandMiddle2.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandMiddle2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftMiddleIntermediate;
                        if (LeftHandMiddle3.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandMiddle3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftMiddleDistal;
                        //if (LeftHandMiddle4.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandMiddle4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                    {
                        if (LeftHandPinky1.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandPinky1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftLittleProximal;
                        if (LeftHandPinky2.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandPinky2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftLittleIntermediate;
                        if (LeftHandPinky3.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandPinky3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftLittleDistal;
                        //if (LeftHandPinky4.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandPinky4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                    {
                        if (LeftHandRing1.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandRing1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftRingProximal;
                        if (LeftHandRing2.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandRing2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftRingIntermediate;
                        if (LeftHandRing3.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandRing3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftRingDistal;
                        //if (LeftHandRing4.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandRing4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                    {
                        if (LeftHandThumb1.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandThumb1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftThumbProximal;
                        if (LeftHandThumb2.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandThumb2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftThumbIntermediate;
                        if (LeftHandThumb3.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandThumb3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.LeftThumbDistal;
                        //if (LeftHandThumb4.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftHandThumb4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                }
                {
                    if (Neck.GetComponent<UMI3DClientUserTrackingBone>() == null) Neck.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Neck;
                    if (Head.GetComponent<UMI3DClientUserTrackingBone>() == null) Head.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Head;
                    //if (HeadTop.GetComponent<UMI3DClientUserTrackingBone>() == null) HeadTop.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.None;
                    //if (LeftEye.GetComponent<UMI3DClientUserTrackingBone>() == null) LeftEye.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.None;
                    //if (RightEye.GetComponent<UMI3DClientUserTrackingBone>() == null) RightEye.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.None;
                }
                {
                    if (RightShoulder.GetComponent<UMI3DClientUserTrackingBone>() == null) RightShoulder.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightShoulder;
                    if (RightArm.GetComponent<UMI3DClientUserTrackingBone>() == null) RightArm.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightUpperArm;
                    if (RightForearm.GetComponent<UMI3DClientUserTrackingBone>() == null) RightForearm.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightForearm;
                    if (RightHand.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHand.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightHand;
                    if (RightHandBodyInteraction == null) RightHandBodyInteraction = RightHand.AddComponent<VirtualObjectBodyInteraction>();
                    {
                        if (RightHandIndex1.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandIndex1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightIndexProximal;
                        if (RightHandIndex2.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandIndex2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightIndexIntermediate;
                        if (RightHandIndex3.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandIndex3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightIndexDistal;
                        //if (RightHandIndex4.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandIndex4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                    {
                        if (RightHandMiddle1.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandMiddle1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightMiddleProximal;
                        if (RightHandMiddle2.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandMiddle2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightMiddleIntermediate;
                        if (RightHandMiddle3.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandMiddle3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightMiddleDistal;
                        //if (RightHandMiddle4.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandMiddle4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                    {
                        if (RightHandPinky1.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandPinky1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                        if (RightHandPinky2.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandPinky2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightLittleIntermediate;
                        if (RightHandPinky3.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandPinky3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightLittleDistal;
                        //if (RightHandPinky4.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandPinky4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                    {
                        if (RightHandRing1.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandRing1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightRingProximal;
                        if (RightHandRing2.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandRing2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightRingIntermediate;
                        if (RightHandRing3.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandRing3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightRingDistal;
                        //if (RightHandRing4.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandRing4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                    {
                        if (RightHandThumb1.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandThumb1.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightThumbProximal;
                        if (RightHandThumb2.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandThumb2.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightThumbIntermediate;
                        if (RightHandThumb3.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandThumb3.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.RightThumbDistal;
                        //if (RightHandThumb4.GetComponent<UMI3DClientUserTrackingBone>() == null) RightHandThumb4.AddComponent<UMI3DClientUserTrackingBone>().boneType = BoneType.Hips;
                    }
                }
            }
        }

        protected void SetComponents()
        {
            LeftHandBodyInteraction.goal = AvatarIKGoal.LeftHand;
            RightHandBodyInteraction.goal = AvatarIKGoal.RightHand;


        }

        protected void SetHierarchy()
        {
            {
                Hips.Add(LeftUpLeg);
                LeftUpLeg.Add(LeftLeg);
                LeftLeg.Add(LeftFoot);
                LeftFoot.Add(LeftToeBase);
                LeftToeBase.Add(LeftToeEnd);
            }
            {
                Hips.Add(RightUpLeg);
                RightUpLeg.Add(RightLeg);
                RightLeg.Add(RightFoot);
                RightFoot.Add(RightToeBase);
                RightToeBase.Add(RightToeEnd);
            }
            {
                Hips.Add(Spine0);
                Spine0.Add(Spine1);
                Spine1.Add(Spine2);
                {
                    Spine2.Add(LeftShoulder);
                    LeftShoulder.Add(LeftArm);
                    LeftArm.Add(LeftForearm);
                    LeftForearm.Add(LeftHand);
                    {
                        LeftHand.Add(LeftHandIndex1);
                        LeftHandIndex1.Add(LeftHandIndex2);
                        LeftHandIndex2.Add(LeftHandIndex3);
                        LeftHandIndex3.Add(LeftHandIndex4);
                    }
                    {
                        LeftHand.Add(LeftHandMiddle1);
                        LeftHandMiddle1.Add(LeftHandMiddle2);
                        LeftHandMiddle2.Add(LeftHandMiddle3);
                        LeftHandMiddle3.Add(LeftHandMiddle4);
                    }
                    {
                        LeftHand.Add(LeftHandPinky1);
                        LeftHandPinky1.Add(LeftHandPinky2);
                        LeftHandPinky2.Add(LeftHandPinky3);
                        LeftHandPinky3.Add(LeftHandPinky4);
                    }
                    {
                        LeftHand.Add(LeftHandRing1);
                        LeftHandRing1.Add(LeftHandRing2);
                        LeftHandRing2.Add(LeftHandRing3);
                        LeftHandRing3.Add(LeftHandRing4);
                    }
                    {
                        LeftHand.Add(LeftHandThumb1);
                        LeftHandThumb1.Add(LeftHandThumb2);
                        LeftHandThumb2.Add(LeftHandThumb3);
                        LeftHandThumb3.Add(LeftHandThumb4);
                    }
                }
                {
                    Spine2.Add(Neck);
                    Neck.Add(Head);
                    Head.Add(HeadTop);
                    Head.Add(LeftEye);
                    Head.Add(RightEye);
                }
                {
                    Spine2.Add(RightShoulder);
                    RightShoulder.Add(RightArm);
                    RightArm.Add(RightForearm);
                    RightForearm.Add(RightHand);
                    {
                        RightHand.Add(RightHandIndex1);
                        RightHandIndex1.Add(RightHandIndex2);
                        RightHandIndex2.Add(RightHandIndex3);
                        RightHandIndex3.Add(RightHandIndex4);
                    }
                    {
                        RightHand.Add(RightHandMiddle1);
                        RightHandMiddle1.Add(RightHandMiddle2);
                        RightHandMiddle2.Add(RightHandMiddle3);
                        RightHandMiddle3.Add(RightHandMiddle4);
                    }
                    {
                        RightHand.Add(RightHandPinky1);
                        RightHandPinky1.Add(RightHandPinky2);
                        RightHandPinky2.Add(RightHandPinky3);
                        RightHandPinky3.Add(RightHandPinky4);
                    }
                    {
                        RightHand.Add(RightHandRing1);
                        RightHandRing1.Add(RightHandRing2);
                        RightHandRing2.Add(RightHandRing3);
                        RightHandRing3.Add(RightHandRing4);
                    }
                    {
                        RightHand.Add(RightHandThumb1);
                        RightHandThumb1.Add(RightHandThumb2);
                        RightHandThumb2.Add(RightHandThumb3);
                        RightHandThumb3.Add(RightHandThumb4);
                    }
                }
            }
        }
    }
}

public static class GameObjectExtensions
{
    public static void Add(this GameObject parent, GameObject child)
        => child.transform.parent = parent.transform;
} 
