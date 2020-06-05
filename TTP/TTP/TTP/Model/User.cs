using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Model
{
    public class User
    {
        [JsonProperty(PropertyName = "userId")]
        public long UserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "passWord")]
        public string PassWord { get; set; }

        [JsonProperty(PropertyName = "tomatoPoints")]
        public int TomatoPoints { get; set; }

        [JsonProperty(PropertyName = "imgurl")]
        public string Imgurl { get; set; }

        public TimeSpan TotalTimes { get; set; }

    }
}
