using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyDrink.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace MyDrink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profiles : ContentPage
    {
        public class ImagesURL
        {
            public string name;
            public ImagesURL()
            {

            }
        }

        public Profiles()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }
    }
}