using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyDrink.Views;
using Xamarin.Forms.Xaml;
using MyDrink.Helpers;
using MyDrink.Models;
using MyDrink.Helpers;
using System.Net.Http;
using Newtonsoft.Json;

namespace MyDrink.ViewModels
{
    public class Data
    {
        public bool success { get; set; }
        public User data { get; set; }
    }
    public class LoginViewModel : INotifyPropertyChanged
    {
        public class FormLogin
        {
            public string phoneNumber { get; set; }
            public string password { get; set; }
            public FormLogin()
            {
            }
            public FormLogin(string phoneNumber, string pass)
            {
                this.phoneNumber = phoneNumber;
                this.password = pass;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        Database db = new Database();
        public bool isBusy { get; set; }
        public LoginViewModel()
        {
            this.isBusy = false;
            LoginCommand = new Command(async () => await Login());
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
        async Task Login ()
        {
            if ( phoneNumber.Length != 0 && password.Length !=0)
            {
                try
                {
                    FormLogin data = new FormLogin(phoneNumber, password);
                    _ = await GetLoginAsync(data);
                }
                catch
                {

                }
            } else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "All fields is required", "ok");
            }
            
        }
        public StateLogin SaveLogin(string id, int admin)
        {
            StateLogin store = new StateLogin(id, admin);
            return store;
        }
        async Task<User> GetLoginAsync(FormLogin login)
        {
            User user = null;
            //Data data = null;
            this.isBusy = true;
            try
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync("https://mydrink-api.herokuapp.com/api/user/login", login);
                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                    db.createDatabase();
                    if (db.InsertStateLogin(SaveLogin(user._id, user.isAdmin)))
                    {
                        this.isBusy = false;
                        Application.Current.MainPage.DisplayAlert("Alert", "Login Success", "ok");
                        Application.Current.MainPage = new MainShell();
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alert", "Error", "ok");
                    }

                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Login Fail", "ok");
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
            }
            

            
            return user;
        }
        string phoneNumber;
        string password;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
        public string DisplayMessageValue
        {
            get
            {
                return phoneNumber + " ---- "+password;
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

        public Command LoginCommand { get; }
    }
}
