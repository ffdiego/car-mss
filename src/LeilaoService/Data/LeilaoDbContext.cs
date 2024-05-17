using Microsoft.EntityFrameworkCore;

namespace LeilaoService;

public class LeilaoDbContext : DbContext
{
    public LeilaoDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Leilao> Leiloes { get; set; }
    
}
