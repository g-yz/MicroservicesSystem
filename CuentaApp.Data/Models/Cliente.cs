using System;
using System.Collections.Generic;

namespace CuentaApp.Data.Models;

public partial class Cliente
{
    public Guid Id { get; set; }

    public string Nombres { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
