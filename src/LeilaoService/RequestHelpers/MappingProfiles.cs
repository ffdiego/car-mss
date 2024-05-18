using AutoMapper;

namespace LeilaoService;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Leilao, LeilaoDTO>().IncludeMembers(x => x.Item);
        CreateMap<Item, LeilaoDTO>();
        CreateMap<NovoLeilaoDTO, Leilao>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s));
        CreateMap<NovoLeilaoDTO, Item>();
    }
}
