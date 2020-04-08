using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingMallPage : ContentPage
    {
        public ShoppingMallPage()
        {
            InitializeComponent();
            BindingContext = new GoodsViewModel();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //选择商品事件
            await Navigation.PushAsync(new GoodsDetail
            {
                BindingContext = new GoodsViewModel()
            });
        }
    }
}