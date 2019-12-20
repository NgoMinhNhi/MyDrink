using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDrink.Models
{
    public class User
    {
        //[PrimaryKey, AutoIncrement]
        public string _id { get; set; }
        public string userName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set;  }
        public string email { get; set; }
        public int isAdmin { get; set; }
        public string status { get; set; }
        //public bool success { get; set; }
    }
}
