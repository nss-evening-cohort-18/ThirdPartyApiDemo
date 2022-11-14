using ThirdPartyApiUsageDemo.Models;

namespace ThirdPartyApiUsageDemo.Repositories
{
    public interface ISwapiRepository
    {
        Task<Planet?> GetPlanet(int id);
        Task<List<Planet>?> GetPlanets(int page = 1, List<Planet>? planets = null);
    }
}