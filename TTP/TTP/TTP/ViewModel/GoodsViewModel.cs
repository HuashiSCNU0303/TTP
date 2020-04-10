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
            goodsModels.Clear();
            list.ForEach(g => goodsModels.Add(g));
        }
    }
}
