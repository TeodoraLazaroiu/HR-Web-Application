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
        [HttpGet("{firstName}/{lastName}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeByFullName(string firstName, string lastName)
        {
            var employee = await unitOfWork.Employees.GetEmployeeByFullName(firstName, lastName);

            if (employee == null)
            {
                return NotFound("Employee with this name doesn't exist");
            }

            return new EmployeeDTO(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{firstName}/{lastName}")]
        public async Task<IActionResult> PutEmployee(string firstName, string lastName, EmployeeDTO employee)
        {
            var employeeInDb = await unitOfWork.Employees.GetEmployeeByFullName(firstName, lastName);

            if (employeeInDb == null)
            {
                return BadRequest("Employee with this name doesn't exist");
            }

            employeeInDb.FirstName = employee.FirstName;
            employeeInDb.LastName = employee.LastName;
            employeeInDb.EmailAddress = employee.EmailAddress;
            employeeInDb.BirthDate = employee.BirthDate;
            employeeInDb.TeamId = employee.TeamId;
            employeeInDb.CurrentJobId = employee.CurrentJobId;
            employeeInDb.HireDate = employee.HireDate;
            employeeInDb.Salary = employee.Salary;

            await unitOfWork.Employees.Update(employeeInDb);
            unitOfWork.Save();

            return Ok();
        }

        // POST: api/Employees
       [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employee)
        {
            var employeeIdDb = await unitOfWork.Employees.GetEmployeeByFullName(employee.FirstName, employee.LastName);

            if (employeeIdDb != null)
            {
                return BadRequest("Employee with this name already exists");
            }

            var employeeToAdd = new Employee();
            employeeToAdd.FirstName = employee.FirstName;
            employeeToAdd.LastName = employee.LastName;
            employeeToAdd.EmailAddress = employee.EmailAddress;
            employeeToAdd.BirthDate = employee.BirthDate;
            employeeToAdd.TeamId = employee.TeamId;
            employeeToAdd.CurrentJobId = employee.CurrentJobId;
            employeeToAdd.HireDate = employee.HireDate;
            employeeToAdd.Salary = employee.Salary;

            await unitOfWork.Employees.Create(employeeToAdd);
            unitOfWork.Save();

            return Ok();
        }

        // DELETE: api/Employees/firstName/lastName
        [HttpDelete("{firstName}/{lastName}")]
        public async Task<IActionResult> DeleteEmployee(string firstName, string lastName)
        {
            var employeeInDb = await unitOfWork.Employees.GetEmployeeByFullName(firstName, lastName);

            if (employeeInDb == null)
            {
                return NotFound("Employee with this name doesn't exist");
            }

            await unitOfWork.Employees.Delete(employeeInDb);
            unitOfWork.Save();

            return Ok();
        }
    }
}
