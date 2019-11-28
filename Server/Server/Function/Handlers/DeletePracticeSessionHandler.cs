﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using Server.DAL;
using Server.SystemInterface.Requests.Handlers;

namespace Server.Function.Handlers
{
    class DeletePracticeSessionHandler : MiddleRequestHandler<DeletePracticeSessionRequest, DeletePracticeSessionResponse>
    {
        protected override DeletePracticeSessionResponse InnerHandle(DeletePracticeSessionRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new DeletePracticeSessionResponse { AccessDenied = true };
            }
            var db = new DatabaseEntities();

            var prac = db.practicesessions.Find(request.Id);

            if (prac != null)
            {
                var captain = prac.trainer;
                var team = prac.practiceteam;
                var ps = db.playsessions.Find(prac.PlaySessionID);
                captain.practicesessions.Remove(prac);
                team.practicesessions.Remove(prac);

                db.practicesessionexercises.RemoveRange(prac.practicesessionexercises);
                db.focuspoints.RemoveRange(prac.subfocuspoints);
                prac.mainfocuspoint = null;

                if (ps != null)
                {
                    var fb = ps.feedbacks;
                    db.feedbacks.RemoveRange(fb);

                    db.playsessions.Remove(ps);
                }
            }

            db.SaveChanges();
            return new DeletePracticeSessionResponse();
        }
    }
}
