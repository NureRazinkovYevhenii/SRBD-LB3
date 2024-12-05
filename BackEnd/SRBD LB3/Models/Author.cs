using System;
using System.Collections.Generic;

namespace SRBD_LB3.Models;

public partial class Author
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public string Country { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
