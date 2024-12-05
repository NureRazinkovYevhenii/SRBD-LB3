using System;
using System.Collections.Generic;

namespace SRBD_LB3.Models;

public partial class Film
{
    public int FilmId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public int AuthorId { get; set; }

    public int CompanyId { get; set; }

    public string Country { get; set; } = null!;

    public decimal Price { get; set; }

    public int? WatchCount { get; set; }

    public double? Rating { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual FilmCompany Company { get; set; } = null!;

    public virtual ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}
