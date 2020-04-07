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
        private ObservableCollection<GoodsModel> goodsModels;
        public GoodsViewModel() {
            goodsModels = new ObservableCollection<GoodsModel>();
            goodsModels.Add(new GoodsModel() { Description="第一个测试商品"});
            goodsModels.Add(new GoodsModel() { Description="第二个测试商品"});
            goodsModels.Add(new GoodsModel() { Description="第三个测试商品"});
            goodsModels.Add(new GoodsModel() { Description="第四个测试商品"});
        }

        public ObservableCollection<GoodsModel> GoodsModels {
            get { return goodsModels; }
            set { goodsModels = value; RaisePropertyChanged(); }
        }
    }
}
