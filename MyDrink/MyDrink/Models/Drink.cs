using System;
using System.Collections.Generic;
using System.Text;

namespace MyDrink.Models
{
    public class Drink
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string imgSrc { get; set; }
        public float price { get; set; }
        public string type { get; set; }
        public string detail { get; set; }
        public int totalLikes { get; set; }
        public string status { get; set; }
        public Drink (string id, string n, float p, string t, string dt, string st, string src)
        {
            this._id = id;
            this.name = n;
            this.price = p;
            this.type = t;
            this.detail = dt;
            this.status = st;
            this.imgSrc = src;
        }

    }
}
