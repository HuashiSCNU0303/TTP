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
            goodsModels.Add(new GoodsModel() { Id = "001", Name = "书", Price = "50", Type = TYPE.AAA, Owner = "张三", Date="2020.1.1", Description = "第一个测试商品" });
            goodsModels.Add(new GoodsModel() { Id = "002", Name = "笔", Price = "20", Type = TYPE.AAA, Owner = "张三", Date = "2020.1.2", Description ="第二个测试商品"});
            goodsModels.Add(new GoodsModel() { Id = "003", Name = "斗破苍穹单行本", Price = "100", Type = TYPE.BBB, Owner = "李四", Date = "2020.2.1", Description ="第三个测试商品"});
            goodsModels.Add(new GoodsModel() { Id = "004", Name = "充电宝", Price = "3000", Type = TYPE.CCC, Owner = "王五", Date = "2020.1.14", Description ="第四个测试商品"});
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
    }
}
