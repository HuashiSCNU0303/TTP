using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using TTP.Services;
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
            showTime();
        }

        public void showTime()
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
                UserId=16
            };
            App.TomatoTimeManager.AddTomatoTimeTaskAsync(time);
        }

        public async void GetExampleAsync(long UserId)
        {
            //UserId=16
            List<TomatoTime> allTime = await App.TomatoTimeManager.GetAllTomatoTimeTasksAsync(16);
        }
    }
}