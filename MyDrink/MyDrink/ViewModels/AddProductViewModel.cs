using MyDrink.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDrink.ViewModels
{
    class AddProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string name_product;
        public float price_product;
        public string size_product;
        public string detail_product;
        public string status_product;
        public List<StatusProduct> listStatus { get; set; }
        public List<TypeProduct> listTypes { get; set; }
        public int selectedIndex = 0;
        public int selectedIndexType = 0;
        //public string[] listStatus = { "Available", "Hot", "Percent Off", "Out Stock", "Delete" };
        public AddProductViewModel()
        {
            listStatus = GetStatusProduct().ToList();
            listTypes = GetTypeProduct().ToList();
            CreateProductCommand = new Command(async () => await CreateProduct());
        }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public class StatusProduct
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
        public class TypeProduct
        {
            public int Key { get; set; }
            public string Value { get; set; }
        }
        public List<StatusProduct> GetStatusProduct()
        {
            var listSize = new List<StatusProduct>()
            {
                new StatusProduct(){ Key = 1, Value = "Available"},
                new StatusProduct(){ Key = 2, Value = "New"},
                new StatusProduct(){ Key = 3, Value = "Hot"},
                new StatusProduct(){ Key = 4, Value = "Percent Off"},
                new StatusProduct(){ Key = 5, Value = "Out Stock"},
                new StatusProduct(){ Key = 6, Value = "Delete"}
            };
            return listSize;
        }
        public List<TypeProduct> GetTypeProduct()
        {
            var listType = new List<TypeProduct>
            {
                new TypeProduct(){ Key = 1, Value = "Milk Tea"},
                new TypeProduct(){ Key = 2, Value = "Coffee"},
                new TypeProduct(){ Key = 3, Value = "Juice"},
                new TypeProduct(){ Key = 4, Value = "Ice blended"},
                new TypeProduct(){ Key = 5, Value = "Hot Drink"},
                new TypeProduct(){ Key = 6, Value = "Cocktail"},
            };
            return listType;
        }
        public string Name
        {
            get { return name_product; }
            set
            {
                name_product = value;
                OnPropertyChanged();
            }
        }
        public float Price
        {
            get { return price_product; }
            set
            {
                price_product = value;
                OnPropertyChanged();
            }
        }
        public string Size
        {
            get { return size_product; }
            set
            {
                size_product = value;
                OnPropertyChanged();
            }
        }
        public string Detail
        {
            get { return detail_product; }
            set
            {
                detail_product = value;
                OnPropertyChanged();
            }
        }
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }
 
        public int SelectedIndexType
        {
            get { return selectedIndexType; }
            set
            {
                selectedIndexType = value;
                OnPropertyChanged();
            }
        }
        public Command CreateProductCommand { get; }

        async Task CreateProduct()
        {

            if (Name.Length != 0 && Detail.Length != 0 && Price > 0)
            {
                try
                {
                    FormData data = new FormData(Name, Price, listStatus[selectedIndex].Value, listTypes[selectedIndexType].Value, Detail);
                    CreateProduct(data);
                }
                catch
                {

                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Alert", "All fields is required", "ok");
            }

        }
        async void CreateProduct(FormData data)
        {
            try
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync("https://mydrink-api.herokuapp.com/api/drink/create-product", data);
                if (response.IsSuccessStatusCode)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Create Product Success", "ok");
                    Application.Current.MainPage = new MainShell();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Create Product Faill", "ok");
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
            }
        }
        public class FormData
        {
            public string name { get; set; }
            public float price { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public string detail { get; set; }
            public FormData( string n, float p, string s, string t, string d)
            {
                this.name = n;
                this.price = p;
                this.status = s;
                this.type = t;
                this.detail = d;
            }
        }
    }
}
