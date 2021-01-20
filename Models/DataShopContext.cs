using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Project.Models
{
    public class DataShopContext : IdentityDbContext<AppUser>
    {
        public DataShopContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CT_Color> CT_Colors { get; set; }
        public DbSet<CT_Size> CT_Sizes { get; set; }
        public DbSet<CT_Material> CT_Materials { get; set; }
        public DbSet<CT_WarrantyTime> CT_WarrantyTimes { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Banner> Banners { get; set; }

        public DbSet<Relate> Relatives { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductColorSize> ProductColorSizes { get; set; }
        public DbSet<CT_Province> CT_Provinces { get; set; }
        public DbSet<CT_District> CT_Districts { get; set; }
        public DbSet<CT_Ward> CT_Wards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CustomerInfo> CustomerInfos { get; set; }

      
    }
}