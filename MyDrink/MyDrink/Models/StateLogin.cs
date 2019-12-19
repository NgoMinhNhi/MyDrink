using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDrink.Models
{
    public class StateLogin
    {
        public string _id { get; set; }
        public int isAdmin { get; set; }
        public StateLogin()
        {
        }
        public StateLogin(string id , int admin)
        {
            this._id = id;
            this.isAdmin = admin;
        }
    }
}
