using System;
using System.Collections.Generic;

namespace SRBD_LB3.Models;

public partial class FilmCompany
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Country { get; set; }

    public int? EstablishedYear { get; set; }

    public string? Website { get; set; }

    public string? Indormation { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
