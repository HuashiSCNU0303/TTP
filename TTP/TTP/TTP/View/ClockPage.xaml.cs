using CHD;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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
            var timeRecord = BindingContext as TimeRecordModel;
            timeRecord.startTime = startTime;
            timeRecord.endTime = DateTime.Now;
            timeRecord.description = description;
            timeRecord.setDuration();
            timeRecord.setTimeString();
            Console.WriteLine("结束时：" + description);

            // 往数据库提交时间，这里先用一个list代替
            App.RecordManager.AddRecord(timeRecord);
            TimeRecordViewModel.refresh();
            

            App.IsClockPageOn = false;
            await DisplayAlert("锁机结束", "结束啦！", "OK");
            await PopupNavigation.Instance.PopAsync();
        }
    }
}