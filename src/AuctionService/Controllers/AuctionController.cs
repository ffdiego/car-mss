using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService;

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
    public async Task<ActionResult<List<AuctionDTO>>> GetAllAuctions(string date)
    {
        var query = _context.Auctions.OrderBy(x => x.Item.Make).AsQueryable();

        if(!string.IsNullOrWhiteSpace(date)) 
        {
            query = query.Where(x => x.UpdateAt
                .CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }
        
        return await query
            .ProjectTo<AuctionDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDTO>> GetAuctionByID(Guid id)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (auction == null) 
        {
            return NotFound();
        }

        return _mapper.Map<AuctionDTO>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDTO>> NewAuction(CreateAuctionDTO newAuctionDTO) 
    {
        var auction = _mapper.Map<Auction>(newAuctionDTO);
        
        // TODO: adicionar usuário atual como Seller
        auction.Seller = "UsuarioHardcoded";

        _context.Auctions.Add(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) 
        {
            return BadRequest("Não foi possível adicionar Leilao no DB");
        }

        return CreatedAtAction(nameof(GetAuctionByID), new {auction.Id}, _mapper.Map<AuctionDTO>(auction));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuction(Guid id, UpdateActionDTO updateAuctionDTO)
    {
        var auction = await _context.Auctions
            .Include(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (auction == null)
        {
            return NotFound();
        }

        // TODO: check seller == username

        auction.Item.Make = updateAuctionDTO.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDTO.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDTO.Color ?? auction.Item.Color;
        auction.Item.Mileage = updateAuctionDTO.Mileage ?? auction.Item.Mileage;
        auction.Item.Year = updateAuctionDTO.Year ?? auction.Item.Year;

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) 
        {
            return BadRequest("Houve um erro persistir as mudanças no banco");
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuction(Guid id)
    {
        var auction = await _context.Auctions
            .FindAsync(id);
        
        if (auction == null)
        {
            return NotFound();
        }

        // TODO: check seller == username

        _context.Auctions.Remove(auction);

        var result = await _context.SaveChangesAsync() > 0;

        if (!result) 
        {
            return BadRequest("Houve um erro persistir as mudanças no banco");
        }

        return Ok();
    }
}

