using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice1;

public partial class TrainingFormat
{
    public int Id { get; set; }
    [Display(Name="Название")]
    public string Name { get; set; } = null!;
    [Display(Name="Лимит посетителей")]
    public int MaxClients { get; set; }

    public virtual ICollection<Tariff> Tariffs { get; set; } = new List<Tariff>();

    public virtual ICollection<Training> Training { get; set; } = new List<Training>();
}
