using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyProj;

namespace DataRpo
{
    public interface IDataRepo
    {
        Task<List<Company>> getAllComp();
    }
}
