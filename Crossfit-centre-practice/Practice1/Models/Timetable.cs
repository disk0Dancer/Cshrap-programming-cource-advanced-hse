using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice1;

public partial class Timetable
{
    public int Id { get; set; }
    [Display(Name="Начало")]
    public DateTime Start { get; set; }
    [Display(Name="Конец")]
    public DateTime End { get; set; }
    [Display(Name="Тренировка")]
    public int TrainingId { get; set; }
    [Display(Name="Тренер")]
    public int UserId { get; set; }

    public virtual Training Training { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
