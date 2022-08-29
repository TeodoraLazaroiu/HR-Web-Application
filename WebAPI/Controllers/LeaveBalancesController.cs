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
    }
}
