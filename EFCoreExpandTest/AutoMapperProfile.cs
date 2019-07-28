using AutoMapper;
using EFCoreExpandTest.DomainModels;
using EFCoreExpandTest.DTOModels;

namespace EFCoreExpandTest.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dto => dto.ProductPrices, dest => dest.MapFrom(x => x.ProductPrices))
                .ForMember(dto => dto.ProductPrices, dest => dest.ExplicitExpansion())
                .ForMember(dto => dto.ProductPrices, conf => conf.AllowNull());
            CreateMap<ProductPrice, ProductPriceDTO>()
                .ForMember(dto => dto.Product, conf => conf.AllowNull())
                .ForMember(dto => dto.Vendor, conf => conf.AllowNull()); 
            CreateMap<Vendor, VendorDTO>();
        }
    }
}
