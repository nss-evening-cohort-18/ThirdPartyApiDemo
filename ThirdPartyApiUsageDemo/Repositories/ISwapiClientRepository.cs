using ThirdPartyApiUsageDemo.Models;

namespace ThirdPartyApiUsageDemo.Repositories
{
    public interface ISwapiClientRepository
    {
        Task<SwapiPerson?> GetPerson(int id);
        Task<List<SwapiPerson>?> GetPeople(int page = 1, List<SwapiPerson>? people = null);
    }
}