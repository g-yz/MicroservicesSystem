using AutoMapper;
using CuentaAPI.Contracts;
using CuentaAPI.Models;

namespace CuentaAPI;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<CuentaCreateRequest, Cuenta>();
        CreateMap<CuentaUpdateRequest, Cuenta>();
        CreateMap<Cuenta, CuentaGetResponse>();

        CreateMap<MovimientoAddRequest, Movimiento>();
        CreateMap<Movimiento, ReporteMovimientosGetResponse>()
            .ForMember(dest => dest.Nombre, opts => opts.MapFrom(src => "TO DO"))
            .ForMember(dest => dest.NumeroCuenta, opts => opts.MapFrom(src => src.Cuenta.NumeroCuenta))
            .ForMember(dest => dest.SaldoInicial, opts => opts.MapFrom(src => src.Saldo - src.Valor))
            .ForMember(dest => dest.Movimiento, opts => opts.MapFrom(src => src.Valor))
            .ForMember(dest => dest.SaldoDisponible, opts => opts.MapFrom(src => src.Saldo))
            .ForMember(dest => dest.Estado, opts => opts.MapFrom(src => src.Cuenta.Estado));
    }
}
