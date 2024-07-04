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
      $("#tdCIF").text(companie.cif);
      $("#tdDenumire").text(companie.denumire);
      $("#tdAdresa").text(companie.adresa);
      $("#tdJudet").text(companie.judet);
      $("#tdTelefon").text(companie.telefon);
    } else {
      $.ajax({
        url: "https://api.openapi.ro/api/companies/" + CIF,
        type: "GET",
        beforeSend: function (xhr) {
          xhr.setRequestHeader("x-api-key", API_KEY);
        },
        success: function (data) {
          localStorage.setItem(CIF, JSON.stringify(data));
          $("#content").show();
          $("#tdCIF").text(data.cif);
          $("#tdDenumire").text(data.denumire);
          $("#tdAdresa").text(data.adresa);
          $("#tdJudet").text(data.judet);
          $("#tdTelefon").text(data.telefon);
        },
        error: function (err) {
          alert("error: " + err);
        },
      });
    }
  });

  // carousel
  createCarousel("#carousel", createTableHTML(allData), 2000);
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
    var CIFTable = JsonData.cif;
    var DenumireTable = JsonData.denumire;
    var AdresaTable = JsonData.adresa;
    var JudetTable = JsonData.judet;
    var TelefonTable = JsonData.telefon;
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
