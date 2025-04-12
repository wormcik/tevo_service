using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tevo_service.Entities
{
    public class AddressInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AddressInfoId { get; set; }

        [Column(TypeName = "VARCHAR(100)")]
        public string? Type { get; set; }

        [Column(TypeName = "VARCHAR(1000)")]
        public string? Value { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string? Longitude { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string? Latitude { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
