using System;
using System.Collections.Generic;

namespace BackInfinity.Models;

public partial class Appointment
{
    public int IdAppointment { get; set; }

    public string? NameUs { get; set; }

    public long Tel { get; set; }

    public DateTime HorAppoint { get; set; }

    public DateTime HorCreate { get; set; }

    public string EstatePay { get; set; } = null!;

    public int? IdService { get; set; }

    public virtual Service? IdServiceNavigation { get; set; }
}
