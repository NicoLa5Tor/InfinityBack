using System;
using System.Collections.Generic;

namespace BackInfinity.Models;

public partial class Service
{
    public int Id { get; set; }

    public TimeSpan HorMin { get; set; }

    public string? NameService { get; set; }

    public string? Description1 { get; set; }

    public string? Description2 { get; set; }

    public string? Image1 { get; set; }

    public string? Image2 { get; set; }

    public string? Image3 { get; set; }

    public int Price { get; set; }
}
