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
using System.Diagnostics;

namespace MyDrink.ViewModels
{
    public class Data
    {
        public bool success { get; set; }
        public User data { get; set; }
    }
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Database db = new Database();
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }
        async Task Login ()
        {
            try
            {
                //if(string.Compare(userName, "ngominhnhi") == 0 && string.Compare(password, "ngominhnhi")== 0)
                //{
                //    //await Application.Current.MainPage.Navigation.PushModalAsync(new MainShell());
                //    db.createDatabase();

                //    if (db.InsertStateLogin(SaveLogin()))
                //    {
                //        Application.Current.MainPage.DisplayAlert("Alert", "Login Success", "ok");
                //    } else
                //    {
                //        Application.Current.MainPage.DisplayAlert("Alert", "Login Fail", "ok");
                //    }
                //    Application.Current.MainPage = new MainShell();


                //}
                //else
                //{
                //    Application.Current.MainPage.DisplayAlert("Alert", userName + "+" + password + "@" + string.Compare(userName, "ngominhnhi")+ "@"+ string.Compare(password, "ngominhnhi"), "ok");
                //}
                StateLogin data = new StateLogin(userName, password);
                _ = await GetProductAsync(data);
            }
            catch
            {

            }
        }
        public StateLogin SaveLogin()
        {
            StateLogin store = new StateLogin
            {
                userName = userName,
                password = password
            };
            return store;
        }
        
        async Task<User> GetProductAsync(StateLogin login)
        {
            User user = null;
            Data data = null;
            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("https://mydrink-api.herokuapp.com/api/user/login", login);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<Data>(resp);
                //Application.Current.MainPage.DisplayAlert("Alert", "Login Successsssss" + data.success, "ok");
                Console.WriteLine(data);
                Debug.WriteLine(data);
                //Application.Current.MainPage.DisplayAlert("Alert", "Login Successsssss" + data.success, "ok");
            } else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Login Fail" + response.IsSuccessStatusCode, "ok");
            }
            return user;
        }
        string userName;
        string password;
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
        public string DisplayMessageValue
        {
            get
            {
                return userName + " ---- "+password;
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
