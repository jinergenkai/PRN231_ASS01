using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PRN231.ASS01.Repository.Models;
using System.Text;

namespace PRN231.ASS01.BookStoreClient.Client
{
    public class BookAuthorClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthorClient _authorClient;

        private readonly string URL = "http://localhost:5290/odata/BookAuthors";
        private readonly JsonSerializerSettings jsonConvertSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        public BookAuthorClient(HttpClient httpClient, AuthorClient authorClient)
        {
            _httpClient = httpClient;
            _authorClient = authorClient;
        }
        public async Task<IEnumerable<BookAuthor>> GetAll(int id)
        {
            var response = await _httpClient.GetAsync(URL + $"?$filter=BookId eq {id}");
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<BookAuthor>>(json["value"].ToString());
            foreach (BookAuthor b in data)
            {
                //Console.WriteLine(b.AuthorId);
                b.Author = await _authorClient.GetById(b.AuthorId);
            }
            return data;
        }
        public async Task Add(BookAuthor bookAuthor)
        {
            string jsonPayload = JsonConvert.SerializeObject(bookAuthor, jsonConvertSettings);
            Console.WriteLine(jsonPayload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(URL, content);

        }
        public async Task<BookAuthor> GetById(int? bookId, int? authorId)
        {
            var response = await _httpClient.GetAsync(URL + $"(AuthorId={authorId},BookId={bookId})");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception();
            }
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<BookAuthor>(content);
            data.Author = await _authorClient.GetById(data.AuthorId);

            return data;
        }
        public async Task Update(int bookId, int authorId, BookAuthor bookAuthor)
        {
            string jsonPayload = JsonConvert.SerializeObject(bookAuthor, jsonConvertSettings);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(URL + $"(AuthorId={authorId},BookId={bookId})", content);
        }
        public async Task Delete(int bookId, int authorId)
        {
            await _httpClient.DeleteAsync(URL + $"(AuthorId={authorId},BookId={bookId})");
        }
    }
}
