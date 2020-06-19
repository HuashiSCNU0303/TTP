using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Model
{
    class TomatoTask
    {
        public string TaskName { get; set; }
        public TimeSpan TotalTimes { get; set; }
        public List<TomatoTime> TimeRecords { get; set; }
    }
}
