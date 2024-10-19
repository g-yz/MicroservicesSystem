namespace ClientApp.Services.Contracts;

public class ClientCreateRequest
{
    public required string FullName { get; set; }

    public required string Address { get; set; }

    public required string Phone { get; set; }

    public required string Email { get; set; }

    public bool Status { get; set; } = true;

    public string? Document { get; set; }

    public byte? Years { get; set; }

    public int? TypeGenderId { get; set; }
}
