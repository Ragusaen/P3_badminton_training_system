using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class GetFocusPointDescriptorRequest : Request
    {
        public int Id;
    }

    [XmlRoot]
    public class GetFocusPointDescriptorResponse : Response
    {
        public FocusPointDescriptor FocusPointDescriptor;
    }
}
