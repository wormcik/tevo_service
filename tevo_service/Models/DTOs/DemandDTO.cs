using System.ComponentModel.DataAnnotations.Schema;
using tevo_service.Entities;

namespace tevo_service.Models.DTOs
{
    public class DemandDTO
    {
        public long DemandId { get; set; }
        public decimal? Demanded { get; set; }
        public decimal? Delivered { get; set; }
        public decimal? Price { get; set; }
        public string? Currency { get; set; }
        public string? State { get; set; }
        public string? PaymentMethod { get; set; }
        public string? DelivererUserName { get; set; }
        public string? RecipientUserName { get; set; }
        public long? ProductId { get; set; }
        public DateTime? Date { get; set; }
        public ContactInfoDTO? ContactInfoModel { get; set; }
        public AddressInfoDTO? AddressInfoModel { get; set; }
        public ContactInfoDTO? SellerContactInfoModel { get; set; }
        public AddressInfoDTO? SellerAddressInfoModel { get; set; }
    }
}
