using MyDrink.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDrink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel();
        }
        public HomePage(string fill, string value)
        {
            InitializeComponent();
            BindingContext = new HomePageViewModel(fill, value);
        }

    }
}