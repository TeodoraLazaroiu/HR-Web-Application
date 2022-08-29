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
            var leaveHistories = (await unitOfWork.LeaveHistories
                .GetAll()).Select(a => new LeaveHistoryDTO(a)).ToList();
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

        // PUT: api/LeaveTypes/id
        // for HR to accept or decline the leave
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveHistory(int id, LeaveHistoryDTO leaveHistory)
        {
            var leaveHistoryInDb = await unitOfWork.LeaveHistories.GetById(id);

            if (leaveHistoryInDb == null)
            {
                return NotFound("Leave History with this id doesn't exist");
            }

            leaveHistoryInDb.Status = leaveHistory.Status;
            await unitOfWork.LeaveHistories.Update(leaveHistoryInDb);

            // updating employee's balance in status is ACCEPTED
            if (leaveHistory.Status.ToUpper() == "ACCEPTED")
            {
                int numberOfDays = unitOfWork.LeaveHistories.GetNumberOfDays(leaveHistoryInDb);
                var balance = await unitOfWork.LeaveBalances.GetById(leaveHistory.EmployeeId);

                if (balance == null)
                {
                    return BadRequest("Balance doesn't exist for this employee");
                }

                balance = unitOfWork.LeaveBalances.ReduceBalance(balance, numberOfDays);
                await unitOfWork.LeaveBalances.Update(balance);
            }

            unitOfWork.Save();

            return Ok();
        }

        // POST: api/LeaveHistories
        // for employees to request a leave
        [HttpPost]
        public async Task<ActionResult<LeaveHistoryDTO>> PostLeaveHistory(LeaveHistoryDTO leaveHistory)
        {
            var leaveHistoryToAdd = new LeaveHistory();
            leaveHistoryToAdd.StartDate = leaveHistory.StartDate;
            leaveHistoryToAdd.EndDate = leaveHistory.EndDate;
            leaveHistoryToAdd.LeaveTypeId = leaveHistory.LeaveTypeId;
            leaveHistoryToAdd.EmployeeId = leaveHistory.EmployeeId;
            leaveHistoryToAdd.Status = "PENDING";

            await unitOfWork.LeaveHistories.Create(leaveHistoryToAdd);
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

            // updating employee's balance if it was already accepted
            if (leaveHistoryInDb.Status.ToUpper() == "ACCEPTED")
            {
                int numberOfDays = unitOfWork.LeaveHistories.GetNumberOfDays(leaveHistoryInDb);
                var balance = await unitOfWork.LeaveBalances.GetById(leaveHistoryInDb.EmployeeId);

                if (balance == null)
                {
                    return BadRequest("Balance doesn't exist for this employee");
                }

                balance = unitOfWork.LeaveBalances.IncreaseBalance(balance, numberOfDays);
                await unitOfWork.LeaveBalances.Update(balance);
            }

            unitOfWork.Save();

            return Ok();
        }
    }
}
