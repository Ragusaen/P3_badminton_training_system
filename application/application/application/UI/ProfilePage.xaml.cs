using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using application.SystemInterface;
using application.ViewModel;
using Common.Model;
using Common.Serialization;
using Microcharts;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace application.UI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage
    {
        private ProfilePageViewModel _vm;

        public ProfilePage(int profileId, RequestCreator requestCreator) : base(requestCreator)
        {
            InitializeComponent();
            SetupCommentEvents();

            _vm = new ProfilePageViewModel(profileId, requestCreator, Navigation);
            BindingContext = _vm;

            if (_vm.Player != null)
                FeedbackSection.IsVisible = _vm.Player.Feedbacks.Count > 0;

            ShownOnlyRelevantInfo();
            Settingsicon.Source = ImageSource.FromResource("application.Images.settingsicon.jpg");
            if (_vm.Player != null && _vm.Player.Sex == Sex.Unknown)
                NoSexLabel.IsVisible = true;
        }

        private void ShownOnlyRelevantInfo()
        {
            BothRelevant.IsVisible = _vm.Member.MemberType != MemberType.None;
            TrainerRelevant.IsVisible = _vm.Trainer != null;
            PlayerRelevant.IsVisible = _vm.Player != null;
            Settingsicon.IsVisible = RequestCreator.LoggedInMember.MemberType.HasFlag(MemberType.Trainer); // Only trainers can view profile settings
        }

        private void SetupCommentEvents()
        {
            var commentTap = new TapGestureRecognizer();
            commentTap.Tapped += (s, a) =>
            {
                Comment.IsVisible = false;
                CommentEntry.IsVisible = true;
                CommentEntry.Text = Comment.Text;
                if (CommentEntry.Text == "Click to add comment")
                    CommentEntry.Text = null;
                CommentEntry.Focus();
            };
            Comment.GestureRecognizers.Add(commentTap);

            CommentEntry.Unfocused += (s, a) =>
            {
                Comment.IsVisible = true;
                CommentEntry.IsVisible = false;
                if (CommentEntry?.Text?.Length > 0)
                {
                    Comment.Text = CommentEntry.Text;
                    if(_vm.SetComment(CommentEntry.Text))
                        _vm.Member.Comment = CommentEntry.Text;
                }
            };
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var focusPoint = (FocusPointItem) e.SelectedItem;
            if (focusPoint != null)
                _vm.PopupFocusPoint(focusPoint);
            FocusPointList.SelectedItem = null;
        }
    }
}