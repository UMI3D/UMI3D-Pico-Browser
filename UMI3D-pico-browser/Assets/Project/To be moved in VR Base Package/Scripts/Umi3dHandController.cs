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
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dHandController
    {
        [HideInInspector]
        public AvatarIKGoal Goal;

        [HideInInspector]
        public GameObject RootHand;
        [HideInInspector]
        public GameObject IkTarget;
        [HideInInspector]
        public GameObject BasicHand;
        [HideInInspector]
        public GameObject TeleportArc;

        public void CreateHand()
        {
            if (RootHand == null) RootHand = new GameObject($"UMI3D {Goal} Anchor");
            if (IkTarget == null) IkTarget = new GameObject($"IK {Goal} Anchor");
            if (BasicHand == null) BasicHand = new GameObject($"Basic {Goal}");
            if (TeleportArc == null) TeleportArc = new GameObject($"{Goal} Teleport Arc");
            CreateBasicHand();
            CreateTeleportArc();

            SetHierarchy();
        }

        protected void CreateBasicHand()
        {

        }

        protected void CreateTeleportArc()
        {

        }

        protected void SetHierarchy()
        {
            RootHand.Add(IkTarget);
            RootHand.Add(BasicHand);
            RootHand.Add(TeleportArc);
        }
    }
}
