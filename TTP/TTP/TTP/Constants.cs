using System;
using System.Collections.Generic;
using System.Text;

namespace TTP
{
    public static class Constants
    {
        public static string BaseUrl = "http://47.97.196.50:8886";
        public static string WsUrl = "ws://192.168.1.6:8080/test-one/{0}";
        public static string GoodsUrl = BaseUrl+"/rest/goods-data/{0}";
        public static string PicUrl = BaseUrl + "/rest/api";
        public static string UserUrl = BaseUrl + "/rest/user/{0}";
        public static string TomatoTimeUrl = BaseUrl + "/rest/tomatoTime/{0}";
        public static long sendToUserId = 21;
        public static long currentUserId = 12;
    }
}
