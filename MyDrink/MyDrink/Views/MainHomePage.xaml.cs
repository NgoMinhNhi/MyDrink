using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyDrink.Helpers;
using MyDrink.Models;
using MyDrink.ViewModels;
using Quobject.SocketIoClientDotNet.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDrink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainHomePage : ContentPage
    {
        //public PoolWebsocket dataSource = new PoolWebsocket();
        public MainHomePage()
        {
            InitializeComponent();
            BindingContext = new MainHomePageViewModel();
            Instance = this;
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store != null)
            {
                //var socket = IO.Socket("https://mydrink-api.herokuapp.com");
                //socket.On(Socket.EVENT_CONNECT, () =>
                //{
                //    socket.Emit("authentication", store._id);
                //});
                //socket.On("NEW_ORDER", async (data) =>
                //{
                //    //if (data != null)
                //    //{
                //    //    //Noti.Text = "New Order";
                //    //    // Debug.WriteLine("aaaba " + Noti.Text);
                //    //    //await this.DisplayAlert("Alert", "You have been alerted", "OK");
                       
                //    //}
                //    //Console.WriteLine(data);

                //});

                //socket.On(Socket.EVENT_DISCONNECT, (data) =>
                //{
                //  Console.WriteLine(data.ToString());
                //});
            }
           

        }
        public static MainHomePage Instance { get; private set; }
        public async void Show()
        {
            InitializeComponent();
            Debug.WriteLine("aaaba vo ham`");
            
            await DisplayAlert("Alert", "You have been alerted", "OK");
      
            Debug.WriteLine("aaaab out ham`");
        }
    }
}