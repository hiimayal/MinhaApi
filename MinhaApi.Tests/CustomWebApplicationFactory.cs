using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinhaApi.Models;

namespace MinhaApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<LojaDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<LojaDbContext>(options =>
            {
                options.UseInMemoryDatabase("BancoDeTeste");
            });
        });
    }

    public void SeedDatabase()
    {
        using var scope = Services.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<LojaDbContext>();

        context.Categorias.Add(new Categoria
        {
            Id = 1,
            Nome = "Eletrônicos"
        });

        context.SaveChanges();
    }
}