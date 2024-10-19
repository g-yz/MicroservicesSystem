namespace ClienteAPI.Contracts;

public class ClienteUpdateRequest
{
    public string? Nombres { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Password { get; set; }

    public bool? Estado { get; set; }

    public string? Identificacion { get; set; }

    public byte? Edad { get; set; }

    public int? TipoGeneroId { get; set; }
}
