using MyDrink.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyDrink.ViewModels
{
    class MainHomePageViewModel : INotifyPropertyChanged
    {
        public Command<string> FillByStatusCommand { get; }
        public Command<string> FillByTypeCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public MainHomePageViewModel()
        {
            FillByStatusCommand = new Command<string>(async (value) => await FillByStatus(value));
            FillByTypeCommand = new Command<string>(async (value) => await FillByType(value));
        }
        public async Task FillByStatus(string status)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage("status", status));
        }
        public async Task FillByType(string type)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new HomePage("type", type));
        }
    }
}
