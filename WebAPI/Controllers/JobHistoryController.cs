using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobHistoryController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public JobHistoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/JobHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobHistoryDTO>>> GetJobHistories()
        {
            var jobHistories = (await unitOfWork.JobHistories.GetAll()).Select(a => new JobHistoryDTO(a)).ToList();
            return jobHistories;
        }

        // GET: api/JobHistories/eid/jid
        [HttpGet("{eid}/{jid}")]
        public async Task<ActionResult<JobHistoryDTO>> GetJob(int EmployeeId, int JobId)
        {
            var jobHistory = await unitOfWork.JobHistories.GetByBothIds(EmployeeId, JobId);

            if (jobHistory == null)
            {
                return NotFound("Job History with those ids doesn't exist");
            }

            return new JobHistoryDTO(jobHistory);
        }

        // POST: api/JobHistories
        [HttpPost]
        public async Task<ActionResult<JobHistoryDTO>> PostJob(JobHistoryDTO jobHistory)
        {
            var jobHistoryToAdd = new JobHistory();
            jobHistoryToAdd.EmployeeId = jobHistory.EmployeeId;
            jobHistoryToAdd.JobId = jobHistory.JobId;

            await unitOfWork.JobHistories.Create(jobHistoryToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/JobHistories/eid/jid
        [HttpDelete("{eid}/{jid}")]
        public async Task<IActionResult> DeleteJob(int EmployeeId, int JobId)
        {
            var jobHistoryInDb = await unitOfWork.JobHistories.GetByBothIds(EmployeeId, JobId);

            if (jobHistoryInDb == null)
            {
                return NotFound("Job History with those ids doesn't exist");
            }

            await unitOfWork.JobHistories.Delete(jobHistoryInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
