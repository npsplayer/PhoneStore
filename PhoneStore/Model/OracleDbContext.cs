using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneStore.Model
{
    class OracleDbContext : DbContext
    {
        public OracleDbContext() : base("OracleDbContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("APPUSER");
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }
        public virtual DbSet<Option> Options { get; set; }
        public virtual DbSet<OptionType> OptionTypes { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<OrderHistory> OrderHistories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
    }
}
