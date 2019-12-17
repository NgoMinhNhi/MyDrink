using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using MyDrink.Models;
using Xamarin.Forms;
using MyDrink.Views;
namespace MyDrink.ViewModels
{
    class HomePageViewModel: INotifyPropertyChanged
    {
        public Command OpenOtherPageCommand { get; }
        public Command CreateProductCommand { get; }

        //INavigation Navigation;

        public HomePageViewModel()
        {
            OpenOtherPageCommand = new Command(async () => await OpenOtherPage());
            CreateProductCommand = new Command(async () => await CreateProduct());
        }
        public async Task OpenOtherPage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailDrink());
        }
        public async Task CreateProduct()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddProduct());
        }
        public event PropertyChangedEventHandler PropertyChanged;

        

    }
}
