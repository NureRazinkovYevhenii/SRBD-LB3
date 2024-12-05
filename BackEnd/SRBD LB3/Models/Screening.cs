using System;
using System.Collections.Generic;

namespace SRBD_LB3.Models;

public partial class Screening
{
    public int Id { get; set; }

    public int? FilmId { get; set; }

    public DateTime? ScreeningDate { get; set; }

    public virtual Film? Film { get; set; }
}
