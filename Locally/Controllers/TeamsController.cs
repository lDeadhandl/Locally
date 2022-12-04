using System;
using Locally.Models;
using Locally.Services;
using Microsoft.AspNetCore.Mvc;

namespace Locally.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly TeamsService _teamsService;

        public TeamsController(TeamsService teamService) =>
            _teamsService = teamService;

        [HttpGet]
        public async Task<List<Team>> Get() =>
            await _teamsService.GetTeams();

        [HttpGet("{name}")]
        public async Task<ActionResult<Team>> Get(string name)
        {
            var teams = await _teamsService.GetAsync(name);

            if (teams is null)
            {
                return NotFound();
            }

            return teams;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            // TODO: should be called once a day to update team records
            var teams = await _teamsService.GetTeams();

            if (teams is null)
            {
                return NotFound();
            }

            await _teamsService.CreateAsync(teams);

            return Ok();
        }
    }
}
