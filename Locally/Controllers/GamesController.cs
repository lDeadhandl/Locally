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

        public GamesController(GamesService gameService) =>
            _gamesService = gameService;

        [HttpGet()]
        public async Task<ActionResult<GamesObject>> GetGames()
        {
            var games = await _gamesService.GetGames();

            if (games is null)
            {
                return NotFound();
            }

            return games;
        }
    }
}
