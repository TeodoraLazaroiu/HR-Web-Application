using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Entities
{
    [Table("Location")]
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; } = 0;
        public string Country { get; set; } = string.Empty;
        public IEnumerable<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
