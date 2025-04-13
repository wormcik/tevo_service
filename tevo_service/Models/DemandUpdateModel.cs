namespace tevo_service.Models
{
    public class DemandUpdateModel
    {
        public long DemandId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Delivered { get; set; }
        public string? State { get; set; }
    }
}
