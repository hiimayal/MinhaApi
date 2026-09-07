using MinhaApi.Models;
namespace MinhaApi.Services;

public class ProdutoService
{
    private readonly LojaDbContext context;

    public ProdutoService(LojaDbContext context)
    {
        this.context = context;
    }

    public List<Produto> GetProdutos()
    {
        return context.Produtos.ToList();
    }

    public Produto? GetProdutoPorId(int id)
    {
        return context.Produtos.Find(id);
    }

    public Produto AddProduto(Produto produto)
    {
        context.Produtos.Add(produto);
        context.SaveChanges();

        return produto;
    }

    public Produto? UpdateProduto(int id, Produto produtoAtualizado)
    {
        var produto = context.Produtos.Find(id);

        if (produto is null)
        {
            return null;
        }
        produto.Nome = produtoAtualizado.Nome;
        produto.Preco= produtoAtualizado.Preco;
        context.SaveChanges();

        return produto;
    }
    public Produto? UpdateParcialmenteProduto(int id, ProdutoAtualizadoParcialmenteDto produtoAtualizadoParcialmente)
    {
        var produto = context.Produtos.Find(id);

        if (produto is null)
            return null;
    
        if (produtoAtualizadoParcialmente.Nome is not null)
            produto.Nome = produtoAtualizadoParcialmente.Nome;

        if (produtoAtualizadoParcialmente.Preco is not null)
            produto.Preco = produtoAtualizadoParcialmente.Preco.Value;
        context.SaveChanges();

        return produto;

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