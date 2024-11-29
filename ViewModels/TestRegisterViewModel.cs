using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TaskApp.ViewModels
{
    public class TestRegisterViewModel: INotifyPropertyChanged
    {
        private bool _isBusy = false;
        private bool _UsernameNotValid=false;
        private string _userNameErrors;
        private string _userName;
        public string UserName { get { return _userName; } set {

                if (value != _userName)
                {
                    _userName = value;
                    ValidateUserName();
                    OnPropertyChanged();
                }
            } }

        private void ValidateUserName()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                UserNameNotValid = true;
                UserNameErrors = "User name is required!";
            }

            else
            {
                UserNameNotValid = false;
            }
        }

        public string UserNameErrors
        {
            get { return _userNameErrors; }
            set { _userNameErrors = value; OnPropertyChanged(); }

        }
        public bool UserNameNotValid
        {
            get { return _UsernameNotValid; }
            set { _UsernameNotValid = value; OnPropertyChanged(); }

        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }

        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {

                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RegisterCommand { get; }

        public TestRegisterViewModel()
        {
            
            RegisterCommand = new RelayCommand(async ()=>await Register());
        }

        private async Task Register()
        {
            IsBusy = true;

           await AppShell.Current.DisplayAlert("", "Register Called", "Ok");
           IsBusy = false;

        }



        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
