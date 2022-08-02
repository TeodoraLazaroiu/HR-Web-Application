using WebAPI.Models.Entities;

namespace WebAPI.Models.DTOs
{
    public class TeamDTO
    {
        public string TeamName { get; set; } = string.Empty;
        public int TeamLeadId { get; set; }
        public int? LocationId { get; set; }

        public TeamDTO(Team team)
        {
            TeamName = team.TeamName;
            TeamLeadId = team.TeamLeadId;
            LocationId = team.LocationId;
        }
    }
}
