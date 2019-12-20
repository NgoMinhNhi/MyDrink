using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyDrink.Models
{
    public class OrderData
    {
        public string _id { get; set; }
        public DateTime timeOrder { get; set; }
        public string userId { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public ObservableCollection<OrderItem> listItem { get; set; }
        public OrderData()
        {
        }
        public OrderData(string _id, DateTime time, string userId, string phoneNumber, string address, ObservableCollection<OrderItem> listItem )
        {
            this._id = _id;
            this.timeOrder = time;
            this.userId = userId;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.listItem = listItem;
        }
    }
}
