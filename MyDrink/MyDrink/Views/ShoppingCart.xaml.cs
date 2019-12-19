﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDrink.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyDrink.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCart : ContentPage
    {
        public ShoppingCart()
        {
            InitializeComponent();
            BindingContext = new ShoppingCartViewModel();
        }
    }
}