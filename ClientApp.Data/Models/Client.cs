using System;
using System.Collections.Generic;

namespace ClientApp.Data.Models;

public partial class Client: Person
{
    public string Email { get; set; } = null!;

    public bool Status { get; set; }

    public virtual Person IdNavigation { get; set; } = null!;
}
