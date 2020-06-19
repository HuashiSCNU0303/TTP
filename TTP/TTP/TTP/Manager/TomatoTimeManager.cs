using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public class TomatoTimeManager
    {
		ITomatoTimeService restService;
		public Dictionary<string, List<TomatoTime>> UserTomatoTimes { get; set; }
		public TomatoTimeManager(ITomatoTimeService service)
		{
			restService = service;
            UserTomatoTimes = new Dictionary<string, List<TomatoTime>>();
        }

		public Task<List<TomatoTime>> GetAllTomatoTimeTasksAsync(long id)
		{
			return restService.GetAllTomatoTimeAsync(id);
		}

		public Task AddTomatoTimeTaskAsync(TomatoTime tomatoTime)
		{
			return restService.AddTomatoTimeAsync(tomatoTime);
		}

        public async Task<bool> InitUserRecords(long id)
        {
            UserTomatoTimes.Clear();
            List<TomatoTime> records = await GetAllTomatoTimeTasksAsync(id);
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
            foreach (var record in records)
            {
                record.BeginTimeDate = Convert.ToDateTime(record.BeginTime).Date.ToShortDateString();
                record.SpanString = Convert.ToDateTime(record.BeginTime).ToShortTimeString() + " → " + Convert.ToDateTime(record.EndTime).ToShortTimeString();
                App.StaticUser.TotalTimes += Convert.ToDateTime(record.EndTime) - Convert.ToDateTime(record.BeginTime);
                string key = record.Description;
                if (!UserTomatoTimes.ContainsKey(key))
                {
                    UserTomatoTimes.Add(key, new List<TomatoTime>());
                }
                UserTomatoTimes[key].Add(record);
            }
            return true;
        }

        public void AddTimeRecord(TomatoTime tomatoTime)
        {
            string key = tomatoTime.Description;
            if (!UserTomatoTimes.ContainsKey(key))
            {
                UserTomatoTimes.Add(key, new List<TomatoTime>());
            }
            UserTomatoTimes[key].Insert(0, tomatoTime);
        }
    }
}
