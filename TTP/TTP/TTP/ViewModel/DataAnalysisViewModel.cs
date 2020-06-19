using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TTP.Model;

namespace TTP.ViewModel
{
    class DataAnalysisViewModel
    {
        public ObservableCollection<DateData> DataList { get; set; }
        public ObservableCollection<TaskData> TaskList { get; set; }
        public DataAnalysisViewModel()
        {
            DataList = new ObservableCollection<DateData>();
            TaskList = new ObservableCollection<TaskData>();

            var tasks = App.TomatoTimeManager.UserTomatoTimes;
            var records = new List<TomatoTime>();
            foreach (var task in tasks)
            {
                foreach (var record in task.Value)
                {
                    records.Add(record);
                }
            }
            records.Sort((o1, o2) =>
            {
                if (Convert.ToDateTime(o1.BeginTime) > Convert.ToDateTime(o2.BeginTime))
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });

            DateData tempDateData = new DateData();
            string dateString = "";
            foreach (var record in records) 
            {
                // 新的一天
                if (record.BeginTimeDate != dateString)
                {
                    if (dateString != "")
                    {
                        var tempDay = Convert.ToDateTime(dateString).AddDays(1);
                        while (!tempDay.ToShortDateString().Equals(record.BeginTimeDate))
                        {
                            Console.WriteLine("此时日期：" + tempDay.ToShortDateString());
                            Console.WriteLine("比较的日期：" + record.BeginTimeDate);
                            DataList.Add(new DateData { Date = tempDay, Minute = 0 });
                            tempDay = tempDay.AddDays(1);
                        }
                    }
                    dateString = record.BeginTimeDate;
                    tempDateData = new DateData();
                    var dateTime = Convert.ToDateTime(dateString);
                    tempDateData.Date = dateTime;
                    tempDateData.Minute = 0;
                    DataList.Add(tempDateData);
                }
                tempDateData.Minute += (int)(Convert.ToDateTime(record.EndTime) - Convert.ToDateTime(record.BeginTime)).TotalSeconds;
            }

            if (records.Count > 0)
            {
                var tempDay = Convert.ToDateTime(records[records.Count - 1].BeginTimeDate).AddDays(1);
                var today = DateTime.Now.Date;
                while (!tempDay.ToShortDateString().Equals(today.AddDays(1).ToShortDateString()))
                {
                    DataList.Add(new DateData { Date = tempDay, Minute = 0 });
                    tempDay = tempDay.AddDays(1);
                }
            }

            foreach (var task in tasks) 
            {
                var taskData = new TaskData { TaskName = task.Key, Minute = 0 };
                foreach (var record in task.Value)
                {
                    taskData.Minute += (int)(Convert.ToDateTime(record.EndTime) - Convert.ToDateTime(record.BeginTime)).TotalSeconds;
                }
                TaskList.Add(taskData);
            }
        }

        public class DateData
        {
            public DateTime Date { get; set; }
            public int Minute { get; set; }
        }

        public class TaskData
        {
            public string TaskName { get; set; }
            public int Minute { get; set; }
        }
    }
}
