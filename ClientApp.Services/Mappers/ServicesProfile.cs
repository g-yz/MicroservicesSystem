using AutoMapper;
using ClientApp.Services.Contracts;
using ClientApp.Data.Models;

namespace ClientApp.Services.Mappers;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<ClientCreateRequest, Client>();
        CreateMap<ClientUpdateRequest, Client>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Client, ClientGetResponse>()
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.TypeGender != null ? src.TypeGender.Description : "Desconocido"));
    }
}
