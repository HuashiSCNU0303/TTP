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

        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() == null) {
                return;
            }
            ReceiveModel receiver = e.CurrentSelection.FirstOrDefault() as ReceiveModel;
            CollectionView cv = sender as CollectionView;
            cv.SelectedItem = null;
            CharPageViewModel.AUser CurrentUser = new CharPageViewModel.AUser()
            {
                UserId = App.StaticUser.UserId,
                Name = App.StaticUser.Name,
                Avatar = App.StaticUser.Imgurl
            };
            CharPageViewModel.AUser sdUser = new CharPageViewModel.AUser()
            {
                UserId = receiver.UserId,
                Name = receiver.Name,
                Avatar = receiver.Avatar
            };
            string[] msgs = App.Receive[sdUser.UserId].Split('|');
            Navigation.PushAsync(new ChatPage()
            {
                BindingContext = new CharPageViewModel(CurrentUser, sdUser,msgs)
            });
        }
    }
}