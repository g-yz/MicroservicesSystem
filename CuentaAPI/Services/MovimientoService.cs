using AutoMapper;
using CuentaAPI.Contracts;
using CuentaAPI.Models;
using CuentaAPI.Repositories;

namespace CuentaAPI.Services;

public class MovimientoService : IMovimientoService
{
    private readonly IMovimientoRepository _movimientoRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMapper _mapper;
    public MovimientoService(IMovimientoRepository movimientoRepository, ICuentaRepository cuentaRepository, IMapper mapper)
    {
        _movimientoRepository = movimientoRepository;
        _cuentaRepository = cuentaRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateAsync(MovimientoAddRequest movimientoAddRequest)
    {
        var cuenta = await _cuentaRepository.GetAsync(movimientoAddRequest.CuentaId);
        if(cuenta == null) return Guid.Empty;
        var movimiento = _mapper.Map<Movimiento>(movimientoAddRequest);
        var movimientos = await _movimientoRepository.GetByCuentaAsync(movimientoAddRequest.CuentaId);
        var saldo = movimientos.Any() ? movimientos.OrderBy(x => x.Fecha).Last().Saldo : cuenta.SaldoInicial;
        movimiento.Saldo = saldo + movimiento.Valor;
        if (movimiento.Saldo < 0) throw new ArgumentException("Saldo no disponible.");
        return await _movimientoRepository.CreateAsync(movimiento);
    }

    public async Task<IEnumerable<ReporteMovimientosGetResponse>> GetReporteAsync(MovimientoReporteFilter filters)
    {
        var movimientos = await _movimientoRepository.GetReporteAsync(filters);
        return _mapper.Map<IEnumerable<ReporteMovimientosGetResponse>>(movimientos);
    }
}
