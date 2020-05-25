using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public interface IGoodsRestService
    {
        Task DeleteGoodsAsync(long id);

        Task AddGoodsAsync(GoodsModel item);

        Task ModifyGoodsAsync(GoodsModel item);

        Task <List<GoodsModel>> GetGoodsAsync();
    }
}
