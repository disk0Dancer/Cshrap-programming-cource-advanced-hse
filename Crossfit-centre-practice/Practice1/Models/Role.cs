using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice1;

public partial class Role
{
    public Role(){}
    public Role(string _name)
    {
        Name = _name;
    }
    public int Id { get; set; }
    [Display(Name="Роль")]
    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public async Task CreateAsync(Role p0)
    {
        new Role();
    }
}
