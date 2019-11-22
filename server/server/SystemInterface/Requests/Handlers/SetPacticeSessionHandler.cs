using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
        class SetPracticeSessionHandler : MiddleRequestHandler<SetPracticeSessionRequest, SetPracticeSessionResponse>
        {
        protected override SetPracticeSessionResponse InnerHandle(SetPracticeSessionRequest request, member requester)
        {
                if ((requester.MemberType == (int)MemberType.Trainer))
                    return null;
                var db = new DatabaseEntities();
                var e = request.Practice;
                var dbPS = new PracticeSession
                {
                    End = e.End,
                    Trainer = e.Trainer,
                    Location = e.Location
                    
                    
                };

                db.practicesessions.Add(dbPS);
                db.SaveChanges();
                return null;

        }
    }
}
