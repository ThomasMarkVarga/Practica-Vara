using Console_Apelare_API;
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


namespace CompanyFinderAPICall
{
    public partial class Form1 : Form
    {
        DataRepository dataLayer = new DataRepository();

        public Form1()
        {
            InitializeComponent();
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

        private void populateDataGrid()
        {
            dataGridToate.Rows.Clear();

            Company[] arr = dataLayer.getAllCompanies();

            foreach (var comp in arr)
            {
                dataGridToate.Rows.Add(comp.companyCIF, comp.companyName, comp.companyAddress, comp.companyCounty, comp.companyPhone);
            }
        }

        private async void btSearchCIF_Click(object sender, EventArgs e)
        {
            dataGridCautare.Rows.Clear();

            string url = "https://api.openapi.ro/api/companies/";
            string apiKey = "ucvF8o3CRpMXUxHtrauhHgENHLjQJrPHNF4fWkxdPeXyz8eNLw";

            Company searchCompanyByCIF = dataLayer.getCompany(tbInsertCIF.Text);

            if (searchCompanyByCIF != null)
            {
                dataGridCautare.Rows.Add(searchCompanyByCIF.companyCIF, searchCompanyByCIF.companyName, searchCompanyByCIF.companyAddress, searchCompanyByCIF.companyCounty, searchCompanyByCIF.companyPhone);
            }
            else if (searchCompanyByCIF == null)
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
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

                await dataLayer.insertCompany(
                    (string)json["cif"],
                    (string)json["denumire"],
                    (string)json["adresa"],
                    (string)json["judet"],
                    (string)json["telefon"]);

                Company newComp = dataLayer.getCompany(tbInsertCIF.Text);
                dataGridCautare.Rows.Add(newComp.companyCIF, newComp.companyName, newComp.companyAddress, newComp.companyCounty, newComp.companyPhone);


                // repopulare dataViewToate pentru a avea lista firme actualizata
                populateDataGrid();
            }
            tbInsertCIF.Text = "";
        }

        private async void btnAddCompany_Click(object sender, EventArgs e)
        {
            await dataLayer.insertCompany(tbAddCIF.Text, tbAddNume.Text, tbAddAdresa.Text, tbAddJudet.Text, tbAddTelefon.Text);
            tbAddCIF.Text = "";
            tbAddNume.Text = "";
            tbAddAdresa.Text = "";
            tbAddJudet.Text = "";
            tbAddTelefon.Text = "";

            // repopulare dataViewToate pentru a avea lista firme actualizata
            populateDataGrid();
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            Company compToRemove = dataLayer.getCompany(tbRemoveCIF.Text);
            tbRemoveCIF.Text = "";
            if (compToRemove != null)
            {
                var result = MessageBox.Show("Are you sure you want to delete " + compToRemove.companyName + "?", "Deleting company", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await dataLayer.removeCompany(compToRemove);

                    // repopulare dataViewToate pentru a avea lista firme actualizata
                    populateDataGrid();
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            String CIF, nume, adresa, judet, telefon;
            Company compToUpdate = dataLayer.getCompany(tbUpdateCIF.Text);

            if (compToUpdate != null)
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

                await dataLayer.updateCompany(compToUpdate.companyCIF, CIF, nume, adresa, judet, telefon);

                populateDataGrid();
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
