using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading;
using Common.Model;
using Common.Serialization;

namespace application.ViewModel
{
    class PlaySessionViewModel : BaseViewModel
    {
        public PlaySession PlaySession { get; set; }
        public PracticeSession PracticeTeam { get; set; }
        public TeamMatch TeamMatch { get; set; }

        private ObservableCollection<ExerciseItem> _exercises;

        public ObservableCollection<ExerciseItem> Exercises
        {
            get => _exercises;
            set => SetProperty(ref _exercises, value);
        }


        public PlaySessionViewModel(PlaySession playSession)
        {
            PlaySession = playSession;

            var ps = PlaySession.Start.ToString("D");
        }


    }
}
