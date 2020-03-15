﻿// <auto-generated />
using System;
using EternalStore.DataAccess.StoreManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EternalStore.DataAccess.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    partial class StoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idCategory")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("isEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("categories_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idOrder")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInformation")
                        .HasColumnName("additionalInformation")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CustomerAddress")
                        .IsRequired()
                        .HasColumnName("customerAddress")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnName("customerName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerNumber")
                        .IsRequired()
                        .HasColumnName("customerNumber")
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnName("deliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnName("isApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelivered")
                        .HasColumnName("isDelivered")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnName("orderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("orders_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idOrderItem")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Qty")
                        .HasColumnName("qty")
                        .HasColumnType("int");

                    b.Property<int?>("idOrder")
                        .HasColumnType("int");

                    b.Property<int?>("idProduct")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idOrder");

                    b.HasIndex("idProduct");

                    b.ToTable("orderItems_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idProduct")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal");

                    b.Property<int?>("idCategory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idCategory");

                    b.ToTable("products_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.OrderItem", b =>
                {
                    b.HasOne("EternalStore.Domain.StoreManagement.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("idOrder")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EternalStore.Domain.StoreManagement.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("idProduct")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Product", b =>
                {
                    b.HasOne("EternalStore.Domain.StoreManagement.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("idCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
