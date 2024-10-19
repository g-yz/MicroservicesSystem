namespace ClientApp.Services.Contracts;

public class ClientUpdateRequest
{
    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public bool? Status { get; set; }

    public string? Document { get; set; }

    public byte? Years { get; set; }

    public int? TypeGenderId { get; set; }
}
