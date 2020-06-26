using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.Buttons;
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
            lsvRecentRecords.BindingContext = new TomatoTaskViewModel();
            App.LogInStatusChanged += async (userId) => 
            {
                bool isSuccess = await App.TomatoTimeManager.InitUserRecords(userId);
                App.AppManager.GetWhiteApps();
                TomatoTaskViewModel.RefreshUserTasks();
                if (lblRecordHint.IsVisible && TomatoTaskViewModel.RecordCount > 0)
                {
                    lblRecordHint.IsVisible = false;
                }
                lblTomatoTimeLength.Text = App.StaticUser.TotalTimes.ToString();
                lblTomatoPoints.Text = App.StaticUser.TomatoPoints.ToString();
            };
            TomatoTaskViewModel.RecordCountChanged += () =>
            {
                if (TomatoTaskViewModel.RecordCount > 0 && lblRecordHint.IsVisible) 
                {
                    lblRecordHint.IsVisible = false;
                }
                else if (TomatoTaskViewModel.RecordCount == 0 && !lblRecordHint.IsVisible)
                {
                    lblRecordHint.IsVisible = true;
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

        private async void btnAddTask_Clicked(object sender, EventArgs e)
        {
            var newPage = new AddTaskPage();
            newPage.AddTaskEvent += (s) =>
            {
                TomatoTaskViewModel.AddTask(s);
            };
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

        private async void btnAddRecord_Clicked(object sender, EventArgs e)
        {
            var btn = sender as ImageButton;
            var item = btn.Parent.BindingContext as TomatoTask;
            var newPage = new AddTimeRecordPage { Description = item.TaskName };
            newPage.SetTimeEvent += OnTimeSet;
            await PopupNavigation.Instance.PushAsync(newPage);
        }

        private void btnDeleteTask_Clicked(object sender, EventArgs e)
        {
            var btn = sender as ImageButton;
            var item = btn.Parent.BindingContext as TomatoTask;
            TomatoTaskViewModel.DeleteTask(item);
        }
    }
}