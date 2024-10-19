using System;
using System.Collections.Generic;

namespace ClientApp.Data.Models;

public partial class Person
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Document { get; set; }

    public byte? Years { get; set; }

    public int? TypeGenderId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual TypeGender? TypeGender { get; set; }
}
