using MinhaApi.Models;
using MinhaApi.Dtos;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Interfaces;

namespace MinhaApi.Services;

public class ProdutoService : IProdutoService
{
    private readonly LojaDbContext context;

    public ProdutoService(LojaDbContext context)
    {
        this.context = context;
    }

    public async Task<List<ProdutoDto>> GetProdutos()
    {
        return await context.Produtos.Include(p => p.Categoria).Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Categoria = p.Categoria.Nome
            }).ToListAsync();
    }

    public async Task<ProdutoDto?> GetProdutoPorId(int id)
    {
        return await context.Produtos.Include(p => p.Categoria)
        .Where(p => p.Id == id)
        .Select(p => new ProdutoDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            Categoria = p.Categoria.Nome
        })
        .FirstOrDefaultAsync();
    }

    public async Task<ProdutoDto> AddProduto(Produto produto)
    {
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var produtoComCategoria = await context.Produtos
        .Include(p => p.Categoria)
        .FirstAsync(p => p.Id == produto.Id);

        return new ProdutoDto
        {
        Id = produto.Id,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Categoria = produto.Categoria.Nome
        };
    }

    public async Task<ProdutoDto?> UpdateProduto(int id, Produto produtoAtualizado)
    {
        var produto = await context.Produtos.FindAsync(id);

        if (produto is null)
        {
            return null;
        }
        produto.Nome = produtoAtualizado.Nome;
        produto.Preco= produtoAtualizado.Preco;
        produto.CategoriaId = produtoAtualizado.CategoriaId;
        await context.SaveChangesAsync();

        var produtoComCategoria = await context.Produtos
        .Include(p => p.Categoria)
        .FirstAsync(p => p.Id == id);

        return new ProdutoDto
    {
        Id = produto.Id,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Categoria = produto.Categoria.Nome
    };
    }
    public async Task<ProdutoDto?> UpdateParcialmenteProduto(int id, ProdutoAtualizadoParcialmenteDto produtoAtualizadoParcialmente)
    {
        var produto = await context.Produtos.Include(p => p.Categoria)
        .FirstOrDefaultAsync(p => p.Id == id);;

        if (produto is null)
            return null;
    
        if (produtoAtualizadoParcialmente.Nome is not null)
            produto.Nome = produtoAtualizadoParcialmente.Nome;

        if (produtoAtualizadoParcialmente.Preco is not null)
            produto.Preco = produtoAtualizadoParcialmente.Preco.Value;
        await context.SaveChangesAsync();

        return new ProdutoDto
    {
        Id = produto.Id,
        Nome = produto.Nome,
        Preco = produto.Preco,
        Categoria = produto.Categoria.Nome
    };

    }

    public async Task<Produto?> DeleteProduto(int id)
    {
        var produto = await context.Produtos.FindAsync(id);
        if (produto is null)
            return null;
        context.Produtos.Remove(produto);
        await context.SaveChangesAsync();
        return produto;
    }

    public bool CategoriaExiste(int categoriaId)
    {
        return context.Categorias.Any(c => c.Id == categoriaId);
    }   

    public bool ProdutoExiste(string nome)
    {
        return context.Produtos.Any(p => p.Nome == nome);
    }

    public bool ProdutoExisteParcial(int id, string nome)
{
    return context.Produtos.Any(p => p.Nome == nome && p.Id != id);
}
}