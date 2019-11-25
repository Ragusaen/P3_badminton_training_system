using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class DeletePracticeTeamRequest : PermissionRequest
    {
        public PracticeTeam PracticeTeam;
    }

    [XmlRoot]
    public class DeletePracticeTeamResponse : PermissionResponse
    {
    }
}
