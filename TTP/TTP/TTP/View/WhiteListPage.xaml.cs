using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TTP.Services;
using TTP.ViewModel;
using TTP.Model;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhiteListPage : PopupPage
    {
        public WhiteListPage()
        {
            InitializeComponent();
            BindingContext = new WhiteListViewModel();
        }

        private async void btnQuit_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void appListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as AppModel;
            if (item.PackageName.Equals("com.companyname.ttp"))
            {
                return;
            }
            DependencyService.Get<IOpenAppService>().Launch(item.PackageName);
        }

        private void appListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var item = e.ItemData as AppModel;
            if (item.PackageName.Equals("com.companyname.ttp"))
            {
                return;
            }
            DependencyService.Get<IOpenAppService>().Launch(item.PackageName);
        }

        private void appListView_SelectionChanged(object sender, Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs e)
        {
            var item = e.AddedItems[0] as AppModel;
            if (item.PackageName.Equals("com.companyname.ttp"))
            {
                return;
            }
            DependencyService.Get<IOpenAppService>().Launch(item.PackageName);
        }
    }
}