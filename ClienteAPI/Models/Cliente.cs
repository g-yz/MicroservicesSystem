using System;
using System.Collections.Generic;

namespace ClienteAPI.Models;

public partial class Cliente : Persona
{
    public string Password { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual Persona Persona { get; set; } = null!;
}
