﻿using System;
using System.Collections.Generic;

namespace CuentaAPI.Models;

public partial class TiposMovimiento
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
