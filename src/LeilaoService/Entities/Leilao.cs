using System.ComponentModel.DataAnnotations.Schema;

namespace LeilaoService;

[Table("Leiloes")]
public class Leilao
{
    public Guid Id { get; set; }
    public int PrecoReserva { get; set; } = 0;
    public string Vendedor { get; set; }
    public string Vencedor { get; set; } 
    public int? QuantidadeVendida { get; set; }
    public int? LanceAtualMaisAlto { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
    public DateTime FinalizadoEm { get; set; }
    public Status Status { get; set; } 
    public Item Item { get; set; } 
}
