import { createCarousel } from "./carousel.js";
const API_KEY = "8AxoCqMAn-XYnRyziJPdzKxvda_FBNmnD6RgsZd8GkRZ5pEvxg";
const allData = allStored();
var CIF;

$(document).ready(function () {
  $("#content").hide();
  $("#srcBtn").click(function () {
    CIF = $("#inputCIF").val();
    if (localStorage.getItem(CIF)) {
      let companie = JSON.parse(localStorage.getItem(CIF));
      $("#content").show();
      $("#tdCIF").text(companie.companyCIF);
      $("#tdDenumire").text(companie.companyName);
      $("#tdAdresa").text(companie.companyAddress);
      $("#tdJudet").text(companie.companyCounty);
      $("#tdTelefon").text(companie.companyPhone).hide();
    } else {
      $.ajax({
        url: "https://api.openapi.ro/api/companies/" + CIF,
        type: "GET",
        beforeSend: function (xhr) {
          xhr.setRequestHeader("x-api-key", API_KEY);
        },
        success: function (data) {
          let company = {
            companyCIF: data.cif,
            companyName: data.denumire,
            companyAddress: data.adresa,
            companyCounty: data.judet,
            companyPhone: data.telefon,
          };
          localStorage.setItem(CIF, JSON.stringify(company));
          $("#content").show();
          $("#tdCIF").text(company.companyCIF);
          $("#tdDenumire").text(company.companyName);
          $("#tdAdresa").text(company.companyAddress);
          $("#tdJudet").text(company.companyCounty);
          $("#tdTelefon").text(company.companyPhone);
        },
        error: function (err) {
          alert("error: " + err);
        },
      });
    }
  });

  $("#togglePhoneVis").click(function () {
    $("#tdTelefon").toggle();
  });

  // carousel
  createCarousel("#carousel", createTableHTML(allData), 5000);
});

// function retrievs all data from localStorage
function allStored() {
  var values = [];
  var keys = Object.keys(localStorage);
  var noOfKeys = keys.length;
  while (noOfKeys--) {
    values[noOfKeys] = localStorage.getItem(keys[noOfKeys]);
  }
  return values;
}

// create html table code
function createTableHTML(data) {
  var htmlTable = [];
  for (let i = 0; i < data.length; i++) {
    var JsonData = JSON.parse(data[i]);
    var CIFTable = JsonData.companyCIF;
    var DenumireTable = JsonData.companyName;
    var AdresaTable = JsonData.companyAddress;
    var JudetTable = JsonData.companyAddress;
    var TelefonTable = JsonData.companyPhone;
    var htmlTableIter =
      `
      <h2>Recent Searches</h2>
      <table>
        <tr>
            <th>CIF</th>
            <th>Company</th>
            <th>Address</th>
            <th>County</th>
            <th>Phone</th>
        </tr>
        <tr>
            <td id="CIFTable">` +
      CIFTable +
      `</td>
            <td>` +
      DenumireTable +
      `</td>
            <td>` +
      AdresaTable +
      `</td>
            <td>` +
      JudetTable +
      `</td>
            <td>` +
      TelefonTable +
      `</td>
        </tr>
    </table> `;

    htmlTable[i] = htmlTableIter;
  }
  return htmlTable;
}
