using SystemApp.Shared.Messages;
using AutoMapper;
using CuentaApp.Data.Models;
using CuentaApp.Services.Contracts;

namespace CuentaApp.Services.Mappers;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<CuentaCreateRequest, Cuenta>();
        CreateMap<CuentaUpdateRequest, Cuenta>();
        CreateMap<Cuenta, CuentaGetResponse>()
            .ForMember(dest => dest.TipoCuenta, opts => opts.MapFrom(src => src.TipoCuenta.Descripcion));

        CreateMap<MovimientoAddRequest, Movimiento>()
            .ForMember(dest => dest.TipoMovimientoId, opts => opts.MapFrom(src => src.Valor >= 0 ? 1 : 2));
        CreateMap<Movimiento, ReporteMovimientosGetResponse>()
            .ForMember(dest => dest.Fecha, opts => opts.MapFrom(src => src.Fecha.ToString("d")))
            .ForMember(dest => dest.Nombre, opts => opts.MapFrom(src => src.Cuenta.Cliente.Nombres))
            .ForMember(dest => dest.NumeroCuenta, opts => opts.MapFrom(src => src.Cuenta.NumeroCuenta))
            .ForMember(dest => dest.SaldoInicial, opts => opts.MapFrom(src => src.Saldo - src.Valor))
            .ForMember(dest => dest.Movimiento, opts => opts.MapFrom(src => src.Valor))
            .ForMember(dest => dest.SaldoDisponible, opts => opts.MapFrom(src => src.Saldo))
            .ForMember(dest => dest.Estado, opts => opts.MapFrom(src => src.Cuenta.Estado));
        CreateMap<Movimiento, MovimientoGetResponse>()
            .ForMember(dest => dest.Fecha, opts => opts.MapFrom(src => src.Fecha.ToString("dd/MM/yyyy HH:mm")))
            .ForMember(dest => dest.NumeroCuenta, opts => opts.MapFrom(src => src.Cuenta.NumeroCuenta))
            .ForMember(dest => dest.Tipo, opts => opts.MapFrom(src => src.Cuenta.TipoCuenta.Descripcion))
            .ForMember(dest => dest.SaldoInicial, opts => opts.MapFrom(src => src.Saldo))
            .ForMember(dest => dest.Movimiento, opts => opts.MapFrom(src => $"{src.TipoMovimiento.Descripcion} de {src.Valor}"))
            .ForMember(dest => dest.Estado, opts => opts.MapFrom(src => src.Cuenta.Estado));

        CreateMap<ClienteCreatedEvent, Cliente>();
        CreateMap<ClienteUpdatedEvent, Cliente>();
    }
}
