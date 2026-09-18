using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SupplierProductExercise.Business.Entities;

namespace SupplierProductExercise.Business.Context
{
    public interface ISupplierProductDbContext
    {
        DbSet<SupplierProduct> SupplierProducts { get; set; }
        DbSet<SupplierProductVariant> SupplierProductVariants { get; set; }
        DbSet<SupplierServiceParameter> SupplierServiceParameters { get; set; }
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
