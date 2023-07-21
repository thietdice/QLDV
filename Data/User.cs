using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLDV.Data;

public partial class User
{
    public int Id { get; set; }

    public int UserCatalogueId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? IdStudent { get; set; }

    public int? ClassId { get; set; }

    public string Fullname { get; set; } = null!;

    public DateTime? Birthday { get; set; }

    public string? Gender { get; set; }

    public string? Ethnic { get; set; }

    public string? Religion { get; set; }

    public string? IdCard { get; set; }

    public string? Profession { get; set; }

    public string? LevelEducation { get; set; }

    public string? LevelSpecialize { get; set; }

    public string? LevelPolitics { get; set; }

    public string? LevelComputer { get; set; }

    public string? LevelLanguage { get; set; }

    public DateTime? DayInUnion { get; set; }

    public string? Phone { get; set; }

    public string? Image { get; set; }

    public string? ResidenceAddress { get; set; }

    public string? ResidenceCity { get; set; }

    public string? ResidenceDistrict { get; set; }

    public string? ResidenceWard { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UseridCreated { get; set; }

    public int? UseridUpdated { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<EventUser> EventUsers { get; set; } = new List<EventUser>();

    public virtual UserCatalogue UserCatalogue { get; set; } = null!;

    [NotMapped]

    public IFormFile imageFile { get; set; }
}
