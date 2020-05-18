using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTP.Model;
using TTP.ViewModel;
using TTP.Services;
using TTP.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimeRecordPage : PopupPage
    {
        public event Action<int, int, string> SetTimeEvent;
        int hours_int = 0, minutes_int = 0;
        string description;
        public AddTimeRecordPage()
        {
            InitializeComponent();
        }

        private void btnReturn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }

        protected override void OnDisappearing()
        {
            SetTimeEvent(hours_int, minutes_int, description);
            base.OnDisappearing();
        }

        private async void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            string hours = entryHours.Text;
            string minutes = entryMinutes.Text;
            description = entryDescription.Text;
            try
            {
                hours_int = Convert.ToInt32(hours);
                minutes_int = Convert.ToInt32(minutes);
            }
            catch (InvalidCastException)
            {
                await DisplayAlert("输入有误", "输入不正确，请重新输入！", "OK");
                return;
            }
            if (minutes_int >= 60)
            {
                await DisplayAlert("输入有误", "分钟数应该＜60，请重新输入！", "OK");
                return;
            }

            Console.WriteLine("输入时：" + description);
            await DisplayAlert("添加成功", "开始锁机啦！", "OK");
            await PopupNavigation.Instance.PopAsync();

        }
    }
}