using AutoMapper;
using ClientApp.Data.Models;
using ClientApp.Data.Repositories;
using ClientApp.Messaging.Producers;
using ClientApp.Services.Contracts;
using ClientApp.Services.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ClientApp.UnitTests;

public class ClientAppUnitTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IClientEventPublisher> _clientEventPublisherMock;
    private readonly Mock<IValidator<ClientCreateRequest>> _clientCreateValidatorMock;
    private readonly Mock<IValidator<ClientUpdateRequest>> _clientUpdateValidatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ClientService _clientService;

    public ClientAppUnitTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _clientEventPublisherMock = new Mock<IClientEventPublisher>();
        _clientCreateValidatorMock = new Mock<IValidator<ClientCreateRequest>>();
        _clientUpdateValidatorMock = new Mock<IValidator<ClientUpdateRequest>>();
        _mapperMock = new Mock<IMapper>();
        _clientService = new ClientService(_clientRepositoryMock.Object,
            _mapperMock.Object,
            _clientCreateValidatorMock.Object,
            _clientUpdateValidatorMock.Object,
            _clientEventPublisherMock.Object);
    }

    [Fact]
    public async Task CreateClient_ShouldReturn_ClientId()
    {
        // Arrange
        var clientRequest = new ClientCreateRequest
        {
            FullName = "John Doe",
            Address = "123 Main St",
            Phone = "123-456-7890",
            Password = "password123"
        };

        var client = new Client { Id = Guid.NewGuid() };
        _clientCreateValidatorMock.Setup(v => v.Validate(clientRequest)).Returns(new ValidationResult());
        _mapperMock.Setup(m => m.Map<Client>(clientRequest)).Returns(client);
        _clientRepositoryMock.Setup(repo => repo.CreateAsync(client)).ReturnsAsync(client);

        // Act
        var result = await _clientService.CreateAsync(clientRequest);

        // Assert
        Assert.Equal(client.Id, result);
        _clientCreateValidatorMock.Verify(v => v.Validate(clientRequest), Times.Once);
        _mapperMock.Verify(m => m.Map<Client>(clientRequest), Times.Once);
        _clientRepositoryMock.Verify(repo => repo.CreateAsync(client), Times.Once);
        _clientEventPublisherMock.Verify(pub => pub.PublicarClienteCreado(client), Times.Once);
    }

    [Fact]
    public async Task GetClients_ShouldReturn_ListOfClients()
    {
        // Arrange
        var clients = new List<Client>
        {
            new Client
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                Address = "123 Main St",
                Phone = "123-456-7890"
            },
            new Client
            {
                Id = Guid.NewGuid(),
                FullName = "Jane Doe",
                Address = "456 Main St",
                Phone = "987-654-3210"
            }
        };

        var clientResponses = new List<ClientGetResponse>
        {
            new ClientGetResponse
            {
                Id = clients[0].Id,
                FullName = "John Doe",
                Address = "123 Main St",
                Phone = "123-456-7890",
                Password = "password",
                Gender = "1",
            },
            new ClientGetResponse
            {
                Id = clients[1].Id,
                FullName = "Jane Doe",
                Address = "456 Main St",
                Phone = "987-654-3210",
                Password = "password",
                Gender = "1",
            }
        };

        _clientRepositoryMock.Setup(repo => repo.ListAsync()).ReturnsAsync(clients);
        _mapperMock.Setup(m => m.Map<List<ClientGetResponse>>(clients)).Returns(clientResponses);

        // Act
        var result = await _clientService.ListAsync();

        // Assert
        Assert.Equal(clientResponses, result);
        _clientRepositoryMock.Verify(repo => repo.ListAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<List<ClientGetResponse>>(clients), Times.Once);
    }

    [Fact]
    public async Task DeleteClient_ShouldReturn_Success()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        _clientRepositoryMock.Setup(repo => repo.DeleteAsync(clientId)).ReturnsAsync(true);

        // Act
        var result = await _clientService.DeleteAsync(clientId);

        // Assert
        Assert.True(result);
        _clientRepositoryMock.Verify(repo => repo.DeleteAsync(clientId), Times.Once);
        _clientEventPublisherMock.Verify(publisher => publisher.PublicarClienteEliminado(clientId), Times.Once);
    }

    [Fact]
    public async Task GetClienteById_ShouldReturn_Cliente()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        var client = new Client
        {
            Id = clientId,
            FullName = "John Doe",
            Address = "123 Main St",
            Phone = "123-456-7890"
        };

        var clientResponse = new ClientGetResponse
        {
            Id = clientId,
            FullName = "John Doe",
            Address = "123 Main St",
            Phone = "123-456-7890",
            Password = "password",
            Gender = "1",
        };

        _clientRepositoryMock.Setup(repo => repo.GetAsync(clientId)).ReturnsAsync(client);
        _mapperMock.Setup(m => m.Map<ClientGetResponse>(client)).Returns(clientResponse);

        // Act
        var result = await _clientService.GetAsync(clientId);

        // Assert
        Assert.Equal(clientResponse, result);
        _clientRepositoryMock.Verify(repo => repo.GetAsync(clientId), Times.Once);
        _mapperMock.Verify(m => m.Map<ClientGetResponse>(client), Times.Once);
    }

    [Fact]
    public async Task UpdateClient_ShouldReturn_True()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clientUpdateRequest = new ClientUpdateRequest { FullName = "Updated Name" };
        var client = new Client { Id = id, FullName = "Original Name" };

        _clientUpdateValidatorMock.Setup(v => v.Validate(clientUpdateRequest)).Returns(new ValidationResult());
        _clientRepositoryMock.Setup(repo => repo.GetAsync(id)).ReturnsAsync(client);
        _clientRepositoryMock.Setup(repo => repo.UpdateAsync(id, client)).ReturnsAsync(true);

        // Act
        var result = await _clientService.UpdateAsync(id, clientUpdateRequest);

        // Assert
        Assert.True(result);
        _clientUpdateValidatorMock.Verify(v => v.Validate(clientUpdateRequest), Times.Once);
        _clientRepositoryMock.Verify(repo => repo.GetAsync(id), Times.Once);
        _clientRepositoryMock.Verify(repo => repo.UpdateAsync(id, client), Times.Once);
        _clientEventPublisherMock.Verify(pub => pub.PublicarClienteModificado(client), Times.Once);
    }
}