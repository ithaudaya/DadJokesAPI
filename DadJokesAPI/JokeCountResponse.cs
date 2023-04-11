namespace DadJokeAPI
{
    public class JokeCountResponse
    {
        public bool Success { get; set; }
        public int Count { get; set; }

        public JokeCountResponse(bool success, int count)
        {
            Success = success;
            Count = count;
        }
    }

}