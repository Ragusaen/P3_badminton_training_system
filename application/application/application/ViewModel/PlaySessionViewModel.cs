using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
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
            PlaySession = playSession;

            var ps = PlaySession.Start.ToString("D");
        }


    }
}
