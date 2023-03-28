using System;

namespace ShopBridge.Domain.DTO.Response
{
    public class AuditResponseModel
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
