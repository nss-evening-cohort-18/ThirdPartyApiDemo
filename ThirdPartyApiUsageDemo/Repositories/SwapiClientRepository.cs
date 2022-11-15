using ThirdPartyApiUsageDemo.Models;
using ThirdPartyApiUsageDemo.Repositories;

namespace ThirdPartyApiUsageDemo.Clients;

public class SwapiClientRepository : ISwapiClientRepository
{
    private readonly HttpClient _swapiClient;

    public SwapiClientRepository(IHttpClientFactory clientFactory)
    {
        //this IHttpClientFactory is from Services in Program.cs, just like any other dependency injection
        //the name "Swapi" comes from the setup of this in Program.cs, go check it out.  You can set up
        //multiple named clients and call the correct name wherever used.
        _swapiClient = clientFactory.CreateClient("Swapi");
    }

    //This GetPeople method is a bit more complex than you would normally have for a GetAll
    //because of the way this particular API returns only 10 results at a time, forcing us
    //to fetch multiple pages of results.
    public async Task<List<SwapiPerson>?> GetPeople(int page = 1, List<SwapiPerson>? people = null)
    {
        //notice we only have to pass in the end of the URL since the BaseAddress property was set in Program.cs
        using var httpResponse = await _swapiClient.GetAsync($"people/?page={page}");

        if (httpResponse.IsSuccessStatusCode)
        {
            var result = await httpResponse.Content.ReadFromJsonAsync<SwapiPaginatedResponse<SwapiPerson>>();

            if (people == null) people = new List<SwapiPerson>();

            people.AddRange(result.Results.ToList());

            if (result?.Next != null)
            {
                page += 1;
                await GetPeople(page, people);
            }
        }

        return people;
    }

    public async Task<SwapiPerson?> GetPerson(int id)
    {
        SwapiPerson? planet = null;

        var httpResponseMessage = await _swapiClient.GetAsync($"people/{id}");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            planet = await httpResponseMessage.Content.ReadFromJsonAsync<SwapiPerson>();
        }

        return planet;
    }


}
