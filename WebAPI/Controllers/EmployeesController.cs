using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRAPI.Data;
using HRAPI.Models.Entities;
using HRAPI.Models.DTOs;
using HRAPI.Repository.Interfaces;

namespace HRAPI.Controllers
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

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
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
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'DataContext.Employees'  is null.");
          }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
    }
}
