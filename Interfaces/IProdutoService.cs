using MinhaApi.Models;
using MinhaApi.Dtos;

namespace MinhaApi.Interfaces;

public interface IProdutoService
{
    Task<List<ProdutoDto>> GetProdutos();

    Task<ProdutoDto?> GetProdutoPorId(int id);

    Task<ProdutoDto> AddProduto(Produto produto);

    Task<ProdutoDto?> UpdateProduto(int id, Produto produtoAtualizado);

    Task<ProdutoDto?> UpdateParcialmenteProduto(
        int id,
        ProdutoAtualizadoParcialmenteDto produtoAtualizadoParcialmente);

    Task<Produto?> DeleteProduto(int id);

    bool CategoriaExiste(int categoriaId);

    bool ProdutoExiste(string nome);

    bool ProdutoExisteParcial(int id, string nome);
}