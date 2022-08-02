using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("LeaveType")]
    public class LeaveType
    {
        [Key]
        public int Id { get; set; }
        public string LeaveName { get; set; } = string.Empty;
        public int NumberOfDays { get; set; }
        public string Description { get; set; } = string.Empty;
        public IEnumerable<LeaveHistory> LeaveHistories { get; set; } = new HashSet<LeaveHistory>();
    }
}
