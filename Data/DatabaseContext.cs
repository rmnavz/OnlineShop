using Microsoft.EntityFrameworkCore;
using Online_Shop.Models;

namespace Online_Shop
{
    /// <summary>
    /// The database representational model of the Application
    /// </summary>
    public class DatabaseContext : DbContext
    {
        #region Constructor

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion

        #region Public Properties
            public DbSet<AccountModel> Accounts { get; set; }
            public DbSet<SellerModel> Sellers { get; set; }
            public DbSet<CustomerModel> Customers { get; set; }

            public DbSet<StoreModel> Stores { get; set; }
            public DbSet<StoreSellerModel> StoreSellers { get; set; }
            public DbSet<ProductModel> Products { get; set; }
            public DbSet<VariantModel> Variants { get; set; }

            
            public DbSet<CartModel> Carts { get; set; }
            public DbSet<OrderModel> Orders { get; set; }

            public DbSet<ImageModel> Images { get; set; }
            
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Fluent API

                //Account
                modelBuilder.Entity<AccountModel>()
                    .HasIndex(i => i.EmailAddress).IsUnique();

                modelBuilder.Entity<AccountModel>()
                    .HasOne<SellerModel>(a => a.Seller)
                    .WithOne(s => s.Account)
                    .HasForeignKey<SellerModel>(s => s.AccountID);

                modelBuilder.Entity<AccountModel>()
                    .HasOne<CustomerModel>(a => a.Customer)
                    .WithOne(c => c.Account)
                    .HasForeignKey<CustomerModel>(c => c.AccountID);

                //Store and Seller Relationship
                modelBuilder.Entity<StoreSellerModel>()
                    .HasKey(ss => new { ss.StoreID , ss.SellerID });

                modelBuilder.Entity<StoreSellerModel>()
                    .HasOne<StoreModel>(ss => ss.Store)
                    .WithMany(s => s.StoreSeller)
                    .HasForeignKey(ss => ss.StoreID);

                modelBuilder.Entity<StoreSellerModel>()
                    .HasOne<SellerModel>(ss => ss.Seller)
                    .WithMany(s => s.StoreSeller)
                    .HasForeignKey(ss => ss.SellerID);

                //Store
                modelBuilder.Entity<StoreModel>()
                    .HasMany<ProductModel>(s => s.Products)
                    .WithOne(s => s.Store)
                    .HasForeignKey(p => p.StoreID);

                //Products
                modelBuilder.Entity<ProductModel>()
                    .HasMany<ImageModel>(v => v.Images)
                    .WithOne(i => i.Product)
                    .HasForeignKey(i => i.ProductID);

                modelBuilder.Entity<ProductModel>()
                    .HasMany<VariantModel>(v => v.Variants)
                    .WithOne(v => v.Product)
                    .HasForeignKey(v => v.ProductID);

                //Variants
                modelBuilder.Entity<VariantModel>()
                    .HasMany<ImageModel>(v => v.Images)
                    .WithOne(i => i.Variant)
                    .HasForeignKey(i => i.VariantID);

                modelBuilder.Entity<VariantModel>()
                    .HasMany<OrderModel>(v => v.Orders)
                    .WithOne(o => o.Variant)
                    .HasForeignKey(o => o.VariantID);

                //Customers
                modelBuilder.Entity<CustomerModel>()
                    .HasMany<CartModel>(cart => cart.Carts)
                    .WithOne(customer => customer.Customer)
                    .HasForeignKey(customer => customer.CustomerID);

                modelBuilder.Entity<VariantModel>()
                    .Ignore(v => v.Currency);

                modelBuilder.Entity<VariantModel>()
                    .Property(v => v.TwoLetterISOLanguageName)
                    .HasColumnName("Currency");

                //Carts
                modelBuilder.Entity<CartModel>()
                    .HasIndex(i => i.ID).IsUnique();

                modelBuilder.Entity<CartModel>()
                    .HasMany<OrderModel>(cart => cart.Orders)
                    .WithOne(order => order.Cart)
                    .HasForeignKey(order => order.CartID);


                //Orders
                modelBuilder.Entity<OrderModel>()
                    .HasIndex(i => i.ID).IsUnique();

                

                
            #endregion

        }
    }
}