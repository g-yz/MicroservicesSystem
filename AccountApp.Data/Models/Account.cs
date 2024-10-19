using System;
using System.Collections.Generic;

namespace AccountApp.Data.Models;

public partial class Account
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = null!;

    public decimal OpeningBalance { get; set; }

    public int TypeAccountId { get; set; }

    public bool Status { get; set; }

    public Guid ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();

    public virtual TypeAccount TypeAccount { get; set; } = null!;
}
