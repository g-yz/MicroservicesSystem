using System;
using System.Collections.Generic;

namespace AccountApp.Data.Models;

public partial class Movement
{
    public Guid Id { get; set; }

    public decimal Value { get; set; }

    public decimal Balance { get; set; }

    public DateTime Date { get; set; }

    public int TypeMovementId { get; set; }

    public Guid AccountId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual TypeMovement TypeMovement { get; set; } = null!;
}
