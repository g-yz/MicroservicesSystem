namespace Shared.Messages;

public class ClientUpdatedEvent
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public bool Status { get; set; }
}
