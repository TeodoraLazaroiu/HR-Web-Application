using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("LeaveBalance")]
    public class LeaveBalance
    {
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public int DaysTotal { get; set; } = 20;
        public int DaysRemaining { get; set; } = 20;
        public int DaysTaken { get; set; } = 0;
    }
}
