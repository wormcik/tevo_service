using System.ComponentModel.DataAnnotations.Schema;

namespace tevo_service.Models.DTOs
{
    public class ContactInfoDTO
    {
        public long? ContactInfoId { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
    }
}
