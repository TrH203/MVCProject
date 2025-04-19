namespace JokeMVCApp.Models
{
    public class Jokes
    {
        public int Id { get; set; }
        public string JokeQuestion { get; set; } = string.Empty;
        public string JokeAnswer { get; set; } = string.Empty;
    }
}
