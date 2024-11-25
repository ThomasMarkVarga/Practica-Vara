using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Aspose.Cells;
using Aspose.Cells.Utility;
using System.Drawing;

namespace Console_Apelare_API
{
    class Program
    { 
        static async Task Main(string[] args)
        {

            string url = "https://api.openapi.ro/api/companies/";
            string apiKey = "ucvF8o3CRpMXUxHtrauhHgENHLjQJrPHNF4fWkxdPeXyz8eNLw";

            DataRepository dataLayer = new DataRepository();

            int userChoice;
            int userSecondChoice;

            do
            {
                Console.WriteLine("---Company Finder---\nChoose one of the following options:\n" +
                    "0 - exit\n" +
                    "1 - search company using CIF\n" +
                    "2 - see previously searched comapnies\n" +
                    "3 - search company in cache\n" +
                    "4 - manage companies\n" +
                    "5 - export");
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

                        Company searchedCompanyByCIF = dataLayer.getCompany(CIF); // cautam prin dataLayer compania

                        if(searchedCompanyByCIF != null)    // daca exista afisam
                        {
                            Console.WriteLine(searchedCompanyByCIF);
                        }
                        else if(searchedCompanyByCIF == null)   // daca nu exista facem call la API
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

                            dataLayer.insertCompany(
                                (string)json["cif"],
                                (string)json["denumire"],
                                (string)json["adresa"],
                                (string)json["judet"],
                                (string)json["telefon"]);   // introducem datele in dataLayer

                            Console.WriteLine(dataLayer.getCompany(CIF)); // afisam compania introdusa acum 
                        }
                        break;
                    case 2:
                        displayCompanies(dataLayer); // afisam toate companiile salvate
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

                                Company[] compName = dataLayer.searchName(name);  // query pe baza numelui
                                if(compName.Length == 0)
                                {
                                    Console.WriteLine("No company with this name found!");
                                }
                                else
                                {
                                   foreach(var item in compName)
                                    {
                                        Console.WriteLine(item);
                                    }
                                }

                                break;
                            case 2:
                                Console.WriteLine("Insert address / part of address:");
                                string address = Console.ReadLine();

                                Company[] compAddress = dataLayer.seachAddress(address);    // query adresa
                                if(compAddress.Length == 0)
                                {
                                    Console.WriteLine("No company with this address found!");
                                }
                                else
                                {
                                    foreach(var item in compAddress)
                                    {
                                        Console.WriteLine(item);
                                    }
                                }

                                break;
                            case 3:
                                Console.WriteLine("Insert city / part of city name:");
                                string city = Console.ReadLine();

                                Company[] compCity = dataLayer.searchCity(city); // query oras
                                if(compCity.Length == 0)
                                {
                                    Console.WriteLine("No company found in this city!");
                                }
                                else
                                {
                                    foreach(var item in compCity)
                                    {
                                        Console.WriteLine(item);
                                    }
                                }

                                break;
                            case 4:
                                Console.WriteLine("Insert county:");
                                string county = Console.ReadLine();
                                Console.WriteLine("There are {0} companies in this county!", dataLayer.noOfCompInCounty(county));  //  query judet
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                        break;
                    case 4:
                        manageCompanies(dataLayer);
                        break;
                    case 5:
                        Console.WriteLine("Which format do you want? PDF or XLS?");
                        string formatChoice = Console.ReadLine();
                        ReportGeneratorFactory reportGeneratorFactory = new ReportGeneratorFactory();
                        IGenerateReport reportGen = reportGeneratorFactory.GetReportGenerator(formatChoice);
                        reportGen.export(dataLayer);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;

                }
            } while (userChoice != 0);
        }

        public static void displayCompanies(DataRepository dataLayer)
        {
            var comp = dataLayer.getAllCompanies();

            foreach(var company in comp)
            {
                Console.WriteLine(company);
            }
        }

        public static void exportAsPDF(DataRepository dataLayer)
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

            wbJsonToPdf.Save("..\\..\\..\\output.xls", SaveFormat.Auto);

            Console.WriteLine("Exported as PDF");
        }

        // implementation based on use case diagram 

        public static void manageCompanies(DataRepository dataLayer)
        {
            int choice;

            do
            {
                Console.WriteLine("What kind of operation do you want to perform?\n" +
                    "0 - back\n" +
                    "1 - find company\n" +
                    "2 - update company\n" +
                    "3 - remove company\n" +
                    "4 - add company");

                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        break;
                    case 1:
                        Console.WriteLine("Insert company CIF you want to see:");
                        string searchCIF = Console.ReadLine();
                        findCompany(searchCIF, dataLayer);
                        break;
                    case 2:
                        Console.WriteLine("Insert company CIF you want to update:");
                        string updateCIF = Console.ReadLine();
                        updateCompany(updateCIF, dataLayer);
                        break;
                    case 3:
                        Console.WriteLine("Insert company CIF you want to remove:");
                        string removeCIF = Console.ReadLine();
                        removeCompany(removeCIF, dataLayer);
                        break;
                    case 4:
                        addCompany(dataLayer);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            } while (choice != 0);
        }

        public static void findCompany(string CIF, DataRepository datLayer)
        {
            Company company = datLayer.getCompany(CIF);

            if (company == null)
            {
                Console.WriteLine("Company not found!" +
                    "\n1 - Do you want to add it?" +
                    "\n2 - Cancel");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                    addCompany(datLayer);
                if (choice == 2)
                    Console.WriteLine("Operation canceled!");
            }
            else
                Console.WriteLine(company);
        }

        public static void addCompany(DataRepository dataLayer)
        { 
            Console.WriteLine("Add company\n" +
                "Insert CIF:");
            string CIF= Console.ReadLine();

            Console.WriteLine("Insert company name:");
            string Name = Console.ReadLine();

            Console.WriteLine("Insert company address:");
            string Address = Console.ReadLine();

            Console.WriteLine("Insert county:");
            string County = Console.ReadLine();

            Console.WriteLine("Insert phone number:");
            string Phone = Console.ReadLine();

            dataLayer.insertCompany(CIF, Name, Address, County, Phone);
        }

        public static void removeCompany(string CIF, DataRepository dataLayer)
        {
            Company company = dataLayer.getCompany(CIF);

            if (company != null)
            {
                Console.WriteLine("Are you sure you want to remove:\n" + company +
                    "\n1 - yes" +
                    "\n2 - no");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    dataLayer.removeCompany(company);
                }
                else
                {
                    Console.WriteLine("Action canceled");
                }
            }
        }

        public static void updateCompany(string CIF, DataRepository dataLayer)
        {
            Company compToUpdate = dataLayer.getCompany(CIF);

            if (compToUpdate != null)
            {
                Console.WriteLine("Company found!\n" + compToUpdate);

                int updateChoice;
                string newCIF = "", 
                    newName = "", 
                    newAddress = "", 
                    newCounty = "", 
                    newPhone = "";
                bool changeCIF = false,
                    changeName = false,
                    changeAddress = false,
                    changeCounty = false,
                    changePhone = false;

                do
                {
                    Console.WriteLine("What do you want to change?" +
                        "\n1 - CIF" +
                        "\n2 - name" +
                        "\n3 - address" +
                        "\n4 - county" +
                        "\n5 - phone number" +
                        "\n9 - Save Changes" +
                        "\n0 - Cancel");
                    updateChoice = int.Parse(Console.ReadLine());
                    switch (updateChoice)
                    {
                        case 0:
                            Console.WriteLine("Update company canceled");
                            break;
                        case 1:
                            Console.WriteLine("Insert new CIF:");
                            newCIF = Console.ReadLine();
                            changeCIF = true;
                            break;
                        case 2:
                            Console.WriteLine("Insert new name:");
                            newName = Console.ReadLine();
                            changeName = true;
                            break;
                        case 3:
                            Console.WriteLine("Insert new address:");
                            newAddress = Console.ReadLine();
                            changeAddress = true;
                            break;
                        case 4:
                            Console.WriteLine("Insert new county:");
                            newCounty = Console.ReadLine();
                            changeCounty = true;
                            break;
                        case 5:
                            Console.WriteLine("Insert new phone number:");
                            newPhone = Console.ReadLine();
                            changePhone = true;
                            break;
                        case 9:
                            if (!changeCIF) newCIF = compToUpdate.companyCIF;
                            if (!changeName) newName = compToUpdate.companyName;
                            if (!changeAddress) newAddress = compToUpdate.companyAddress;
                            if (!changeCounty) newCounty = compToUpdate.companyCounty;
                            if (!changePhone) newPhone = compToUpdate.companyPhone;

                            dataLayer.updateCompany(CIF, newCIF, newName, newAddress, newCounty, newPhone);

                            Console.WriteLine("Changes saved");
                            updateChoice = 0;
                            break;
                    }
                } while (updateChoice != 0);
            }
            else
            {
                Console.WriteLine("Company not found!\n" +
                    "Choose option:\n" +
                    "1 - add company\n" +
                    "2 - cancel update");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        addCompany(dataLayer);
                        break;
                    case 2:
                        Console.WriteLine("Canceled update");
                        break;
                }

            }
        }
    }
}
