using AutoMapper;
using DataAccessLayer.Entities;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductEntity, ProductDTO>().ReverseMap();

            CreateMap<UserEntity, UserDTO>().ReverseMap();

            CreateMap<CartItemEntity, CartItemDTO>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ReverseMap()
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
}
