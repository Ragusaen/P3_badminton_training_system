using application.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.ViewModel
{
    class CreatePracticeViewModel : BaseViewModel
    {
        private string _focusPointsSearchText;

        public string FocusPointsSearchText
        {
            get { return _focusPointsSearchText; }
            set
            {
                SetProperty(ref _focusPointsSearchText, value);
            }
        }

        private List<FocusPoint> _focusPointsSearchResult;

        public List<FocusPoint> FocusPointsSearchResult
        {
            get { return _focusPointsSearchResult; }
            set
            {
                SetProperty(ref _focusPointsSearchResult, value);
            }
        }

        private int _focusPointListHeight;

        public int FocusPointListHeight
        {
            get { return _focusPointListHeight; }
            set { SetProperty(ref _focusPointListHeight, value); }
        }
    }
}
