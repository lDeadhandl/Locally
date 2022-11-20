using Locally.Models;
using Locally.Services;
using Microsoft.AspNetCore.Mvc;

namespace Locally.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly GamesService _gamesService;

        public GamesController(GamesService gameService)
        {
            _gamesService = gameService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<Game>>> GetDailyGames(string name, string year, string month, string day )
        {
            var games = await _gamesService.GetDailyGames(name, year, month, day);

            if (games is null)
            {
                return NotFound();
            }

            return games;
        }

        //[HttpGet()]
        //public async Task<ActionResult<List<Game>>> GetGames()
        //{
        //    var games = await _gamesService.GetAsync();

        //    if (games is null)
        //    {
        //        return NotFound();
        //    }

        //    return games;
        //}

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var games = await _gamesService.GetGames();

            if (games is null)
            {
                return NotFound();
            }

            await _gamesService.CreateAsync(games);

            return Ok();
        }
    }
}
