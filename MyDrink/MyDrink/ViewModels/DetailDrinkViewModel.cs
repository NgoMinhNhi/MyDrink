using MyDrink.Helpers;
using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDrink.ViewModels
{
    public class DetailDrinkViewModel: INotifyPropertyChanged
    {
        public List<SizeDrink> listSizeDrink { get; set; }
        public List<QuantityDrink> listQuantityDrink { get; set; }
        public Command AddToCartCommand { get; }
        public SizeDrink _selectedSize { get; set; }
        public string detail { get; set; }
        public bool isBusy { get; set; }
        public SizeDrink SelectedSize
        {
            get { return _selectedSize; }
            set
            {
                if (_selectedSize != value)
                {
                    _selectedSize = value;
                }
            }
        }
        public Drink detailDrink { get; set; }
        public DetailDrinkViewModel(Drink drink)
        {
            this.detailDrink = drink;
            listSizeDrink = GetListSizeDrink().OrderBy(t => t.Value).ToList();
            listQuantityDrink = GetListQuantityDrink().OrderBy(t => t.Value).ToList();
            AddToCartCommand = new Command(async () => await AddToCart());
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public List<SizeDrink> GetListSizeDrink()
        {
            var listSize = new List<SizeDrink>()
            {
                new SizeDrink(){ Key = 1, Value = "M"},
                new SizeDrink(){ Key = 2, Value = "L"},
            };
            return listSize;
        }
        public List<QuantityDrink> GetListQuantityDrink()
        {
            var listQuantity = new List<QuantityDrink>()
            {
                new QuantityDrink(){ Value = 1},
                new QuantityDrink(){Value = 2},
                new QuantityDrink(){Value = 3},
                new QuantityDrink(){Value = 4},
                 new QuantityDrink(){Value = 5},
            };
            return listQuantity;
        }
        async Task AddToCart()
        {
            DatabaseOrder db = new DatabaseOrder();
            this.isBusy = false;
            db.createDatabase();
            if (db.GetOrderItem(this.detailDrink._id) == null)
            {
                if (db.InsertOrderItem(new OrderItem(this.detailDrink._id, this.detailDrink.name, this.detailDrink.price, this.detailDrink.price* listQuantityDrink[selectedQuantityIndex].Value, listQuantityDrink[selectedQuantityIndex].Value, detail)))
                {
                    this.isBusy = false;
                    Application.Current.MainPage.DisplayAlert("Alert", "Add To Cart Success", "ok");
                    this.selectedQuantityIndex = 0;
                    this.detail = null;
                    List<OrderItem> order = db.GetOrder();
                    Console.WriteLine(order);
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Add To Cart Error", "ok");
                }
            } else
            {
                OrderItem existItem = db.GetOrderItem(this.detailDrink._id);
                existItem.quantity += listQuantityDrink[selectedQuantityIndex].Value;
                existItem.totalPrice = existItem.quantity * this.detailDrink.price;
                if (db.UpdateOrderItem(existItem))
                {
                    this.isBusy = false;
                    Application.Current.MainPage.DisplayAlert("Alert", "Add More To Cart Success", "ok");
                } else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Add To Cart Error", "ok");
                }
            }
            
        }
        public int selectedSizeIndex = 0;
        public int selectedQuantityIndex = 0;
        public int SelectedSizeIndex
        {
            get { return selectedSizeIndex; }
            set { 
                selectedSizeIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedQuantityIndex
        {
            get { return selectedQuantityIndex; }
            set
            {
                selectedQuantityIndex = value;
                OnPropertyChanged();
            }
        }
        public string Detail
        {
            get { return detail; }
            set
            {
                detail = value;
                OnPropertyChanged();
            }
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

    }
    public class SizeDrink
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
    public class QuantityDrink
    {
        public int Value { get; set; }
    }
}
