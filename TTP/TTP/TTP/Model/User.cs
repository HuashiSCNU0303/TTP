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
    }
}
