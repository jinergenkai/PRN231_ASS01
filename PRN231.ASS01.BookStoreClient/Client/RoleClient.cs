using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.BookStoreClient.Client
{
    public class RoleClient
    {
        private readonly HttpClient _httpClient;

        private readonly string URL = "http://localhost:5290/odata/Roles";
        private readonly JsonSerializerSettings jsonConvertSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        public RoleClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<Role>>(json["value"].ToString());
            return data;
        }
        public async Task<Role> GetById(int? id)
        {
            var response = await _httpClient.GetAsync(URL + $"({id})");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Role>(content);
            return data;
        }
    }
}
