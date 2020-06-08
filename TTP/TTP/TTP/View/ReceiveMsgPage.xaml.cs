using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReceiveMsgPage : ContentPage
    {
        public ReceiveMsgPage()
        {
            InitializeComponent();
            this.Appearing += (sender, args) => { getBinding(); };
        }

        private void getBinding() {
            BindingContext = new ReceiveMsgViewModel();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView lv = sender as ListView;

            var auser = lv.SelectedItem as CharPageViewModel.AUser;

            if (auser == null) return;
            lv.SelectedItem = null;

            CharPageViewModel.AUser CurrentUser = new CharPageViewModel.AUser()
            {
                UserId = App.StaticUser.UserId,
                Name = App.StaticUser.Name,
                Avatar = App.StaticUser.Imgurl
            };
            Navigation.PushAsync(new ChatPage()
            {
                BindingContext = new CharPageViewModel(CurrentUser, auser)
            });
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {

        }
    }
}