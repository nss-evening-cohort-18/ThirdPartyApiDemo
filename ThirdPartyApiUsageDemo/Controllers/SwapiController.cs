using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using ThirdPartyApiUsageDemo.Models;
using ThirdPartyApiUsageDemo.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThirdPartyApiUsageDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwapiController : ControllerBase
    {
        private readonly ISwapiRepository _swapiRepo;

        public SwapiController(ISwapiRepository swapiRepository)
        {
            _swapiRepo = swapiRepository;
        }

        // GET: api/<SwapiController>
        [HttpGet("/planets")]
        public async Task<IActionResult> GetPlanets()
        {
            var results = await _swapiRepo.GetPlanets();

            if (results == null)
            {
                return StatusCode(422, "The Star Wars API could not be reached.");
            }

            return Ok(results);
        }

        [HttpGet("/planets/{id}")]
        public async Task<IActionResult> GetPlanet(int id)
        {
            var results = await _swapiRepo.GetPlanet(id);

            if (results == null)
            {
                return NotFound("Couldn't find a planet with that ID");
            }

            return Ok(results);
        }
    }
}
