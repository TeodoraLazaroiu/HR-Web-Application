using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
        public int CurrentJobId { get; set; }
        public IEnumerable<JobHistory> JobHistories { get; set; } = new HashSet<JobHistory>();
        public int Salary { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int LeaveBalanceId { get; set; }
        public LeaveBalance? LeaveBalance { get; set; }
        public IEnumerable<LeaveHistory> LeaveHistories { get; set; } = new HashSet<LeaveHistory>();

        public Employee()
        {
            UserId = EmployeeId;
            LeaveBalanceId = EmployeeId;
        }
    }
}
