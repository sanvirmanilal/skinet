using Core.Interfaces;
using Infrastracture.Data;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>( opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));    
});


builder.Services.AddScoped<IProductRepository, ProductRepository>();
var app = builder.Build();

app.MapControllers();

try
{
    using var scoped = app.Services.CreateScope();
    var dbContext = scoped.ServiceProvider.GetRequiredService<StoreContext>();
    await dbContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(dbContext);
}
catch (System.Exception)
{
    
    throw;
}

app.Run();
