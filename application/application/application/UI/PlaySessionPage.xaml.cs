using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.ViewModel;
using Common.Model;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaySessionPage : ContentPage
    {
        private PlaySessionViewModel _vm;

        public PlaySessionPage(PlaySession playSession)
        {
            InitializeComponent();
            _vm = new PlaySessionViewModel(playSession);
            BindingContext = _vm;
            _vm.Navigation = Navigation;

            Time.Text = _vm.PlaySession.Start.ToString("hh:mm") + " - " + _vm.PlaySession.End.ToString("hh:mm");
            Date.Text = _vm.PlaySession.Start.ToString("dddd, d MMMM");
            Location.Text = _vm.PlaySession.Location;


            if (_vm.TeamMatch != null)
            {
                Name.Text = _vm.TeamMatch.OpponentName;
                TeamMatchRelevant.IsVisible = true;
            }
            else if (_vm.PracticeSession != null)
                SetPracticeVisibility();
                
            BullsEyeIcon.Source = ImageSource.FromResource("application.Images.bullseyeicon.png");
            EditButton.Source = ImageSource.FromResource("application.Images.editicon.png");

            EditButton.Clicked += (s,a) => _vm.EditButtonClicked(this);
        }

        private void SetPracticeVisibility()
        {
            Name.Text = _vm.PracticeSession.PracticeTeam.Name;


            if (_vm.PracticeSession.MainFocusPoint != null)
            {
                var MFPTapGest = new TapGestureRecognizer();
                MFPTapGest.Tapped += (s, a) => GoToFocusPoint(_vm.PracticeSession.MainFocusPoint.Descriptor);
                MainFocusPoint.GestureRecognizers.Add(MFPTapGest);
            }

                PracticeRelevant.IsVisible = true;

            if (RequestCreator.LoggedInMember.MemberType.HasFlag(MemberType.Trainer))
            {
                FocusPointList.IsVisible = true;
                ExerciseStack.IsVisible = true;
                SetExercises();
            }
        }

        private void GoToFocusPoint(FocusPointDescriptor fpd)
        {
            PopupNavigation.Instance.PushAsync(new StringAndHeaderPopup(fpd));
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            FocusPointList.SelectedItem = null;
            GoToFocusPoint(((FocusPointItem)e.SelectedItem).Descriptor);
        }

        private void SetExercises()
        {
            foreach (ExerciseItem e in _vm.PracticeSession.Exercises)
            {
                Frame frame = new Frame()
                {
                    CornerRadius = 5,
                    HasShadow = true,
                    Margin = new Thickness(0, 10, 0, 10),
                };

                Grid grid = new Grid()
                {
                    RowDefinitions = new RowDefinitionCollection()
                    {
                        new RowDefinition() {Height = 40},
                        new RowDefinition() {Height = GridLength.Auto}
                    },
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition {Width = 40},
                        new ColumnDefinition {Width = GridLength.Star}
                    }
                };

                grid.Children.Add(new Label() {Text=e.Minutes.ToString(), HorizontalOptions = LayoutOptions.CenterAndExpand, FontSize=20}, 0,0);
                grid.Children.Add(new Label() {Text = e.ExerciseDescriptor.Name, HorizontalOptions = LayoutOptions.FillAndExpand, FontSize = 18}, 1,0);
                var descriptionEditor = new Label() {Text = e.ExerciseDescriptor.Description, LineBreakMode=LineBreakMode.WordWrap, HorizontalOptions = LayoutOptions.FillAndExpand};
                grid.Children.Add(descriptionEditor, 0, 1);
                Grid.SetColumnSpan(descriptionEditor, 2);

                frame.Content = grid;

                ExerciseStack.Children.Add(frame);
            }
        }
    }
}