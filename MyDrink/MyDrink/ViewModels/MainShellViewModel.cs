using MyDrink.Helpers;
using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyDrink.Views;
using System.Threading;

namespace MyDrink.ViewModels
{
    public class MainShellViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Database db = new Database();
        public string title { get; set; }
        public Command LogoutCommand { get; }
        public Command CheckInfoCommand { get; }

        public MainShellViewModel ()
        {
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store.isAdmin == 0)
            {
                this.title = "Order Log";
            }
            else
            {
                this.title = "Management Order";
            }
            LogoutCommand = new Command(async () => await Logout());
            CheckInfoCommand = new Command(async () => await CheckInfo());
        }
        async Task Logout()
        {
            try
            {
                if(db.DeleteStateLogin())
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Logout Success", "ok");
                    DatabaseOrder db = new DatabaseOrder();
                    db.DeleteTableOrder();
                    Thread.Sleep(500);
                    Application.Current.MainPage = new Welcome();
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Logout Fails", "ok");
                }
            }
            catch
            {

            }
        }
        async Task CheckInfo()
        {
            try
            {
                StateLogin store = db.GetStateLogin();
                Application.Current.MainPage.DisplayAlert("Alert", "inof" + store._id, "ok");
            }
            catch
            {

            }
        }
    }
}
