using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Apelare_API
{
    public interface IGenerateReport
    {
        void export(DataRepository dataLayer);
    }
}
