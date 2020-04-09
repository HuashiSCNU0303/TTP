using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TTP.Model;

namespace TTP.Data
{
    public class GoodsRestService : IGoodsRestService
    {
        HttpClient _client;

        public List<GoodsModel> GoodsItems { get; private set; }

        public GoodsRestService()
        {
            _client = new HttpClient();
        }
        public async Task AddGoodsAsync(GoodsModel item)
        {
            var uri = new Uri(string.Format(Constants.GoodsUrl, string.Empty));

            try
            {

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteGoodsAsync(long id)
        {
            var uri = new Uri(string.Format(Constants.GoodsUrl, id));

            try
            {
                var response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task<List<GoodsModel>> GetGoodsAsync()
        {
            GoodsItems = new List<GoodsModel>();


            var uri = new Uri(string.Format(Constants.GoodsUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    GoodsItems = JsonConvert.DeserializeObject<List<GoodsModel>>(content);
                }
                else {
                    Debug.WriteLine(@"\tERROR {0}", response.Content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return GoodsItems;
        }

        public async Task ModifyGoodsAsync(GoodsModel item)
        {
            var uri = new Uri(string.Format(Constants.GoodsUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(item);
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
