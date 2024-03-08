using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PRN231.ASS01.Repository.Models;
using System.Text;

namespace PRN231.ASS01.BookStoreClient.Client
{
    public class AuthorClient
    {
        private readonly HttpClient _httpClient;

        private readonly string URL = "http://localhost:5290/odata/Authors";
        private readonly JsonSerializerSettings jsonConvertSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        public AuthorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Author>> GetAll()
        {
            var response = await _httpClient.GetAsync(URL);
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<Author>>(json["value"].ToString());

            return data;
        }
        public async Task<Author> GetById(int? id)
        {
            var response = await _httpClient.GetAsync(URL + $"({id})");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Author>(content);

            return data;
        }
        public async Task Add(Author author)
        {
            string jsonPayload = JsonConvert.SerializeObject(author, jsonConvertSettings);
            Console.WriteLine(jsonPayload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);

        }
        public async Task Update(int id, Author author)
        {
            string jsonPayload = JsonConvert.SerializeObject(author, jsonConvertSettings);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(URL + $"({id})", content);
        }
        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync(URL + $"({id})");
        }
    }
}
