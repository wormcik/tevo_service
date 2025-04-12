using tevo_service.Entities;
using tevo_service.Models.DTOs;

namespace tevo_service.Models
{
    public class ProfileUpdateModel
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public List<ContactInfoDTO>? ContactInfoList { get; set; }
        public List<AddressInfoDTO>? AddressInfoList { get; set; }
    }
}
