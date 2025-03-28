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
using inetum.unityUtils;
using System.Collections;
using System.Collections.Generic;
using umi3d.browser.utils;
using umi3dVRBrowsersBase.ikManagement;
using UnityEngine;

public partial class WaitingRoomManager : PersistentSingleBehaviour<WaitingRoomManager>
{
    

}

public partial class WaitingRoomManager : ISpawnablePoint
{
    public Transform SpawnPoint;

    [ContextMenu("Teleport Player")]
    /// <summary>
    /// Teleport player at <see cref="SpawnPoint"/>.
    /// </summary>
    public void TeleportPlayer() => Umi3dPlayerManager.Instance.Teleport(SpawnPoint.position);
}
