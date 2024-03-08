using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PRN231.ASS01.Repository.Models;
using System.Text;

namespace PRN231.ASS01.BookStoreClient.Client
{
    public class BookClient
    {
        private readonly HttpClient _httpClient;
        private readonly PublisherClient _publisherClient;
        private readonly string URL = "http://localhost:5290/odata/Books";
        private readonly JsonSerializerSettings jsonConvertSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        public BookClient(HttpClient httpClient, PublisherClient publisherClient)
        {
            _httpClient = httpClient;
            _publisherClient = publisherClient;

        }
        public async Task<IEnumerable<Book>> GetAll(string searchName, decimal minPrice, decimal maxPrice)
        {
            var response = await _httpClient.GetAsync(URL + $"?$filter=contains(toLower(Title), toLower('{searchName}')) and Price le {maxPrice} and Price ge {minPrice}");
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<Book>>(json["value"].ToString());
            foreach (Book u in data)
            {
                u.Publisher = await _publisherClient.GetById(u.PubId);
            }
            return data;
        }
        public async Task<Book> GetById(int? id)
        {
            var response = await _httpClient.GetAsync(URL + $"({id})");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Book>(content);
            data.Publisher = await _publisherClient.GetById(data.PubId);

            return data;
        }
        public async Task Add(Book book)
        {
            string jsonPayload = JsonConvert.SerializeObject(book, jsonConvertSettings);
            Console.WriteLine(jsonPayload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);

        }
        public async Task Update(int id, Book book)
        {
            string jsonPayload = JsonConvert.SerializeObject(book, jsonConvertSettings);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(URL + $"({id})", content);
        }
        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync(URL + $"({id})");
        }
    }
}
