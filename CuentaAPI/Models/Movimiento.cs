using System;
using System.Collections.Generic;

namespace CuentaAPI.Models;

public partial class Movimiento
{
    public Guid Id { get; set; }

    public decimal Valor { get; set; }

    public decimal Saldo { get; set; }

    public DateTime Fecha { get; set; }

    public Guid CuentaId { get; set; }

    public virtual Cuenta Cuenta { get; set; } = null!;
}
