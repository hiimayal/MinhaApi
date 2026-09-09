using MinhaApi.Models;
using MinhaApi.Services;
using MinhaApi.Dtos;
using System.ComponentModel.DataAnnotations;
using MinhaApi.Interfaces;

namespace MinhaApi.Endpoints;

public static class ProdutoEndpoints
{
    public static void MapProdutoEndpoints(this WebApplication app)
{
    app.MapGet("/produtos", async (IProdutoService service) =>
    {
        var produtos = await service.GetProdutos();
        return Results.Ok(produtos);
    });

    app.MapGet("/produtos/{id}", async (int id, IProdutoService service) =>
    {
        var produto = await service.GetProdutoPorId(id);
        if (produto is null)
        {
            return Results.NotFound("Produto não encontrado");
        }
        return Results.Ok(produto);
        
    });

    app.MapPost("/produtos", async (CriarProdutoDto dto, IProdutoService service) =>
    {
        var contexto = new ValidationContext(dto);
        var erros = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(
            dto,
            contexto,
            erros,
            validateAllProperties: true
        );
        if (!valido)
        {
            return Results.BadRequest(erros);
        }
        if (!service.CategoriaExiste(dto.CategoriaId))
        {
            return Results.BadRequest("Categoria não encontrada");
        }
        if (service.ProdutoExiste(dto.Nome))
        {
            return Results.Conflict("Já existe um produto com esse nome");
        }

        var produto = new Produto(
        dto.Nome,
        dto.Preco,
        dto.CategoriaId
    );
        var novoProduto = await service.AddProduto(produto);
        return Results.Created($"/produtos/{novoProduto.Id}", novoProduto);
    });
    
    app.MapPut("/produtos/{id}", async (int id, AtualizarProdutoDto dto, IProdutoService service) =>
    {
        var contexto = new ValidationContext(dto);
        var erros = new List<ValidationResult>();
        var valido = Validator.TryValidateObject(
            dto,
            contexto,
            erros,
            validateAllProperties: true
        );
        if (!valido)
        {
            return Results.BadRequest(erros);
        }
         if (!service.CategoriaExiste(dto.CategoriaId))
        {
            return Results.BadRequest("Categoria não encontrada");
        }
        if (service.ProdutoExisteParcial(id, dto.Nome))
        {
            return Results.Conflict("Já existe um produto com esse nome");
        }

        var produto = new Produto(
            dto.Nome,
            dto.Preco,
            dto.CategoriaId
        );

        var produtoAtualizadoBanco = await service.UpdateProduto(id, produto);

        if (produtoAtualizadoBanco is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(produtoAtualizadoBanco);
        });

    app.MapPatch("/produtos/{id}", async (int id, ProdutoAtualizadoParcialmenteDto dto, IProdutoService service) =>
    {
        if (dto.Nome is null && dto.Preco is null)
        {
            return Results.BadRequest("Nenhum campo informado para atualização");
        }
        var contexto = new ValidationContext(dto);
        var erros = new List<ValidationResult>();

        var valido = Validator.TryValidateObject(
            dto,
            contexto,
            erros,
            validateAllProperties: true
        );

        if (!valido)
        {
            return Results.BadRequest(erros);
        }

        // Verifica se o produto existe
        var produto = await service.GetProdutoPorId(id);
        if (produto is null)
        {
            return Results.NotFound();
        }

        if (dto.Nome is not null && service.ProdutoExisteParcial(id, dto.Nome))
        {
            return Results.Conflict("Já existe outro produto com esse nome");
        }

        var produtoAtualizado = await service.UpdateParcialmenteProduto(id, dto);

        
        return Results.Ok(produtoAtualizado);
    });

    app.MapDelete("/produtos/{id}", async (int id, IProdutoService service) =>
    {
        var produto = await service.DeleteProduto(id);
        if (produto is null)
        {
            return Results.NotFound();
        }
        return Results.NoContent();
    });
}
}