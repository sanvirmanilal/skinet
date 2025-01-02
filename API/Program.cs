using Core.Entities;
using Core.Interfaces;
using Infrastracture.Data;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(async opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    opt.UseAsyncSeeding(async (context, _, cancellationToken) =>
    {
        if (!await context.Set<Product>().AnyAsync())
        {
            await context.Set<Product>().AddRangeAsync(await StoreContextSeed.GetSeedDataAsync(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    });
    opt.EnableSensitiveDataLogging();
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
var app = builder.Build();

app.MapControllers();

try
{
    using var scoped = app.Services.CreateScope();
    var dbContext = scoped.ServiceProvider.GetRequiredService<StoreContext>();
    await dbContext.Database.EnsureCreatedAsync();
    await dbContext.Database.MigrateAsync();
}
catch (System.Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
