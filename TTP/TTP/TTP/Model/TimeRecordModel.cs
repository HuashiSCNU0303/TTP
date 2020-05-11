using System;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace TTP.Model
{
    public class TimeRecordModel : ViewModelBase
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public TimeSpan timeSpan { get; set; }
        public string description { get; set; }
        public string timeString { get; set; }
        public bool isEnabled { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TimeRecordModel model &&
                   description == model.description &&
                   timeString == model.timeString &&
                   isEnabled == model.isEnabled;
        }

        public override int GetHashCode()
        {
            var hashCode = 1270755892;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(description);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(timeString);
            hashCode = hashCode * -1521134295 + isEnabled.GetHashCode();
            return hashCode;
        }

        public void setDuration()
        {
            timeSpan = endTime - startTime;
        }
        public void setTimeString()
        {
            timeString = startTime.ToString("HH:mm") + " → " + endTime.ToString("HH:mm");
        }
    }
}
