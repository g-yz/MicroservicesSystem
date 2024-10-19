using AppShared.Messages;
using AutoMapper;
using ClienteAPI.Contracts;
using ClienteAPI.Models;

namespace ClienteAPI;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<ClienteCreateRequest, Cliente>();
        CreateMap<ClienteUpdateRequest, Cliente>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Cliente, ClienteGetResponse>()
            .ForMember(dest => dest.Genero, opts => opts.MapFrom(src => src.TipoGenero != null? src.TipoGenero.Descripcion : "Desconocido"));

        CreateMap<Cliente, ClienteCreatedEvent>();
        CreateMap<Cliente, ClienteUpdatedEvent>();
    }
}
