using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Practice1;

public partial class Tariff
{
    public int Id { get; set; }
    [Display(Name="Название")]
    public string Name { get; set; } = null!;
    [Display(Name="Описание")]
    public string Description { get; set; } = null!;
    [Display(Name="Формат тренировок")]
    public int TrainingFormatId { get; set; }
    
    [Display(Name="Цена")]
    public int Price { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    [Display(Name="Формат тренировок")]
    public virtual TrainingFormat TrainingFormat { get; set; } = null!;
}
