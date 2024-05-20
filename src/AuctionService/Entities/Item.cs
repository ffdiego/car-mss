using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoService;

[Table("Items")]
public class Item
{
    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public string ImageUrl { get; set; }

    // propriedades de navegação
    public Auction Leilao { get; set; }
    public Guid LeilaoId { get; set; }
}
