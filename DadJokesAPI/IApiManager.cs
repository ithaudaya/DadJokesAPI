namespace DadJokeAPI
{
    public interface IApiManager
    {
        Task<JokeResponse> GetRandomJokeAsync();

        Task<JokeResponse> GetRandomJokesAsync(int count);
        
        Task<JokeCountResponse> GetJokeCountAsync();

    }
}
