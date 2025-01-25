using AutoMapper;
using PSM.Application.Contracts.Products;
using PSM.Domain.Products;

namespace PSM.Application
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            //CreateMap<List<Product>, List<ProductDto>>();
        }
    }
}
