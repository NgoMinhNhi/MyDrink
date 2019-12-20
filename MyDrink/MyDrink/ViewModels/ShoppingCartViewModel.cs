using MyDrink.Helpers;
using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDrink.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<OrderItem> listOrders { get; set; } = new ObservableCollection<OrderItem>();
        public float totalPriceOrder { get; set; }
        public Command SubmitOrderCommand { get; }

        public ShoppingCartViewModel()
        {
            this.listOrders = GetCart();
            Thread.Sleep(1000);
            this.totalPriceOrder = GetTotalPricePrder();
            SubmitOrderCommand = new Command(async () => await SubmitOrder());
        }
        public ObservableCollection<OrderItem> GetCart()
        {
            DatabaseOrder db = new DatabaseOrder();
            List<OrderItem> listOrders = db.GetOrder();
            if (listOrders == null)
            {
                return null;
            } else
            {
                return new ObservableCollection<OrderItem>(listOrders);
            }
            
        }
        public float GetTotalPricePrder()
        {
            float total = 0;
            if( listOrders != null)
            {
                for (int i = 0; i < listOrders.Count; i++)
                {
                    total += listOrders[i].totalPrice;
                }
            }
            
            return total;
        }
        public class OrderForm
        {
            public string userId { get; set; }
            public string phoneNumber { get; set; }
            public string address { get; set; }
            public List<OrderItem> listItem { get; set; }
            public OrderForm()
            {

            }
            public OrderForm( string userId, string phoneNumber, string address, List<OrderItem> listItem)
            {
                this.userId = userId;
                this.phoneNumber = phoneNumber;
                this.address = address;
                this.listItem = listItem;
            }
        }
        async Task SubmitOrder()
        {
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store != null)
            {
                User user = null;
                try
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync("https://mydrink-api.herokuapp.com/api/user/" + store._id);
                    if (response.IsSuccessStatusCode)
                    {
                        user = await response.Content.ReadAsAsync<User>();
                        if (user == null)
                        {
                            Application.Current.MainPage.DisplayAlert("Alert", "Ocur Error", "ok");
                        }
                        else
                        {
                            List<OrderItem> data = new List<OrderItem>();
                            for (int i = 0; i < this.listOrders.Count; i++)
                            {
                                data.Add(this.listOrders[i]);
                            }
                            OrderForm orderForm = new OrderForm(store._id, user.phoneNumber, user.address, data);
                            var clientPut = new HttpClient();
                            HttpResponseMessage responsePut = await client.PostAsJsonAsync("https://mydrink-api.herokuapp.com/api/order/create-order", orderForm);
                            if (responsePut.IsSuccessStatusCode)
                            {
                                DatabaseOrder dbOrder = new DatabaseOrder();
                                dbOrder.DeleteTableOrder();
                                this.listOrders = null;
                                Application.Current.MainPage.DisplayAlert("Alert", "Order Success", "ok");

                            }
                            else
                            {
                                Application.Current.MainPage.DisplayAlert("Alert", "Order Fail", "ok");
                            }
                        }
                        Console.WriteLine(user);
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Alert", "Error", "ok");
                    }
                }
                catch
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
                }
                
                
            }
        }
    }
}
