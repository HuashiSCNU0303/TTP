using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public class GoodsItemManager
    {
		IGoodsRestService restService;

		public GoodsItemManager(IGoodsRestService service)
		{
			restService = service;
		}

		public Task<List<GoodsModel>> GetGoodsTasksAsync(string id)
		{
			return restService.GetGoodsAsync(id);
		}

		public Task AddGoodsTaskAsync(GoodsModel item)
		{
			return restService.AddGoodsAsync(item);
		}

		public Task DeleteGoodsTaskAsync(GoodsModel item)
		{
			return restService.DeleteGoodsAsync(item.Id);
		}

		public Task ModifyGoodsTaskAsync(GoodsModel item) {
			return restService.ModifyGoodsAsync(item);
		}
	}
}
