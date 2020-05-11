using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using TTP.Model;
using TTP.Services;
using Xamarin.Forms;

namespace TTP.ViewModel
{
    class WhiteListViewModel : ViewModelBase
    {
        private static ObservableCollection<AppModel> whiteList;
        public WhiteListViewModel()
        {
            whiteList = new ObservableCollection<AppModel>();
            refresh();
        }

        public ObservableCollection<AppModel> WhiteList
        {
            get { return whiteList; }
            set { whiteList = value; RaisePropertyChanged(); }
        }

        public static void refresh()
        {
            List<string> packageNames = App.AppWhiteList; 
            whiteList.Clear();
            packageNames.ForEach(g => whiteList.Add(new AppModel
            {
                PackageName = g, 
                AppName = DependencyService.Get<IOpenAppService>().GetAppName(g),
                AppIcon = DependencyService.Get<IOpenAppService>().GetAppIcon(g)
            }));
        }
    }
}
