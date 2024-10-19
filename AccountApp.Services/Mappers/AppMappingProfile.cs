using Shared.Messages;
using AutoMapper;
using AccountApp.Data.Models;
using AccountApp.Services.Contracts;

namespace AccountApp.Services.Mappers;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<AccountCreateRequest, Account>();
        CreateMap<AccountUpdateRequest, Account>();
        CreateMap<Account, AccountGetResponse>()
            .ForMember(dest => dest.TypeAccount, opts => opts.MapFrom(src => src.TypeAccount.Description));

        CreateMap<MovementAddRequest, Movement>()
            .ForMember(dest => dest.TypeMovementId, opts => opts.MapFrom(src => src.Value >= 0 ? 1 : 2));
        CreateMap<Movement, ReportOfMovementsGetResponse>()
            .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString("d")))
            .ForMember(dest => dest.FullName, opts => opts.MapFrom(src => src.Account.Client.FullName))
            .ForMember(dest => dest.AccountNumber, opts => opts.MapFrom(src => src.Account.AccountNumber))
            .ForMember(dest => dest.OpeningBalance, opts => opts.MapFrom(src => src.Balance - src.Value))
            .ForMember(dest => dest.Movement, opts => opts.MapFrom(src => src.Value))
            .ForMember(dest => dest.AvailableBalance, opts => opts.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Account.Status));
        CreateMap<Movement, MovementGetResponse>()
            .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToString("dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.AccountNumber, opts => opts.MapFrom(src => src.Account.AccountNumber))
            .ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Account.TypeAccount.Description))
            .ForMember(dest => dest.OpeningBalance, opts => opts.MapFrom(src => src.Balance))
            .ForMember(dest => dest.Movement, opts => opts.MapFrom(src => $"{src.TypeMovement.Description} de {src.Value}"))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Account.Status));

        CreateMap<ClientCreatedEvent, Client>();
        CreateMap<ClientUpdatedEvent, Client>();
    }
}
