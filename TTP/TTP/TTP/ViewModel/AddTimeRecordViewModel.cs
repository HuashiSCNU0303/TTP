using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using TTP.Model;

namespace TTP.ViewModel
{
    class AddTimeRecordViewModel : ViewModelBase
    {
        private TomatoTime tomatoTime;
        public AddTimeRecordViewModel()
        {
            tomatoTime = new TomatoTime();
        }

        public TomatoTime TomatoTime
        {
            get { return tomatoTime; }
            set { tomatoTime = value; RaisePropertyChanged(); }
        }

        public async void SetRecord(DateTime startTime, string description)
        {
            DateTime currentTime = DateTime.Now;
            tomatoTime.BeginTime = startTime.ToString("yyyy-MM-dd HH:mm:ss");
            tomatoTime.EndTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");
            tomatoTime.UserId = App.StaticUser.UserId;
            tomatoTime.Description = description;
            tomatoTime.BeginTimeDate = startTime.Date.ToShortDateString();
            tomatoTime.SpanString = startTime.ToShortTimeString() + " → " + currentTime.ToShortTimeString();

            int p = (DateTime.Now.Subtract(startTime).Minutes + 1) / 15 + 1;

            App.StaticUser.TomatoPoints += p;
            App.StaticUser.TotalTimes += Convert.ToDateTime(tomatoTime.EndTime) - Convert.ToDateTime(tomatoTime.BeginTime);
            App.TomatoTimeManager.AddTimeRecord(tomatoTime);
            TomatoTaskViewModel.AddTimeRecord(tomatoTime);

            await App.TomatoTimeManager.AddTomatoTimeTaskAsync(tomatoTime);
            await App.UserManager.ModifyUserTaskAsync(App.StaticUser);
        }
    }
}
