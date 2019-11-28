using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class EditFocusPointRequest : PermissionRequest
    {
        public FocusPointDescriptor FP;
    }

    [XmlRoot]
    public class EditFocusPointResponse : PermissionResponse
    { }
}
