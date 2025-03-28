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

namespace umi3d.common
{
    /// <summary>
    /// Essential data to enable the connection to an environment using a Forge server.
    /// </summary>
    [System.Serializable]
    public class EnvironmentConnectionDto : UMI3DDto
    {
        public string name;
        public string httpUrl;
        public string resourcesUrl;
        public bool authorizationInHeader;
        public string forgeHost;
        public string forgeMasterServerHost;
        public string forgeNatServerHost;
        public ushort forgeServerPort;
        public ushort forgeMasterServerPort;
        public ushort forgeNatServerPort;

        /// <summary>
        /// Umi3d version of the environment.
        /// </summary>
        /// Versions are Major.Minor.Status.Date
        public string version;

        public EnvironmentConnectionDto() : base() { }
    }
}
