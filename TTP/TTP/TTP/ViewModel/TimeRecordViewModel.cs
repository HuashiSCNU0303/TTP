using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using TTP.Model;
using TTP.Data;
using System.ComponentModel;

namespace TTP.ViewModel
{
    class TimeRecordViewModel : ViewModelBase
    {
        private static ObservableCollection<TimeRecordModel> timeRecordModels;
        public TimeRecordViewModel()
        {
            timeRecordModels = new ObservableCollection<TimeRecordModel>();
        }

        public ObservableCollection<TimeRecordModel> TimeRecordModels
        {
            get { return timeRecordModels; }
            set { timeRecordModels = value; RaisePropertyChanged(); }
        }


        public static void refresh()
        {
            List<TimeRecordModel> records = App.RecordManager.timeRecords;
            timeRecordModels.Clear();
            records.ForEach(g => timeRecordModels.Add(g));
        }
    }
}