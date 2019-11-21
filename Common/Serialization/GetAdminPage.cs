using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Common.Model;

namespace Common.Serialization
{
    [XmlRoot]
    public class GetAdminPageRequest : PermissionRequest
    {
    }

    [XmlRoot]
    public class GetAdminPageResponse : PermissionResponse
    {
        public List<Member> Members;
        public List<PracticeTeam> PracticeTeams;
        public List<FocusPointDescriptor> FocusPoints;
    }
}
