namespace tevo_service.Models.DTOs
{
    public class UserProfileDTO
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public List<ContactInfoDTO>? ContactInfoList { get; set; }
        public List<AddressInfoDTO>? AddressInfoList { get; set; }
    }
}