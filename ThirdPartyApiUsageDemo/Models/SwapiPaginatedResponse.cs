namespace ThirdPartyApiUsageDemo.Models;

public class SwapiPaginatedResponse<T>
{
    public string? Next { get; set; }
    public IEnumerable<T>? Results { get; set; }
}
