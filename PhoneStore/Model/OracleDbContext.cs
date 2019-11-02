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
        public virtual DbSet<Phone> Phones { get; set; }
    }
}
