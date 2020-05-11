using System;
using System.Collections.Generic;
using System.Text;

using TTP.Model;

namespace TTP.Data
{
    public class TimeRecordManager
    {
        public List<TimeRecordModel> timeRecords; // 保存在数据库
        public TimeRecordManager()
        {
            timeRecords = new List<TimeRecordModel>();
        }
        public void AddRecord(TimeRecordModel timeRecord)
        {
            timeRecords.Add(timeRecord);
        }
    }
}
