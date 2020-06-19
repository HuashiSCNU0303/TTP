using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TTP.Model;

namespace TTP.ViewModel
{
    class GoodsViewModel : ViewModelBase
    {
        private static ObservableCollection<GoodsModel> goodsModels;
        public GoodsViewModel() {
            goodsModels = new ObservableCollection<GoodsModel>();
            refresh();
        }

        public ObservableCollection<GoodsModel> GoodsModels {
            get { return goodsModels; }
            set { goodsModels = value; RaisePropertyChanged(); }
        }

        public async static void refresh() {
            List<GoodsModel> list = await App.GoodsManager.GetGoodsTasksAsync();
            list.Sort((o1, o2) =>
            {
                if (Convert.ToDateTime(o1.Date) > Convert.ToDateTime(o2.Date)) 
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            });
            goodsModels.Clear();
            list.ForEach(async g => 
            {
                User user = await App.UserManager.GetUserTasksAsync(g.UserId);
                g.User = user;
                goodsModels.Add(g);
            });
        }
    }
}
