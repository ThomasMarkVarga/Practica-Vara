using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Apelare_API
{
    public class ReportGeneratorFactory
    {
        public IGenerateReport GetReportGenerator(string format)
        {
            switch (format.ToLower())
            {
                case "xls":
                    return new GenerateReportXLS();
                case "pdf":
                    return new GenerateReportPDF();
                default:
                    throw new ApplicationException("Format not suported");
            }
        }
    }
}
