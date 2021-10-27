using Microsoft.EntityFrameworkCore;
using ShopRUs.Core.Models;
using System;

namespace ShopRUs.Core
{
    public class ShopRUsContext : DbContext
    {
        public ShopRUsContext(DbContextOptions<ShopRUsContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Item> Items { get; set; }

    }
}
