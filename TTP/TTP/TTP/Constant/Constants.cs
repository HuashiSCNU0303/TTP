using System;
using System.Collections.Generic;
using System.Text;

namespace TTP
{
    public static class Constants
    {
        public static string BaseUrl = "http://47.97.196.50:8885";
        public static string WsUrl = "ws://47.97.196.50:8885/test-one/{0}";
        public static string GoodsUrl = BaseUrl+"/rest/goods-data/{0}";
        public static string PicUrl = BaseUrl + "/rest/api";
        public static string UserUrl = BaseUrl + "/rest/user/{0}";
        public static string UserApiUrl = BaseUrl + "/rest/user/api/{0}";
        public static string TomatoTimeUrl = BaseUrl + "/rest/tomatoTime/{0}";
        public static string AppUrl = BaseUrl + "/rest/userPackage/{0}";
    }
}
