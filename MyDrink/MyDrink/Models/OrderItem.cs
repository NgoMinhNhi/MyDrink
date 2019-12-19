using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDrink.Models
{
    public class OrderItem
    {
        [PrimaryKey, AutoIncrement]
        public int _id { get; set; }
        public string drinkId { get; set; }
        public string drinkName { get; set; }
        public float drinkPrice { get; set; }
        public float totalPrice { get; set; }
        public int drinkQuantity { get; set; }
        public int quantity { get; set; }
        public string detail { get; set; }
        public OrderItem()
        {

        }
        public OrderItem ( string dId, string dName, float price, float dPrice, int q, string dt)
        {
            this.drinkId = dId;
            this.drinkName = dName;
            this.drinkPrice = price;
            this.totalPrice = dPrice;
            this.quantity = q;
            this.detail = dt;
        }
    }
}
