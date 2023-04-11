namespace DadJokeAPI
{
    public class JokeResponse
    {
        public bool Success { get; set; }

        public Joke[] Body { get; set; }

        public JokeResponse (bool success, Joke[] body )
        {
            Success = success;
            Body = body;

        }
    }
}