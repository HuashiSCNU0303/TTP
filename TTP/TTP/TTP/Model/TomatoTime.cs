using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Model
{
    public class TomatoTime
    {
        [JsonProperty(PropertyName = "tomatoTimeId")]
        public string TomatoTimeId { get; set; }

        //结束时间
        [JsonProperty(PropertyName = "endTime")]
        public string EndTime { get; set; }

        //开始时间
        [JsonProperty(PropertyName = "beginTime")]
        public string BeginTime { get; set; }

        //当前用户id
        [JsonProperty(PropertyName = "userId")]
        public long UserId { get; set; }

        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }
    }
}
