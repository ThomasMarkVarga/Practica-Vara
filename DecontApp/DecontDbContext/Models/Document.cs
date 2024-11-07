using System;
using System.Collections.Generic;

namespace DecontDbContext.Models;

public partial class Document
{
    public int Id { get; set; }

    public int Numar { get; set; }

    public DateOnly? Data { get; set; }

    public string? Explicatie { get; set; }

    public int StatusId { get; set; }

    public DateOnly? DataPlata { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RandDocument>? RandDocuments { get; set; } = new List<RandDocument>();

    public virtual Status? Status { get; set; } = null!;
}
