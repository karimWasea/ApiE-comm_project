using apistudy.Models.Entityies;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System.Reflection.Emit;

namespace apistudy.Models
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }
        public virtual DbSet<OrderDetail>  OrderDetails { get; set; }
        public virtual DbSet<OrderHeader>  OrderHeaders { get; set; }
        public virtual DbSet<ShoppingCart>  ShoppingCarts { get; set; }
        public virtual DbSet<ProductImage>  ProductImages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
