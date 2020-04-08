using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Model
{   //物品类型种类
    public enum TYPE { AAA, BBB, CCC }
    public class GoodsModel : ViewModelBase
    {
        
        
        //物品ID
        public string Id { get; set; }
        //物品名字
        public string name { get; set; }
        //物品价格
        public string price { get; set; }
        //物品类型
        public TYPE type { get; set; }
        //物品拥有者
        public string owner { get; set; }
        //物品发布时间
        public string date { get; set; }
        //物品的描述，一般由发布者写入
        public string Description { get; set;}
    }
}
