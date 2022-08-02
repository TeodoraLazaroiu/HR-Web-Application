using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("Job")]
    public class Job
    {
        [Key]
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public IEnumerable<JobHistory> JobHistories { get; set; } = new HashSet<JobHistory>();
    }
}
