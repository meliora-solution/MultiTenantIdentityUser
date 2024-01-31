﻿// <auto-generated />
using System;
using EasyStockDb.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EasyStockDb.Migrations
{
    [DbContext(typeof(EasyStockDbContext))]
    partial class EasyStockDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("EasyStock")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EasyStockDb.Entity.Contact", b =>
                {
                    b.Property<int>("ContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContactId"));

                    b.Property<string>("Address")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.HasKey("ContactId");

                    b.HasIndex("DataKey");

                    b.HasIndex("Name");

                    b.ToTable("Contacts", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ProductImage")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DataKey");

                    b.HasIndex("UnitId");

                    b.ToTable("Products", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.HasKey("CategoryId");

                    b.HasIndex("Category");

                    b.HasIndex("DataKey");

                    b.ToTable("ProductCategories", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.ProductTransfer", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DateTime");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Qty")
                        .HasPrecision(9, 2)
                        .HasColumnType("Decimal(18,2)");

                    b.Property<int>("StoreIdFrom")
                        .HasColumnType("int");

                    b.Property<int>("StoreIdTo")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("DateTime");

                    b.HasKey("TransactionId");

                    b.HasIndex("DataKey");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductTransfers", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.ProductUnit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"));

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("UnitId");

                    b.HasIndex("DataKey");

                    b.HasIndex("Unit");

                    b.ToTable("ProductUnits", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Stock", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<decimal>("qty")
                        .HasPrecision(9, 2)
                        .HasColumnType("Decimal(18,2)");

                    b.HasKey("ProductId", "StoreId");

                    b.HasIndex("DataKey");

                    b.HasIndex("StoreId");

                    b.ToTable("stocks", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("StoreId");

                    b.HasIndex("DataKey");

                    b.ToTable("Stores", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<int?>("ContactId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("DateTime");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(2000)");

                    b.Property<bool>("IsPosted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("DateTime");

                    b.HasKey("TransactionId");

                    b.HasIndex("ContactId");

                    b.HasIndex("DataKey");

                    b.ToTable("Transactions", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.TransactionLine", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.Property<string>("DataKey")
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(12)");

                    b.Property<decimal>("Price")
                        .HasPrecision(9, 2)
                        .HasColumnType("Decimal(18,2)");

                    b.Property<decimal>("Qty")
                        .HasPrecision(9, 2)
                        .HasColumnType("Decimal(18,2)");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("char(2)");

                    b.HasKey("ProductId", "TransactionId");

                    b.HasIndex("DataKey");

                    b.HasIndex("StoreId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionLines", "EasyStock");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Product", b =>
                {
                    b.HasOne("EasyStockDb.Entity.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyStockDb.Entity.ProductUnit", "ProductUnit")
                        .WithMany("Products")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");

                    b.Navigation("ProductUnit");
                });

            modelBuilder.Entity("EasyStockDb.Entity.ProductTransfer", b =>
                {
                    b.HasOne("EasyStockDb.Entity.Product", "Product")
                        .WithMany("ProductTransfers")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Stock", b =>
                {
                    b.HasOne("EasyStockDb.Entity.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyStockDb.Entity.Store", "Store")
                        .WithMany("Stocks")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Transaction", b =>
                {
                    b.HasOne("EasyStockDb.Entity.Contact", "Contact")
                        .WithMany("Transactions")
                        .HasForeignKey("ContactId");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("EasyStockDb.Entity.TransactionLine", b =>
                {
                    b.HasOne("EasyStockDb.Entity.Product", "Product")
                        .WithMany("TransactionLines")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyStockDb.Entity.Store", "Store")
                        .WithMany("LineTransactions")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyStockDb.Entity.Transaction", "Transaction")
                        .WithMany("LineTransactions")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Contact", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Product", b =>
                {
                    b.Navigation("ProductTransfers");

                    b.Navigation("Stocks");

                    b.Navigation("TransactionLines");
                });

            modelBuilder.Entity("EasyStockDb.Entity.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EasyStockDb.Entity.ProductUnit", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Store", b =>
                {
                    b.Navigation("LineTransactions");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("EasyStockDb.Entity.Transaction", b =>
                {
                    b.Navigation("LineTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
