using Shared.Messages;
using AutoMapper;
using ClientApp.Data.Models;

namespace ClientApp.Messaging.Mappers;

public class MessaggingProfile : Profile
{
    public MessaggingProfile()
    {
        CreateMap<Client, ClientCreatedEvent>();
        CreateMap<Client, ClientUpdatedEvent>();
    }
}
