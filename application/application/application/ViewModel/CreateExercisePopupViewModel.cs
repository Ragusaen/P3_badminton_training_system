using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Serialization;
using application.SystemInterface;
using Rg.Plugins.Popup.Services;

namespace application.ViewModel
{
    class CreateExercisePopupViewModel : BaseViewModel
    {
        public ExerciseDescriptor Exercise { get; set; }
        public CreateExercisePopupViewModel()
        {
            Exercise = new ExerciseDescriptor();
        }
        private RelayCommand _createNewExerciseCommand;

        public RelayCommand CreateNewExerciseCommand
        {
            get
            {
                return _createNewExerciseCommand ?? (_createNewExerciseCommand = new RelayCommand(param => CreateNewExerciseClick(param)));
            }
        }
        private void CreateNewExerciseClick(object param)
        {
            RequestCreator.SetExerciseDiscriptor(Exercise);
            PopupNavigation.Instance.PopAsync();
        }
    }
}
