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
            var jobHistories = (await unitOfWork.JobHistories
                .GetAll()).Select(a => new JobHistoryDTO(a)).ToList();
            return jobHistories;
        }

        // GET: api/JobHistories/eid/jid
        [HttpGet("{eid}/{jid}")]
        public async Task<ActionResult<JobHistoryDTO>> GetJob(int eid, int jid)
        {
            var jobHistory = await unitOfWork.JobHistories.GetByBothIds(eid, jid);

            if (jobHistory == null)
            {
                return NotFound("Job History with those ids doesn't exist");
            }

            return new JobHistoryDTO(jobHistory);
        }

        // PUT: api/JobHistories/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobHistory(int id, JobHistoryDTO job)
        {
            var jobHistoryInDb = await unitOfWork.JobHistories.GetById(id);

            if (jobHistoryInDb == null)
            {
                return NotFound("Job with this id doesn't exist");
            }

            jobHistoryInDb.EmployeeId = job.EmployeeId;
            jobHistoryInDb.JobId = job.JobId;

            await unitOfWork.JobHistories.Update(jobHistoryInDb);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/JobHistories
        [HttpPost]
        public async Task<ActionResult<JobHistoryDTO>> PostJobHistory(JobHistoryDTO jobHistory)
        {
            var jobHistoryToAdd = new JobHistory(jobHistory);

            await unitOfWork.JobHistories.Create(jobHistoryToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/JobHistories/eid/jid
        [HttpDelete("{eid}/{jid}")]
        public async Task<IActionResult> DeleteJob(int eid, int jid)
        {
            var jobHistoryInDb = await unitOfWork.JobHistories.GetByBothIds(eid, jid);

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
