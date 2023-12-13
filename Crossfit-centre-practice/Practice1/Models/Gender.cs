using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice1;

public partial class Gender
{
    public Gender(){}
    public Gender(string _name)
    {
        Name = _name;
    }

    public int Id { get; set; }
    [Display(Name="Пол")]
    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public async Task CreateAsync(Gender p0)
    {
        new Gender();
    }
}
