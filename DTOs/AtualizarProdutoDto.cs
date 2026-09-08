namespace MinhaApi.Dtos;

public class AtualizarProdutoDto
{
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
}