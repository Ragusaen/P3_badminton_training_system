using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Serialization;
using application.SystemInterface;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace application.ViewModel
{
    class CreateExercisePopupViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (SetProperty(ref _name, value))
                {
                    CreateNewExerciseCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public event EventHandler<ExerciseDescriptor> CallBackEvent;

        private RelayCommand _createNewExerciseCommand;

        public RelayCommand CreateNewExerciseCommand
        {
            get
            {
                return _createNewExerciseCommand ?? (_createNewExerciseCommand = new RelayCommand(param => CreateNewExerciseClick(param), param => CanCreateNewExerciseClick(param)));
            }
        }

        private bool CanCreateNewExerciseClick(object param)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private void CreateNewExerciseClick(object param)
        {
            if (ValidateUserInput())
            {
                ExerciseDescriptor exercise = new ExerciseDescriptor()
                {
                    Description = Description,
                    Name = Name
                };

                RequestCreator.SetExerciseDiscriptor(exercise);
                CallBackEvent?.Invoke(this, exercise);
                PopupNavigation.Instance.PopAsync();
            }
        }

        private bool ValidateUserInput()
        {
            if (Name.Length > 64)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Name can not contain more than 64 characters", "Ok");
                return false;
            }

            if (Description != null && Description.Length > 256)
            {
                Application.Current.MainPage.DisplayAlert("Invalid input", "Description can not contain more than 256 characters", "Ok");
                return false;
            }
            return true;
        }
    }
}
