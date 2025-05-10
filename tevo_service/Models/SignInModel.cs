using tevo_service.Entities;
using tevo_service.Models.DTOs;

namespace tevo_service.Models
{
    public class SignInModel
    {
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } // Buyer, Seller
        public ContactInfoDTO ContactInfoModel { get; set; }
        public AddressInfoDTO AddressInfoModel { get; set; }
    }

}
