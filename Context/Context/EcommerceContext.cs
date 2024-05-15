using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Context.Context
{
    public class EcommerceDbContext : IdentityDbContext<User>
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(product =>
            {
                // Create Primary Key
                product.HasKey(p => p.ProductID);

                // Set Constraints to the the Properties
                product.Property(p => p.Name).IsRequired();
                product.Property(p => p.Status).HasDefaultValue("Created");

                // Define the Relations
                product.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);

                product.HasOne(p => p.Vendor)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.VendorID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Category>(category =>
            {
                // Create Primary Key
                category.HasKey(p => p.CategoryID);

                // Set Constraints to the the Properties
                category.Property(p => p.Name).IsRequired();
            });

            builder.Entity<Order>(order =>
            {
                // Create Primary Key
                order.HasKey(o => o.OrderID);

                // Set Constraints to the the Properties
                order.Property(o => o.Status).IsRequired().HasDefaultValue("Success");

                // Define the Relations
                order.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<OrderProduct>(orderproduct =>
            {
                orderproduct.HasKey(op => new
                {
                    op.OrderID,
                    op.ProductID,
                });

                //Define the Relations
                orderproduct.HasOne(op => op.Product)
                    .WithMany(p => p.Products)
                    .HasForeignKey(op => op.ProductID)
                    .OnDelete(DeleteBehavior.Cascade);

                orderproduct.HasOne(op => op.Order)
                    .WithMany(p => p.Products)
                    .HasForeignKey(op => op.OrderID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(builder);
        }
    }
}
