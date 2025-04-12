using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tevo_service.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Column(TypeName = "VARCHAR(200)")]
        public string? UserName { get; set; }

        [Column(TypeName = "VARCHAR(500)")]
        public string? Password { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? Role { get; set; } //Seller, Buyer, Admin
        public bool? IsBanned { get; set; }
        public string? BanReason { get; set; }
        public List<ContactInfo>? ContactInfoList { get; set; }
        public List<AddressInfo>? AddressInfoList { get; set; }

        [InverseProperty("DelivererUser")]
        public List<Demand>? DeliveredDemands { get; set; }

        [InverseProperty("RecipientUser")]
        public List<Demand>? ReceivedDemands { get; set; }
    }
}
