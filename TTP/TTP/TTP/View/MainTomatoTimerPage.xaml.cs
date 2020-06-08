using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using TTP.Services;
using TTP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TTP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTomatoTimerPage : ContentPage
    {
        public MainTomatoTimerPage()
        {
            InitializeComponent();
            lsvRecentRecords.BindingContext = new TomatoTimeViewModel();
            lsvRecentRecords.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "BeginTimeDate",
            });
            App.LogInStatusChanged += async (userId) => 
            {
                bool isSuccess = await TomatoTimeViewModel.refreshRecords(userId);
                if (lblRecordHint.IsVisible)
                {
                    if (TomatoTimeViewModel.recordCount() > 0)
                    {
                        lblRecordHint.IsVisible = false;
                    }
                    else
                    {
                        lblRecordHint.Text = "最近没有使用记录哦！快开始锁机学习吧！";
                    }
                }
                lblTomatoTimeLength.Text = App.StaticUser.TotalTimes.ToString();
                lblTomatoPoints.Text = App.StaticUser.TomatoPoints.ToString();
            };
            TomatoTimeViewModel.RecordCountChanged += () =>
            {
                if (lblRecordHint.IsVisible)
                {
                    lblRecordHint.IsVisible = false;
                }
                lblTomatoTimeLength.Text = App.StaticUser.TotalTimes.ToString();
                lblTomatoPoints.Text = App.StaticUser.TomatoPoints.ToString();
            };
            ShowTime();
        }

        public void ShowTime()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                Device.BeginInvokeOnMainThread(() => labelTime.Text = DateTime.Now.ToString());
                return true;
            });
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            var newPage = new AddTimeRecordPage();
            newPage.SetTimeEvent += OnTimeSet;
            await PopupNavigation.Instance.PushAsync(newPage);
        }

        private async void OnTimeSet(int hours, int minutes, string description)
        {
            if (hours != 0 || minutes != 0)
            {
                await PopupNavigation.Instance.PushAsync(new ClockPage
                {
                    recordLength = TimeSpan.FromHours(hours).Add(TimeSpan.FromMinutes(minutes)),
                    dynamicLength = TimeSpan.FromHours(hours).Add(TimeSpan.FromMinutes(minutes)),
                    startTime = DateTime.Now,
                    description = description
                });
            }
        }

        public void AddExample() {
            TomatoTime time = new TomatoTime()
            {
                BeginTime= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                EndTime= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                UserId=121
            };
            App.TomatoTimeManager.AddTomatoTimeTaskAsync(time);
        }

        public async void GetExampleAsync(long UserId)
        {
            //UserId=16
            List<TomatoTime> allTime = await App.TomatoTimeManager.GetAllTomatoTimeTasksAsync(121);
        }
    }
}