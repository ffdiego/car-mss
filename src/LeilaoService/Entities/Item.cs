using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoService;

[Table("Items")]
public class Item
{
    public Guid Id { get; set; }
    public string Fabrica { get; set; }
    public string Modelo { get; set; }
    public int Ano { get; set; }
    public string Cor { get; set; }
    public int Quilometragem { get; set; }
    public string URLImagem { get; set; }

    // propriedades de navegação
    public Leilao Leilao { get; set; }
    public Guid LeilaoId { get; set; }
}
