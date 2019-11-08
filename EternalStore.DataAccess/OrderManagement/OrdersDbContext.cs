﻿using EternalStore.DataAccess.OrderManagement.Configuration;
using EternalStore.Domain.OrderManagement;
using Microsoft.EntityFrameworkCore;

namespace EternalStore.DataAccess.OrderManagement
{
    public sealed class OrdersDbContext : DbContext
    {
        private string ConnectionString { get; set; }

        public DbSet<Order> Orders { get; set; }

        public OrdersDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        }
    }
}
