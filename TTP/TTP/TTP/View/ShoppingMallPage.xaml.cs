using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingMallPage : ContentPage
    {
        public  ShoppingMallPage()
        {
            InitializeComponent();
            BindingContext = new GoodsViewModel();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView lv = sender as ListView;
            
            var model = lv.SelectedItem as GoodsModel;

            if (model == null) return;

            lv.SelectedItem = null;
            await Navigation.PushAsync(new GoodsDetailPage()
            {
                BindingContext = new GoodsDetailViewModel() 
                { 
                    GoodsModel=model
                }
            });
            
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddGoodsPage());
        }
    }
}