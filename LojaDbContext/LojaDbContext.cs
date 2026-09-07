using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

public class LojaDbContext : DbContext
{
    public LojaDbContext(DbContextOptions<LojaDbContext> options)
        : base(options)
    {
    }
    public DbSet<Produto> Produtos { get; set; }
}