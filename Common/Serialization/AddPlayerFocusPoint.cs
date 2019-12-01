using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class AddPlayerFocusPointRequest : PermissionRequest
    {
        public Player Player;
        public FocusPointDescriptor FocusPointDescriptor;
    }

    [XmlRoot]
    public class AddPlayerFocusPointResponse : PermissionResponse
    {
        public bool WasSuccessful;
    }
}
