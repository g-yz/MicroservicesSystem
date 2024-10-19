using ClienteAPI.Contracts;
using System.Net.Http.Json;

namespace ClienteApp.IntegrationTests;

public class ClienteAppntegrationTests
{
    private readonly HttpClient _client;
    public ClienteAppntegrationTests()
    {
        _client = new HttpClient { BaseAddress = new Uri("https://localhost:6011") };
    }

    private ClienteCreateRequest GenerateClient()
    {
        return new ClienteCreateRequest
        {
            Nombres = "Juan Perez",
            Direccion = "123 Main St",
            Telefono = "999-555-1234",
            Password = "password-seguro",
            Estado = true,
            Identificacion = "123456789",
            Edad = 30,
            TipoGeneroId = 1
        };
    }

    [Fact]
    public async Task CreateClient_ShouldReturn_ClientId()
    {
        // Arrange
        var newClient = GenerateClient();

        // Act
        var response = await _client.PostAsJsonAsync("/api/Clientes", newClient);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdClienteId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, createdClienteId);
    }

    [Fact]
    public async Task DeleteClient_ShouldReturn_Success()
    {
        // Arrange
        var newClient = GenerateClient();
        var postResponse = await _client.PostAsJsonAsync("/api/Clientes", newClient);
        var createdClienteId = await postResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/clientes/{createdClienteId}");

        // Assert
        deleteResponse.EnsureSuccessStatusCode();
        var status = await deleteResponse.Content.ReadFromJsonAsync<bool>();
        Assert.True(status);
    }

    [Fact]
    public async Task GetClients_ShouldReturn_ListOfClients()
    {
        // Arrange
        var newClient = GenerateClient();
        await _client.PostAsJsonAsync("/api/Clientes", newClient);

        // Act
        var getResponse = await _client.GetAsync($"/api/clientes");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var listOfClients = await getResponse.Content.ReadFromJsonAsync<IEnumerable<ClienteGetResponse>>();
        Assert.NotNull(listOfClients);
        Assert.NotEmpty(listOfClients);
        Assert.True(listOfClients.Any());
    }

    [Fact]
    public async Task GetClienteById_ShouldReturn_Cliente()
    {
        // Arrange
        var newClient = GenerateClient();
        var postResponse = await _client.PostAsJsonAsync("/api/Clientes", newClient);
        var createdClienteId = await postResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var getResponse = await _client.GetAsync($"/api/clientes/{createdClienteId}");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var retrievedCliente = await getResponse.Content.ReadFromJsonAsync<ClienteGetResponse>();

        Assert.NotNull(retrievedCliente);
        Assert.Equal(createdClienteId, retrievedCliente.Id);
        Assert.Equal("Juan Perez", retrievedCliente.Nombres);
    }

    [Fact]
    public async Task UpdateClient_ShouldReturn_True()
    {
        // Arrange
        var newClient = GenerateClient();
        var postResponse = await _client.PostAsJsonAsync("/api/clientes", newClient);
        var createdClienteId = await postResponse.Content.ReadFromJsonAsync<Guid>();
        newClient.Nombres = "Modified";

        // Act
        var patchResponse = await _client.PatchAsJsonAsync($"/api/clientes/{createdClienteId}", newClient);

        // Assert
        patchResponse.EnsureSuccessStatusCode();
        var status = await patchResponse.Content.ReadFromJsonAsync<bool>();
        Assert.True(status);
    }
}
