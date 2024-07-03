const API_KEY = "8AxoCqMAn-XYnRyziJPdzKxvda_FBNmnD6RgsZd8GkRZ5pEvxg";
var CIF;
var cache = {};

$(document).ready(function () {
  $("table").hide();
  $("#srcBtn").click(function () {
    CIF = $("#inputCIF").val();
    if (cache[CIF]) {
      $("table").show();
      $("#tdCIF").text(cache[CIF].cif);
      $("#tdDenumire").text(cache[CIF].denumire);
      $("#tdAdresa").text(cache[CIF].adresa);
      $("#tdJudet").text(cache[CIF].judet);
      $("#tdTelefon").text(cache[CIF].telefon);
    } else {
      $.ajax({
        url: "https://api.openapi.ro/api/companies/" + CIF,
        type: "GET",
        beforeSend: function (xhr) {
          xhr.setRequestHeader("x-api-key", API_KEY);
        },
        success: function (data) {
          cache[CIF] = data;
          $("table").show();
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
});
