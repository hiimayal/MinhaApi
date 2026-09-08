namespace MinhaApi.Dtos;
using System.ComponentModel.DataAnnotations;

public class AtualizarProdutoDto
{
    [Required]
    public string Nome { get; set; } = "";

    [Range(0.01, double.MaxValue)]
    public decimal Preco { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoriaId { get; set; }
}