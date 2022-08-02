using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveHistoriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public LeaveHistoriesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/LeaveHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveHistoryDTO>>> GetLeaveHistories()
        {
            var leaveHistories = (await unitOfWork.LeaveHistories.GetAll()).Select(a => new LeaveHistoryDTO(a)).ToList();
            return leaveHistories;
        }

        // GET: api/LeaveHistories/id
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveHistoryDTO>> GetLeaveHistory(int id)
        {
            var leaveHistory = await unitOfWork.LeaveHistories.GetById(id);

            if (leaveHistory == null)
            {
                return NotFound("Leave History with this id doesn't exist");
            }

            return new LeaveHistoryDTO(leaveHistory);
        }

        // POST: api/LeaveHistories
        [HttpPost]
        public async Task<ActionResult<LeaveHistoryDTO>> PostLeaveHistory(LeaveHistoryDTO leaveHistory)
        {
            var leaveHistoryToAdd = new LeaveHistory();
            leaveHistoryToAdd.StartDate = leaveHistory.StartDate;
            leaveHistoryToAdd.EndDate = leaveHistory.EndDate;
            leaveHistoryToAdd.LeaveTypeId = leaveHistory.LeaveTypeId;
            leaveHistoryToAdd.EmployeeId = leaveHistory.EmployeeId;
            leaveHistoryToAdd.Status = leaveHistory.Status;

            await unitOfWork.LeaveHistories.Create(leaveHistoryToAdd);
            int numberOfDays = unitOfWork.LeaveHistories.GetNumberOfDays(leaveHistoryToAdd);


            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/LeaveHistories/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveHistory(int id)
        {
            var leaveHistoryInDb = await unitOfWork.LeaveHistories.GetById(id);

            if (leaveHistoryInDb == null)
            {
                return NotFound("Leave History with this id doesn't exist");
            }

            await unitOfWork.LeaveHistories.Delete(leaveHistoryInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
