namespace ClienteAPI.Contracts;

public class ClienteCreateRequest
{
    public required string Nombres { get; set; }

    public required string Direccion { get; set; }

    public required string Telefono { get; set; }

    public required string Password { get; set; }

    public bool Estado { get; set; }

    public string? Identificacion { get; set; }

    public byte? Edad { get; set; }

    public int? TipoGeneroId { get; set; }
}
