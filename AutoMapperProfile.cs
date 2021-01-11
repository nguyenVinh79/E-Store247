using AutoMapper;
using ShopBanHang.Areas.Admin.Models;
using ShopBanHang.Models;

namespace ShopBanHang
{
    internal class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
            CreateMap<Supplier, SupplierVM>().ReverseMap();
            CreateMap<CT_Color, ColorModel>().ReverseMap();
            CreateMap<CT_WarrantyTime, WarrantyModel>().ReverseMap();
        }
    }
}