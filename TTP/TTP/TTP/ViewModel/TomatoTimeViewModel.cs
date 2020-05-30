using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

        public static async void refreshRecords(long userId)
        {
            List<TomatoTime> records = await App.TomatoTimeManager.GetAllTomatoTimeTasksAsync(userId);
            timeRecords.Clear();
            records.ForEach(g =>
            {
                // 不确定这段代码是否能成功，能联网了再试试
                g.BeginTimeDate = Convert.ToDateTime(g.BeginTime).Date.ToShortDateString();
                g.SpanString = Convert.ToDateTime(g.BeginTime).ToShortTimeString() + " → " + Convert.ToDateTime(g.EndTime).ToShortTimeString();
                timeRecords.Add(g);
            });
        }

        public static void addRecord(TomatoTime timeRecord)
        {
            timeRecords.Add(timeRecord);
            RecordCountChanged();
        }

        public static int recordCount()
        {
            return timeRecords.Count;
        }
    }
}
