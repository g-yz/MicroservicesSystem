using AutoMapper;
using CuentaAPI.Contracts;
using CuentaAPI.Models;
using CuentaAPI.Repositories;
using FluentValidation;

namespace CuentaAPI.Services;

public class MovimientoService : IMovimientoService
{
    private readonly IMovimientoRepository _movimientoRepository;
    private readonly ICuentaRepository _cuentaRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<MovimientoAddRequest> _addValidator;
    private readonly IValidator<MovimientoReporteFilter> _filterValidator;
    public MovimientoService(IMovimientoRepository movimientoRepository, 
        ICuentaRepository cuentaRepository, 
        IMapper mapper, 
        IValidator<MovimientoAddRequest> addValidator,
        IValidator<MovimientoReporteFilter> filterValidator)
    {
        _movimientoRepository = movimientoRepository;
        _cuentaRepository = cuentaRepository;
        _mapper = mapper;
        _addValidator = addValidator;
        _filterValidator = filterValidator;
    }

    public async Task<Guid> CreateAsync(MovimientoAddRequest movimientoAddRequest)
    {
        var result = _addValidator.Validate(movimientoAddRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var cuenta = await _cuentaRepository.GetAsync(movimientoAddRequest.CuentaId);
        if(cuenta == null) throw new NotFoundException($"La cuenta {movimientoAddRequest.CuentaId} no existe.");

        var movimiento = _mapper.Map<Movimiento>(movimientoAddRequest);
        var movimientos = await _movimientoRepository.GetByCuentaAsync(movimientoAddRequest.CuentaId);
        var saldo = movimientos.Any() ? movimientos.OrderBy(x => x.Fecha).Last().Saldo : cuenta.SaldoInicial;
        movimiento.Saldo = saldo + movimiento.Valor;
        if (movimiento.Saldo < 0) throw new ArgumentException($"Saldo no disponible para la cuenta {movimientoAddRequest.CuentaId}.");

        return await _movimientoRepository.CreateAsync(movimiento);
    }

    public async Task<IEnumerable<ReporteMovimientosGetResponse>> GetReporteAsync(MovimientoReporteFilter filters)
    {
        var result = _filterValidator.Validate(filters);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var movimientos = await _movimientoRepository.GetReporteAsync(filters);
        return _mapper.Map<IEnumerable<ReporteMovimientosGetResponse>>(movimientos);
    }

    public async Task<IEnumerable<MovimientoGetResponse>> ListAsync()
    {
        var movimientos = await _movimientoRepository.ListAsync();
        return _mapper.Map<IEnumerable<MovimientoGetResponse>>(movimientos);
    }
}
