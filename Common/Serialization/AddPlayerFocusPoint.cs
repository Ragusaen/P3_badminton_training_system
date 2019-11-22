using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class AddPlayerFocusPointRequest : PermissionRequest
    {
        public Player Player;
        public FocusPointDescriptor FocusPointDescriptor;
    }

    [Serializable, XmlRoot]
    public class AddPlayerFocusPointResponse : PermissionResponse
    {
        public bool WasSuccessful;
    }
}
