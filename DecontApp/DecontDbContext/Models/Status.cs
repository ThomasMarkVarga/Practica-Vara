using System;
using System.Collections.Generic;

namespace DecontDbContext.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Status1 { get; set; } = null!;

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
