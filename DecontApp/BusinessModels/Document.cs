using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class Document
    {
        public int Id { get; set; }

        public int Numar { get; set; }

        public DateOnly? Data { get; set; }

        public string? Explicatie { get; set; }

        public int StatusId { get; set; }

        public DateOnly? DataPlata { get; set; }

        public bool? IsActive { get; set; }

        public ICollection<RandDocument> RandDocuments { get; set; }
    }
}
