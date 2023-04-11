using Newtonsoft.Json;
using System.Net.Http;

namespace DadJokeAPI
{
    public class ApiManager : IApiManager
    {
        private readonly HttpClient _httpClient;
        private const string BaseURL = "https://dad-jokes.p.rapidapi.com";
        private readonly string _apiKey;

        public ApiManager(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;

        }
        public async Task<JokeResponse> GetRandomJokeAsync()
        {
            var request = GetHeaders();
            var response = await _httpClient.GetAsync(BaseURL + "/random/joke", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var joke = JsonConvert.DeserializeObject<JokeResponse>(json);
            return new JokeResponse(joke.Success, joke.Body);
        }

        public async Task<JokeResponse> GetRandomJokesAsync(int count)
        {
            var request = GetHeaders();
            var jokeList = new JokeResponse[count];
            if (count < 1 || count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be between 1 and 5.");
            }

            var response = await _httpClient.GetAsync(BaseURL + $"random/joke?count={count}", HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var jokes = JsonConvert.DeserializeObject<JokeResponse>(json);
            return new JokeResponse(jokes.Success, jokes.Body);
            //return jokes.Select(j => $"{j.Setup} {j.Punchline}").ToArray();
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
