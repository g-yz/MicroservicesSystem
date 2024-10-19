using AutoMapper;
using ClienteApp.Services.Contracts;
using ClienteApp.Data.Models;

namespace ClienteApp.Services.Mappers;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<ClienteCreateRequest, Cliente>();
        CreateMap<ClienteUpdateRequest, Cliente>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Cliente, ClienteGetResponse>()
            .ForMember(dest => dest.Genero, opts => opts.MapFrom(src => src.TipoGenero != null ? src.TipoGenero.Descripcion : "Desconocido"));
    }
}
