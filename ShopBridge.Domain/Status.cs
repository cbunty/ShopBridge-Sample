using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Domain
{
    public class Status : Audit
    {
        [Key]
        public byte Id { get; set; }
        [Column(TypeName = "VARCHAR(50)")]
        public string Name { get; set; }
    }
}
