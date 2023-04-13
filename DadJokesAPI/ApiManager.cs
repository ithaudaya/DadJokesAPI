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
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}random/joke", HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var jokeResponse = JsonConvert.DeserializeObject<JokeResponse>(json);
                if (jokeResponse != null)
                {
                    return jokeResponse;
                }
                else
                {
                    throw new Exception("Error deserializing JSON response");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception or rethrow it
                throw new Exception("Error fetching random joke", ex);
            }
        }

        public async Task<JokeResponse[]> GetRandomJokesAsync(int count)
        {
            if (count < 1 || count > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be between 1 and 5.");
            }

            var request = GetHeaders();
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}random/joke?count={count}", HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var jokeResponses = JsonConvert.DeserializeObject<JokeResponse[]>(json);
                if (jokeResponses != null)
                {
                    return jokeResponses;
                }
                else
                {
                    throw new Exception("Error deserializing JSON response");
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle the exception or rethrow it
                throw new Exception("Error fetching random jokes", ex);
            }
        }

        public async Task<JokeCountResponse> GetJokeCountAsync()
        {
            var request = GetHeaders();
            try
            {
                var response = await _httpClient.GetAsync(BaseUrl + "joke/count", HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var jokeCountResponse = JsonConvert.DeserializeObject<JokeCountResponse>(json);
                if (jokeCountResponse != null)
                {
                    return jokeCountResponse;
                }
                else
                {
                    throw new Exception("Error deserializing JSON response");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error fetching joke count", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error parsing JSON", ex);
            }
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
