namespace WebAPI.Models.DTOs
{
    public class UserDTO
    {
        public int EmployeeId { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string Role { get; set; }

        public UserDTO()
        {

        }
    }
}
