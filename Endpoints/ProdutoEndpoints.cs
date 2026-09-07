using MinhaApi.Models;
using MinhaApi.Services;

namespace MinhaApi.Endpoints;

public static class ProdutoEndpoints
{
    public static void MapProdutoEndpoints(this WebApplication app)
{
    app.MapGet("/produtos", (ProdutoService service) =>
    {
        return service.GetProdutos();
    });

    app.MapGet("/produtos/{id}", (int id, ProdutoService service) =>
    {
        var produto = service.GetProdutoPorId(id);
        if (produto is null)
        {
            return Results.NotFound("Produto não encontrado");
        }
        return Results.Ok(produto);
        
    });

    app.MapPost("/produtos", (CriarProdutoDto produto, ProdutoService service) =>
    {
        var novoProduto = new Produto(
        produto.Nome,
        produto.Preco,
        produto.CategoriaId
    );
        service.AddProduto(novoProduto);
        return Results.Created($"/produtos/{novoProduto.Id}", novoProduto);
    });
    
    app.MapPut("/produtos/{id}", (int id, AtualizarProdutoDto produtoAtualizado, ProdutoService service) =>
    {
    var produto = new Produto(
        produtoAtualizado.Nome,
        produtoAtualizado.Preco,
        produtoAtualizado.CategoriaId
    );
    var produtoAtualizadoBanco = service.UpdateProduto(id, produto);

    if (produtoAtualizadoBanco is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(produtoAtualizadoBanco);
    });

    app.MapPatch("/produtos/{id}", (int id, ProdutoAtualizadoParcialmenteDto produtoAtualizadoParcialmente, ProdutoService service) =>
    {
        var produto = service.UpdateParcialmenteProduto(id, produtoAtualizadoParcialmente);

        if (produto is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(produto);
    });

    app.MapDelete("/produtos/{id}", (int id, ProdutoService service) =>
    {
        var produto = service.DeleteProduto(id);
        if (produto is null)
        {
            return Results.NotFound();
        }
        return Results.NoContent();
    });
}
}