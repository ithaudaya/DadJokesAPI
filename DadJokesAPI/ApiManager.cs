using Newtonsoft.Json;
using System.Net.Http;

namespace DadJokeAPI
{
    public class ApiManager : IApiManager
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://dad-jokes.p.rapidapi.com";
        private readonly string _apiKey;

        public ApiManager(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;

        }
        public async Task<JokeResponse> GetRandomJokeAsync()
        {
            var request = GetHeaders();
            var response = await _httpClient.GetAsync(BaseUrl + "random/joke", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var jokeResponse = JsonConvert.DeserializeObject<JokeResponse>(json);
            return jokeResponse;
        }


        public async Task<JokeResponse[]> GetRandomJokesAsync(int count)
        {
            if (count < 1 || count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be between 1 and 5.");
            }

            var request = GetHeaders();
            var response = await _httpClient.GetAsync(BaseUrl + $"random/joke?count={count}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var jokeResponse = JsonConvert.DeserializeObject<JokeResponse[]>(json);
            return jokeResponse;
        }

        public JokeCountResponse GetJokeCountAsync()
        {
            var request = GetHeaders();
            var response = _httpClient.GetAsync(BaseUrl + "joke/count", HttpCompletionOption.ResponseHeadersRead).Result;
            response.EnsureSuccessStatusCode();
            var json = response.Content.ReadAsStringAsync().Result;
            var count = JsonConvert.DeserializeObject<int>(json);
            return new JokeCountResponse(true, count);
        }


        private HttpRequestMessage GetHeaders()
        {
            var request = new HttpRequestMessage();
            request.Headers.Add("x-rapidapi-key", _apiKey);
            request.Headers.Add("x-rapidapi-host", "dad-jokes.p.rapidapi.com");
            return request;
        }
    }
}
