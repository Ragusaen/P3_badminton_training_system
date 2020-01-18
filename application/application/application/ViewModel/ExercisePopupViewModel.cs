using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using Common.Model;
using Xamarin.Forms;

namespace application.ViewModel
{
    class ExercisePopupViewModel : BaseViewModel
    {
        private string _searchText;

        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                Exercises = new ObservableCollection<ExerciseDescriptor> (Exercises.OrderByDescending((x => StringExtension.LongestCommonSubsequence(x.Name.ToLower(), SearchText.ToLower()))).ThenBy(x => x.Name.Length).ToList());
            }
        }

        private ObservableCollection<ExerciseDescriptor> _exercises;

        public ObservableCollection<ExerciseDescriptor> Exercises
        {
            get => _exercises;
            set => SetProperty(ref _exercises, value);
        }

        public ExercisePopupViewModel(PracticeSession practice, RequestCreator requestCreator, INavigation navigation) : base(requestCreator, navigation)
        {
            var list = RequestCreator.GetExercises();
            Exercises = new ObservableCollection<ExerciseDescriptor>(list);
        }

    }
}
