using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TTP.Services
{
    public interface IOpenAppService
    {
        Task<bool> Launch(string packageName);
        string GetAppName(string packageName);
        ImageSource GetAppIcon(string packageName);
        List<string> GetApps();
    }
}
