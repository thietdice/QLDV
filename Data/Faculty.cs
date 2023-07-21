using System;
using System.Collections.Generic;

namespace QLDV.Data;

public partial class Faculty
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string ShortTitle { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UseridCreated { get; set; }

    public int? UseridUpdated { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
