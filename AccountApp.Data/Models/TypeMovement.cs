using System;
using System.Collections.Generic;

namespace AccountApp.Data.Models;

public partial class TypeMovement
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
}
