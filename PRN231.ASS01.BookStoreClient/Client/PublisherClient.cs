using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PRN231.ASS01.Repository.Models;
using System.Text;

namespace PRN231.ASS01.BookStoreClient.Client
{
    public class PublisherClient
    {
        private readonly HttpClient _httpClient;
        private readonly string PUBLISHER_URL = "http://localhost:5290/odata/Publishers";
        private readonly JsonSerializerSettings jsonConvertSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
        public PublisherClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Publisher>> GetAll()
        {
            var response = await _httpClient.GetAsync(PUBLISHER_URL);
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<Publisher>>(json["value"].ToString());
            return data;
        }
        public async Task<Publisher> GetById(int? id)
        {
            var response = await _httpClient.GetAsync(PUBLISHER_URL + $"({id})");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Publisher>(content);
            return data;
        }
        public async Task Add(Publisher publisher)
        {
            string jsonPayload = JsonConvert.SerializeObject(publisher, jsonConvertSettings);
            Console.WriteLine(jsonPayload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(PUBLISHER_URL, content);

        }
        public async Task Update(int id, Publisher publisher)
        {
            string jsonPayload = JsonConvert.SerializeObject(publisher, jsonConvertSettings);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(PUBLISHER_URL + $"({id})", content);
        }
        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync(PUBLISHER_URL + $"({id})");
        }
    }
}
