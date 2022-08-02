using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("Team")]
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int TeamLeadId { get; set; }
        public Employee? TeamLead { get; set; }
        public int? LocationId { get; set; }
        public Location? Location { get; set; }
        public IEnumerable<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
