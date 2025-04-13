namespace tevo_service.Models
{
    public class DemandCreateModel
    {
        public decimal Demanded { get; set; }
        public decimal? Delivered { get; set; }
        public Guid DelivererUserId { get; set; }
        public Guid RecipientUserId { get; set; }
        public string Currency { get; set; } = "₺";
        public long ContactInfoId { get; set; }
        public long AddressInfoId { get; set; }
        public long ProductId { get; set; }
        public DateTime Date { get; set; }
    }
}
