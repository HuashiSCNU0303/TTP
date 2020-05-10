using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TTP.Model
{
    public class ChatMessage
    {
        [JsonProperty(PropertyName = "senderId")]
        public long SenderId { get; set; }

        [JsonProperty(PropertyName = "receiverId")]
        public long ReceiverId { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
