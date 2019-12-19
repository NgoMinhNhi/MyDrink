﻿using System;
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
            public string userName { get; set; }
            public string password { get; set; }
            public FormLogin()
            {
            }
            public FormLogin(string username, string pass)
            {
                this.userName = username;
                this.password = pass;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        Database db = new Database();
        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }
        async Task Login ()
        {
            if ( userName.Length != 0 && password.Length !=0)
            {
                try
                {
                    FormLogin data = new FormLogin(userName, password);
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
            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("https://mydrink-api.herokuapp.com/api/user/login", login);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
                db.createDatabase();
                if (db.InsertStateLogin(SaveLogin(user._id, user.isAdmin)))
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Login Success", "ok");
                    Application.Current.MainPage = new MainShell();
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Error", "ok");
                }
                
            } else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Login Fail", "ok");
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
