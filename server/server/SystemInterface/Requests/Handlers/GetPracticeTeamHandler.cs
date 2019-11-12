﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Serialization;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetPracticeTeamHandler : MiddleRequestHandler<GetPracticeTeamRequest, GetPracticeTeamResponse>
    {
        protected override GetPracticeTeamResponse InnerHandle(GetPracticeTeamRequest request)
        {
            var db = new DatabaseEntities();

            var response = new GetPracticeTeamResponse
            {
                Team = db.practiceteams.First(p => p.ID == request.Id);
            }

            return response;
        }
    }
}