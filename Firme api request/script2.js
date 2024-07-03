const allData = allStored();
//populate the carousel for the first time
function populateCarousel() {
  let comp = JSON.parse(allData[0]);
  $("#tdCIFRecent").text(comp.cif);
  $("#tdDenumireRecent").text(comp.denumire);
  $("#tdAdresaRecent").text(comp.adresa);
  $("#tdJudetRecent").text(comp.judet);
  $("#tdTelefonRecent").text(comp.telefon);
}

// next slide
function nextSlide() {
  var currentIndex = allData.findIndex((element) =>
    element.includes($("#tdCIFRecent").text())
  );
  var nextIndex = ++currentIndex;
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
  var currentIndex = allData.findIndex((element) =>
    element.includes($("#tdCIFRecent").text())
  );
  var nextIndex = --currentIndex;
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

export { populateCarousel, nextSlide, prevSlide };
