using System;
using System.Collections.Generic;
using QLDV.Data;

namespace QLDV.Data;

public partial class UserCatalogue
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
