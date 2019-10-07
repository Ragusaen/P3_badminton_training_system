using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Model
{
    abstract class PlaySession
    {
        public DateTime SessionStart { get; set; }

        private List<Trainer> _trainers = new List<Trainer>();

        public void AssignTrainer(Trainer trainer)
        {
            trainer.AssignToPlaySession(this);
        }

        private Feedback _feedback;
    }
}
