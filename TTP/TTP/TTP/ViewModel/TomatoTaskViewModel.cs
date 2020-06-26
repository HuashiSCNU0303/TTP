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
            tasks.Clear();
            foreach (var task in records)
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
                foreach (var record in task.Value)
                {
                    tomatoTask.TotalTimes += Convert.ToDateTime(record.EndTime) - Convert.ToDateTime(record.BeginTime);
                }
                tasks.Add(tomatoTask);
            }
            RecordCountChanged();
        }

        public static async void AddTask(string description)
        {
            if (App.CurrentUserID != -1)
            {
                DateTime currentTime = DateTime.Now;
                // 上传一个起止时间相同的时间记录，表明有这个任务
                TomatoTime time = new TomatoTime()
                {
                    BeginTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    EndTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    UserId = App.StaticUser.UserId,
                    Description = description,
                    BeginTimeDate = currentTime.Date.ToShortDateString(),
                    SpanString = currentTime.ToShortTimeString() + " → " + currentTime.ToShortTimeString()
                };
                await App.TomatoTimeManager.AddTomatoTimeTaskAsync(time);
                await App.UserManager.ModifyUserTaskAsync(App.StaticUser);
            }

            TomatoTask tomatoTask = new TomatoTask();
            tomatoTask.TaskName = description;
            tomatoTask.TimeRecords = new List<TomatoTime>();
            tomatoTask.TotalTimes = new TimeSpan();
            tasks.Insert(0, tomatoTask);
            RecordCountChanged();
        }

        public static void DeleteTask(TomatoTask task)
        {
            tasks.Remove(task);
            RecordCountChanged();
        }

        public static void AddTimeRecord(TomatoTime tomatoTime)
        {
            var tempTasks = new List<TomatoTask>();
            for (int i = 0; i < tasks.Count; i++) 
            {
                if (tasks[i].TaskName == tomatoTime.Description)
                {
                    tasks[i].TimeRecords.Insert(0, tomatoTime);
                    tasks[i].TotalTimes += Convert.ToDateTime(tomatoTime.EndTime) - Convert.ToDateTime(tomatoTime.BeginTime);
                }
                tempTasks.Add(tasks[i]);
            }
            tasks.Clear();
            tempTasks.ForEach(g => tasks.Add(g));
        }
    }
}
