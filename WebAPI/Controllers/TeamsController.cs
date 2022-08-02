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

        // GET: api/Teams/id
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDTO>> GetTeamByName(int id)
        {
            var team = await unitOfWork.Teams.GetById(id);

            if (team == null)
            {
                return NotFound("Team with this id doesn't exist");
            }

            return new TeamDTO(team);
        }

        // PUT: api/Teams/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, TeamDTO team)
        {
            var teamInDb = await unitOfWork.Teams.GetById(id);

            if (teamInDb == null)
            {
                return NotFound("Team with this id doesn't exist");
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
            var teamToAdd = new Team();
            teamToAdd.TeamName = team.TeamName;
            teamToAdd.TeamLeadId = team.TeamLeadId;
            teamToAdd.LocationId = team.LocationId;

            await unitOfWork.Teams.Create(teamToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Teams/name
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var teamInDb = await unitOfWork.Teams.GetById(id);

            if (teamInDb == null)
            {
                return NotFound("Team with this id doesn't exist");
            }

            await unitOfWork.Teams.Delete(teamInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
