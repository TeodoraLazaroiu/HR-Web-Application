using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public LeaveTypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/LeaveTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveTypeDTO>>> GetLeaveTypes()
        {
            var leaveTypes = (await unitOfWork.LeaveTypes.GetAll()).Select(a => new LeaveTypeDTO(a)).ToList();
            return leaveTypes;
        }

        // GET: api/LeaveTypes/name
        [HttpGet("{name}")]
        public async Task<ActionResult<LeaveTypeDTO>> GetLeaveType(string name)
        {
            var leaveType = await unitOfWork.LeaveTypes.GetLeaveTypeByName(name);

            if (leaveType == null)
            {
                return NotFound("Leave Type with this name doesn't exist");
            }

            return new LeaveTypeDTO(leaveType);
        }

        // PUT: api/LeaveTypes/city
        [HttpPut("{name}")]
        public async Task<IActionResult> PutLeaveType(string name, LeaveTypeDTO leaveType)
        {
            var leaveTypeInDb = await unitOfWork.LeaveTypes.GetLeaveTypeByName(name);

            if (leaveTypeInDb == null)
            {
                return NotFound("Leave Type with this name doesn't exist");
            }

            leaveTypeInDb.LeaveName = leaveType.LeaveName;
            leaveTypeInDb.NumberOfDays = leaveType.NumberOfDays;
            leaveTypeInDb.Description = leaveType.Description;

            await unitOfWork.LeaveTypes.Update(leaveTypeInDb);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/LeaveTypes
        [HttpPost]
        public async Task<ActionResult<LeaveTypeDTO>> PostLeaveType(LeaveTypeDTO leaveType)
        {
            var leaveTypeInDb = await unitOfWork.LeaveTypes.GetLeaveTypeByName(leaveType.LeaveName);

            if (leaveTypeInDb != null)
            {
                return NotFound("Leave Type with this name already exist");
            }

            var leaveTypeToAdd = new LeaveType();
            leaveTypeToAdd.LeaveName = leaveType.LeaveName;
            leaveTypeToAdd.NumberOfDays = leaveType.NumberOfDays;
            leaveTypeToAdd.Description = leaveType.Description;


            await unitOfWork.LeaveTypes.Create(leaveTypeToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/LeaveTypes/name
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteLeaveType(string name)
        {
            var leaveTypeInDb = await unitOfWork.LeaveTypes.GetLeaveTypeByName(name);

            if (leaveTypeInDb == null)
            {
                return NotFound("Leave Type with this name doesn't exist");
            }

            await unitOfWork.LeaveTypes.Delete(leaveTypeInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
