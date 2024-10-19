using AutoMapper;
using AccountApp.Data.Models;
using AccountApp.Data.Repositories;
using AccountApp.Services.Contracts;
using FluentValidation;
using Shared.Exceptions;

namespace AccountApp.Services.Services;

public class MovementService : IMovementService
{
    private readonly IMovementRepository _movementRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<MovementAddRequest> _addValidator;
    private readonly IValidator<MovementReporteFilter> _filterValidator;
    public MovementService(IMovementRepository movementRepository,
        IAccountRepository accountRepository,
        IMapper mapper,
        IValidator<MovementAddRequest> addValidator,
        IValidator<MovementReporteFilter> filterValidator)
    {
        _movementRepository = movementRepository;
        _accountRepository = accountRepository;
        _mapper = mapper;
        _addValidator = addValidator;
        _filterValidator = filterValidator;
    }

    public async Task<Guid> CreateAsync(MovementAddRequest movementAddRequest)
    {
        var result = _addValidator.Validate(movementAddRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var account = await _accountRepository.GetAsync(movementAddRequest.AccountId);
        if (account == null) throw new NotFoundException($"The account {movementAddRequest.AccountId} does not exist.");

        var movement = _mapper.Map<Movement>(movementAddRequest);
        var movements = await _movementRepository.GetByAccountAsync(movementAddRequest.AccountId);
        var saldo = movements.Any() ? movements.OrderBy(x => x.Date).Last().Balance : account.OpeningBalance;
        movement.Balance = saldo + movement.Value;
        if (movement.Balance < 0) throw new ArgumentException($"Balance not available for the account {movementAddRequest.AccountId}.");

        return await _movementRepository.CreateAsync(movement);
    }

    public async Task<IEnumerable<ReportOfMovementsGetResponse>> GetReporteAsync(MovementReporteFilter filters)
    {
        var result = _filterValidator.Validate(filters);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var movements = await _movementRepository.GetReporteAsync(filters);
        return _mapper.Map<IEnumerable<ReportOfMovementsGetResponse>>(movements);
    }

    public async Task<IEnumerable<MovementGetResponse>> ListAsync()
    {
        var movements = await _movementRepository.ListAsync();
        return _mapper.Map<IEnumerable<MovementGetResponse>>(movements);
    }
}
