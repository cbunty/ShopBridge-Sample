using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Domain.DTO.Request
{
    public class AuditRequestModel
    {
        [Required]
        [StringLength(200)]
        public string UserId { get; set; }
    }
}
