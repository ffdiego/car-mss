using System.ComponentModel.DataAnnotations;

namespace LeilaoService;

public class NovoLeilaoDTO
{
    [Required]
    public int PrecoReserva { get; set; }

    [Required]
    public DateTime FinalizadoEm { get; set; }

    [Required]
    public string Fabrica { get; set; }

    [Required]
    public string Modelo { get; set; }

    [Required]
    public int Ano { get; set; }

    [Required]
    public string Cor { get; set; }

    [Required]
    public int Quilometragem { get; set; }
    
    [Required]
    public string URLImagem { get; set; }
}
