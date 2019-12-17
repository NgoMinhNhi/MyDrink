using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDrink.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string userName;
        string phoneNumber;
        string password;
        string confirmPassword;
        public RegisterViewModel ()
        {
            CommandSignUp = new Command(async () => await SignUp());
        }
        async Task SignUp()
        {
            if (userName.Length != 0 && phoneNumber.Length != 0 && password.Length !=0 && confirmPassword.Length != 0)
            {
                if (string.Compare(password, confirmPassword) == 0)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", userName + phoneNumber + password + confirmPassword, "ok");
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Confirm password not match", "ok");
                } 
            } else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "You must fill all field", "ok");
            }
            
        }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public Command CommandSignUp { get; }
    }
}
