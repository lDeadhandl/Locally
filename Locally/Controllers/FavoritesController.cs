using Locally.Models;
using Locally.Services;
using Microsoft.AspNetCore.Mvc;

namespace Locally.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly UserService _userService;

        public FavoritesController(UserService userService) =>
            _userService = userService;

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpGet("{username}")]
        public async Task<ActionResult<Favorites>> Get(string username)
        {
            var user = await _userService.GetAsync(username);

            if (user is null)
            {
                return NotFound();
            }

            return user.Favorites.FirstOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }
    }
}
