using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common.Serialization
{
    [Serializable, XmlRoot]
    public class SetTeamMatchRequest : PermissionRequest
    {
        public TeamMatch TeamMatch;
    }

    [Serializable, XmlRoot]
    public class SetTeamMatchResponse : PermissionResponse
    { }
}
