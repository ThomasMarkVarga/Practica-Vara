var index = 0;
// divId - id div in care vrem sa punem carouselul
// data - array care contine html cu continut carousel
// timer (optional) - cat de repede merge animatia
function createCarousel(divId, data, timer) {
  if (timer === undefined) timer = 5000;

  $(divId).html(
    `<div id="carouselContent">` +
      data[index] +
      `</div>    <button id="backBtn">Back</button>
    <button id="nextBtn">Next</button>`
  );

  autoPlay(data, timer);
  $("#backBtn").click(function () {
    prevSlide(data);
  });
  $("#nextBtn").click(function () {
    nextSlide(data);
  });
}

// next slide
function nextSlide(data) {
  index++;
  if (index >= data.length) index = 0;
  $("#carouselContent").html(data[index]);
}

//prev slide
function prevSlide(data) {
  index--;
  if (index < 0) index = data.length - 1;
  $("#carouselContent").html(data[index]);
}

// autoplay
function autoPlay(data, timer) {
  setInterval(function () {
    nextSlide(data);
  }, timer);
}

export { createCarousel };
