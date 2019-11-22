using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class GetAllTrainersRequest : Request
    { }

    [Serializable, XmlRoot]
    public class GetAllTrainersResponse : Response
    {
        public List<Trainer> Trainers;
    }
}
