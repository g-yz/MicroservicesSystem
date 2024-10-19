namespace SystemApp.Shared.Messages;

public class ClienteCreatedEvent
{
    public Guid Id { get; set; }
    public required string Nombres { get; set; }
    public bool Estado { get; set; }
}
