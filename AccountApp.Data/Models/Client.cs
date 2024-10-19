using System;
using System.Collections.Generic;

namespace AccountApp.Data.Models;

public partial class Client
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
