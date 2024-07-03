import { nextSlide, populateCarousel, prevSlide } from "./script2.js";
const API_KEY = "8AxoCqMAn-XYnRyziJPdzKxvda_FBNmnD6RgsZd8GkRZ5pEvxg";
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
  populateCarousel();

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
