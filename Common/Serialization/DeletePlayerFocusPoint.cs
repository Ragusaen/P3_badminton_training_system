using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class DeletePlayerFocusPointRequest : PermissionRequest
    {
        public Player Player;
        public FocusPointItem FocusPointItem;
    }

    [Serializable, XmlRoot]
    public class DeletePlayerFocusPointResponse : PermissionResponse
    { }
}
