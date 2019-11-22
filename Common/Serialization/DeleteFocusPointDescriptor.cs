using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class DeleteFocusPointDescriptorRequest : PermissionRequest
    {
        public FocusPointDescriptor FocusPointDescriptor;
    }

    [XmlRoot]
    public class DeleteFocusPointDescriptorResponse : PermissionResponse
    {
    }
}
