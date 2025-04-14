namespace tevo_service.Models.DTOs
{
    public class UserBuyerDTO
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public List<ContactInfoDTO>? ContactInfoList { get; set; }
        public List<AddressInfoDTO>? AddressInfoList { get; set; }
    }
}
