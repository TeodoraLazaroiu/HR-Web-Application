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
        public DateTime BirthDate { get; set; }
        public int? TeamId { get; set; }
        public Team? Team { get; set; }
        public int? CurrentJobId { get; set; }
        public JobHistory? CurrentJob { get; set; }
        public IEnumerable<JobHistory> JobHistories { get; set; } = new HashSet<JobHistory>();
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
        public User User { get; set; } = new User();
        public LeaveBalance LeaveBalance { get; set; } = new LeaveBalance();
        public IEnumerable<LeaveHistory> LeaveHistories { get; set; } = new HashSet<LeaveHistory>();
    }
}
