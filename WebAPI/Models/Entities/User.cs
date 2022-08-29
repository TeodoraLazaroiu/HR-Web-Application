using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.DTOs;

namespace WebAPI.Models.Entities
{
    [Table("User")]
    public class User
    {
        private int v;
        private string salt;

        [Key]
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public User()
        {

        }
        public User(int employeeId, string emailAddress, string hashedPassword, string salt, string role)
        {
            EmployeeId = employeeId;
            EmailAddress = emailAddress;
            HashedPassword = hashedPassword;
            PasswordSalt = salt;
            Role = role;
        }
    }
}
