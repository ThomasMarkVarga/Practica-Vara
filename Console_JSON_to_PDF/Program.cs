using Newtonsoft.Json;
using Aspose.Cells;
using Aspose.Cells.Utility;
using System.Drawing;
using System.Collections;

namespace Console_JSON_to_PDF
{
    internal class Program
    {
        static void Main(string[] args) 
        {   
            Workbook wbJsonToPdf = new Workbook();

            Worksheet wsDefault = wbJsonToPdf.Worksheets[0];

            wsDefault.PageSetup.Orientation = PageOrientationType.Landscape;
            

            string jsonInput = File.ReadAllText("..\\..\\..\\results.json");            

            JsonLayoutOptions layoutOptions = new JsonLayoutOptions();
            layoutOptions.ArrayAsTable = true;
            JsonUtility.ImportData(jsonInput, wsDefault.Cells, 0, 0, layoutOptions);

            Aspose.Cells.Range range;
            range = wsDefault.Cells.CreateRange("A1", "E4");
            //range.SetOutlineBorders(CellBorderType.Thin, Color.Black);
            
            for(int i = range.FirstRow; i < range.RowCount + range.FirstRow; i++)
            {
                for(int j = range.FirstColumn; j < range.ColumnCount + range.FirstColumn; j++)
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

            wbJsonToPdf.Save("output.pdf", SaveFormat.Auto);

            Console.WriteLine("Done");
        }
    }
}