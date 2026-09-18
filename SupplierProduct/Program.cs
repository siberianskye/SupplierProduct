using Microsoft.EntityFrameworkCore;
using SupplierProductExercise.Business.Context;
using SupplierProductExercise.Business.Importers;
using SupplierProductExercise.Business.Repositories;
using SupplierProductExercise.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//-------------------------DEPENDENCY REGISTRATION--------------------------------

builder.Services.AddScoped<ISupplierProductRepository, SupplierProductRepository>();
builder.Services.AddScoped<ISupplierProductVariantRepository, SupplierProductVariantRepository>();
builder.Services.AddScoped<ISupplierServiceParameterRepository, SupplierServiceParameterRepository>();
builder.Services.AddScoped<ISupplierProductService, SupplierProductService>();
builder.Services.AddScoped<ISupplierProductDataImporter, SupplierProductDataImporter>();

//--------------------------------------------------------------------------------

builder.Services.AddDbContext<ISupplierProductDbContext, SupplierProductDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SupplierProductDbContext"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ISupplierProductDbContext>();

    if (!dbContext.Database.CanConnect())
    {
        throw new NotImplementedException("Cannot connect to DB");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
