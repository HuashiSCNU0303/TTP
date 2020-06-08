using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public class TomatoTimeService : ITomatoTimeService
    {
        HttpClient _client;

        public List<TomatoTime> AllTomatoTime { get; private set; }

        public TomatoTimeService()
        {
            _client = new HttpClient();
        }

        public async Task AddTomatoTimeAsync(TomatoTime tomatoTime)
        {
            var uri = new Uri(string.Format(Constants.TomatoTimeUrl, string.Empty));

            try
            {

                var json = JsonConvert.SerializeObject(tomatoTime);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<TomatoTime>> GetAllTomatoTimeAsync(long id)
        {
            AllTomatoTime = new List<TomatoTime>();


            var uri = new Uri(string.Format(Constants.TomatoTimeUrl, id));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    AllTomatoTime = JsonConvert.DeserializeObject<List<TomatoTime>>(content);
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

            return AllTomatoTime ;
        }
    }
}
