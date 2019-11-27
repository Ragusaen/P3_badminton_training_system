using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class GetTrainerPracticeTeamsRequest : Request
    {
        public Trainer Trainer;
    }

    [XmlRoot]
    public class GetTrainerPracticeTeamsResponse : Response
    {
        public List<PracticeTeam> PracticeTeams;
    }
}