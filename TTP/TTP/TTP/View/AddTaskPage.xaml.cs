using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskPage : PopupPage
    {
        public event Action<string> AddTaskEvent;
        public AddTaskPage()
        {
            InitializeComponent();
        }

        private void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            if (entryTask.Text == "")
            {
                DependencyService.Get<IToastService>().LongAlert("任务名不能为空！");
                return;
            }
            DependencyService.Get<IToastService>().ShortAlert("添加任务成功！");
            AddTaskEvent(entryTask.Text);
            PopupNavigation.Instance.PopAsync();
        }

        private void btnReturn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}