using Aspose.Cells;
using Aspose.Cells.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Apelare_API
{
    internal class GenerateReportPDF : IGenerateReport
    {
        public void export(DataRepository dataLayer)
        {
            Company[] companies = dataLayer.getAllCompanies();

            Workbook wbJsonToPdf = new Workbook();

            Worksheet wsDefault = wbJsonToPdf.Worksheets[0];

            wsDefault.PageSetup.Orientation = PageOrientationType.Landscape;


            string jsonInput = File.ReadAllText("..\\..\\..\\results.json");

            JsonLayoutOptions layoutOptions = new JsonLayoutOptions();
            layoutOptions.ArrayAsTable = true;
            JsonUtility.ImportData(jsonInput, wsDefault.Cells, 0, 0, layoutOptions);

            Aspose.Cells.Range range;
            range = wsDefault.Cells.CreateRange("A1", "E" + (companies.Length + 1));
            //range.SetOutlineBorders(CellBorderType.Thin, Color.Black);

            for (int i = range.FirstRow; i < range.RowCount + range.FirstRow; i++)
            {
                for (int j = range.FirstColumn; j < range.ColumnCount + range.FirstColumn; j++)
                {
                    Cell cell = wsDefault.Cells[i, j];
                    Style style = cell.GetStyle();

                    // top
                    style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.TopBorder].Color = Color.Black;

                    // bottom
                    style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.BottomBorder].Color = Color.Black;

                    // left
                    style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.LeftBorder].Color = Color.Black;

                    // right
                    style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
                    style.Borders[BorderType.RightBorder].Color = Color.Black;

                    cell.SetStyle(style);
                }
            }

            wbJsonToPdf.Save("..\\..\\..\\output.PDF", SaveFormat.Auto);

            Console.WriteLine("Exported as PDF");
        }
    }
}
