using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;
using TTP.Services;
using TTP.ViewModel;
using Xamarin.Forms;

namespace TTP.Data
{
    public class AppManager
    {
        public List<AppModel> Apps { get; set; } // 系统全部应用列表
        public List<string> WhiteList { get; set; } // 白名单应用列表
        IAppRestService restService;

        public AppManager(IAppRestService service)
        {
            restService = service;
            Apps = new List<AppModel>();
            WhiteList = new List<string>();
        }

        public async void InitAllApps()
        {
            // 获得本机所有应用
            List<string> packageNames = DependencyService.Get<IOpenAppService>().GetApps();
            packageNames.ForEach(g =>
                Apps.Add(new AppModel
                {
                    PackageName = g,
                    AppName = DependencyService.Get<IOpenAppService>().GetAppName(g),
                    AppIcon = DependencyService.Get<IOpenAppService>().GetAppIcon(g),
                    IsInWhiteList = false
                }
            ));
            // 获取该用户的白名单应用包名字符串
            Console.WriteLine("加载应用完成！");
            string whiteAppString = await GetAppsAsync(App.StaticUser.UserId);
            List<string> whiteAppStrings = new List<string>(whiteAppString.Split(';'));
            whiteAppStrings.ForEach(g =>
            {
                var app = Apps.Find(x => x.PackageName.Equals(g));
                if (app != null)
                {
                    Console.WriteLine("应用的名字：" + app.AppName);
                    app.IsInWhiteList = true;
                    WhiteList.Add(g);
                }
            });
        }

        public Task ModifyAppsAsync(long id, string whiteList)
        {
            return restService.ModifyAppsAsync(id, whiteList);
        }

        public Task<string> GetAppsAsync(long id)
        {
            return restService.GetAppsAsync(id);
        }
    }
}
