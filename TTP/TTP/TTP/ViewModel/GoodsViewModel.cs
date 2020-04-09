using GalaSoft.MvvmLight;
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
            getAll();
        }

        public ObservableCollection<GoodsModel> GoodsModels {
            get { return goodsModels; }
            set { goodsModels = value; RaisePropertyChanged(); }
        }

        public static void add(GoodsModel gm) {
            goodsModels.Add(gm);
        }

        public static void delete(GoodsModel gm) {
            goodsModels.Remove(gm);
        }

        public async void getAll() {
            List<GoodsModel> list = await App.GoodsManager.GetGoodsTasksAsync();
            list.ForEach(g=> goodsModels.Add(g));
        }

        public async static void refresh() {
            List<GoodsModel> list = await App.GoodsManager.GetGoodsTasksAsync();
            list.ForEach(g => goodsModels.Add(g));
        }
    }
}
