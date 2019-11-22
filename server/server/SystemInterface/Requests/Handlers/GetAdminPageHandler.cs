﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Serialization;
using NLog;
using Server.DAL;

namespace Server.SystemInterface.Requests.Handlers
{
    class GetAdminPageHandler : MiddleRequestHandler<GetAdminPageRequest, GetAdminPageResponse>
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        protected override GetAdminPageResponse InnerHandle(GetAdminPageRequest request, member requester)
        {
            if (!((Common.Model.MemberType)requester.MemberType).HasFlag(MemberType.Trainer))
            {
                return new GetAdminPageResponse { AccessDenied = true };
            }

            var db = new DatabaseEntities();
            var focusPoints = db.focuspoints.ToList().Select(fp => (FocusPointDescriptor) fp).ToList();
            var members = db.members.ToList().Select(m => (Member) m).ToList();
            var practiceTeams = db.practiceteams.ToList().Select(pt => (PracticeTeam) pt).ToList();

            _log.Debug($"Loading Admin Model. Focus Points: {focusPoints.Count}, Members: {members.Count}, Practice Teams: {practiceTeams.Count}");

            return new GetAdminPageResponse()
            {
                FocusPoints = focusPoints,
                Members = members,
                PracticeTeams = practiceTeams
            };
        }
    }
}
