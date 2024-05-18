using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeilaoService;

[ApiController]
[Route("api/leiloes")]
public class LeilaoController: ControllerBase
{
    private readonly LeilaoDbContext _context;
    private readonly IMapper _mapper;

    public LeilaoController(LeilaoDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<LeilaoDTO>>> GetAllLeiloes()
    {
        var leiloes = await _context.Leiloes
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Fabrica)
            .ToListAsync();
        
        return _mapper.Map<List<LeilaoDTO>>(leiloes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LeilaoDTO>> GetLeilaoById(Guid id)
    {
        var leilao = await _context.Leiloes
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (leilao == null) 
        {
            return NotFound();
        }

        return _mapper.Map<LeilaoDTO>(leilao);
    }
}

