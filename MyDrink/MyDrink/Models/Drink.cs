using System;
using System.Collections.Generic;
using System.Text;

namespace MyDrink.Models
{
    public class Drink
    {
        public string _id { get; set; }
        public string name { get; set; }
        //public string image { get; set; }
        public float price { get; set; }
        //public string size { get; set; }
        public string detail { get; set; }
        public int totalLikes { get; set; }
        public string status { get; set; }
        public Drink (string id, string n, float p, string dt)
        {
            this._id = id;
            this.name = n;
            this.price = p;
            this.detail = dt;
        }

    }
}
