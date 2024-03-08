using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PRN231.ASS01.Repository.Models;
using System.Text;

namespace PRN231.ASS01.BookStoreClient.Client
{
    public class UserClient
    {
        private readonly HttpClient _httpClient;
        private readonly PublisherClient _publisherClient;
        private readonly RoleClient _roleClient;
        private readonly string URL = "http://localhost:5290/odata/Users";
        private readonly JsonSerializerSettings jsonConvertSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        public UserClient(HttpClient httpClient, PublisherClient publisherClient, RoleClient roleClient)
        {
            _httpClient = httpClient;
            _publisherClient = publisherClient;
            _roleClient = roleClient;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<User>>(json["value"].ToString());
            foreach (User u in data)
            {
                u.Publisher = await _publisherClient.GetById(u.PubId);
                u.Role = await _roleClient.GetById(u.RoleId);
            }
            return data;
        }
        public async Task<User> GetById(int? id)
        {
            var response = await _httpClient.GetAsync(URL + $"({id})");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<User>(content);
            data.Publisher = await _publisherClient.GetById(data.PubId);
            data.Role = await _roleClient.GetById(data.RoleId);
            return data;
        }
        public async Task<User> GetByEmailAndPassword(string email, string password)
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<User>>(json["value"].ToString());
            foreach (User u in data)
            {
                u.Publisher = await _publisherClient.GetById(u.PubId);
                u.Role = await _roleClient.GetById(u.RoleId);
            }
            return data.FirstOrDefault(u => u.EmailAddress == email && u.Password == password);
        }
        public async Task Add(User user)
        {
            string jsonPayload = JsonConvert.SerializeObject(user, jsonConvertSettings);
            Console.WriteLine(jsonPayload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);

        }
        public async Task Update(int id, User user)
        {
            string jsonPayload = JsonConvert.SerializeObject(user, jsonConvertSettings);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(URL + $"({id})", content);
        }
        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync(URL + $"({id})");
        }
    }
}
