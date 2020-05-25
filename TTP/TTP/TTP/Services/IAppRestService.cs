using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TTP.Data
{
    public interface IAppRestService
    {
        Task<string> GetAppsAsync(long id);
        Task ModifyAppsAsync(long id, string whiteList);
    }
}
