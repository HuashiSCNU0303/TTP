using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public interface ITomatoTimeService
    {
        Task<List<TomatoTime>> GetAllTomatoTimeAsync(long id);

        Task AddTomatoTimeAsync(TomatoTime tomatoTime);
    }
}
