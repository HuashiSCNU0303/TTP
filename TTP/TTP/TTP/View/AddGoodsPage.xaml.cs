using System;
using System.Collections.Generic;
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
    public partial class AddGoodsPage : ContentPage
    {
        public AddGoodsPage()
        {
            InitializeComponent();
            this.BindingContext = new AddGoodsViewModel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            AddGoodsViewModel agvm = BindingContext as AddGoodsViewModel;
            await App.GoodsManager.AddGoodsTaskAsync(agvm.GoodsModel);
            await DisplayAlert("提示", "增加成功！", "OK");
            await Navigation.PopAsync();
        }
    }
}