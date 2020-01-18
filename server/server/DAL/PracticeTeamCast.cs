using Common.Model;

namespace server.DAL
{
    partial class practiceteam
    {
        public static explicit operator Common.Model.PracticeTeam(practiceteam pt)
        {
            var res = new PracticeTeam
            {
                Name = pt.Name,
                Id = pt.ID,
                Trainer = pt.trainer == null ? null : (Common.Model.Trainer) pt.trainer
            };

            return res;
        }
    }
}
