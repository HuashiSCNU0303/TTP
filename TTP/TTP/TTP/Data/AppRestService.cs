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
                }
                else
                {
                    Debug.WriteLine(@"\tERROR {0}", response.Content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return WhiteApps;
        }

        public async Task ModifyAppsAsync(long id, string whiteList)
        {
            var uri = new Uri(string.Format(Constants.AppUrl, string.Empty));
            try
            {
                Dictionary<long, string> whiteApps = new Dictionary<long, string>
                {
                    { id, whiteList }
                };
                string json = JsonConvert.SerializeObject(whiteApps);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully saved.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }
    }
}
