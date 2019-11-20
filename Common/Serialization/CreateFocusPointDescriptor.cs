using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class CreateFocusPointDescriptorRequest : Request
    {
        public FocusPointDescriptor FocusPointDescriptor;
    }
    [Serializable, XmlRoot]
    public class CreateFocusPointDescriptorResponse : Response
    {
        public FocusPointDescriptor FocusPointDescriptor;
    }
}
