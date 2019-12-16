using application.ViewModel;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreatePracticePage : ContentPage
    {
        CreatePracticeViewModel _vm;

        //Future works, constructor uses date from today
        public CreatePracticePage() : this(DateTime.Today)
        {

        }

        //Sets time to the selected date if its after current time
        public CreatePracticePage(DateTime time)
        {
            if (time < DateTime.Today)
                time = DateTime.Today;

            Init(() => new CreatePracticeViewModel(time));
        }

        //Edit mode for PracticeSession
        public CreatePracticePage(PracticeSession ps)
        {
            Init(() => new CreatePracticeViewModel(ps));
            SetExercises();
        }


        private void Init(Func<CreatePracticeViewModel> CreateViewModelFunc)
        {
            InitializeComponent();
            //Sets BindingContext ViewModel
            _vm = CreateViewModelFunc();
            BindingContext = _vm;
            _vm.Navigation = Navigation;

            //Happens when FocusPoint on list is selected
            FocusPointList.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                //Checks if selected FocusPoint is already MainFocusPoint
                bool resetAll = false;
                if (_vm.Practice.MainFocusPoint == (FocusPointItem)e.SelectedItem)
                {
                    _vm.Practice.MainFocusPoint = null;
                    resetAll = true;
                }

                //Makes font bold if new MainFocusPoint
                SetFocusPointBoldness(e.SelectedItemIndex, (FocusPointItem)e.SelectedItem, resetAll);

                //Unselect
                FocusPointList.SelectedItem = null;
            };

            //Initiate MainFocusPoint
            if (_vm.Practice.MainFocusPoint != null)
                SetFocusPointBoldness(0, _vm.Practice.MainFocusPoint, false);

            //Initiate Icons
            SaveIcon.Source = ImageSource.FromResource("application.Images.saveicon.png");
            BullsEyeIcon.Source = ImageSource.FromResource("application.Images.bullseyeicon.png");
            //DeleteIcon.Source = ImageSource.FromResource("application.Images.deleteicon.png");
        }

        private void SetFocusPointBoldness(int index, FocusPointItem fpi, bool resetAll)
        {
            //Sets MainFocusPoint and makes it bold
            for (int i = 0; i < FocusPointList.TemplatedItems.Count; i++)
            {
                FontAttributes fa = FontAttributes.None;
                if (i == index && !resetAll)
                {
                    fa = FontAttributes.Bold;
                    _vm.Practice.MainFocusPoint = fpi;
                }

                ((Label)FocusPointList.TemplatedItems[i].FindByName("FocusPointName")).FontAttributes = fa;
            }
        }

        private void SetExercises()
        {
            //Clear stack layout ExerciseStack
            ExerciseStack.Children.Clear();

            //Create Exercises
            foreach (ExerciseItem e in _vm.PlanElement)
            {
                //Makes Frame
                Frame frame = new Frame()
                {
                    CornerRadius = 5,
                    HasShadow = true,
                    Margin = new Thickness(0, 10, 0, 10),
                };

                //Define Grid
                Grid grid = new Grid()
                {
                    RowDefinitions = new RowDefinitionCollection()
                    {
                        new RowDefinition() {Height = 50},
                        new RowDefinition() {Height = GridLength.Auto},
                    },
                    ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition {Width = 40},
                        new ColumnDefinition {Width = GridLength.Star},
                        new ColumnDefinition {Width = 40}
                    }
                };

                //Makes Entry only for int 
                var minutesEntry = new Entry
                {
                    Keyboard = Keyboard.Numeric, Placeholder = "Min", HorizontalOptions = LayoutOptions.Start,
                    HorizontalTextAlignment = TextAlignment.End
                };
                minutesEntry.Completed += (s, a) =>
                {
                    if (Double.TryParse(minutesEntry.Text, out double d))
                    {
                        e.Minutes = (int) Math.Round(d);
                        minutesEntry.Text = e.Minutes.ToString();
                    }
                    else
                    {
                        minutesEntry.Text = "";
                    }

                };
                minutesEntry.Unfocused += (s,a) => minutesEntry.SendCompleted();
                minutesEntry.Focused += (s, a) => minutesEntry.Text = "";

                //Makes the move exercise up button
                Button upBtn = new Button
                {
                    Text = "⬆️",
                    BackgroundColor = Color.White,
                    WidthRequest = 50,
                    VerticalOptions = LayoutOptions.End,
                };

                //Makes the click even to exercise up
                upBtn.Clicked += (s, r) =>
                {
                    int indexOld = _vm.PlanElement.IndexOf(e);
                    _vm.PlanElement.Move(indexOld, indexOld - 1);
                    SetExercises();
                };

                //Makes the move exercise up button
                Button downBtn = new Button
                {
                    Text = "⬇",
                    BackgroundColor = Color.White,
                    WidthRequest = 50,
                    VerticalOptions = LayoutOptions.End,
                };

                //Makes the click even to exercise up
                downBtn.Clicked += (s, r) =>
                {
                    int indexOld = _vm.PlanElement.IndexOf(e);
                    _vm.PlanElement.Move(indexOld, indexOld + 1);
                    SetExercises();
                };

                //Makes the delete button
                Button deleteBtn = new Button
                {
                    Text = "❌",
                    BackgroundColor = Color.White,
                    WidthRequest = 50,
                    VerticalOptions = LayoutOptions.End,
                };

                //Makes the click event for deleting exercise
                deleteBtn.Clicked += (s, r) =>
                {
                    _vm.PlanElement.Remove(e); 
                    SetExercises();
                };

                //Makes stacklayout for up and down buttons
                StackLayout upAndDownStack = new StackLayout();
                if (e != _vm.PlanElement.FirstOrDefault())
                    upAndDownStack.Children.Add(upBtn);
                if (e != _vm.PlanElement.LastOrDefault())
                    upAndDownStack.Children.Add(downBtn);

                //Add buttons to grid
                grid.Children.Add(deleteBtn, 2, 0);
                grid.Children.Add(upAndDownStack, 2, 1);

                //Add Entry To grid
                minutesEntry.Text = e.Minutes.ToString();
                grid.Children.Add(minutesEntry, 0, 0);

                //Add Labels Exercise name and Exercise Description
                grid.Children.Add(new Label() { Text = e.ExerciseDescriptor.Name, HorizontalOptions = LayoutOptions.FillAndExpand, FontSize = 18 }, 1, 0);
                var descriptionLabel = new Label() { Text = e.ExerciseDescriptor.Description, LineBreakMode = LineBreakMode.WordWrap, HorizontalOptions = LayoutOptions.FillAndExpand };
                grid.Children.Add(descriptionLabel, 0, 1);
                Grid.SetColumnSpan(descriptionLabel, 2);

                //Puts Grid in Frame
                frame.Content = grid;

                //Adds Frame to StackLayout
                ExerciseStack.Children.Add(frame);
            }
        }

        //Adds Exercise to list and update UI
        private void AddNewElementButton_OnClicked(object sender, EventArgs e)
        {
            _vm.AddNewPlanElement((s, ex) =>
            {
                _vm.PlanElement.Add(new ExerciseItem() { ExerciseDescriptor = ex });
                SetExercises();
            } );
        }
    }
}