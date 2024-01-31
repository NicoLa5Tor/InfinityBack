using System;
using System.Collections.Generic;

namespace BackInfinity.Models;

public partial class Access
{
    public int IdAcces { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;
}
