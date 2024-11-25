using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyProject;
using MessageAPIObjectProject;


namespace CompanyFinderAPICall
{
    public partial class Form1 : Form
    {
        private readonly ICallAPI callAPI;

        public Form1(ICallAPI _callAPI)
        {
            InitializeComponent();
            this.callAPI = _callAPI;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridCautare.ColumnCount = 5;
            dataGridCautare.Columns[0].Name = "CIF";
            dataGridCautare.Columns[1].Name = "Nume";
            dataGridCautare.Columns[2].Name = "Adresa";
            dataGridCautare.Columns[3].Name = "Judet";
            dataGridCautare.Columns[4].Name = "Telefon";

            dataGridToate.ColumnCount = 5;
            dataGridToate.Columns[0].Name = "CIF";
            dataGridToate.Columns[1].Name = "Nume";
            dataGridToate.Columns[2].Name = "Adresa";
            dataGridToate.Columns[3].Name = "Judet";
            dataGridToate.Columns[4].Name = "Telefon";

            populateDataGrid();
        }

        private async void populateDataGrid()
        {
            dataGridToate.Rows.Clear();

            var (arr, message) = await callAPI.getAllCompaniesAPI();
            if (message.status == StatusCode.OK)
            {
                foreach (var comp in arr)
                {
                    dataGridToate.Rows.Add(comp.companyCIF, comp.companyName, comp.companyAddress, comp.companyCounty, comp.companyPhone);
                }
            }
            else if(message.status == StatusCode.NoContent)
            {
                MessageBox.Show(message.ErrorMessage);
            }
        }

        private async void btSearchCIF_Click(object sender, EventArgs e)
        {
            dataGridCautare.Rows.Clear();

            string url = "https://api.openapi.ro/api/companies/";
            string apiKey = "ucvF8o3CRpMXUxHtrauhHgENHLjQJrPHNF4fWkxdPeXyz8eNLw";

            var (searchCompanyByCIF, searchMessage) = await callAPI.getCompanyAPI(tbInsertCIF.Text);

            if (searchMessage.status == StatusCode.OK)
            {
                dataGridCautare.Rows.Add(searchCompanyByCIF.companyCIF, searchCompanyByCIF.companyName, searchCompanyByCIF.companyAddress, searchCompanyByCIF.companyCounty, searchCompanyByCIF.companyPhone);
            }
            else if (searchMessage.status == StatusCode.NotFound)
            {
                JObject json = new JObject();

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url + tbInsertCIF.Text);

                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                    json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }

                await callAPI.insertCompanyAPI(
                    (string)json["cif"],
                    (string)json["denumire"],
                    (string)json["adresa"],
                    (string)json["judet"],
                    (string)json["telefon"]);

                var (newComp, newCompMessage) = await callAPI.getCompanyAPI(tbInsertCIF.Text);
                if (newCompMessage.status == StatusCode.OK)
                {
                    dataGridCautare.Rows.Add(newComp.companyCIF, newComp.companyName, newComp.companyAddress, newComp.companyCounty, newComp.companyPhone);
                }
                else if (newCompMessage.status == StatusCode.NotFound)
                {
                    MessageBox.Show(newCompMessage.ErrorMessage);
                }

                // repopulare dataViewToate pentru a avea lista firme actualizata
                populateDataGrid();
            }
            tbInsertCIF.Text = "";
        }

        private async void btnAddCompany_Click(object sender, EventArgs e)
        {
            var (comp, addMessage) = await callAPI.insertCompanyAPI(tbAddCIF.Text, tbAddNume.Text, tbAddAdresa.Text, tbAddJudet.Text, tbAddTelefon.Text);
            if (addMessage.status == StatusCode.OK)
            {
                tbAddCIF.Text = "";
                tbAddNume.Text = "";
                tbAddAdresa.Text = "";
                tbAddJudet.Text = "";
                tbAddTelefon.Text = "";

                MessageBox.Show(addMessage.SuccessMessage + "\n" + comp.ToString());

                // repopulare dataViewToate pentru a avea lista firme actualizata
                populateDataGrid();
            }
            else if(addMessage.status == StatusCode.NotFound)
            {
                MessageBox.Show(addMessage.ErrorMessage);
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            MessageObjectAPI response = await callAPI.deleteCompanyAPI(tbRemoveCIF.Text);
            tbRemoveCIF.Text = "";
            if(response.status == StatusCode.NotFound)
            {
                MessageBox.Show(response.ErrorMessage);
            }
            else if(response.status == StatusCode.BadRequest)
            {
                MessageBox.Show(response.ErrorMessage);
            }
            else if(response.status == StatusCode.OK)
            {
                MessageBox.Show(response.SuccessMessage);
                populateDataGrid();
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            String CIF, nume, adresa, judet, telefon;
            var (compToUpdate, updateMessage) = await callAPI.getCompanyAPI(tbUpdateCIF.Text);

            if (updateMessage.status == StatusCode.OK)
            {
                if (tbUpdateNewCIF.Text != "")
                    CIF = tbUpdateNewCIF.Text;
                else
                    CIF = compToUpdate.companyCIF;

                if (tbUpdateNume.Text != "")
                    nume = tbUpdateNume.Text;
                else
                    nume = compToUpdate.companyName;

                if (tbUpdateAdresa.Text != "")
                    adresa = tbUpdateAdresa.Text;
                else
                    adresa = compToUpdate.companyAddress;

                if (tbUpdateJudet.Text != "")
                    judet = tbUpdateJudet.Text;
                else
                    judet = compToUpdate.companyCounty;

                if (tbUpdateTelefon.Text != "")
                    telefon = tbUpdateTelefon.Text;
                else
                    telefon = compToUpdate.companyPhone;

                var (updatedComp, message) = await callAPI.updateCompanyAPI(compToUpdate.companyCIF, CIF, nume, adresa, judet, telefon);
                if(message.status == StatusCode.OK)
                {
                    MessageBox.Show(message.SuccessMessage + "\n" + updatedComp.ToString());
                    populateDataGrid();
                }
            }
            else if (updateMessage.status == StatusCode.NotFound)
            {
                MessageBox.Show(updateMessage.ErrorMessage);
            }

            tbUpdateCIF.Text = "";
            tbUpdateNewCIF.Text = "";
            tbUpdateNume.Text = "";
            tbUpdateAdresa.Text = "";
            tbUpdateJudet.Text = "";
            tbUpdateTelefon.Text = "";
        }
    }
}
