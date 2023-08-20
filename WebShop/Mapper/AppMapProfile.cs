using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using WebShop.Data.Entites;
using WebShop.Data.Entities;
using WebShop.Models;
using WebShop.Models.Order;

namespace WebShop.Mapper
{
    public class AppMapProfile : Profile
    {
        public AppMapProfile()
        {
            CreateMap<CategoryEntity, CategoryItemViewModel>()
                .ForMember(x => x.ParentName, opt => opt.MapFrom(x => x.Parent.Name));

            CreateMap<CategoryCreateViewModel, CategoryEntity>()
                .ForMember(x => x.ParentId, opt => opt.MapFrom(x => x.ParentId == 0 ? null : x.ParentId))
                .ForMember(x => x.Image, opt => opt.Ignore());

            CreateMap<ProductImageEntity, ProdcutImageItemViewModel>();


            CreateMap<OrderEntity, OrderViewModel>();

          
           
             CreateMap< OrderCreateViewModel, OrderEntity>();

          
        }

      
    }
}

//var configuration = new MapperConfiguration(cfg => {
//    cfg.CreateMap<OrderCreateViewModel, OrderEntity>();
//});

//var mapper = configuration.CreateMapper();

//CreateMap<OrderEntity, OrderViewModel>()
//   .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.UserId));