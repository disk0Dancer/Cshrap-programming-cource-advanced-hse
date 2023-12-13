using System;
using System.Collections.Generic;

namespace TodoAPI;

public partial class TodoItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Iscomplete { get; set; }

    public int CategoryId { get; set; }

    public virtual ItemCategory? Category { get; set; } = null!;
}
