﻿using MyDrink.Helpers;
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
namespace MyDrink.ViewModels
{
    public class OrderLogViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrderData> listOrder { get; set; } = new ObservableCollection<OrderData>();

        public Command GetListOrderCartCommand { get; }
        public string title { get; set; }
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
        }
        
        async Task GetListOrder()
        {
            string path = "https://mydrink-api.herokuapp.com/api/order/get-all-order";
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
            Console.WriteLine(this.listOrder);
        }
    }
}
