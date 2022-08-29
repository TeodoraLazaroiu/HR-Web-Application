using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("LeaveBalance")]
    public class LeaveBalance
    {
        [Key]
        public int LeaveBalanceId { get; set; }
        public Employee? Employee { get; set; }
        public int DaysTotal { get; set; } = 20;
        public int DaysRemaining { get; set; } = 20;
        public int DaysTaken { get; set; } = 0;

        public LeaveBalance(int employeeId)
        {
            this.LeaveBalanceId = employeeId;
        }

        public LeaveBalance()
        {

        }
    }
}
