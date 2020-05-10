using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using TTP.ViewModel;
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
            GoodsDetailViewModel gdvm = BindingContext as GoodsDetailViewModel;
            await App.GoodsManager.DeleteGoodsTaskAsync(gdvm.GoodsModel);
            GoodsViewModel.refresh();
            await DisplayAlert("提示", "删除商品成功", "OK");
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            GoodsDetailViewModel gdvm = BindingContext as GoodsDetailViewModel;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatPage());
        }
    }
}