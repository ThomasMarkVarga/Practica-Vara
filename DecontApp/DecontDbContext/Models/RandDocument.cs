using System;
using System.Collections.Generic;

namespace DecontDbContext.Models;

public partial class RandDocument
{
    public int Id { get; set; }

    public int DocumentId { get; set; }

    public int CheltuialaId { get; set; }

    public string? Explicatie { get; set; }

    public decimal Valoare { get; set; }

    public bool? IsActive { get; set; }

    public virtual TipCheltuiala Cheltuiala { get; set; } = null!;

    public virtual Document Document { get; set; } = null!;
}
