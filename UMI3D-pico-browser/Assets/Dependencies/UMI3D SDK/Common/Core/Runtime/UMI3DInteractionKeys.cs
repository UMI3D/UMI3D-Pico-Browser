﻿/*
Copyright 2019 - 2021 Inetum

Licensed under the Apache License; Version 2.0 (the );
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing; software
distributed under the License is distributed on an  BASIS;
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND; either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

namespace umi3d.common
{
    /// <summary>
    /// Contains the keys for the available interactions within the UMI3D protocol.
    /// </summary>
    /// Those keys are used when exchaning interaction DTOs between the server and clients to identify 
    /// which interaction is triggered when a user starts them.
    public static class UMI3DInteractionKeys
    {
        public const byte None = 0;

        public const byte Event = 1;
        public const byte Manipulation = 2;

        public const byte Form = 10;
        public const byte Link = 11;

        public const byte BooleanParameter = 20;
        public const byte StringParameter = 21;

        public const byte LocalInfoParameter = 23;
        public const byte StringEnumParameter = 24;
        public const byte UploadParameter = 25;

        public const byte FloatRangeParameter = 26;

    }
}
