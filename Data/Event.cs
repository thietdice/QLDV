using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDV.Data;

public partial class Event
{
    public int Id { get; set; }

    public int SemesterId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Content { get; set; }

    public DateTime DayStart { get; set; }

    public DateTime DayEnd { get; set; }

    public int Score { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? Publish { get; set; }

    public int? UseridCreated { get; set; }

    public int? UseridUpdated { get; set; }

    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();

    public virtual Semester Semester { get; set; } = null!;

    [NotMapped]

    public IFormFile imageFile { get; set; }
}
