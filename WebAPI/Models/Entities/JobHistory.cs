namespace WebAPI.Models.Entities
{
    public class JobHistory
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public int JobId { get; set; }
        public Job? Job { get; set; }

        public JobHistory(int eId, int jId)
        {
            this.EmployeeId = eId;
            this.JobId = jId;
        }

        public JobHistory()
        {

        }
    }
}
