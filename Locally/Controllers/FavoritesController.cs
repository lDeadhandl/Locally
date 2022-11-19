using Locally.Models;
using Locally.Services;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Locally.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TeamsService _teamService;


        public FavoritesController(UserService userService, TeamsService teamService)
        {
            _userService = userService;
            _teamService = teamService;
        }

        //[HttpGet]
        //public async Task<List<User>> Get() =>
        //    await _userService.GetAsync();

        [HttpGet("{username}")]
        public async Task<ActionResult<Favorites?>> Get(string username)
        {
            var user = await _userService.GetAsync(username);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user.Favorites);
        }


        [HttpPost("{username}/{teamname}")]
        public async Task<IActionResult> Post(string username, string teamname)
        {
            var team = await _teamService.GetAsync(teamname);
            var user = await _userService.GetAsync(username);

            if(user.Favorites is null)
            {
                user.Favorites = new Favorites();
                user.Favorites.Teams = new List<Team>();
            }

            user.Favorites.Teams.Add(team);

            await _userService.UpdateAsync(username, user);

            return Ok();
        }

        [HttpDelete("{username}/{teamname}")]
        public async Task<IActionResult> Delete(string username, string teamname)
        {
            var team = await _teamService.GetAsync(teamname);
            var user = await _userService.GetAsync(username);

            user.Favorites.Teams?.RemoveAll(x => x.Name == team.Name);

            await _userService.UpdateAsync(username, user);

            return Ok();
        }


    }
}
