using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Server.DAL
{
    partial class playsession
    {
        public static explicit operator Common.Model.PlaySession(playsession p)
        {
            PlaySession playSession;
            if (p.Type == (int)PlaySession.Type.Practice)
            {
                var practice = p.practicesession;
                playSession = new PracticeSession()
                {
                    PracticeTeam = (Common.Model.PracticeTeam)practice.yearplansection.practiceteam;
                };
            }
            else
            {
                var match = p.teammatch;
                playSession = new Match()
                {

                };
            }

            playSession.End = p.EndDate;
            playSession.Start = p.StartDate;
            playSession.Location = p.Location;
            playSession.Id = p.ID; 

            return 
        }
    }
}
