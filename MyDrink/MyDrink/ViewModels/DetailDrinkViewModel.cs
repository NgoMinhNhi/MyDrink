using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyDrink.ViewModels
{
    public class DetailDrinkViewModel: INotifyPropertyChanged
    {
        public List<SizeDrink> listSizeDrink { get; set; }
        public List<QuantityDrink> listQuantityDrink { get; set; }
        public SizeDrink _selectedSize { get; set; }
        public string detail { get; set; }
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
