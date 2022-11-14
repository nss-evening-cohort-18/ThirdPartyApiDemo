using System.Text.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using ThirdPartyApiUsageDemo.Models;
using ThirdPartyApiUsageDemo.Models.SwapiResponses;

namespace ThirdPartyApiUsageDemo.Repositories;

public class SwapiRepository : ISwapiRepository
{
    private readonly HttpClient _swapiClient;

    public SwapiRepository(IHttpClientFactory clientFactory)
    {
        //this IHttpClientFactory is from Services in Program.cs, just like any other dependency injection
        //the name "Swapi" comes from the setup of this in Program.cs, go check it out.  You can set up
        //multiple named clients and call the correct name wherever used.
        _swapiClient = clientFactory.CreateClient("Swapi");
    }

    //This GetPlanets is a bit more complex than you would normally have for a GetAll
    //because of the way this particular API returns only 10 results at a time, forcing us
    //to fetch multiple pages of results.
    public async Task<List<Planet>?> GetPlanets(int page = 1, List<Planet>? planets = null)
    {
        //notice we only have to pass in the end of the URL since the BaseAddress property was set in Program.cs
        using var httpResponse = await _swapiClient.GetAsync($"planets/?page={page}");

        if (httpResponse.IsSuccessStatusCode)
        {
            var result = await httpResponse.Content.ReadFromJsonAsync<PlanetsResponseResult>();

            if (planets == null) planets= new List<Planet>();

            planets.AddRange(result.Results.ToList());

            if (result?.Next != null)
            {
                page += 1;
                await GetPlanets(page, planets);
            }
        }

        return planets;
    }

    public async Task<Planet?> GetPlanet(int id)
    {
        Planet? planet = null;

        var httpResponseMessage = await _swapiClient.GetAsync($"planets/{id}");

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            planet = await httpResponseMessage.Content.ReadFromJsonAsync<Planet>();
        }

        return planet;
    }

    
}
