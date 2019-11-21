using System;
using System.Collections.Generic;
using System.Text;
using Common.Model;

namespace application.ViewModel
{
    class PlaySessionViewModel : BaseViewModel
    {
        public PlaySession PlaySession { get; set; }
        public PracticeTeam PracticeTeam { get; set; }
        public TeamMatch TeamMatch { get; set; }

        public PlaySessionViewModel(PlaySession playSession)
        {
            
        }
    }
}
