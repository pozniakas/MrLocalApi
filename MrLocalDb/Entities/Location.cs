using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrLocalDb.Entities
{
    public class Location
    {
        [Key]
        [Column(TypeName = "nvarchar(36)")]
        public string LocationId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Latitude { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Longitude { get; set; }
    }
}
