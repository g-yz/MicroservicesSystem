using System;
using System.Collections.Generic;

namespace CuentaAPI.Models;

public partial class TiposCuenta
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cuenta> Cuenta { get; set; } = new List<Cuenta>();
}
