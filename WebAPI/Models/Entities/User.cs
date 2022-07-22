using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRAPI.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public string EmailAddress { get; set; } = string.Empty;
        public string HashedPassword { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
