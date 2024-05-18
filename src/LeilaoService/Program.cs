using LeilaoService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<LeilaoDbContext>(opt => 
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

try 
{
    DbInitializer.InitDB(app);
}
catch (Exception e) 
{
    Console.WriteLine(e);
}

app.Run();