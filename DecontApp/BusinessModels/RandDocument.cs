using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class RandDocument
    {
        public int Id { get; set; }

        public int DocumentId { get; set; }

        public int CheltuialaId { get; set; }

        public string? Explicatie { get; set; }

        public decimal Valoare { get; set; }

        public bool? IsActive { get; set; }
    }
}
