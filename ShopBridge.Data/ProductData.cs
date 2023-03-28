using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopBridge.Data.Contexts;
using ShopBridge.Data.Extension;
using ShopBridge.Data.Interface;
using ShopBridge.Domain;
using ShopBridge.Domain.Configuration;
using ShopBridge.Domain.DTO.Request;
using ShopBridge.Domain.DTO.Response;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Data
{
    public class ProductData : IProductData
    {
        private readonly ShopBridgeContext _shopBridgeContext;
        private readonly IMapper _mapper;
        public ProductData(ShopBridgeContext shopBridgeContext, IMapper mapper)
        {
            _shopBridgeContext = shopBridgeContext;
            _mapper = mapper;
        }
        public async Task<ProductResponseModel> AddProduct(ProductRequestModel productRequest)
        {
            if(await CheckIfAlreadyExists(productRequest))
                throw new BadRequestException<Product>($"Product already exists with name {productRequest.Name}");
            var product = _mapper.Map<Product>(productRequest);
            _shopBridgeContext.Products.Add(product);
            return await SaveAndGetProduct(product);
        }

        public async Task<ProductResponseModel> UpdateProduct(ProductRequestModel productRequest)
        {
            if (await CheckIfAlreadyExists(productRequest))
                throw new BadRequestException<Product>($"Product already exists with name {productRequest.Name}");
            var product = await GetProductById(productRequest.Id);
            _mapper.Map(productRequest, product);
            _shopBridgeContext.Products.Update(product);
            return await SaveAndGetProduct(product);
        }

        public async Task<ProductResponseModel> GetProduct(int productId)
        {
            var response = await _mapper.ProjectTo<ProductResponseModel>(_shopBridgeContext.Products.Where(x => x.Id == productId)).FirstOrDefaultAsync();
            if(response == null)
                throw new EntityNotFoundException<Product>($"Product not found for Id - {productId}");
            return response;
        }

        public async Task<PagedResults<ProductResponseModel>> GetProducts(PageRequest pageRequest)
        {
            var query = _shopBridgeContext.Products.WhereIf(!string.IsNullOrEmpty(pageRequest?.SearchParam), prd => prd.Name.Contains(pageRequest.SearchParam) || prd.Description.Contains(pageRequest.SearchParam))
                                                   .Where(x => x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active);

            var queryData = query.ProjectTo<ProductResponseModel>(_mapper.ConfigurationProvider);
            if (pageRequest.PageSize != 0)
                queryData = queryData.TakePage(pageRequest.PageNumber, pageRequest.PageSize);
            var totalRecords = query.Count();

            return new PagedResults<ProductResponseModel>
            {
                PageNumber = pageRequest.PageNumber,
                PageSize = pageRequest.PageSize == 0 ? totalRecords : pageRequest.PageSize,
                TotalNumberOfRecords = totalRecords,
                Results = await queryData.ToListAsync()
            };
        }

        public async Task DeleteProduct(int id, AuditRequestModel auditRequestModel)
        {
            var product = await GetProductById(id);
            product.StatusId = (byte)Domain.Enumerations.StatusEnum.InActive;
            product.ModifiedBy = auditRequestModel.UserId;
            _shopBridgeContext.Products.Update(product);
            await _shopBridgeContext.SaveChangesAsync();
        }

        private async Task<bool> CheckIfAlreadyExists(ProductRequestModel productRequestModel)
        {
            if (await _shopBridgeContext.Products.AnyAsync(x => x.Name == productRequestModel.Name && x.StatusId == (byte)Domain.Enumerations.StatusEnum.Active && x.Id != productRequestModel.Id))
                return true;
            return false;
        }

        private async Task<Product> GetProductById(int id)
        {
            var product = await _shopBridgeContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                throw new EntityNotFoundException<Product>($"Product not found for Id - {id}");
            return product;
        }

        private async Task<ProductResponseModel> SaveAndGetProduct(Product product)
        {
            await _shopBridgeContext.SaveChangesAsync();
            return await GetProduct(product.Id);
        }
    }
}
