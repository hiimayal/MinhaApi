using System.Text.Json.Serialization;

namespace MinhaApi.Models;

public class Categoria
{
    public int Id {get; set;}
    public string Nome { get; set; } = "";
    
    //Propriedade de navegação
    [JsonIgnore]
    public List<Produto> Produtos { get; set; } = new();
}