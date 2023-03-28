using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Domain.DTO.Request
{
    public class ProductRequestModel : ProductSearchRequestModel
    {
        [StringLength(2000)]
        public string Description { get; set; }
        [Required]
        [Range(1, 10000000, ErrorMessage = "The Price field must be greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The Quantity field must be greater or eqaul than 0")]
        public int Quantity { get; set; }

    }

    public class ProductSearchRequestModel: BaseRequestModel
    {
        public int Id { get; set; }
        [StringLength(500)]
        [Required]
        public string Name { get; set; }
    }
}
