using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.ViewModel
{
    class TomatoTaskViewModel : ViewModelBase
    {
        private static ObservableCollection<TomatoTask> tasks;
        public static event Action RecordCountChanged;
        public TomatoTaskViewModel()
        {
            tasks = new ObservableCollection<TomatoTask>();
            RefreshUserTasks();
        }

        public ObservableCollection<TomatoTask> Tasks
        {
            get { return tasks; }
            set { tasks = value; RaisePropertyChanged(); }
        }

        public static int RecordCount { get { return tasks.Count; } }

        public static void RefreshUserTasks()
        {
            var records = App.TomatoTimeManager.UserTomatoTimes;
            int originCount = RecordCount;
            tasks.Clear();
            foreach(var task in records)
            {
                TomatoTask tomatoTask = new TomatoTask();
                tomatoTask.TaskName = task.Key;
                tomatoTask.TimeRecords = task.Value;
                tomatoTask.TotalTimes = new TimeSpan();
                // 去掉占位的时间记录
                for (int i = task.Value.Count - 1; i >= 0; i--) 
                {
                    var record = task.Value[i];
                    if (record.BeginTime == record.EndTime)
                    {
                        tomatoTask.TimeRecords.Remove(record);
                        break;
                    }
                }
                foreach(var record in task.Value)
                {
                    tomatoTask.TotalTimes += Convert.ToDateTime(record.EndTime) - Convert.ToDateTime(record.BeginTime);
                }
                tasks.Add(tomatoTask);
            }
            if (originCount != RecordCount) 
            {
                RecordCountChanged();
            }
        }
    }
}
