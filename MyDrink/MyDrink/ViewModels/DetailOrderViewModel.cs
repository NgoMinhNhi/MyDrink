using MyDrink.Helpers;
using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace MyDrink.ViewModels
{
    public class DetailOrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public float totalPriceOrder { get; set; }
        public OrderData order { get; set; }
        public Command<string> CommandCall { get; }
        public int selectedItem = 0;
        public string status { get; set; }
        public List<StatusOrder> listStatus;
        public bool showSwitch { get; set; }
        public OrderData orderData { get; set; }
        public DetailOrderViewModel(OrderData data)
        {
             Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store != null && store.isAdmin == 1)
            {
                this.showSwitch = true;
            } else
            {
                this.showSwitch = false;
            }
            this.orderData = data;
            this.order = data;
            this.totalPriceOrder = GetTotalPricePrder(data);
            this.status = data.status;
            this.listStatus = GetStatusOrder();
            this.selectedItem = getTypeIndex(data.status);
            CommandCall = new Command<string>(async (phone) => await Call(phone));
        }
        public List<StatusOrder> ListStatus
        {
            get { return listStatus; }
            set
            {
                listStatus = value;
                OnPropertyChanged();
            }
        }
        public class StatusOrder
        {
            public int Key { get; set; }
            public string Value { get; set; }
            public StatusOrder()
            {

            }
            public StatusOrder(int k, string v)
            {
                this.Key = k;
                this.Value = v;
            }
        }
        public List<StatusOrder> GetStatusOrder()
        {
            var listType = new List<StatusOrder>
            {
                new StatusOrder(){Key = 1, Value = "Wait Confirm" },
                new StatusOrder(){ Key = 2, Value = "Doing" },
                new StatusOrder(){Key= 3, Value = "Serviced" },
                new StatusOrder(){Key = 4, Value = "Reject" }
            };
            return listType;
        }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                if (this.orderData.status != listStatus[value].Value)
                {
                    this.orderData.status = listStatus[value].Value;
                    ChangeStatusOrder(orderData);
                }
                
                OnPropertyChanged();
            }
        }
        public int getTypeIndex(string status)
        {
            int temp = 0;
            foreach (var i in this.listStatus)
            {
                if (i.Value == status)
                {
                    temp = this.listStatus.IndexOf(i);
                }
            }
            return temp;
        }
        async Task Call(string phone)
        {
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store.isAdmin == 1)
            {
                Device.OpenUri(new Uri("tel:" + phone));
            }
            
        }
        public class FormEdit
        {
            public string id { get; set; }
            public string status { get; set; }
            public FormEdit()
            {

            }
        }
        async Task ChangeStatusOrder(OrderData order)
        {
            try
            {
                var client = new HttpClient();
                FormEdit data = new FormEdit() { id = order._id, status = order.status };
                HttpResponseMessage response = await client.PutAsJsonAsync("https://mydrink-api.herokuapp.com/api/order/5e04b87168591400246f0628", data);
                if (response.IsSuccessStatusCode)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Update Order Success", "ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Update Order Faill", "ok");
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Fail", "ok");
            }
            
        }
        public float GetTotalPricePrder(OrderData data)
        {
            float total = 0;
            if (data.listItem != null)
            {
                for (int i = 0; i < data.listItem.Count; i++)
                {
                    total += data.listItem[i].totalPrice;
                }
            }

            return total;
        }
    }
}
