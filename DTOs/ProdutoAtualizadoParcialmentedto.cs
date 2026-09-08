using System.ComponentModel.DataAnnotations;

namespace MinhaApi.Dtos;

public class ProdutoAtualizadoParcialmenteDto
{
    public string? Nome {get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal? Preco {get; set; }
}
