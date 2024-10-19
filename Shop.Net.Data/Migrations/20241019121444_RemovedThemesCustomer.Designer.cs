﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Net.Data;

#nullable disable

namespace Shop.Net.Data.Migrations
{
    [DbContext(typeof(ShopDbContext))]
    [Migration("20241019121444_RemovedThemesCustomer")]
    partial class RemovedThemesCustomer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DefaultBillingAddressId")
                        .HasColumnType("int");

                    b.Property<int?>("DefaultShippingAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("varchar(160)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("varchar(72)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("char(14)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.HasIndex("DefaultBillingAddressId");

                    b.HasIndex("DefaultShippingAddressId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.CustomerPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerPermissions");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.CustomerRoleMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RoleId");

                    b.ToTable("CustomerRoleMappings");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("varchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.ResetPasswordRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("OtpCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("char(6)")
                        .IsFixedLength();

                    b.Property<DateTime>("ValidTill")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("ResetPasswordRequests");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("varchar(72)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.RolePermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Permission")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("FlatNumber")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoadNumber")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int?>("StateProvinceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("StateProvinceId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("varchar(72)");

                    b.Property<string>("PhoneCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("ThreeLetterCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("char(3)")
                        .IsFixedLength();

                    b.Property<string>("TwoLetterCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("char(2)")
                        .IsFixedLength();

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.District", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DivisionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("varchar(72)");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.Division", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(72)
                        .HasColumnType("varchar(72)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("StateProvinces");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Inventories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LongDescription")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(280)
                        .HasColumnType("varchar(280)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Inventories.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DeletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LongDescription")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("varchar(1024)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasMaxLength(280)
                        .HasColumnType("varchar(280)");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Inventories.ProductCategoryMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategoryMappings");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.Customer", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Directories.Address", "DefaultBillingAddress")
                        .WithMany()
                        .HasForeignKey("DefaultBillingAddressId");

                    b.HasOne("Shop.Net.Core.Domains.Directories.Address", "DefaultShippingAddress")
                        .WithMany()
                        .HasForeignKey("DefaultShippingAddressId");

                    b.Navigation("DefaultBillingAddress");

                    b.Navigation("DefaultShippingAddress");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.CustomerPermission", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Customers.Customer", "Customer")
                        .WithMany("CustomerPermissions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.CustomerRoleMapping", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Customers.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Net.Core.Domains.Customers.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.Password", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Customers.Customer", "Customer")
                        .WithOne("Password")
                        .HasForeignKey("Shop.Net.Core.Domains.Customers.Password", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.ResetPasswordRequest", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Customers.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.RolePermission", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Customers.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.Address", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Directories.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Net.Core.Domains.Directories.Division", "StateProvince")
                        .WithMany()
                        .HasForeignKey("StateProvinceId");

                    b.Navigation("District");

                    b.Navigation("StateProvince");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.District", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Directories.Division", "Division")
                        .WithMany("Districts")
                        .HasForeignKey("DivisionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.Division", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Directories.Country", "Country")
                        .WithMany("Divisions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Inventories.ProductCategoryMapping", b =>
                {
                    b.HasOne("Shop.Net.Core.Domains.Inventories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Net.Core.Domains.Inventories.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Customers.Customer", b =>
                {
                    b.Navigation("CustomerPermissions");

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.Country", b =>
                {
                    b.Navigation("Divisions");
                });

            modelBuilder.Entity("Shop.Net.Core.Domains.Directories.Division", b =>
                {
                    b.Navigation("Districts");
                });
#pragma warning restore 612, 618
        }
    }
}
