using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public Employee? Employee { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
