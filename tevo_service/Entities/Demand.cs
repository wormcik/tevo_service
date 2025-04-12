using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tevo_service.Entities
{
    public class Demand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DemandId { get; set; }
        public decimal? DemandedMilk { get; set; }
        public decimal? DeliveredMilk { get; set; }
        public decimal? Price { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string? Currency { get; set; }
        public DateTime? Date { get; set; }

        [Column(TypeName = "VARCHAR(50)")]
        public string? State { get; set; }
        /*
         * State değerleri ve açıklamaları:
         * "Talep Oluşturuldu"   → Alıcı talebi oluşturdu ve satıcıyı seçti.
         * "Teklif Verildi"      → Satıcı fiyat ve teslim miktarını belirledi.
         * "Alıcı Onayladı"      → Alıcı teklifi onayladı.
         * "Teslim Edildi"       → Satıcı sütü teslim etti.
         * "Tamamlandı"          → Tüm süreç başarıyla tamamlandı.
         * "İptal Edildi"        → Talep iptal edildi (isteğe bağlı).
         */

        [ForeignKey(nameof(ContactInfo))]
        public long? ContactInfoId { get; set; }


        [ForeignKey(nameof(AddressInfo))]
        public long? AddressInfoId { get; set; }


        [ForeignKey(nameof(DelivererUser))]
        public Guid DelivererUserId { get; set; }
        public User? DelivererUser { get; set; }


        [ForeignKey(nameof(RecipientUser))]
        public Guid RecipientUserId { get; set; }
        public User? RecipientUser { get; set; }
    }
}
