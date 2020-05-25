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
		public TomatoTimeManager(ITomatoTimeService service)
		{
			restService = service;
		}

		public Task<List<TomatoTime>> GetAllTomatoTimeTasksAsync(long id)
		{
			return restService.GetAllTomatoTimeAsync(id);
		}

		public Task AddTomatoTimeTaskAsync(TomatoTime tomatoTime)
		{
			return restService.AddTomatoTimeAsync(tomatoTime);
		}
	}
}
