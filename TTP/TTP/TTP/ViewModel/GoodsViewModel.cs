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
            goodsModels.Add(new GoodsModel() { Id = "001", name = "书", price = "50", type = TYPE.AAA, owner = "张三", date="2020.1.1", Description = "第一个测试商品" });
            goodsModels.Add(new GoodsModel() { Id = "002", name = "笔", price = "20", type = TYPE.AAA, owner = "张三", date = "2020.1.2", Description ="第二个测试商品"});
            goodsModels.Add(new GoodsModel() { Id = "003", name = "斗破苍穹单行本", price = "100", type = TYPE.BBB, owner = "李四", date = "2020.2.1", Description ="第三个测试商品"});
            goodsModels.Add(new GoodsModel() { Id = "004", name = "充电宝", price = "3000", type = TYPE.CCC, owner = "王五", date = "2020.1.14", Description ="第四个测试商品"});
        }

        public ObservableCollection<GoodsModel> GoodsModels {
            get { return goodsModels; }
            set { goodsModels = value; RaisePropertyChanged(); }
        }
    }
}
