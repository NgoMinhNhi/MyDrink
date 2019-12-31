using MyDrink.Models;
using MyDrink.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDrink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProduct : ContentPage
    {
        public AddProduct()
        {
            InitializeComponent();
            BindingContext = new AddProductViewModel();
            Instance = this;
        }

        public AddProduct(Drink drink)
        {
            InitializeComponent();
            BindingContext = new AddProductViewModel(drink);
            Instance = this;
        }
        public static AddProduct Instance { get; private set; }
        public void ReloadPage(string src)
        {
            //InitializeComponent();
            ImageSource image = ImageSource.FromUri(new Uri(src));
            //image.Source = src;
        }
    }
}