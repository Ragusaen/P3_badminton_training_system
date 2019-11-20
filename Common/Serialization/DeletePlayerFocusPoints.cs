using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class DeletePlayerFocusPointRequest : Request
    {
        public int MemberId;
        public int FocusPointId;
    }

    [Serializable, XmlRoot]
    public class DeletePlayerFocusPointResponse : Response
    {
        public bool WasSuccessful;
    }
}
