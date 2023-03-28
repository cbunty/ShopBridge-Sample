using Microsoft.AspNetCore.Mvc;
using ShopBridge.Data.Interface;
using ShopBridge.Domain.DTO.Request;
using ShopBridge.Domain.DTO.Response;
using System.Threading.Tasks;


namespace ShopBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductData _productData;

        public ProductController(IProductData productData)
        {
            _productData = productData;
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="productRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProductResponseModel> Product(ProductRequestModel productRequestModel)
        {
            return await _productData.AddProduct(productRequestModel);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productRequestModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ProductResponseModel> Product(int id, ProductRequestModel productRequestModel)
        {
            productRequestModel.Id = id;
            return await _productData.UpdateProduct(productRequestModel);
        }

        /// <summary>
        ///  Get Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ProductResponseModel> Product(int id)
        {
            return await _productData.GetProduct(id);
        }

        /// <summary>
        /// Get Products
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResults<ProductResponseModel>> Product([FromQuery] PageRequest pageRequest)
        {
            return await _productData.GetProducts(pageRequest);
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id, AuditRequestModel auditRequestModel)
        {
            await _productData.DeleteProduct(id, auditRequestModel);
        }
    }
}
