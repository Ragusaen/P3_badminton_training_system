using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using application.Controller;
using application.SystemInterface;
using Common.Model;

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
                /*if (string.IsNullOrEmpty(_searchtext))
                    FocusPoints.OrderByDescending(p => p.Descriptor.Name);
                else*/
                Exercises.OrderByDescending((x => StringSearch.LongestCommonSubsequence(x.Name.ToLower(), SearchText.ToLower()))).ThenBy(x => x.Name.Length).ToList();
            }
        }

        private ObservableCollection<ExerciseDescriptor> _exercises;

        public ObservableCollection<ExerciseDescriptor> Exercises
        {
            get => _exercises;
            set => SetProperty(ref _exercises, value);
        }

        public ExercisePopupViewModel(PracticeSession practice)
        {
            var list = RequestCreator.GetExercises();
            list = list.Where(p => practice.Exercises.All(q => q.ExerciseDescriptor.Id != p.Id)).ToList();
            Exercises = new ObservableCollection<ExerciseDescriptor>(list);
        }

    }
}
