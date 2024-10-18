using System;
using System.Collections.Generic;

namespace ClienteAPI.Models;

public partial class Persona
{
    public Guid Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Identificacion { get; set; }

    public byte? Edad { get; set; }

    public int? GeneroId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Genero? Genero { get; set; }
}
