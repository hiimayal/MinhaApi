namespace MinhaApi.Models;
public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public decimal Preco { get; set;}
    //FK
    public int CategoriaId { get; set; }
    //Propriedade de navegação
    public Categoria Categoria {get; set;} = null!;


    public Produto(string Nome, decimal Preco, int CategoriaId)
    {
        this.Nome = Nome;
        this.Preco = Preco;
        this.CategoriaId = CategoriaId;
    }
}






