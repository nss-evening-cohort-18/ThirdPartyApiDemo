using Microsoft.AspNetCore.Mvc;
using ThirdPartyApiUsageDemo.Models;
using ThirdPartyApiUsageDemo.Repositories;

namespace ThirdPartyApiUsageDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ISwapiClientRepository _swapiClient;
    private readonly IUserSqlRepository _userRepo;

    public UsersController(ISwapiClientRepository swapiClient, IUserSqlRepository userRepo)
    {
        _swapiClient = swapiClient;
        _userRepo = userRepo;
    }

    // GET: api/<UsersController>
    [HttpGet("/{id}")]
    public async Task<IActionResult> Profile(int id)
    {
        var user = _userRepo.GetUser(id);
        
        if (user == null)
        {
            return NotFound();
        }

        var favoriteCharacter = await _swapiClient.GetPerson(user.FavoriteCharacterId);
        var personOptions = await CreatePersonOptions();

        ProfileViewModel vm = new ProfileViewModel
        {
            User = user,
            FavoriteStarWarsCharacter = favoriteCharacter,
            PersonOptions = personOptions
        };

        return Ok(vm);
    }


    //this particular API doesn't return an ID property, so I have to get the ID from the provided URL
    private async Task<List<SwapiPersonOptions>> CreatePersonOptions()
    {
        var people = await _swapiClient.GetPeople();
        var options = people.Select(p =>
        {
            var splitURL = p.Url.Split('/'); /* [ "https:","","swapi.dev","api","people","47","" ] */
            var id = int.Parse(splitURL[5]); /* 47 */
            return new SwapiPersonOptions { Id= id, Name = p.Name };
        });

        return options.ToList();
    }
}
