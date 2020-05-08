using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Model
{
    public class User
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "passWord")]
        public string PassWord { get; set; }

        [JsonProperty(PropertyName = "imgurl")]
        public string Imgurl { get; set; }
    }
}
