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
using umi3dVRBrowsersBase.interactions.selection;
using umi3dVRBrowsersBase.navigation;
using UnityEngine;

namespace umi3dVRBrowsersBase.ikManagement
{
    [System.Serializable]
    public class Umi3dHandController : IUmi3dPlayerLife, IUmi3dPlayer
    {
        [HideInInspector]
        public AvatarIKGoal Goal;

        [HideInInspector]
        public GameObject RootHand;
        [HideInInspector]
        public GameObject IkTarget;
        [HideInInspector]
        public GameObject TeleportArc;

        [HideInInspector]
        public Umi3dInputController InputController;
        [HideInInspector]
        public VirtualObjectBodyInteraction IkTargetBodyInteraction;
        [HideInInspector]
        public Umi3dBasicHand BasicHand;
        [HideInInspector]
        public GameObject ArcImpactNotPossible;
        [HideInInspector]
        public GameObject ArcImpact;
        [HideInInspector]
        public TeleportArc ArcController;

        #region IUmi3dPlayerLife

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.Create()
        {
            if (RootHand == null) RootHand = new GameObject($"UMI3D {Goal} Anchor");
            if (IkTarget == null) IkTarget = new GameObject($"IK {Goal} Target");
            if (TeleportArc == null) TeleportArc = new GameObject($"{Goal} Teleport Arc");

            if (InputController == null) InputController = new Umi3dInputController { Goal = Goal };
            if (BasicHand == null) BasicHand = new Umi3dBasicHand { Goal = Goal };

            (BasicHand as IUmi3dPlayerLife).Create();
            (InputController as IUmi3dPlayerLife).Create();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.AddComponents()
        {
            (BasicHand as IUmi3dPlayerLife).AddComponents();
            (InputController as IUmi3dPlayerLife).AddComponents();

            if (IkTargetBodyInteraction == null) IkTargetBodyInteraction = IkTarget.AddComponent<VirtualObjectBodyInteraction>();
            if (ArcController == null) ArcController = TeleportArc.AddComponent<TeleportArc>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.SetComponents()
        {
            (BasicHand as IUmi3dPlayerLife).SetComponents();
            (InputController as IUmi3dPlayerLife).SetComponents();

            IkTargetBodyInteraction.goal = Goal;

            UnityEngine.Debug.Log("<color=green>TODO: </color>" + $"Add navmesh to layer");
            ArcController.rayStartPoint = TeleportArc.transform;
            ArcController.navmeshLayer = LayerMask.NameToLayer("Navmesh"); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.SetHierarchy()
        {
            (BasicHand as IUmi3dPlayerLife).SetHierarchy();
            (InputController as IUmi3dPlayerLife).SetHierarchy();

            RootHand.Add(InputController.Controller);
            RootHand.Add(IkTarget);
            RootHand.Add(BasicHand.BasicHand);
            RootHand.Add(TeleportArc);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayerLife.Clear()
        {
            (BasicHand as IUmi3dPlayerLife).Clear();
            (InputController as IUmi3dPlayerLife).Clear();
        }

        #endregion

        #region IUmi3dPlayer

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPlayerFieldUpdate()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnMainCameraFieldUpdate()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnLeftHandFieldUpdate()
        {
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnRightHandFieldUpdate()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        void IUmi3dPlayer.OnPrefabYBotFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabUnityYBot == null) return;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPrefabArcImpactNotPossibleFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabArcImpactNotPossible == null) return;

            if (ArcImpactNotPossible == null) ArcImpactNotPossible = GameObject.Instantiate(Umi3dPlayerManager.Instance.PrefabArcImpactNotPossible);
            TeleportArc.Add(ArcImpactNotPossible);
            ArcController.impactPoint = ArcImpact;
            ArcImpactNotPossible.SetActive(false);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPrefabArcImpactFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabArcImpact == null) return;

            if (ArcImpact == null) ArcImpact = GameObject.Instantiate(Umi3dPlayerManager.Instance.PrefabArcImpact);
            TeleportArc.Add(ArcImpact);
            ArcController.errorPoint = ArcImpactNotPossible;
            ArcImpact.SetActive(false);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        void IUmi3dPlayer.OnPrefabArcStepDisplayerFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabArcStepDisplayer == null) return;
            ArcController.stepDisplayerPrefab = Umi3dPlayerManager.Instance.PrefabArcStepDisplayer;
        }

        void IUmi3dPlayer.OnPrefabSelectorFieldUpdate()
        {
            if (Umi3dPlayerManager.Instance.PrefabSelector == null)
            {
                InputController.Selector = null;
                InputController.SelectionManager = null;
                return;
            }

            if (InputController.Selector == null)
            {
                InputController.Selector = GameObject.Instantiate(Umi3dPlayerManager.Instance.PrefabSelector);
                InputController.Controller.Add(InputController.Selector);
                if (InputController.SelectionManager == null) InputController.SelectionManager = InputController.Selector.GetComponent<VRSelectionManager>();
                InputController.SelectionManager.controller = InputController.VrController;
            }

        }

        #endregion
    }
}
