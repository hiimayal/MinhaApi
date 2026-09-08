using MinhaApi.Models;
using MinhaApi.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MinhaApi.Services;

public class ProdutoService
{
    private readonly LojaDbContext context;

    public ProdutoService(LojaDbContext context)
    {
        this.context = context;
    }

    public List<ProdutoDto> GetProdutos()
    {
        return context.Produtos.Include(p => p.Categoria).Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Categoria = p.Categoria.Nome
            }).ToList();
    }

    public ProdutoDto? GetProdutoPorId(int id)
    {
        return context.Produtos.Include(p => p.Categoria)
    .Where(p => p.Id == id)
    .Select(p => new ProdutoDto
    {
        Id = p.Id,
        Nome = p.Nome,
        Preco = p.Preco,
        Categoria = p.Categoria.Nome
    })
    .FirstOrDefault();
    }

    public ProdutoDto AddProduto(Produto produto)
    {
        context.Produtos.Add(produto);
        context.SaveChanges();

        return new ProdutoDto
        {
        Id = produto.Id,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Categoria = produto.Categoria.Nome
        };
    }

    public ProdutoDto? UpdateProduto(int id, Produto produtoAtualizado)
    {
        var produto = context.Produtos.Find(id);

        if (produto is null)
        {
            return null;
        }
        produto.Nome = produtoAtualizado.Nome;
        produto.Preco= produtoAtualizado.Preco;
        produto.CategoriaId = produtoAtualizado.CategoriaId;
        context.SaveChanges();

        return new ProdutoDto
    {
        Id = produto.Id,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Categoria = produto.Categoria.Nome
    };
    }
    public ProdutoDto? UpdateParcialmenteProduto(int id, ProdutoAtualizadoParcialmenteDto produtoAtualizadoParcialmente)
    {
        var produto = context.Produtos.Include(p => p.Categoria)
        .FirstOrDefault(p => p.Id == id);;

        if (produto is null)
            return null;
    
        if (produtoAtualizadoParcialmente.Nome is not null)
            produto.Nome = produtoAtualizadoParcialmente.Nome;

        if (produtoAtualizadoParcialmente.Preco is not null)
            produto.Preco = produtoAtualizadoParcialmente.Preco.Value;
        context.SaveChanges();

        return new ProdutoDto
    {
        Id = produto.Id,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Categoria = produto.Categoria.Nome
    };

    }

    public Produto? DeleteProduto(int id)
    {
        var produto = context.Produtos.Find(id);
        if (produto is null)
            return null;
        context.Produtos.Remove(produto);
        context.SaveChanges();
        return produto;
    }
}