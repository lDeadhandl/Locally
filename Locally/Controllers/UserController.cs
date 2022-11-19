using Locally.Data;
using Locally.Models;
using Locally.Services;
using Microsoft.AspNetCore.Mvc;

namespace Locally.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpGet("{name}")]
        public async Task<ActionResult<User>> Get(string name)
        {
            var user = await _userService.GetAsync(name);

            if (user is null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<IActionResult> Post(User newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, User updatedUser)
        {
            var user = await _userService.GetAsync(name);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Name = user.Name;

            await _userService.UpdateAsync(name, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }
    }
}