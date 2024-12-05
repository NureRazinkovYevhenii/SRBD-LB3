using System;
using System.Collections.Generic;

namespace SRBD_LB3.Models;

public partial class FilmsLog
{
    public int LogId { get; set; }

    public DateTime AttemptDate { get; set; }

    public string? FilmName { get; set; }

    public string ErrorMessage { get; set; } = null!;
}
