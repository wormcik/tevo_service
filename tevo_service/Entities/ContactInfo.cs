using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tevo_service.Entities
{
    public class ContactInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ContactInfoId { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? Type { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Value { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
