using MyDrink.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyDrink.Models;

namespace MyDrink.Helpers
{
    public class DatabaseOrder
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool createDatabase()
        {
            try
            {
                //tạo csdl
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "MyDrinkOrder.db")))
                {
                    connection.CreateTable<OrderItem>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                //
                return false;
            }
        }
        public List<OrderItem> GetOrder()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "MyDrinkOrder.db")))
                {
                    var data = (from orderitem in connection.Table<OrderItem>() select orderitem).ToList();
                    return data;
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }
        public OrderItem GetOrderItem(string id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "MyDrinkOrder.db")))
                {
                    return connection.Table<OrderItem>().FirstOrDefault(t => t.drinkId == id);
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }
        public bool InsertOrderItem(OrderItem data)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "MyDrinkOrder.db")))
                {
                    connection.Insert(data);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public bool UpdateOrderItem(OrderItem data)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "MyDrinkOrder.db")))
                {
                    connection.Update(data);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public bool DeleteOrderItem(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "MyDrinkOrder.db")))
                {
                    connection.Delete<OrderItem>(id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {

                return false;
            }
        }
        public bool DeleteTableOrder()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "MyDrinkOrder.db")))
                {
                    connection.DropTable<OrderItem>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }
    }

}
