using AutoMapper;
using ShopBridge.Domain;
using ShopBridge.Domain.DTO.Request;
using ShopBridge.Domain.DTO.Response;

namespace ShopBridge.API.AutoMapperProfiles
{
    public class ProductAutoMapperProfile : Profile
    {
        public ProductAutoMapperProfile()
        {
            CreateMap<ProductRequestModel, Product>()
              .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Product, ProductResponseModel>()
                .ForMember(x => x.Status, src => src.MapFrom(x => x.Status));

            CreateMap<Status, StatusResponseModel>()
                .ForMember(x => x.StatusId, src => src.MapFrom(x => x.Id)); ;
        }
    }
}
