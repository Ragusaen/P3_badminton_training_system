using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [XmlRoot]
    public class DeleteTeamMatchRequest : PermissionRequest
    {
        public int Id;
    }

    [XmlRoot]
    public class DeleteTeamMatchResponse : PermissionResponse
    { }
}
