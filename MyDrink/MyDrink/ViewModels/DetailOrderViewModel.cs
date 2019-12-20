using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyDrink.ViewModels
{
    public class DetailOrderViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public float totalPriceOrder { get; set; }
        public OrderData order { get; set; }

        public DetailOrderViewModel(OrderData data)
        {
            this.totalPriceOrder = GetTotalPricePrder(data);
            this.order = data;
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
