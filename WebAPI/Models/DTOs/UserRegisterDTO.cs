using WebAPI.Models.Entities;

namespace WebAPI.Models.DTOs
{
    public class UserRegisterDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public UserRegisterDTO()
        {

        }
    }
}
