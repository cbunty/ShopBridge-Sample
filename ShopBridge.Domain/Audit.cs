using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Domain
{
    public class Audit
    {
        [Column(TypeName = "VARCHAR(200)")]
        public string CreatedBy { get; set; }
        [Column(TypeName = "VARCHAR(200)")]
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        [NotMapped]
        public string UserId { get; set; }
    }
}
