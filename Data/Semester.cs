using System;
using System.Collections.Generic;

namespace QLDV.Data;

public partial class Semester
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime DayStart { get; set; }

    public DateTime DayEnd { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UseridCreated { get; set; }

    public int? UseridUpdated { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
