using System;
using System.Collections.Generic;

namespace TodoAPI;

public partial class ItemCategory
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}
