﻿// <auto-generated />
using System;
using EternalStore.DataAccess.OrderManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EternalStore.DataAccess.Migrations.OrdersDb
{
    [DbContext(typeof(OrdersDbContext))]
    [Migration("20191129163400_ordersFirstCommit")]
    partial class ordersFirstCommit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EternalStore.Domain.OrderManagement.Order", b =>
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

            modelBuilder.Entity("EternalStore.Domain.OrderManagement.OrderItem", b =>
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

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("EternalStore.Domain.OrderManagement.OrderItem", b =>
                {
                    b.HasOne("EternalStore.Domain.OrderManagement.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("idOrder")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("EternalStore.Domain.StoreManagement.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("idProduct")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Product", b =>
                {
                    b.HasOne("EternalStore.Domain.StoreManagement.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}