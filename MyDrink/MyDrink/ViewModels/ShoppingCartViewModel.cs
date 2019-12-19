using MyDrink.Helpers;
using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace MyDrink.ViewModels
{
    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<OrderItem> listOrders { get; set; } = new ObservableCollection<OrderItem>();
        public float totalPriceOrder { get; set; }
        public ShoppingCartViewModel()
        {
            this.listOrders = GetCart();
            Thread.Sleep(1000);
            this.totalPriceOrder = GetTotalPricePrder();
        }
        public ObservableCollection<OrderItem> GetCart()
        {
            DatabaseOrder db = new DatabaseOrder();
            List<OrderItem> listOrders = db.GetOrder();
            if (listOrders == null)
            {
                return null;
            } else
            {
                return new ObservableCollection<OrderItem>(listOrders);
            }
            
        }
        public float GetTotalPricePrder()
        {
            float total = 0;
            if( listOrders != null)
            {
                for (int i = 0; i < listOrders.Count; i++)
                {
                    total += listOrders[i].totalPrice;
                }
            }
            
            return total;
        }
    }
}
