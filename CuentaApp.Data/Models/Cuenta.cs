using System;
using System.Collections.Generic;

namespace CuentaApp.Data.Models;

public partial class Cuenta
{
    public Guid Id { get; set; }

    public string NumeroCuenta { get; set; } = null!;

    public decimal SaldoInicial { get; set; }

    public int TipoCuentaId { get; set; }

    public bool Estado { get; set; }

    public Guid ClienteId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual TiposCuenta TipoCuenta { get; set; } = null!;
}
