using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TTP.Data
{
    public class AppRestService : IAppRestService
    {
        HttpClient _client;
        public string WhiteApps { get; private set; }
        public AppRestService()
        {
            _client = new HttpClient();
        }

        
        public async Task<string> GetAppsAsync(long id)
        {
            WhiteApps = "com.companyname.ttp";
            var uri = new Uri(string.Format(Constants.AppUrl, id));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JObject jObject = JsonConvert.DeserializeObject<JObject>(content);
                    WhiteApps = jObject["userPackage"].ToString();
                    Console.WriteLine("成功！！！");
                }
                else
                {
                    Console.WriteLine("错误1");
                    Debug.WriteLine(@"\tERROR {0}", response.Content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误2");
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return WhiteApps;
        }

        public async Task ModifyAppsAsync(long id, string whiteList)
        {
            var uri = new Uri(string.Format(Constants.AppUrl, string.Empty));
            try
            {
                
                var whiteApps = new Dictionary<string, string>
                {
                    { "userId", id.ToString() },
                    { "userPackage", whiteList }
                };
                string json = JsonConvert.SerializeObject(whiteApps);
                Console.WriteLine("解析的json：" + json);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("成功2");
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }
                else
                {
                    Console.WriteLine("失败333");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("失败！！");
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
