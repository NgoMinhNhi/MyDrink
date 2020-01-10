using MyDrink.Helpers;
using MyDrink.Models;
using MyDrink.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
        public Command DeleteItemCommand { get; }
        public string userAddress { get; set; }
        public User user { get; set; }
        public ShoppingCartViewModel()
        {
            GetCart();
            GetUserInfo();
            Thread.Sleep(1000);
            this.totalPriceOrder = GetTotalPricePrder();
            SubmitOrderCommand = new Command(async () => await SubmitOrder());
            DeleteItemCommand = new Command<int>(async (id) => await DeleteItem(id));
        }
        public async Task GetCart()
        {
            DatabaseOrder db = new DatabaseOrder();
            List<OrderItem> listOrders = db.GetOrder();
            if (listOrders == null)
            {

            }
            else
            {
                this.ListOrders = new ObservableCollection<OrderItem>(listOrders);
            }

        }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public float TotalPriceOrder
        {
            get { return GetTotalPricePrder(); }
            set
            {
                totalPriceOrder = value;
                OnPropertyChanged();
            }
        }
        public string UserAddress
        {
            get { return userAddress; }
            set
            {
                userAddress = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<OrderItem> ListOrders
        {
            get { return listOrders; }
            set
            {
                listOrders = value;
                OnPropertyChanged();
            }
        }
        public float GetTotalPricePrder()
        {
            float total = 0;
            DatabaseOrder db = new DatabaseOrder();
            List<OrderItem> list = db.GetOrder();
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    total += list[i].totalPrice;
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
            public OrderForm(string userId, string phoneNumber, string address, List<OrderItem> listItem)
            {
                this.userId = userId;
                this.phoneNumber = phoneNumber;
                this.address = address;
                this.listItem = listItem;
            }
        }
        async Task DeleteItem(int order)
        {
            DatabaseOrder db = new DatabaseOrder();
            if (db.DeleteOrderItem(order))
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Delete Success", "ok");
                //this.listOrders.Clear();
                await GetCart();
                TotalPriceOrder = this.GetTotalPricePrder();
                Console.WriteLine(this.ListOrders);

            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Delete Error", "ok");
            }
            db.DeleteOrderItem(order);
        }
        async Task GetUserInfo()
        {
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store != null)
            {
                try
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync("https://mydrink-api.herokuapp.com/api/user/" + store._id);
                    if (response.IsSuccessStatusCode)
                    {
                        this.user = await response.Content.ReadAsAsync<User>();
                        this.UserAddress = user.address;
                        OnPropertyChanged();
                    }
                }
                catch
                {

                }
            }
        }
            async Task SubmitOrder()
            {
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
                    OrderForm orderForm = new OrderForm(user._id, user.phoneNumber, this.userAddress, data);
                    try
                    {
                        var client = new HttpClient();
                        HttpResponseMessage responsePut = await client.PostAsJsonAsync("https://mydrink-api.herokuapp.com/api/order/create-order", orderForm);
                        if (responsePut.IsSuccessStatusCode)
                        {
                            DatabaseOrder dbOrder = new DatabaseOrder();
                            dbOrder.DeleteTableOrder();
                            this.listOrders = null;
                            Application.Current.MainPage.DisplayAlert("Alert", "Order Success", "ok");
                            Application.Current.MainPage = new MainShell();
                        }
                        else
                        {
                            Application.Current.MainPage.DisplayAlert("Alert", "Order Fail", "ok");
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

