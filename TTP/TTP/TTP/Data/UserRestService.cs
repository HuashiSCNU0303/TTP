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
    public class UserRestService : IUserRestService
    {
        HttpClient _client;

        public User RestUser { get; private set; }
        public UserRestService()
        {
            _client = new HttpClient();
        }
        public async Task AddUserAsync(User user)
        {
            var uri = new Uri(string.Format(Constants.UserUrl, string.Empty));
            try
            {

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task DeleteUserAsync(long id)
        {
            var uri = new Uri(string.Format(Constants.UserUrl, id));

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

        public async Task<User> GetUserAsync(long id)
        {
            RestUser = new User();


            var uri = new Uri(string.Format(Constants.UserUrl, id));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    RestUser = JsonConvert.DeserializeObject<User>(content);
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

            return RestUser;
        }

        public async Task ModifyUserAsync(User user)
        {
            var uri = new Uri(string.Format(Constants.GoodsUrl, string.Empty));

            try
            {
                var json = JsonConvert.SerializeObject(user);
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

        public async Task<User> GetUserByNameAsync(String userName)
        {
            RestUser = new User();


            var uri = new Uri(string.Format(Constants.UserApiUrl, userName));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    RestUser = JsonConvert.DeserializeObject<User>(content);
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

            return RestUser;
        }
    }
}
