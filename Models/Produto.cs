namespace MinhaApi.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public decimal Preco { get; set;}
    public int CategoriaId { get; set; }


    public Produto(string Nome, decimal Preco, int CategoriaId)
    {
        this.Nome = Nome;
        this.Preco = Preco;
        this.CategoriaId = CategoriaId;
    }
}

public class AtualizarProdutoDto
{
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
}

public class ProdutoAtualizadoParcialmenteDto
{
    public string? Nome {get; set; }
    public decimal? Preco {get; set; }
}

public class CriarProdutoDto
{
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }

}