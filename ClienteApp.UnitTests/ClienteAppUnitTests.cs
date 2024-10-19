using AutoMapper;
using ClienteApp.Data.Models;
using ClienteApp.Data.Repositories;
using ClienteApp.Messaging.Producers;
using ClienteApp.Services.Contracts;
using ClienteApp.Services.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace ClienteApp.UnitTests;

public class ClienteAppUnitTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly Mock<IClienteEventPublisher> _clientEventPublisherMock;
    private readonly Mock<IValidator<ClienteCreateRequest>> _clientCreateValidatorMock;
    private readonly Mock<IValidator<ClienteUpdateRequest>> _clientUpdateValidatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ClienteService _clienteService;

    public ClienteAppUnitTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _clientEventPublisherMock = new Mock<IClienteEventPublisher>();
        _clientCreateValidatorMock = new Mock<IValidator<ClienteCreateRequest>>();
        _clientUpdateValidatorMock = new Mock<IValidator<ClienteUpdateRequest>>();
        _mapperMock = new Mock<IMapper>();
        _clienteService = new ClienteService(_clienteRepositoryMock.Object,
            _mapperMock.Object,
            _clientCreateValidatorMock.Object,
            _clientUpdateValidatorMock.Object,
            _clientEventPublisherMock.Object);
    }

    [Fact]
    public async Task CreateClient_ShouldReturn_ClientId()
    {
        // Arrange
        var clienteRequest = new ClienteCreateRequest
        {
            Nombres = "John Doe",
            Direccion = "123 Main St",
            Telefono = "123-456-7890",
            Password = "password123"
        };

        var cliente = new Cliente { Id = Guid.NewGuid() };
        _clientCreateValidatorMock.Setup(v => v.Validate(clienteRequest)).Returns(new ValidationResult());
        _mapperMock.Setup(m => m.Map<Cliente>(clienteRequest)).Returns(cliente);
        _clienteRepositoryMock.Setup(repo => repo.CreateAsync(cliente)).ReturnsAsync(cliente);

        // Act
        var result = await _clienteService.CreateAsync(clienteRequest);

        // Assert
        Assert.Equal(cliente.Id, result);
        _clientCreateValidatorMock.Verify(v => v.Validate(clienteRequest), Times.Once);
        _mapperMock.Verify(m => m.Map<Cliente>(clienteRequest), Times.Once);
        _clienteRepositoryMock.Verify(repo => repo.CreateAsync(cliente), Times.Once);
        _clientEventPublisherMock.Verify(pub => pub.PublicarClienteCreado(cliente), Times.Once);
    }

    [Fact]
    public async Task GetClients_ShouldReturn_ListOfClients()
    {
        // Arrange
        var clientes = new List<Cliente>
        {
            new Cliente
            {
                Id = Guid.NewGuid(),
                Nombres = "John Doe",
                Direccion = "123 Main St",
                Telefono = "123-456-7890"
            },
            new Cliente
            {
                Id = Guid.NewGuid(),
                Nombres = "Jane Doe",
                Direccion = "456 Main St",
                Telefono = "987-654-3210"
            }
        };

        var clienteResponses = new List<ClienteGetResponse>
        {
            new ClienteGetResponse
            {
                Id = clientes[0].Id,
                Nombres = "John Doe",
                Direccion = "123 Main St",
                Telefono = "123-456-7890",
                Password = "password",
                Genero = "1",
            },
            new ClienteGetResponse
            {
                Id = clientes[1].Id,
                Nombres = "Jane Doe",
                Direccion = "456 Main St",
                Telefono = "987-654-3210",
                Password = "password",
                Genero = "1",
            }
        };

        _clienteRepositoryMock.Setup(repo => repo.ListAsync()).ReturnsAsync(clientes);
        _mapperMock.Setup(m => m.Map<List<ClienteGetResponse>>(clientes)).Returns(clienteResponses);

        // Act
        var result = await _clienteService.ListAsync();

        // Assert
        Assert.Equal(clienteResponses, result);
        _clienteRepositoryMock.Verify(repo => repo.ListAsync(), Times.Once);
        _mapperMock.Verify(m => m.Map<List<ClienteGetResponse>>(clientes), Times.Once);
    }

    [Fact]
    public async Task DeleteClient_ShouldReturn_Success()
    {
        // Arrange
        var clientId = Guid.NewGuid();
        _clienteRepositoryMock.Setup(repo => repo.DeleteAsync(clientId)).ReturnsAsync(true);

        // Act
        var result = await _clienteService.DeleteAsync(clientId);

        // Assert
        Assert.True(result);
        _clienteRepositoryMock.Verify(repo => repo.DeleteAsync(clientId), Times.Once);
        _clientEventPublisherMock.Verify(publisher => publisher.PublicarClienteEliminado(clientId), Times.Once);
    }

    [Fact]
    public async Task GetClienteById_ShouldReturn_Cliente()
    {
        // Arrange
        var clienteId = Guid.NewGuid();
        var cliente = new Cliente
        {
            Id = clienteId,
            Nombres = "John Doe",
            Direccion = "123 Main St",
            Telefono = "123-456-7890"
        };

        var clienteResponse = new ClienteGetResponse
        {
            Id = clienteId,
            Nombres = "John Doe",
            Direccion = "123 Main St",
            Telefono = "123-456-7890",
            Password = "password",
            Genero = "1",
        };

        _clienteRepositoryMock.Setup(repo => repo.GetAsync(clienteId)).ReturnsAsync(cliente);
        _mapperMock.Setup(m => m.Map<ClienteGetResponse>(cliente)).Returns(clienteResponse);

        // Act
        var result = await _clienteService.GetAsync(clienteId);

        // Assert
        Assert.Equal(clienteResponse, result);
        _clienteRepositoryMock.Verify(repo => repo.GetAsync(clienteId), Times.Once);
        _mapperMock.Verify(m => m.Map<ClienteGetResponse>(cliente), Times.Once);
    }

    [Fact]
    public async Task UpdateClient_ShouldReturn_True()
    {
        // Arrange
        var id = Guid.NewGuid();
        var clienteUpdateRequest = new ClienteUpdateRequest { Nombres = "Updated Name" };
        var cliente = new Cliente { Id = id, Nombres = "Original Name" };

        _clientUpdateValidatorMock.Setup(v => v.Validate(clienteUpdateRequest)).Returns(new ValidationResult());
        _clienteRepositoryMock.Setup(repo => repo.GetAsync(id)).ReturnsAsync(cliente);
        _clienteRepositoryMock.Setup(repo => repo.UpdateAsync(id, cliente)).ReturnsAsync(true);

        // Act
        var result = await _clienteService.UpdateAsync(id, clienteUpdateRequest);

        // Assert
        Assert.True(result);
        _clientUpdateValidatorMock.Verify(v => v.Validate(clienteUpdateRequest), Times.Once);
        _clienteRepositoryMock.Verify(repo => repo.GetAsync(id), Times.Once);
        _clienteRepositoryMock.Verify(repo => repo.UpdateAsync(id, cliente), Times.Once);
        _clientEventPublisherMock.Verify(pub => pub.PublicarClienteModificado(cliente), Times.Once);
    }
}