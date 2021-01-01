using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MrLocalDb.Entities
{
    public class User
    {
        [Key]
        [Column(TypeName = "nvarchar(36)")]
        public string UserId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Username { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Shop shop { get; set; }
    }
}
