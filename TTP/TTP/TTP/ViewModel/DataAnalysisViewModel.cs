using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TTP.ViewModel
{
    class DataAnalysisViewModel
    {
        public ObservableCollection<DateData> DataList { get; set; }
        public DataAnalysisViewModel()
        {
            DataList = new ObservableCollection<DateData>();
            Random rand = new Random();
            DateTime date = new DateTime(2020, 1, 1);
            for (int i = 0; i < 365; i++)
            {
                
                DateData dateData = new DateData();
                dateData.minute = rand.Next(0, 130);
                dateData.date = date;
                date = date.AddDays(1);
                DataList.Add(dateData);
            }
        }
        public class DateData
        {
            public DateTime date { get; set; }
            public int minute { get; set; }
        }
    }
}
