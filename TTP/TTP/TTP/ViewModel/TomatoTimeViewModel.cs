using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.ViewModel
{
    class TomatoTimeViewModel : ViewModelBase
    {
        private static ObservableCollection<TomatoTime> timeRecords;
        public static event Action RecordCountChanged;
        public TomatoTimeViewModel()
        {
            timeRecords = new ObservableCollection<TomatoTime>();
        }

        public ObservableCollection<TomatoTime> TimeRecords
        {
            get { return timeRecords; }
            set { timeRecords = value; RaisePropertyChanged(); }
        }

        public static async Task<bool> refreshRecords(long userId)
        {
            List<TomatoTime> records = await App.TomatoTimeManager.GetAllTomatoTimeTasksAsync(userId);
            timeRecords.Clear();
            records.Sort((o1, o2) =>
            {
                if (Convert.ToDateTime(o1.BeginTime) > Convert.ToDateTime(o2.BeginTime))
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });
            records.ForEach(g =>
            {
                g.BeginTimeDate = Convert.ToDateTime(g.BeginTime).Date.ToShortDateString();
                g.SpanString = Convert.ToDateTime(g.BeginTime).ToShortTimeString() + " → " + Convert.ToDateTime(g.EndTime).ToShortTimeString();
                App.StaticUser.TotalTimes += Convert.ToDateTime(g.EndTime) - Convert.ToDateTime(g.BeginTime);
                timeRecords.Add(g);
            });
            return true;
        }

        public static void addRecord(TomatoTime timeRecord)
        {
            timeRecords.Add(timeRecord);
            App.StaticUser.TotalTimes += Convert.ToDateTime(timeRecord.EndTime) - Convert.ToDateTime(timeRecord.BeginTime);
            RecordCountChanged();
        }

        public static int recordCount()
        {
            return timeRecords.Count;
        }
    }
}
