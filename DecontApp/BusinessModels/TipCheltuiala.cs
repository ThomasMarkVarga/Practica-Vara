using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class TipCheltuiala
    {
        public int Id { get; set; }

        public string Denumire { get; set; } = null!;

        public decimal ValoareImplicita { get; set; } = 0;

        public bool? IsActive { get; set; }
    }
}
