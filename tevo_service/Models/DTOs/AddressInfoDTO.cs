using System.ComponentModel.DataAnnotations.Schema;

namespace tevo_service.Models.DTOs
{
    public class AddressInfoDTO
    {
        public long? AddressInfoId { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
    }
}
