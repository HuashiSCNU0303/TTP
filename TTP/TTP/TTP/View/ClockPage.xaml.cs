using CHD;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTP.Services;
using TTP.Model;
using TTP.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClockPage : PopupPage
    {
        public DateTime startTime;
        public TimeSpan recordLength;
        public TimeSpan dynamicLength;
        public string description;
        public ClockPage()
        {
            InitializeComponent();
            BindingContext = new AddTimeRecordViewModel();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                dynamicLength = dynamicLength.Subtract(TimeSpan.FromSeconds(1));
                header.Text = dynamicLength.ToString();
                range.EndValue = (dynamicLength.TotalSeconds / recordLength.TotalSeconds) * 360;
                if (dynamicLength.ToString().Equals("00:00:00")) // 结束了
                {
                    Finish();
                }
                if (!App.IsClockPageOn)
                {
                    return false;
                }
                Console.WriteLine(DateTime.Now.ToString());
                return true;
            });
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.IsClockPageOn = true;
            bool OnBackButtonPressed()
            {
                return false;
            }
            HWBackButtonManager.OnBackButtonPressedDelegate onBackButtonPressedDelegate = new HWBackButtonManager.OnBackButtonPressedDelegate(OnBackButtonPressed);
            HWBackButtonManager.Instance.SetHWBackButtonListener(onBackButtonPressedDelegate);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.IsClockPageOn = false;
            DependencyService.Get<IToastService>().LongAlert("锁机结束");
            HWBackButtonManager.Instance.RemoveHWBackButtonListener();
        }

        private void btnQuit_Clicked(object sender, EventArgs e)
        {
            Finish();
        }

        private void btnLaunch_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new WhiteListPage());
        }

        private async void Finish()
        {
            var advm = BindingContext as AddTimeRecordViewModel;
            advm.SetRecord(startTime, description);
            await PopupNavigation.Instance.PopAsync();
        }
    }
}