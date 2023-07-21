using System;
using System.Collections.Generic;
using QLDV.Data;

namespace QLDV.Data;

public partial class Class
{
    public int Id { get; set; }

    public int FacultyId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UseridCreated { get; set; }

    public int? UseridUpdated { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
