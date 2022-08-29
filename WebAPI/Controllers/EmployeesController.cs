using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Entities;
using WebAPI.Models.DTOs;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = (await unitOfWork.Employees.GetAll()).Select(a => new EmployeeDTO(a)).ToList();
            return employees;
        }

        // GET: api/Employees/firstName/lastName
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await unitOfWork.Employees.GetById(id);

            if (employee == null)
            {
                return NotFound("Employee with this id doesn't exist");
            }

            return new EmployeeDTO(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employee)
        {
            var employeeInDb = await unitOfWork.Employees.GetById(id);

            if (employeeInDb == null)
            {
                return BadRequest("Employee with this id doesn't exist");
            }

            employeeInDb.FirstName = employee.FirstName;
            employeeInDb.LastName = employee.LastName;
            employeeInDb.EmailAddress = employee.EmailAddress;
            employeeInDb.TeamId = employee.TeamId;
            employeeInDb.CurrentJobId = employee.CurrentJobId;
            employeeInDb.Salary = employee.Salary;

            await unitOfWork.Employees.Update(employeeInDb);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/Employees
       [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employee)
        {

            var employeeToAdd = new Employee();

            employeeToAdd.FirstName = employee.FirstName;
            employeeToAdd.LastName = employee.LastName;
            employeeToAdd.EmailAddress = employee.EmailAddress;
            employeeToAdd.TeamId = employee.TeamId;
            employeeToAdd.CurrentJobId = employee.CurrentJobId;
            employeeToAdd.Salary = employee.Salary;
            await unitOfWork.Employees.Create(employeeToAdd);

            var leaveBalance = new LeaveBalance(employeeToAdd.EmployeeId);
            await unitOfWork.LeaveBalances.Create(leaveBalance);

            var jobHistory = new JobHistory(employeeToAdd.EmployeeId, employeeToAdd.CurrentJobId);
            await unitOfWork.JobHistories.Create(jobHistory);

            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Employees/firstName/lastName
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employeeInDb = await unitOfWork.Employees.GetById(id);

            if (employeeInDb == null)
            {
                return NotFound("Employee with this id doesn't exist");
            }

            await unitOfWork.Employees.Delete(employeeInDb);

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
