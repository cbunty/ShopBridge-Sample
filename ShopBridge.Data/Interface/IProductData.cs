using ShopBridge.Domain.DTO.Request;
using ShopBridge.Domain.DTO.Response;
using System.Threading.Tasks;

namespace ShopBridge.Data.Interface
{
    public interface IProductData
    {
        Task<PagedResults<ProductResponseModel>> GetProducts(PageRequest pageRequest);
        Task<ProductResponseModel> GetProduct(int productId);
        Task<ProductResponseModel> AddProduct(ProductRequestModel productRequest);
        Task<ProductResponseModel> UpdateProduct(ProductRequestModel productRequest);
        Task DeleteProduct(int id, AuditRequestModel auditRequestModel);
    }
}
