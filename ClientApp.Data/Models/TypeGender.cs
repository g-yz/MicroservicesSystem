using System;
using System.Collections.Generic;

namespace ClientApp.Data.Models;

public partial class TypeGender
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
