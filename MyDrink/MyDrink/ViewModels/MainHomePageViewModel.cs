using MyDrink.Helpers;
using MyDrink.Models;
using MyDrink.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDrink.ViewModels
{
    class MainHomePageViewModel : INotifyPropertyChanged
    {
        public Command<string> FillByStatusCommand { get; }
        public Command<string> FillByTypeCommand { get; }
        public Command OpenShoppingCartCommand { get; }
        public Command OrderLogCommand { get; }
        public string noti { get; set; }
        public static string icon { get; set; }
        public string Test 
        {
            get => _test;
            set
            {
                if (value != _test)
                {
                    _test = value;
                    OnPropertyChanged();
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public PoolWebsocket dataSource = new PoolWebsocket();
        private string _test;

        public MainHomePageViewModel()
        {

            start();

            Noti = null;
            icon = "notify.png";
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            dataSource.DataRecieved += async (s, o) =>
            {
                if (o != null)
                {
        
                    this.Noti = "You have new order !";
                    this.noti = "You have new order !";

                }
            };
            if (store != null)
            {
                dataSource.StartLoadingData(store._id);
            }
            FillByStatusCommand = new Command<string>(async (value) => await FillByStatus(value));
            FillByTypeCommand = new Command<string>(async (value) => await FillByType(value));
            OpenShoppingCartCommand = new Command(async () => await OpenShoppingCart());
            OrderLogCommand = new Command(async () => await OrderLog());
            Noti = noti;
        }

        private async void start()
        {
            //await Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
        }

        public string Noti
        {
            get { return noti; }
            set
            {
                noti = value;
                OnPropertyChanged();
            }
        }
        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                OnPropertyChanged();
            }
        }

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public async Task FillByStatus(string status)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage("status", status));
        }
        public async Task FillByType(string type)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage("type", type));
        }
        public async Task OpenShoppingCart()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ShoppingCart());
        }
        public async Task OrderLog()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ShoppingCart());
        }
        
    }
}
