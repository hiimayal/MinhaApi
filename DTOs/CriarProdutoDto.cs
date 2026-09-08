namespace MinhaApi.Dtos;

public class CriarProdutoDto
{
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }

}