using HRAPI.Models.Entities;

namespace HRAPI.Models.DTOs
{
    public class EmployeeDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int? TeamId { get; set; }
        public int? CurrentJobId { get; set; }
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }

        public EmployeeDTO(Employee employee)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            EmailAddress = employee.EmailAddress;
            BirthDate = employee.BirthDate;
            TeamId = employee.TeamId;
            CurrentJobId = employee.CurrentJobId;
            HireDate = employee.HireDate;
            Salary = employee.Salary;
        }
    }
}
