using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Aspose.Cells;
using Aspose.Cells.Utility;
using System.Drawing;
using System.Configuration;

namespace Console_Apelare_API
{
    public class StorageFile
    {
        public static string path = ConfigurationManager.AppSettings["path"];

        public static List<Company> ReadCompanies()
        {
            List<Company> companies = new List<Company>();

            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                companies = JsonConvert.DeserializeObject<List<Company>>(json);
            }
            return companies;
        }

        public static void WriteCompaniesToFile(List<Company> companies)
        {
            string companiesString = JsonConvert.SerializeObject(companies.ToArray());
            File.WriteAllText(path, companiesString);
            Console.WriteLine("Wrote to JSON File");
        }
    }

    public class Company
    {
        public string companyCIF { get; set; }
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string companyCounty { get; set; }
        public string companyPhone { get; set; }

        public override string ToString()
        {
            return "{\n" +this.companyCIF + "\n" 
                + this.companyName + "\n"
                + this.companyAddress + "\n"
                + this.companyCounty + "\n"
                +this.companyPhone + "\n}";
        }
    }
       
    class Program
    {
        public static List<Company> companies = new List<Company>();

        static async Task Main(string[] args)
        { 

            string url = "https://api.openapi.ro/api/companies/";
            string apiKey = "ucvF8o3CRpMXUxHtrauhHgENHLjQJrPHNF4fWkxdPeXyz8eNLw";

            int userChoice;
            int userSecondChoice;

            do
            {
                Console.WriteLine("---Company Finder---\nChoose one of the following options:\n" +
                    "0 - exit\n" +
                    "1 - search company using CIF\n" +
                    "2 - see previously searched comapnies\n" +
                    "3 - search company in cache");
                userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 0:
                        Console.WriteLine("You choose to exit!");
                        break;
                    case 1:
                        // user introduce CIF
                        Console.WriteLine("Type company CIF:");
                        string CIF = Console.ReadLine();
                        bool found = false;
                        // cauta in lista, daca nu gasim face call la api
                        companies = StorageFile.ReadCompanies();
                        if (companies != null)
                        {
                            foreach (var item in companies)
                            {
                                if (item.companyCIF == CIF)
                                {
                                    Console.WriteLine(item);
                                    found = true;
                                    break;
                                }
                            }
                        }
                        if (!found)
                        {
                            JObject json = new JObject();

                            HttpClient client = new HttpClient();
                            client.BaseAddress = new Uri(url + CIF);

                            client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                            client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                            HttpResponseMessage response = await client.GetAsync("");

                            if (response.IsSuccessStatusCode)
                            {
                                Console.WriteLine("Success");
                                json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                            }
                            else
                            {
                                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                            }

                            Company company = new Company();
                            company.companyCIF = (string)json["cif"];
                            company.companyName = (string)json["denumire"];
                            company.companyAddress = (string)json["adresa"];
                            company.companyCounty = (string)json["judet"];
                            company.companyPhone = (string)json["telefon"];

                            companies.Add(company);
                            StorageFile.WriteCompaniesToFile(companies);
                            Console.WriteLine(company);

                        }
                        break;
                    case 2:
                        // prima implementare - da load la results.json si afiseaza
                        displayComapnies();
                        break;
                    case 3:
                        Console.WriteLine("Seach company by:\n" +
                            "1 - Name\n" +
                            "2 - Address\n" +
                            "3 - See companies in a city\n" +
                            "4 - Number of companies in a county");
                        userSecondChoice = int.Parse(Console.ReadLine());
                        switch (userSecondChoice)
                        {
                            case 1:
                                Console.WriteLine("Insert name / part of name:");
                                string name = Console.ReadLine();
                                cautareNume(name);  // query pe baza numelui
                                
                                break;
                            case 2:
                                Console.WriteLine("Insert address / part of address:");
                                string address = Console.ReadLine();
                                cautareAdresa(address); // query adresa
                                break;
                            case 3:
                                Console.WriteLine("Insert city / part of city name:");
                                string city = Console.ReadLine();
                                cautareOras(city); // query oras
                                break;
                            case 4:
                                Console.WriteLine("Insert county:");
                                string county = Console.ReadLine();
                                noInJudet(county);  //  query judet
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;

                }
            } while (userChoice != 0);
        }

        public static void checkCompaniesList()
        {
            if (!companies.Any())
            {
                companies = StorageFile.ReadCompanies();
            }
        }

        public static void exportAsPDF()
        {
            Workbook wbJsonToPdf = new Workbook();

            Worksheet wsDefault = wbJsonToPdf.Worksheets[0];

            wsDefault.PageSetup.Orientation = PageOrientationType.Landscape;


            string jsonInput = File.ReadAllText("..\\..\\..\\results.json");

            JsonLayoutOptions layoutOptions = new JsonLayoutOptions();
            layoutOptions.ArrayAsTable = true;
            JsonUtility.ImportData(jsonInput, wsDefault.Cells, 0, 0, layoutOptions);

            Aspose.Cells.Range range;
            range = wsDefault.Cells.CreateRange("A1", "E" + (companies.Count + 1));
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

            wbJsonToPdf.Save("..\\..\\..\\output.pdf", SaveFormat.Auto);

            Console.WriteLine("Exported as PDF");
        }

        public static void displayComapnies()
        {
            companies = StorageFile.ReadCompanies();
            if (companies != null)
            {
                foreach (var item in companies)
                {
                    Console.WriteLine(item);
                }
                exportAsPDF();
            }
            else
            {
                Console.WriteLine("No recent searches");
            }
        }

        public static void cautareNume(string name)
        {
            checkCompaniesList();

            IEnumerable<Company> searchName = from company in companies
                                              where company.companyName.ToLower().Contains(name.ToLower())
                                              select company;

            foreach(Company company in searchName)
            {
                Console.WriteLine(company);
            }

        }

        public static void cautareAdresa(string adresa)
        {
            checkCompaniesList();

            IEnumerable<Company> searchAddress = from company in companies
                                                 where company.companyAddress.ToLower().Contains(adresa.ToLower())
                                                 select company;

            foreach(Company company in searchAddress)
            {
                Console.WriteLine(company);
            }
        }

        public static void cautareOras(string oras)
        {
            checkCompaniesList();

            var searchOras = from company in companies
                              where company.companyAddress.ToLower().Contains(oras.ToLower())
                              select company;

            Console.WriteLine("There are {0} companies in this city!", searchOras.Count());
            foreach(var company in searchOras)
            {
                Console.WriteLine(company);
            }

        }

        public static void noInJudet(string judet)
        {
            checkCompaniesList();

            int noComp = (from company in companies
                          where company.companyCounty.ToLower().Contains(judet.ToLower())
                          select company).Count();

            Console.WriteLine("There are {0} companies in this county!", noComp);
        }
    }
}
