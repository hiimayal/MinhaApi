using MinhaApi.Services;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Tests;

public class ProdutoServiceTests
{
    [Fact]
    public async Task DeveRetornarProdutoQuandoIdExiste()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<LojaDbContext>()
            .UseInMemoryDatabase("TesteProduto")
            .Options;

        using var context = new LojaDbContext(options);

        var categoria = new Categoria
        {
            Id = 1,
            Nome = "Eletrônicos"
        };

        var produto = new Produto("Teclado", 100, 1)
        {
            Id = 1,
            Categoria = categoria
        };

        context.Categorias.Add(categoria);
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var service = new ProdutoService(context);

        // Act
        var resultado = await service.GetProdutoPorId(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Teclado", resultado.Nome);
        Assert.Equal(100, resultado.Preco);
        Assert.Equal("Eletrônicos", resultado.Categoria);
    }   

    [Fact]
    public async Task DeveRetornarNullQuandoIdNaoExiste()
    {
    // Arrange
        var options = new DbContextOptionsBuilder<LojaDbContext>()
            .UseInMemoryDatabase("TesteProduto")
            .Options;

        using var context = new LojaDbContext(options);

        var service = new ProdutoService(context);

        // Act
        var resultado = await service.GetProdutoPorId(999);

         // Assert
        Assert.Null(resultado);
    }   

    
    [Fact]
    public async Task DeveRetornarListaDeProdutos()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<LojaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new LojaDbContext(options);

        var categoria = new Categoria
        {
            Id = 40,
            Nome = "Eletrônicos"
        };

        var produto = new Produto("mouse", 100, 40)
        {
            Id = 1,
            Categoria = categoria
        };

        context.Categorias.Add(categoria);
        context.Produtos.Add(produto);

        await context.SaveChangesAsync();

        var service = new ProdutoService(context);

        // Act
        var resultado = await service.GetProdutos();

        // Assert
        Assert.Equal(1, resultado.Count);
        Assert.Contains(resultado, p => p.Nome == "mouse");
    }

    [Fact]
    public async Task DeveCadastrarProduto()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<LojaDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        using var context = new LojaDbContext(options);

        var categoria = new Categoria
        {
            Id = 1,
            Nome = "Eletrônicos"
        };

        var produto = new Produto("tela", 100, 1)
        {
            Id = 1,
            Categoria = categoria
        };

        context.Categorias.Add(categoria);
        await context.SaveChangesAsync();

        var service = new ProdutoService(context);
        var resultado = await service.AddProduto(produto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("tela", resultado.Nome);
        Assert.Equal(100, resultado.Preco);
        Assert.Equal("Eletrônicos", resultado.Categoria);
    }
}