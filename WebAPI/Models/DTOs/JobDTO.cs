using WebAPI.Models.Entities;

namespace WebAPI.Models.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
        public JobDTO(Job job)
        {
            this.JobId = job.JobId;
            this.JobTitle = job.JobTitle;
            this.MinSalary = job.MinSalary;
            this.MaxSalary = job.MaxSalary;
        }
    }
}
