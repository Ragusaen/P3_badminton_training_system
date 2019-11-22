using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Model;
using Common.Serialization;

namespace Server.SystemInterface.Requests.Handlers
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
