﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetPlayerRequest : PermissionRequest
    {
        public Player Player;
    }

    [Serializable, XmlRoot]
    public class SetPlayerResponse : PermissionResponse
    { }
}
