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

    [HttpPost]
    public async Task<ActionResult<LeilaoDTO>> CriaLeilao(NovoLeilaoDTO novoLeilaoDTO) 
    {
        var leilao = _mapper.Map<Leilao>(novoLeilaoDTO);
        
        // TODO: adicionar usuário atual como vendedor
        leilao.Vendedor = "UsuarioHardcoded";

        _context.Leiloes.Add(leilao);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) 
        {
            return BadRequest("Não foi possível adicionar Leilao no DB");
        }

        return CreatedAtAction(nameof(GetLeilaoById), new {leilao.Id}, _mapper.Map<LeilaoDTO>(leilao));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLeilao(Guid id, UpdateLeilaoDTO updateLeilaoDTO)
    {
        var leilao = await _context.Leiloes
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (leilao == null)
        {
            return NotFound();
        }

        // TODO: check seller == username

        leilao.Item.Fabrica = updateLeilaoDTO.Fabrica ?? leilao.Item.Fabrica;
        leilao.Item.Modelo = updateLeilaoDTO.Modelo ?? leilao.Item.Modelo;
        leilao.Item.Cor = updateLeilaoDTO.Cor ?? leilao.Item.Cor;
        leilao.Item.Quilometragem = updateLeilaoDTO.Quilometragem ?? leilao.Item.Quilometragem;
        leilao.Item.Ano = updateLeilaoDTO.Ano ?? leilao.Item.Ano;

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) 
        {
            return BadRequest("Problema saving changes");
        }

        return Ok();
    }
}

