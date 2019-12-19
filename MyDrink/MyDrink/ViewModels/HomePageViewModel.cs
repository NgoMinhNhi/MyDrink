using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using MyDrink.Models;
using Xamarin.Forms;
using MyDrink.Views;
using MyDrink.Helpers;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace MyDrink.ViewModels
{
    class HomePageViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<DrinkPair> listDrinks { get; set; } = new ObservableCollection<DrinkPair>();
        public class DrinkPair
        {
            public Drink Item1 { get; set; }
            public Drink Item2 { get; set; }
            public DrinkPair( Drink item1, Drink item2)
            {
                this.Item1 = item1;
                this.Item2 = item2;
            }
        }
        public Drink _selectedDrinnk { get; set; }
        public Command DetailDrinkCommand { get; }
        public Command CreateProductCommand { get; }
        public HomePageViewModel()
        {
            GetAllDrinksAsync("https://mydrink-api.herokuapp.com/api/drink/get-all-product");
            DetailDrinkCommand = new Command<Drink>(async (drink) => await OpenOtherPage(drink));
            CreateProductCommand = new Command(async () => await CreateProduct());
        }
        public Drink SelectedDrink
        {
            get {
                return _selectedDrinnk; }
            set
            {
                _selectedDrinnk = value;
                OnPropertyChanged();
            }
        }
        public bool isAdmin { get; set; }
        public bool oddDrink { get; set; }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        async public void GetAllDrinksAsync(string path)
        {
            ObservableCollection<Drink> listDrink;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();

                listDrink = JsonConvert.DeserializeObject<ObservableCollection<Drink>>(resp);
                for (int i = 0; i < listDrink.Count; i +=2)
                {
                    if (i+1 < listDrink.Count)
                    {
                        this.listDrinks.Add(new DrinkPair(listDrink[i], listDrink[i + 1]));
                        this.oddDrink = true;
                    } else
                    {
                        this.listDrinks.Add(new DrinkPair(listDrink[i], null));
                        this.oddDrink = false;
                    }
                }
            }
            Console.WriteLine(this.listDrinks);
        }
        public bool IsAdmin
        {
            get {
                Database db = new Database();
                StateLogin store = db.GetStateLogin();
                if (store == null)
                {
                    return false;
                }
                else
                {
                    if (store.isAdmin == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            
        }
        public async Task OpenOtherPage(Drink drink)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailDrink(drink));
        }
        public async Task CreateProduct()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddProduct());
        }
        public event PropertyChangedEventHandler PropertyChanged;

        

    }
}
