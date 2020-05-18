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
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                dynamicLength = dynamicLength.Subtract(TimeSpan.FromSeconds(1));
                header.Text = dynamicLength.ToString();
                range.EndValue = (dynamicLength.TotalSeconds / recordLength.TotalSeconds) * 360;
                if (dynamicLength.ToString().Equals("00:00:00")) // 结束了
                {
                    Finish();
                }
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
            TomatoTime time = new TomatoTime()
            {
                BeginTime = startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                UserId = 16
            };
            await App.TomatoTimeManager.AddTomatoTimeTaskAsync(time);

            App.IsClockPageOn = false;
            await DisplayAlert("锁机结束", "结束啦！", "OK");
            // 加一个铃声提醒
            await PopupNavigation.Instance.PopAsync();
        }
    }
}