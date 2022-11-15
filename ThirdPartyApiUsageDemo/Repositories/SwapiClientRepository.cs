using ThirdPartyApiUsageDemo.Models;
using ThirdPartyApiUsageDemo.Repositories;

namespace ThirdPartyApiUsageDemo.Clients;

//Note that this is still a repository and its job is still to contact a data source,
//but I've called it a Client Repository as opposed to SQL Repository.
public class SwapiClientRepository : ISwapiClientRepository
{
    //this will come from dependency injection in the constructor
    private readonly HttpClient _swapiClient;

    public SwapiClientRepository(IHttpClientFactory clientFactory)
    {
        //This IHttpClientFactory is from Services in Program.cs, just like any other dependency injection.
        //The name "Swapi" comes from the setup of this in Program.cs, go check it out.  You can set up
        //multiple named clients and call the correct name wherever used.  The IHttpClientFactory is used
        //to procure an instance of our HttpClient.
        _swapiClient = clientFactory.CreateClient("Swapi");
    }

    //This GetPeople method is a bit more complex than you would normally have for a GetAll
    //because of the way this particular API returns only 10 results at a time, forcing us
    //to fetch multiple pages of results.  If all of your data is returned in one call then you won't have to 
    //check for result.Next the way I do here.  You can just ReadFromJsonAsync and be done much more quickly.
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

    //This is a much more straightforward example.
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
