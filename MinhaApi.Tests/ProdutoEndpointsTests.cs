using System.Net;
using System.Net.Http.Json;

namespace MinhaApi.Tests;

public class ProdutoEndpointsTests
{
    [Fact]
    public async Task GetProdutos_DeveRetornar200()
    {
        // Arrange
        await using var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/produtos");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetProdutoInexistente_DeveRetornar404()
    {
        // Arrange
        await using var factory = new CustomWebApplicationFactory();
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/produtos/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


   [Fact]
    public async Task PostProduto_DeveRetornar201()
    {
        // Arrange
        await using var factory = new CustomWebApplicationFactory();
        factory.SeedDatabase();

        var client = factory.CreateClient();

        var produto = new
        {
            nome = "Mouse",
            preco = 100,
            categoriaId = 1
        };

        // Act
        var response = await client.PostAsJsonAsync("/produtos", produto);

        var conteudo = await response.Content.ReadAsStringAsync();


        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

    }
}