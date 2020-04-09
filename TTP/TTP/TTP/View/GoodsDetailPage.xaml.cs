﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoodsDetailPage : ContentPage
    {
        public GoodsDetailPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("提示", "删除商品成功", "OK");
            await Navigation.PopAsync();
        }
    }
}