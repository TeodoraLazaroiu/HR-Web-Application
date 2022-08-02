using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public TeamsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            var teams = (await unitOfWork.Teams.GetAll()).Select(a => new TeamDTO(a)).ToList();
            return teams;
        }

        // GET: api/Teams/name
        [HttpGet("{name}")]
        public async Task<ActionResult<TeamDTO>> GetTeamByName(string name)
        {
            var team = await unitOfWork.Teams.GetTeamByName(name);

            if (team == null)
            {
                return NotFound("Team with this name doesn't exist");
            }

            return new TeamDTO(team);
        }

        // PUT: api/Teams/name
        [HttpPut("{name}")]
        public async Task<IActionResult> PutTeam(string name, TeamDTO team)
        {
            var teamInDb = await unitOfWork.Teams.GetTeamByName(name);

            if (teamInDb == null)
            {
                return NotFound("Team with this name doesn't exist");
            }

            teamInDb.TeamName = team.TeamName;
            teamInDb.TeamLeadId = team.TeamLeadId;
            teamInDb.LocationId = team.LocationId;

            await unitOfWork.Teams.Update(teamInDb);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/Teams
        [HttpPost]
        public async Task<ActionResult<TeamDTO>> PostTeam(TeamDTO team)
        {
            var teamInDb = await unitOfWork.Teams.GetTeamByName(team.TeamName);

            if (teamInDb != null)
            {
                return NotFound("Team with this name already exist");
            }

            var teamToAdd = new Team();
            teamToAdd.TeamName = team.TeamName;
            teamToAdd.TeamLeadId = team.TeamLeadId;
            teamToAdd.LocationId = team.LocationId;

            await unitOfWork.Teams.Create(teamToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Teams/name
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteTeam(string name)
        {
            var teamInDb = await unitOfWork.Teams.GetTeamByName(name);

            if (teamInDb == null)
            {
                return NotFound("Team with this name doesn't exist");
            }

            await unitOfWork.Teams.Delete(teamInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
