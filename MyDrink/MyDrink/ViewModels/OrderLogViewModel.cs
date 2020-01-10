using MyDrink.Helpers;
using MyDrink.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MyDrink.Models;
using MyDrink.Views;

namespace MyDrink.ViewModels
{
    public class OrderLogViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrderData> listOrder { get; set; } = new ObservableCollection<OrderData>();

        public Command GetListOrderCartCommand { get; }
        public string title { get; set; }
        public Command<OrderData> DetailOrderCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public OrderLogViewModel()
        {
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store.isAdmin == 0)
            {
                this.title = "Order Log";
            } else
            {
                title = "Management Order";
            }
            GetListOrder();
            DetailOrderCommand = new Command<OrderData>(async (value) => await DetailOrderClick(value));
        }
        public async Task DetailOrderClick(OrderData data)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailOrder(data));
        }
        async Task GetListOrder()
        {
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            string path;
            if (store.isAdmin == 1)
            {
                path = "https://mydrink-api.herokuapp.com/api/order/get-all-order";
            }
            else
            {
                path = "https://mydrink-api.herokuapp.com/api/order/get-by-user/"+store._id;
            }
          
            
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    ObservableCollection<OrderData> listData;
                    var resp = await response.Content.ReadAsStringAsync();

                    listData = JsonConvert.DeserializeObject<ObservableCollection<OrderData>>(resp);
                    for (int i = 0; i < listData.Count; i++)
                    {
                        this.listOrder.Add(listData[i]);
                    }
                    Console.WriteLine(this.listOrder);

                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
            }
            
           
            Console.WriteLine(this.listOrder);
        }
    }
}
