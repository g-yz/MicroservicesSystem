using AutoMapper;
using AccountApp.Data.Models;
using AccountApp.Data.Repositories;
using AccountApp.Services.Contracts;
using FluentValidation;
using Shared.Exceptions;

namespace AccountApp.Services.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<AccountCreateRequest> _createValidator;
    private readonly IValidator<AccountUpdateRequest> _updateValidator;

    public AccountService(IAccountRepository accountRepository,
        IClientRepository clientRepository,
        IMapper mapper,
        IValidator<AccountCreateRequest> createValidator,
        IValidator<AccountUpdateRequest> updateValidator)
    {
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }
    public async Task<Guid> CreateAsync(AccountCreateRequest accountCreateRequest)
    {
        var result = _createValidator.Validate(accountCreateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var clientExiste = await _clientRepository.GetAsync(accountCreateRequest.ClientId);
        if (clientExiste == null) throw new NotFoundException($"The client {accountCreateRequest.ClientId} does not exist.");

        var account = _mapper.Map<Account>(accountCreateRequest);
        return await _accountRepository.CreateAsync(account);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var result = await _accountRepository.DeleteAsync(id);
        if (!result) throw new NotFoundException($"The account {id} does not exist.");

        return result;
    }

    public async Task<int> DesactivarAccountsByClienteAsync(Guid id)
    {
        return await _accountRepository.DeleteByClienteAsync(id);
    }

    public async Task<AccountGetResponse> GetAsync(Guid id)
    {
        var account = await _accountRepository.GetAsync(id);
        if (account == null) throw new NotFoundException($"The account {id} does not exist.");
        return _mapper.Map<AccountGetResponse>(account);
    }

    public async Task<IEnumerable<AccountGetResponse>> ListAsync()
    {
        return _mapper.Map<List<AccountGetResponse>>(await _accountRepository.ListAsync());
    }

    public async Task<bool> UpdateAsync(Guid id, AccountUpdateRequest accountUpdateRequest)
    {
        var result = _updateValidator.Validate(accountUpdateRequest);
        if (result.Errors.Any()) throw new ValidationException(result.Errors);

        var clientExiste = await _clientRepository.GetAsync(accountUpdateRequest.ClientId);
        if (clientExiste == null) throw new NotFoundException($"The client {accountUpdateRequest.ClientId} does not exist.");

        var account = _mapper.Map<Account>(accountUpdateRequest);
        var status = await _accountRepository.UpdateAsync(id, account);
        if (!status) throw new NotFoundException($"The account {id} does not exist.");

        return status;
    }
}
