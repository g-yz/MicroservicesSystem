using System;
using System.Collections.Generic;

namespace AccountApp.Data.Models;

public partial class TypeAccount
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
