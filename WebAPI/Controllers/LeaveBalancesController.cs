using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveBalancesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public LeaveBalancesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/LeaveBalances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveBalanceDTO>>> GetLeaveBalances()
        {
            var leaveBalances = (await unitOfWork.LeaveBalances.GetAll()).Select(a => new LeaveBalanceDTO(a)).ToList();
            return leaveBalances;
        }

        // GET: api/LeaveBalances/id
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveBalanceDTO>> GetLeaveBalance(int id)
        {
            var leaveBalance = await unitOfWork.LeaveBalances.GetById(id);

            if (leaveBalance == null)
            {
                return NotFound("Leave Balance with this id doesn't exist");
            }

            return new LeaveBalanceDTO(leaveBalance);
        }

        // PUT: api/LeaveBalances/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaveBalance(int id, LeaveBalanceDTO leaveBalance)
        {
            var leaveBalanceInDb = await unitOfWork.LeaveBalances.GetById(id);

            if (leaveBalanceInDb == null)
            {
                return NotFound("Leave Balance with this id doesn't exist");
            }

            leaveBalanceInDb.DaysTotal = leaveBalance.DaysTotal;
            leaveBalanceInDb.DaysTaken = leaveBalance.DaysTaken;
            leaveBalanceInDb.DaysRemaining = leaveBalance.DaysRemaining;

            await unitOfWork.LeaveBalances.Update(leaveBalanceInDb);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/LeaveBalances
        [HttpPost]
        public async Task<ActionResult<LeaveBalanceDTO>> PostLeaveBalance(LeaveBalanceDTO leaveBalance)
        {

            var leaveBalanceToAdd = new LeaveBalance(leaveBalance);

            await unitOfWork.LeaveBalances.Create(leaveBalanceToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/LeaveBalances/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeaveBalance(int id)
        {
            var leaveBalanceInDb = await unitOfWork.LeaveBalances.GetById(id);

            if (leaveBalanceInDb == null)
            {
                return NotFound("Leave Balance with this id doesn't exist");
            }

            await unitOfWork.LeaveBalances.Delete(leaveBalanceInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
