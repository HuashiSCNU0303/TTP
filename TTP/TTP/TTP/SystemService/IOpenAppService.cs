using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TTP.Services
{
    public interface IOpenAppService
    {
        Task<bool> Launch(string packageName); // 打开应用
        string GetAppName(string packageName); // 获取应用名称
        ImageSource GetAppIcon(string packageName); // 获取应用图标
        List<string> GetApps(); // 获取系统全部应用
    }
}
