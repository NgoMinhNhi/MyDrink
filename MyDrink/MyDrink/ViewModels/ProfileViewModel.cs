using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using MyDrink.Views;
using System.Threading.Tasks;
using MyDrink.Helpers;
using MyDrink.Models;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace MyDrink.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public Command OpenOtherPageCommand { get; }
        public User user { get; set; }
        public string test { get; set; }
        public ProfileViewModel()
        {
            GetUser();
            test = "45512552252";
            OpenOtherPageCommand = new Command(async () => await OpenOtherPage());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public string UserName
        {
            get { return user.userName; }
        }
        public string Address
        {
            get { return user.address; }
        }
        public string PhoneNumber
        {
            get { return user.phoneNumber; }
        }
        public string Email
        {
            get { return user.email; }
        }
        async Task GetUser()
        {
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            User user = null;
            if (store != null)
            {
                try
                {

                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync("https://mydrink-api.herokuapp.com/api/user/"+store._id);
                    if (response.IsSuccessStatusCode)
                    {
                        this.user = await response.Content.ReadAsAsync<User>();
                        OnPropertyChanged();
                        Console.WriteLine(user);
                    }
                }
                catch
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
                }
            }
        }
        public async Task OpenOtherPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new EditProfile());
        }
    }
}
