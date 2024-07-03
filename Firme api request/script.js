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
  let comp = JSON.parse(allData[0]);
  $("#tdCIFRecent").text(comp.cif);
  $("#tdDenumireRecent").text(comp.denumire);
  $("#tdAdresaRecent").text(comp.adresa);
  $("#tdJudetRecent").text(comp.judet);
  $("#tdTelefonRecent").text(comp.telefon);

  // next button
  $("#nextBtn").click(function () {
    nextSlide();
  });

  // back button
  $("#backBtn").click(function () {
    prevSlide();
  });

  // autoplay
  setInterval(function () {
    nextSlide();
  }, 5000);
});

// function retrievs all data from localStorage
function allStored() {
  var values = [];
  keys = Object.keys(localStorage);
  noOfKeys = keys.length;
  while (noOfKeys--) {
    values[noOfKeys] = localStorage.getItem(keys[noOfKeys]);
  }
  return values;
}
// next slide
function nextSlide() {
  currentIndex = allData.findIndex((element) =>
    element.includes($("#tdCIFRecent").text())
  );
  nextIndex = ++currentIndex;
  if (nextIndex >= allData.length) {
    nextIndex = 0;
  }

  let comp = JSON.parse(allData[nextIndex]);
  $("#tdCIFRecent").text(comp.cif);
  $("#tdDenumireRecent").text(comp.denumire);
  $("#tdAdresaRecent").text(comp.adresa);
  $("#tdJudetRecent").text(comp.judet);
  $("#tdTelefonRecent").text(comp.telefon);
}

//prev slide
function prevSlide() {
  currentIndex = allData.findIndex((element) =>
    element.includes($("#tdCIFRecent").text())
  );
  nextIndex = --currentIndex;
  if (nextIndex < 0) {
    nextIndex = allData.length - 1;
  }

  let comp = JSON.parse(allData[nextIndex]);
  $("#tdCIFRecent").text(comp.cif);
  $("#tdDenumireRecent").text(comp.denumire);
  $("#tdAdresaRecent").text(comp.adresa);
  $("#tdJudetRecent").text(comp.judet);
  $("#tdTelefonRecent").text(comp.telefon);
}
