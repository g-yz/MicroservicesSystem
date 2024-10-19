using System;
using System.Collections.Generic;

namespace ClienteAPI.Models;

public partial class TiposGenero
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
