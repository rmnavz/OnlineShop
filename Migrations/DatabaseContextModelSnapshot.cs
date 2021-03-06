﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Shop;

namespace Online_Shop.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Online_Shop.Models.AccountModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Nickname");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasFilter("[EmailAddress] IS NOT NULL");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Online_Shop.Models.CartModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<int>("Status");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Online_Shop.Models.CategoryModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Definition");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Online_Shop.Models.CustomerModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountID");

                    b.HasKey("ID");

                    b.HasIndex("AccountID")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Online_Shop.Models.ImageModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContentType");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("FileLocation");

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name");

                    b.Property<int?>("ProductID");

                    b.Property<int?>("StoreID");

                    b.Property<int?>("VariantID");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("StoreID");

                    b.HasIndex("VariantID");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Online_Shop.Models.OrderModel", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CartID");

                    b.Property<DateTime>("DateCreated");

                    b.Property<int>("Quantity");

                    b.Property<int>("Status");

                    b.Property<int>("VariantID");

                    b.HasKey("ID");

                    b.HasIndex("CartID");

                    b.HasIndex("ID")
                        .IsUnique();

                    b.HasIndex("VariantID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Online_Shop.Models.ProductCategoryModel", b =>
                {
                    b.Property<int>("ProductID");

                    b.Property<int>("CategoryID");

                    b.HasKey("ProductID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.ToTable("ProductCategoryModel");
                });

            modelBuilder.Entity("Online_Shop.Models.ProductModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.Property<int>("StoreID");

                    b.Property<int>("ViewCounts");

                    b.HasKey("ID");

                    b.HasIndex("StoreID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Online_Shop.Models.SellerModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountID");

                    b.HasKey("ID");

                    b.HasIndex("AccountID")
                        .IsUnique();

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("Online_Shop.Models.StoreModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Online_Shop.Models.StoreSellerModel", b =>
                {
                    b.Property<int>("StoreID");

                    b.Property<int>("SellerID");

                    b.HasKey("StoreID", "SellerID");

                    b.HasIndex("SellerID");

                    b.ToTable("StoreSellers");
                });

            modelBuilder.Entity("Online_Shop.Models.VariantModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductID");

                    b.Property<int>("Stock");

                    b.Property<string>("TwoLetterISOLanguageName")
                        .HasColumnName("Currency");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("Online_Shop.Models.CartModel", b =>
                {
                    b.HasOne("Online_Shop.Models.CustomerModel", "Customer")
                        .WithMany("Carts")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Online_Shop.Models.CustomerModel", b =>
                {
                    b.HasOne("Online_Shop.Models.AccountModel", "Account")
                        .WithOne("Customer")
                        .HasForeignKey("Online_Shop.Models.CustomerModel", "AccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Online_Shop.Models.ImageModel", b =>
                {
                    b.HasOne("Online_Shop.Models.ProductModel", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductID");

                    b.HasOne("Online_Shop.Models.StoreModel", "Store")
                        .WithMany("Images")
                        .HasForeignKey("StoreID");

                    b.HasOne("Online_Shop.Models.VariantModel", "Variant")
                        .WithMany("Images")
                        .HasForeignKey("VariantID");
                });

            modelBuilder.Entity("Online_Shop.Models.OrderModel", b =>
                {
                    b.HasOne("Online_Shop.Models.CartModel", "Cart")
                        .WithMany("Orders")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Online_Shop.Models.VariantModel", "Variant")
                        .WithMany("Orders")
                        .HasForeignKey("VariantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Online_Shop.Models.ProductCategoryModel", b =>
                {
                    b.HasOne("Online_Shop.Models.CategoryModel", "Category")
                        .WithMany("ProductCategory")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Online_Shop.Models.ProductModel", "Product")
                        .WithMany("ProductCategory")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Online_Shop.Models.ProductModel", b =>
                {
                    b.HasOne("Online_Shop.Models.StoreModel", "Store")
                        .WithMany("Products")
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Online_Shop.Models.SellerModel", b =>
                {
                    b.HasOne("Online_Shop.Models.AccountModel", "Account")
                        .WithOne("Seller")
                        .HasForeignKey("Online_Shop.Models.SellerModel", "AccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Online_Shop.Models.StoreSellerModel", b =>
                {
                    b.HasOne("Online_Shop.Models.SellerModel", "Seller")
                        .WithMany("StoreSeller")
                        .HasForeignKey("SellerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Online_Shop.Models.StoreModel", "Store")
                        .WithMany("StoreSeller")
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Online_Shop.Models.VariantModel", b =>
                {
                    b.HasOne("Online_Shop.Models.ProductModel", "Product")
                        .WithMany("Variants")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
