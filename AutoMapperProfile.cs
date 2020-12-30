using AutoMapper;
using ShopBanHang.Models;

namespace ShopBanHang
{
    internal class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();

        }
    }
}