using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDV.Data;

public partial class EventUser
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int UserId { get; set; }

    public string? Image { get; set; }

    public string? Note { get; set; }

    public string? NoteReviewer { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    [NotMapped]
    public IFormFile imageFile { get; set; }
}
