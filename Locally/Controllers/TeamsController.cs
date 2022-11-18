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

        [HttpGet()]
        public async Task<ActionResult<ConferencesObject>> GetConferences()
        {
            var conferences = await _teamsService.GetConferences();

            if (conferences is null)
            {
                return NotFound();
            }

            return conferences;
        }
    }
}
