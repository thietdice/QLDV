using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDV.Data;

public partial class Article
{
    public int Id { get; set; }

    public int? Viewed { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public string? Pdf { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UseridCreated { get; set; }

    public int? UseridUpdated { get; set; }

    [NotMapped]

    public IFormFile PdfFile { get; set; }
}
