namespace FactsApp.Models
{
    public class Fact
    {
        public int Id { get; set; }

        public string? Author { get; set; }

        public string? FactTitle { get; set; }

        public string? FactBody { get; set; }
    }
}
