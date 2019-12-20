using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyDrink.Helpers;
using MyDrink.Views;

namespace MyDrink.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string userName;
        string phoneNumber;
        string password;
        string confirmPassword;
        string address;
        public class FormRegister
        {
            public string userName { get; set; }
            public string phoneNumber { get; set; }
            public string password { get; set; }
            public string address { get; set; }
            public FormRegister(string username, string phonenumber, string pass, string address)
            {
                this.userName = username;
                this.password = pass;
                this.phoneNumber = phonenumber;
                this.address = address;
            }
        }
        public RegisterViewModel ()
        {
            CommandSignUp = new Command(async () => await SignUp());
        }
        async Task SignUp()
        {
            if (userName.Length != 0 && phoneNumber.Length != 0 && password.Length !=0 && confirmPassword.Length != 0 && address.Length !=0)
            {
                if (string.Compare(password, confirmPassword) == 0)
                {

                    //GetLoginAsync(new FormRegister(userName, phoneNumber, password, address));
                    CheckExistPhoneNumber(phoneNumber);
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Confirm password not match", "ok");
                } 
            } else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "You must fill all field", "ok");
            }
            
        }
        Database db = new Database();
        async Task GetLoginAsync(FormRegister register)
        {
            User user = null;
            //Data data = null;
            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("https://mydrink-api.herokuapp.com/api/user/registry", register);
            if (response.IsSuccessStatusCode)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Register Success", "ok");
                user = await response.Content.ReadAsAsync<User>();
                db.createDatabase();
                if (db.InsertStateLogin(new StateLogin(user._id, user.isAdmin)))
                {
                    Application.Current.MainPage = new MainShell();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Error", "ok");
                }

            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Register Fail" , "ok");
            }
        }
        async Task CheckExistPhoneNumber(string phone)
        {
            User user = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://mydrink-api.herokuapp.com/api/user/get-user-by-phone/"+phone);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
                if( user == null)
                {
                    await GetLoginAsync(new FormRegister(userName, phoneNumber, password, address));
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Phone number has been registered", "ok");
                }
                Console.WriteLine(user);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Register Fail", "ok");
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
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }
        public Command CommandSignUp { get; }
    }
}
