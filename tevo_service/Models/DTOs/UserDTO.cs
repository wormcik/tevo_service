using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using tevo_service.Entities;

namespace tevo_service.Models.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }

    }
}
