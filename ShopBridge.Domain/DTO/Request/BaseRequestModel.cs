using ShopBridge.Domain.Enumerations;

namespace ShopBridge.Domain.DTO.Request
{
    public class BaseRequestModel : AuditRequestModel
    {
        public byte? StatusId { get; set; } = (byte)StatusEnum.Active;
    }
}
