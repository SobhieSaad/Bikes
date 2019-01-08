namespace BikesShop.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BikeOnineShopDBContext : DbContext
    {
        public BikeOnineShopDBContext()
            : base("name=BikesOnlineShopDBEntities1")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomersComment> CustomersComments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductRate> ProductRates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.Customer_Id);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.CustomersComments)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.ProductRates)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CustomersComment>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrdersDetails)
                .WithOptional(e => e.Order)
                .WillCascadeOnDelete();

            modelBuilder.Entity<OrdersDetail>()
                .Property(e => e.OrderNumber)
                .IsUnicode(false);

            modelBuilder.Entity<OrdersDetail>()
                .Property(e => e.Unitprice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.ImageName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Feature)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Brief)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.CustomersComments)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductRates)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}
