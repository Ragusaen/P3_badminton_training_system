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
        public CreatePracticePage() : this(DateTime.Today)
        {

        }

        public CreatePracticePage(DateTime time)
        {
            if (time < DateTime.Today)
                time = DateTime.Today;

            Init(() => new CreatePracticeViewModel(time));
        }

        public CreatePracticePage(PracticeSession ps)
        {
            Init(() => new CreatePracticeViewModel(ps));
            SetExercises();
        }

        private void Init(Func<CreatePracticeViewModel> CreateViewModelFunc)
        {
            InitializeComponent();
            _vm = CreateViewModelFunc();
            BindingContext = _vm;
            _vm.Navigation = Navigation;

            FocusPointList.ItemSelected += (s, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                bool resetAll = false;
                if (_vm.Practice.MainFocusPoint == (FocusPointItem)e.SelectedItem)
                {
                    _vm.Practice.MainFocusPoint = null;
                    resetAll = true;
                }

                SetFocusPointBoldness(e.SelectedItemIndex, (FocusPointItem)e.SelectedItem, resetAll);

                FocusPointList.SelectedItem = null;
            };

            if (_vm.Practice.MainFocusPoint != null)
                SetFocusPointBoldness(0, _vm.Practice.MainFocusPoint, false);


            SaveIcon.Source = ImageSource.FromResource("application.Images.saveicon.png");
            BullsEyeIcon.Source = ImageSource.FromResource("application.Images.bullseyeicon.png");
            //DeleteIcon.Source = ImageSource.FromResource("application.Images.deleteicon.png");
        }

        private void SetFocusPointBoldness(int index, FocusPointItem fpi, bool resetAll)
        {
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
            ExerciseStack.Children.Clear();
            Debug.WriteLine($"Found {_vm.PlanElement.Count} exercises");
            foreach (ExerciseItem e in _vm.PlanElement)
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
                minutesEntry.Focused += (s, a) => minutesEntry.Text = "";
                minutesEntry.Text = e.Minutes.ToString();
                grid.Children.Add(minutesEntry, 0, 0); 

                grid.Children.Add(new Label() { Text = e.ExerciseDescriptor.Name, HorizontalOptions = LayoutOptions.FillAndExpand, FontSize = 18 }, 1, 0);
                var descriptionLabel = new Label() { Text = e.ExerciseDescriptor.Description, LineBreakMode = LineBreakMode.WordWrap, HorizontalOptions = LayoutOptions.FillAndExpand };
                grid.Children.Add(descriptionLabel, 0, 1);
                Grid.SetColumnSpan(descriptionLabel, 2);

                frame.Content = grid;

                ExerciseStack.Children.Add(frame);
            }
        }

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