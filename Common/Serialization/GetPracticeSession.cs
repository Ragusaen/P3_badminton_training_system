using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPracticeSessionRequest : Request
    {
        public int Id;
    }

    [Serializable, XmlRoot]
    public class GetPracticeSessionResponse : Response
    {
        public PracticeSession PracticeSession;
    }
}