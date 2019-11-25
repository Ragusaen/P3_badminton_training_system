using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class SetNonPrivateFocusPointRequest : PermissionRequest
    {
        public FocusPointDescriptor FocusPointDescriptor;
    }

    [XmlRoot]
    public class SetNonPrivateFocusPointResponse : PermissionResponse
    { }
}
