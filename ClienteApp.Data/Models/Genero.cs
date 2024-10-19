using System;
using System.Collections.Generic;

namespace ClienteApp.Data.Models;

public partial class Genero
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Persona> Personas { get; set; } = new List<Persona>();
}
