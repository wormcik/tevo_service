namespace tevo_service.Models
{
    public class DemandCreateModel
    {
        public decimal DemandedMilk { get; set; }
        public Guid DelivererUserId { get; set; }
        public Guid RecipientUserId { get; set; }
        public string Currency { get; set; } = "₺";
        public long ContactInfoId { get; set; }
        public long AddressInfoId { get; set; }
    }
}
