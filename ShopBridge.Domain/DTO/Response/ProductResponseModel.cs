﻿namespace ShopBridge.Domain.DTO.Response
{
    public class ProductResponseModel : BaseResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
