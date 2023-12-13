using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice1;

public partial class Subscription
{
    public int Id { get; set; }
    [Display(Name="Дата приобретения")]
    [DataType(DataType.Date)]
    public DateOnly PurchaseDate { get; set; }
    [Display(Name="Оставшееся количество тренировок")]
    public int TrainingsRest { get; set; }
    [Display(Name="Посетитель")]
    public int UserId { get; set; }
    [Display(Name="Тариф")]
    public int TariffId { get; set; }
    public virtual Tariff Tariff { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Timetable> Timetables { get; set; } = new List<Timetable>();
}
