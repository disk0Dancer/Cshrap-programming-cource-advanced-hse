using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice1;

public partial class Training
{
    public int Id { get; set; }
    [Display(Name="Название")]
    public string Name { get; set; } = null!;
    [Display(Name="Описание")]
    public string Description { get; set; } = null!;
    [Display(Name="Формат тренировки")]
    public int TrainingFormatId { get; set; }
    [Display(Name="Продолжительность")]
    public int Duration { get; set; }
    [Display(Name="Автор")]
    public string? Author { get; set; }

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();

    public virtual TrainingFormat TrainingFormat { get; set; } = null!;
}
