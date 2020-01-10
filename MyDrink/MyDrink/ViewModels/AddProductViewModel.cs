using MyDrink.Models;
using MyDrink.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public string type_product;
        public ImageSource imageSource;
        public List<StatusProduct> listStatus { get; set; }
        public List<TypeProduct> listTypes { get; set; }
        public int selectedIndex = 0;
        public int selectedIndexType = 0;
        public string imgSrc { get; set; }
        public string _id { get; set; }
        //public string[] listStatus = { "Available", "Hot", "Percent Off", "Out Stock", "Delete" };
        public AddProductViewModel()
        {
            this.imgSrc = null;
            listStatus = GetStatusProduct().ToList();
            listTypes = GetTypeProduct().ToList();
            CreateProductCommand = new Command(async () => await CreateProduct());
            TakePhotoCommand = new Command(async () => await TakePhoto());
            PickPhotoCommand = new Command(async () => await PickPhoto());
        }
        public AddProductViewModel(Drink drink)
        {
            this._id = drink._id;
            this.imgSrc = drink.imgSrc;
            this.name_product = drink.name;
            this.price_product = drink.price;
            this.status_product = drink.status;
            this.type_product = drink.type;
            this.detail_product = drink.detail;
            listStatus = GetStatusProduct().ToList();
            listTypes = GetTypeProduct().ToList();
            this.selectedIndex = getStatusIndex(drink.status);
            this.selectedIndexType = getTypeIndex(drink.type);
            CreateProductCommand = new Command(async () => EditProduct());
            TakePhotoCommand = new Command(async () => await TakePhoto());
            PickPhotoCommand = new Command(async () => await PickPhoto());
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
        public string ImgSrc
        {
            get { return imgSrc; }
            set
            {
                imgSrc = value;
                OnPropertyChanged();
            }
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
        public int getStatusIndex(string status)
        {
            int temp = 0;
            foreach (var i in this.listStatus)
            {
                if ( i.Value == status)
                {
                    temp =  this.listStatus.IndexOf(i);
                }
            }
            return temp;
        }
        public int getTypeIndex(string status)
        {
            int temp = 0;
            foreach (var i in this.listTypes)
            {
                if (i.Value == status)
                {
                    temp = this.listTypes.IndexOf(i);
                }
            }
            return temp;
        }
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                status_product = listStatus[value].Value;
                OnPropertyChanged();
            }
        }
 
        public int SelectedIndexType
        {
            get { return selectedIndexType; }
            set
            {
                selectedIndexType = value;
                type_product = listTypes[value].Value;
                OnPropertyChanged();
            }
        }
        public Command CreateProductCommand { get; }
        public Command TakePhotoCommand { get; }
        public Command PickPhotoCommand { get; }

        async Task CreateProduct()
        {

            if (Name != null && Detail != null && Price > 0)
            {
                try
                {
                    FormData data = new FormData(Name, Price, listStatus[selectedIndex].Value, listTypes[selectedIndexType].Value, Detail, ImgSrc);
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
        async void EditProduct()
        {
            try
            {
                Drink drink = new Drink(_id, name_product, price_product, type_product, detail_product, status_product, ImgSrc);
                var client = new HttpClient();
                HttpResponseMessage response = await client.PutAsJsonAsync("https://mydrink-api.herokuapp.com/api/drink/"+drink._id, drink);
                if (response.IsSuccessStatusCode)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Update Product Success", "ok");
                    Application.Current.MainPage = new MainShell();
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Update Product Faill", "ok");
                }
            }
            catch
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Connect Network Error", "ok");
            }
        }
        public async Task TakePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                Application.Current.MainPage.DisplayAlert("No Camera", "😞 No camera available.", "ok");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front
            });

            if (file == null)
                return;
            await addImageAsync(file);
        }
        public async Task PickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                Application.Current.MainPage.DisplayAlert("Photos Not Supported", "😞 Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });
            await addImageAsync(file);
        }
        public async Task addImageAsync(MediaFile file)
        {
            try
            {
                StreamContent scontent = new StreamContent(file.GetStream());
                scontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = "newimage",
                    Name = "image"
                };
                scontent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                var client = new HttpClient();
                var multi = new MultipartFormDataContent();
                multi.Add(scontent);
                client.BaseAddress = new Uri("https://mydrink-api.herokuapp.com/");
                var result = client.PostAsync("api/upload", multi).Result;
                //Debug.WriteLine(result.ReasonPhrase);
                Debug.WriteLine("Hello");
                Debug.WriteLine(result);
                if (result.IsSuccessStatusCode)
                {
                    ImagesURL url = new ImagesURL();
                    url = await result.Content.ReadAsAsync<ImagesURL>();
                    string ulruit = $"https://mydrink-api.herokuapp.com/upload/{url.name}";
                    string nameIMG = url.name;
                    this.ImgSrc = ulruit;
                    Application.Current.MainPage.DisplayAlert("Alert", "Upload image success", "OK");
                    AddProduct.Instance.ReloadPage(ulruit);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        public class FormData
        {
            public string name { get; set; }
            public float price { get; set; }
            public string status { get; set; }
            public string type { get; set; }
            public string detail { get; set; }
            public string imgSrc { get; set; }
            public FormData( string n, float p, string s, string t, string d, string src)
            {
                this.name = n;
                this.price = p;
                this.status = s;
                this.type = t;
                this.detail = d;
                this.imgSrc = src;
            }
        }
        public class ImagesURL
        {
            public string name;
            public ImagesURL()
            {

            }
        }

    }
}
