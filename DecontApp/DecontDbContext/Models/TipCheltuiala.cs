using System;
using System.Collections.Generic;

namespace DecontDbContext.Models;

public partial class TipCheltuiala
{
    public int Id { get; set; }

    public string Denumire { get; set; } = null!;

    public decimal? ValoareImplicita { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RandDocument> RandDocuments { get; set; } = new List<RandDocument>();
}
