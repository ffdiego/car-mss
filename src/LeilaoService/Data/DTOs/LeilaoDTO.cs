namespace LeilaoService;

public class LeilaoDTO
{
    public Guid Id { get; set; }
    public int PrecoReserva { get; set; } = 0;
    public string Vendedor { get; set; }
    public string Vencedor { get; set; } 
    public int QuantidadeVendida { get; set; }
    public int LanceAtualMaisAlto { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
    public DateTime FinalizadoEm { get; set; }
    public string Status { get; set; } 

    // Item
    public string Fabrica { get; set; }
    public string Modelo { get; set; }
    public int Ano { get; set; }
    public string Cor { get; set; }
    public int Quilometragem { get; set; }
    public string URLImagem { get; set; }
}
