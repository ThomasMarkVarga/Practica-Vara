const imgArr = [
  "https://buffer.com/cdn-cgi/image/w=1000,fit=contain,q=90,f=auto/library/content/images/size/w600/2023/10/free-images.jpg",
  "https://images.pexels.com/photos/1563356/pexels-photo-1563356.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/235990/pexels-photo-235990.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/92870/pexels-photo-92870.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
];

const animTimer = 50;

function nextPhoto() {
  let indexCurrentImg = currentIndexFinder();
  let indexNextImg = indexCurrentImg + 1;
  if (indexNextImg >= imgArr.length) {
    indexNextImg = 0;
  }
  fade(document.getElementById("imageContainer"), indexNextImg, animTimer);
}

function backPhoto() {
  let indexCurrentImg = currentIndexFinder();
  let indexNextImg = indexCurrentImg - 1;
  if (indexNextImg < 0) {
    indexNextImg = imgArr.length - 1;
  }
  fade(document.getElementById("imageContainer"), indexNextImg, animTimer);
}

function currentIndexFinder() {
  return imgArr.indexOf(document.getElementById("imageContainer").src);
}

function fade(element, index, timer) {
  var elementOpacity = 1;
  let interval = setInterval(function () {
    element.style.opacity = elementOpacity;
    elementOpacity -= 0.1;
    if (elementOpacity <= 0) {
      element.style.opacity = 0;
      element.src = imgArr[index];
      stopFunction(interval);
      fadeIn(element, timer);
    }
  }, timer);
}

function fadeIn(element, timer) {
  var elementOpacity = 0;
  let interval = setInterval(function () {
    element.style.opacity = elementOpacity;
    elementOpacity += 0.1;
    if (elementOpacity >= 1) {
      element.style.oapcity = 1;
      stopFunction(interval);
    }
  }, timer);
}

function stopFunction(interval) {
  clearInterval(interval);
}
