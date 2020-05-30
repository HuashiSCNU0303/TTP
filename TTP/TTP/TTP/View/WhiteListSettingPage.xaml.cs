using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TTP.Services;
using TTP.Model;
using TTP.ViewModel;
using Syncfusion.XForms.Buttons;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhiteListSettingPage : ContentPage
    {
        public WhiteListSettingPage()
        {
            InitializeComponent();
            appListView.ItemsSource = App.AppManager.Apps;
        }

        private async void btnSet_Clicked(object sender, EventArgs e)
        {
            string whiteAppsString = "";
            App.AppManager.WhiteList.Clear();
            foreach (var item in App.AppManager.Apps)
            {
                if (item.IsInWhiteList) 
                {
                    App.AppManager.WhiteList.Add(item.PackageName);
                    whiteAppsString += item.PackageName + ";";
                }
            }
            DependencyService.Get<IToastService>().LongAlert("设置成功");
            await App.AppManager.ModifyAppsAsync(App.StaticUser.UserId, whiteAppsString);
            await Navigation.PopModalAsync();
        }

        private void swcSetOn_StateChanged(object sender, Syncfusion.XForms.Buttons.SwitchStateChangedEventArgs e)
        {
            var swc = sender as SfSwitch;
            var item = swc.Parent.BindingContext as AppModel;
            if (item.PackageName.Equals("com.companyname.ttp"))
            {
                swc.IsOn = true;
                return;
            }
            item.IsInWhiteList = (bool)e.NewValue;
        }
    }
}