﻿// <auto-generated />
using System;
using EternalStore.DataAccess.NotificationManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EternalStore.DataAccess.Migrations.NotificationDb
{
    [DbContext(typeof(NotificationDbContext))]
    [Migration("20200409000150_SchedulerItemAddIsActiveIsDeleted")]
    partial class SchedulerItemAddIsActiveIsDeleted
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EternalStore.Domain.NotificationManagement.EmailMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RecipientEmail")
                        .IsRequired()
                        .HasColumnName("recipientEmail")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasColumnName("senderEmail")
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("SendingDate")
                        .HasColumnName("sendingDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("idSchedulerMessage")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idSchedulerMessage");

                    b.ToTable("emailMessages_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.NotificationManagement.SchedulerItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("idScheduler")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExecutionDateTime")
                        .HasColumnName("executionDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnName("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("schedulerItems_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.NotificationManagement.SchedulerMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnName("body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnName("subject")
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("idSchedulerItem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idSchedulerItem")
                        .IsUnique()
                        .HasFilter("[idSchedulerItem] IS NOT NULL");

                    b.ToTable("schedulerMessages_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.NotificationManagement.SchedulerSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ExecutionDayOfWeek")
                        .HasColumnName("executionDayOfWeek")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionFrequency")
                        .HasColumnName("executionFrequency")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionHours")
                        .HasColumnName("executionHours")
                        .HasColumnType("int");

                    b.Property<int>("ExecutionMinutes")
                        .HasColumnName("executionMinutes")
                        .HasColumnType("int");

                    b.Property<int?>("idSchedulerItem")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("idSchedulerItem")
                        .IsUnique()
                        .HasFilter("[idSchedulerItem] IS NOT NULL");

                    b.ToTable("schedulerSettings_tb");
                });

            modelBuilder.Entity("EternalStore.Domain.NotificationManagement.EmailMessage", b =>
                {
                    b.HasOne("EternalStore.Domain.NotificationManagement.SchedulerMessage", "Message")
                        .WithMany("EmailMessages")
                        .HasForeignKey("idSchedulerMessage");
                });

            modelBuilder.Entity("EternalStore.Domain.NotificationManagement.SchedulerMessage", b =>
                {
                    b.HasOne("EternalStore.Domain.NotificationManagement.SchedulerItem", "SchedulerItem")
                        .WithOne("Message")
                        .HasForeignKey("EternalStore.Domain.NotificationManagement.SchedulerMessage", "idSchedulerItem")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EternalStore.Domain.NotificationManagement.SchedulerSettings", b =>
                {
                    b.HasOne("EternalStore.Domain.NotificationManagement.SchedulerItem", "SchedulerItem")
                        .WithOne("Settings")
                        .HasForeignKey("EternalStore.Domain.NotificationManagement.SchedulerSettings", "idSchedulerItem")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
