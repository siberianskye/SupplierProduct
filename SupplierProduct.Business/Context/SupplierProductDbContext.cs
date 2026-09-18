using Microsoft.EntityFrameworkCore;
using SupplierProductExercise.Business.Entities;
using System.Reflection;

namespace SupplierProductExercise.Business.Context
{
    public class SupplierProductDbContext : DbContext, ISupplierProductDbContext
    {
        public SupplierProductDbContext(DbContextOptions<SupplierProductDbContext> options)
           : base(options)
        { }

        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<SupplierProductVariant> SupplierProductVariants { get; set; }
        public DbSet<SupplierServiceParameter> SupplierServiceParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
