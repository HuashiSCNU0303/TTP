using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Model
{   //物品类型种类
    //public enum TYPE { 食品, 服装, 鞋帽, 日用品, 家具, 纺织}
    public class GoodsModel : ViewModelBase
    {


        //物品ID
        //数据库主键一般用int
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        //物品名字
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        //物品价格
        [JsonProperty(PropertyName = "price")]
        public int Price { get; set; }
        //物品类型
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        //物品拥有者
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }
        //物品发布时间
        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }
        //物品的描述，一般由发布者写入
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set;}
        //
        [JsonProperty(PropertyName = "uri")]
        public string Uri { get; set; }
    }
}
