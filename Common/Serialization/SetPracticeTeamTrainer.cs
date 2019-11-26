using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class SetPracticeTeamTrainerRequest : PermissionRequest
    {
        public PracticeTeam PracticeTeam;
        public Trainer Trainer;
    }

    [XmlRoot]
    public class SetPracticeTeamTrainerResponse : PermissionResponse
    { }
}
