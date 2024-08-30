#region

using AutoMapper;
using Taste_Haven_API.Models;
using Taste_Haven_API.Models.Dto;

#endregion

namespace Taste_Haven_API.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<MenuItemCreateDto, MenuItem>().ReverseMap();
        CreateMap<MenuItemUpdateDto, MenuItem>().ReverseMap();

        CreateMap<OrderHeaderCreateDto, OrderHeader>().ReverseMap();
        CreateMap<OrderHeaderUpdateDto, OrderHeader>().ReverseMap();
        
        CreateMap<ShoppingCart, ShoppingCartDto>();
        CreateMap<CartItem, CartItemDto>();
        CreateMap<MenuItem, MenuItemDto>();
    }
}