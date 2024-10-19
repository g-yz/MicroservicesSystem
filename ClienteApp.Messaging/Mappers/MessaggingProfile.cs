using SystemApp.Shared.Messages;
using AutoMapper;
using ClienteApp.Data.Models;

namespace ClienteApp.Services.Mappers;

public class MessaggingProfile : Profile
{
    public MessaggingProfile()
    {
        CreateMap<Cliente, ClienteCreatedEvent>();
        CreateMap<Cliente, ClienteUpdatedEvent>();
    }
}
