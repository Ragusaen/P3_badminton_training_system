﻿using Common.Model;
using Common.Serialization;
using server.DAL;

namespace server.Function.Handlers
{
    class SetPlayerHandler : MiddleRequestHandler<SetPlayerRequest, SetPlayerResponse>
    {
        protected override SetPlayerResponse InnerHandle(SetPlayerRequest request, member requester)
        {
            if (!(((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer) ||
                  requester.ID == request.Player.Member.Id))
            {
                return new SetPlayerResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var p = request.Player;
            var dbMem = db.members.Find(request.Player.Member.Id);

            dbMem.Name = p.Member.Name;
            dbMem.MemberType = (int)p.Member.MemberType;
            dbMem.Comment = p.Member.Comment;
            dbMem.Sex = (int)p.Sex;

            db.SaveChanges();
            return new SetPlayerResponse();
        }
    }
}
