using MinhaApi.Models;
using MinhaApi.Services;
using MinhaApi.Dtos;
using System.ComponentModel.DataAnnotations;

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

    app.MapPost("/produtos", (CriarProdutoDto dto, ProdutoService service) =>
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
        var novoProduto = service.AddProduto(produto);
        return Results.Created($"/produtos/{novoProduto.Id}", novoProduto);
    });
    
    app.MapPut("/produtos/{id}", (int id, AtualizarProdutoDto dto, ProdutoService service) =>
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

        var produtoAtualizadoBanco = service.UpdateProduto(id, produto);

        if (produtoAtualizadoBanco is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(produtoAtualizadoBanco);
        });

    app.MapPatch("/produtos/{id}", (int id, ProdutoAtualizadoParcialmenteDto dto, ProdutoService service) =>
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
        var produto = service.GetProdutoPorId(id);
        if (produto is null)
        {
            return Results.NotFound();
        }

        if (dto.Nome is not null && service.ProdutoExisteParcial(id, dto.Nome))
        {
            return Results.Conflict("Já existe outro produto com esse nome");
        }

        var produtoAtualizado = service.UpdateParcialmenteProduto(id, dto);

        
        return Results.Ok(produtoAtualizado);
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