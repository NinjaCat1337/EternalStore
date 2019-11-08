﻿// <auto-generated />
using EternalStore.DataAccess.StoreManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EternalStore.DataAccess.Migrations.StoreDb
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
                        .HasColumnType("nvarchar")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("categories_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idProduct")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("nvarchar")
                        .HasMaxLength(1500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar")
                        .HasMaxLength(50);

                    b.Property<decimal>("Price")
                        .HasColumnName("price")
                        .HasColumnType("decimal");

                    b.Property<int>("idCategory")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idCategory");

                    b.ToTable("products_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.StoreManagement.Product", b =>
                {
                    b.HasOne("EternalStore.Domain.StoreManagement.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("idCategory")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
