using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeilaoService;

[ApiController]
[Route("api/auctions")]
public class AuctionController: ControllerBase
{
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;

    public AuctionController(AuctionDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDTO>>> GetAllLeiloes()
    {
        var leiloes = await _context.Auctions
            .Include(x => x.Item)
            .OrderBy(x => x.Item.Make)
            .ToListAsync();
        
        return _mapper.Map<List<AuctionDTO>>(leiloes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDTO>> GetLeilaoById(Guid id)
    {
        var leilao = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (leilao == null) 
        {
            return NotFound();
        }

        return _mapper.Map<AuctionDTO>(leilao);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDTO>> CriaLeilao(CreateAuctionDTO novoLeilaoDTO) 
    {
        var leilao = _mapper.Map<Auction>(novoLeilaoDTO);
        
        // TODO: adicionar usuário atual como Seller
        leilao.Seller = "UsuarioHardcoded";

        _context.Auctions.Add(leilao);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) 
        {
            return BadRequest("Não foi possível adicionar Leilao no DB");
        }

        return CreatedAtAction(nameof(GetLeilaoById), new {leilao.Id}, _mapper.Map<AuctionDTO>(leilao));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateLeilao(Guid id, UpdateActionDTO updateLeilaoDTO)
    {
        var leilao = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (leilao == null)
        {
            return NotFound();
        }

        // TODO: check seller == username

        leilao.Item.Make = updateLeilaoDTO.Make ?? leilao.Item.Make;
        leilao.Item.Model = updateLeilaoDTO.Model ?? leilao.Item.Model;
        leilao.Item.Color = updateLeilaoDTO.Color ?? leilao.Item.Color;
        leilao.Item.Mileage = updateLeilaoDTO.Mileage ?? leilao.Item.Mileage;
        leilao.Item.Year = updateLeilaoDTO.Year ?? leilao.Item.Year;

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) 
        {
            return BadRequest("Problema saving changes");
        }

        return Ok();
    }
}

