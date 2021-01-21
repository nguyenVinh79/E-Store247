using AutoMapper;
using ECommerce.Project.Areas.Admin.Models;
using ECommerce.Project.Models;

namespace ECommerce.Project
{
    internal class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Supplier, SupplierVM>().ReverseMap();
            CreateMap<CT_Color, ColorModel>().ReverseMap();
            CreateMap<CT_WarrantyTime, WarrantyModel>().ReverseMap();
            CreateMap<CustomerInfo, CustomerInfoModel>().ReverseMap();
            CreateMap<Setting, SettingModel>().ReverseMap();
            CreateMap<Banner, BannerModel>().ReverseMap();
        }
    }
}