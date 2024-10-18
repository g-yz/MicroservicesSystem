namespace ClienteAPI.Contracts;

public class ClienteGetResponse
{
    public required string Nombres { get; set; }

    public required string Direccion { get; set; }

    public required string Telefono { get; set; }

    public required string Password { get; set; }

    public bool Estado { get; set; }
}
