namespace ThirdPartyApiUsageDemo.Models.SwapiResponses
{
    public class PlanetsResponseResult
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public IEnumerable<Planet>? Results { get; set; }

    }
}
