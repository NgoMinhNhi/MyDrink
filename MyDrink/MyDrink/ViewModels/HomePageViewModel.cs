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
        public PoolWebsocket dataSource = new PoolWebsocket();
        public Drink _selectedDrinnk { get; set; }
        public Command DetailDrinkCommand { get; }
        public Command CreateProductCommand { get; }
        public Command OpenShoppingCartCommand { get; }
        public Command ToOrderCommand { get; }
        public string icon { get; set; }
        public string titlePage { get; set; }
        public int marginTop { get; set; }
        public HomePageViewModel()
        {
            icon = "notify.png";
            this.marginTop = 0;
            this.titlePage = "All Product";
            this.isBusy = true;
            GetAllDrinksAsync("https://mydrink-api.herokuapp.com/api/drink/get-all-product");
            DetailDrinkCommand = new Command<Drink>(async (drink) => await OpenDetailDrink(drink));
            CreateProductCommand = new Command(async () => await CreateProduct());
            OpenShoppingCartCommand = new Command(async () => await OpenShoppingCart());
            ToOrderCommand = new Command(async () => await ToOrder());
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            dataSource.DataRecieved += async (s, o) =>
            {
                if (o != null)
                {
                    Console.WriteLine(o);
                    this.Icon = "notification.png";
                    await Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
                    OnPropertyChanged();

                }
            };
            if (store != null)
            {
                dataSource.StartLoadingData(store._id);
            }


        }
        public string Icon
        {
            get { return icon; }
            set { icon = value;
                OnPropertyChanged();
            }
        }
       

        public Thickness Margin
        {
            get { return new Thickness(0, this.marginTop,0, 0); }
        }
        public HomePageViewModel(string fill, string value)
        {
            icon = "notify.png";
            Database db = new Database();
            StateLogin store = db.GetStateLogin();
            if (store.isAdmin == 1)
            {
                this.marginTop = 0;
            } else
            {
                this.marginTop = 60;
            }

            dataSource.DataRecieved += async (s, o) =>
            {
                if (o != null)
                {
                    Console.WriteLine(o);
                    this.Icon = "notification.png";
                    OnPropertyChanged();
                }
            };
            if (store != null)
            {
                dataSource.StartLoadingData(store._id);
            }
            this.titlePage =value;
            this.isBusy = true;
            GetDrinkFilter("https://mydrink-api.herokuapp.com/api/drink/get-product-by-" + fill + "/" + value);
            
            DetailDrinkCommand = new Command<Drink>(async (drink) => {
                if (store.isAdmin == 1)
                {
                    await EditProduct(drink);
                } else
                {
                    await OpenDetailDrink(drink);
                }
                });
            CreateProductCommand = new Command(async () => await CreateProduct());
            OpenShoppingCartCommand = new Command(async () => await OpenShoppingCart());
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
        public bool isBusy { get; set; }
        public bool oddDrink { get; set; }
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
        async public void GetAllDrinksAsync(string path)
        {
            try
            {
                ObservableCollection<Drink> listDrink;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    this.isBusy = false;
                    var resp = await response.Content.ReadAsStringAsync();

                    listDrink = JsonConvert.DeserializeObject<ObservableCollection<Drink>>(resp);
                    for (int i = 0; i < listDrink.Count; i += 2)
                    {
                        if (i + 1 < listDrink.Count)
                        {
                            this.listDrinks.Add(new DrinkPair(listDrink[i], listDrink[i + 1]));
                            this.oddDrink = true;
                        }
                    }
                    if (listDrink.Count % 2 == 1)
                    {
                        this.oddDrink = false;
                        this.listDrinks.Add(new DrinkPair(listDrink[listDrink.Count - 1], null));
                    }
                }
            } 
            catch
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
            }
          
            
            Console.WriteLine(this.listDrinks);
        }
       async public void GetDrinkFilter(string path)
        {
            try
            {
                ObservableCollection<Drink> listDrink;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    this.isBusy = false;
                    var resp = await response.Content.ReadAsStringAsync();

                    listDrink = JsonConvert.DeserializeObject<ObservableCollection<Drink>>(resp);
                    for (int i = 0; i < listDrink.Count; i += 2)
                    {
                        if (i + 1 < listDrink.Count)
                        {
                            this.listDrinks.Add(new DrinkPair(listDrink[i], listDrink[i + 1]));
                        }
                    }
                    if (listDrink.Count % 2 == 1)
                    {
                        this.listDrinks.Add(new DrinkPair(listDrink[listDrink.Count - 1], null));
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Empty product", "ok");
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
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
        public async Task OpenDetailDrink(Drink drink)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new DetailDrink(drink));
        }
        public async Task CreateProduct()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddProduct());
        }
        public async Task EditProduct(Drink drink)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddProduct(drink));
        }
        public async Task OpenShoppingCart()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ShoppingCart());
        }
        public async Task ToOrder()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new OrderLog());
        }
        public event PropertyChangedEventHandler PropertyChanged;

        

    }
}
