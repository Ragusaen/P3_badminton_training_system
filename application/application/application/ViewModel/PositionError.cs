using Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace application.ViewModel
{
    class PositionError : INotifyPropertyChanged
    {
        private Player _player;

        public Player Player
        {
            get { return _player; }
            set
            {
                _player = value;
                Error = string.Empty;
            }
        }

        private Player _otherPlayer;

        public Player OtherPlayer
        {
            get { return _otherPlayer; }
            set
            {
                _otherPlayer = value;
                OtherError = string.Empty;
            }
        }

        public bool IsExtra { get; set; }
        public bool OtherIsExtra { get; set; }


        private string _error;

        public string Error
        {
            get { return _error; }
            set 
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        private string _otherError;

        public string OtherError
        {
            get { return _otherError; }
            set
            {
                _otherError = value;
                OnPropertyChanged();
            }
        }

        public PositionError()
        {

        }

        public PositionError(Position position)
        {
            Player = position.Player;
            IsExtra = position.IsExtra;
            OtherPlayer = position.OtherPlayer;
            OtherIsExtra = position.OtherIsExtra;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
