﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetPracticeSessionFocusPointsRequest : Request
    {
        public int PlaySessionId;
    }

    [Serializable, XmlRoot]
    public class GetPracticeSessionFocusPointsResponse : Response
    {
        public List<FocusPointDescriptor> FocusPoints;
    }
}
