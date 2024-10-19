using ClientApp.Services.Contracts;
using System.Net.Http.Json;

namespace ClientApp.IntegrationTests;

public class ClientAppntegrationTests
{
    private readonly HttpClient _client;
    public ClientAppntegrationTests()
    {
        _client = new HttpClient { BaseAddress = new Uri("https://localhost:6011") };
    }

    private ClientCreateRequest GenerateClient()
    {
        return new ClientCreateRequest
        {
            FullName = "Juan Perez",
            Address = "123 Main St",
            Phone = "999-555-1234",
            Email = "email@mail.com",
            Status = true,
            Document = "123456789",
            Years = 30,
            TypeGenderId = 1
        };
    }

    [Fact]
    public async Task CreateClient_ShouldReturn_ClientId()
    {
        // Arrange
        var newClient = GenerateClient();

        // Act
        var response = await _client.PostAsJsonAsync("/api/Clients", newClient);

        // Assert
        response.EnsureSuccessStatusCode();
        var createdClientId = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.NotEqual(Guid.Empty, createdClientId);
    }

    [Fact]
    public async Task DeleteClient_ShouldReturn_Success()
    {
        // Arrange
        var newClient = GenerateClient();
        var postResponse = await _client.PostAsJsonAsync("/api/Clients", newClient);
        var createdClientId = await postResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var deleteResponse = await _client.DeleteAsync($"/api/clients/{createdClientId}");

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
        await _client.PostAsJsonAsync("/api/Clients", newClient);

        // Act
        var getResponse = await _client.GetAsync($"/api/clients");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var listOfClients = await getResponse.Content.ReadFromJsonAsync<IEnumerable<ClientGetResponse>>();
        Assert.NotNull(listOfClients);
        Assert.NotEmpty(listOfClients);
        Assert.True(listOfClients.Any());
    }

    [Fact]
    public async Task GetClienteById_ShouldReturn_Cliente()
    {
        // Arrange
        var newClient = GenerateClient();
        var postResponse = await _client.PostAsJsonAsync("/api/Clients", newClient);
        var createdClientId = await postResponse.Content.ReadFromJsonAsync<Guid>();

        // Act
        var getResponse = await _client.GetAsync($"/api/clients/{createdClientId}");

        // Assert
        getResponse.EnsureSuccessStatusCode();
        var retrievedCliente = await getResponse.Content.ReadFromJsonAsync<ClientGetResponse>();

        Assert.NotNull(retrievedCliente);
        Assert.Equal(createdClientId, retrievedCliente.Id);
        Assert.Equal("Juan Perez", retrievedCliente.FullName);
    }

    [Fact]
    public async Task UpdateClient_ShouldReturn_True()
    {
        // Arrange
        var newClient = GenerateClient();
        var postResponse = await _client.PostAsJsonAsync("/api/clients", newClient);
        var createdClientId = await postResponse.Content.ReadFromJsonAsync<Guid>();
        newClient.FullName = "Modified";

        // Act
        var patchResponse = await _client.PatchAsJsonAsync($"/api/clients/{createdClientId}", newClient);

        // Assert
        patchResponse.EnsureSuccessStatusCode();
        var status = await patchResponse.Content.ReadFromJsonAsync<bool>();
        Assert.True(status);
    }
}
