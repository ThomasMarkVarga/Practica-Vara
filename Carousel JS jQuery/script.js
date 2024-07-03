const imgArr = [
  "https://buffer.com/cdn-cgi/image/w=1000,fit=contain,q=90,f=auto/library/content/images/size/w600/2023/10/free-images.jpg",
  "https://images.pexels.com/photos/1563356/pexels-photo-1563356.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/235990/pexels-photo-235990.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
  "https://images.pexels.com/photos/92870/pexels-photo-92870.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
];

$(document).ready(function () {
  $("#nextBtn").click(function () {
    var currentIndex = imgArr.indexOf($("#imageContainer").attr("src"));
    var nextIndex = ++currentIndex;
    if (nextIndex >= imgArr.length) {
      nextIndex = 0;
    }
    $("#imageContainer")
      .fadeOut("slow", function () {
        $("#imageContainer").attr("src", imgArr[nextIndex]);
      })
      .fadeIn("slow");
  });

  $("#backBtn").click(function () {
    var currentIndex = imgArr.indexOf($("#imageContainer").attr("src"));
    var nextIndex = --currentIndex;
    if (nextIndex < 0) {
      nextIndex = imgArr.length - 1;
    }

    $("#imageContainer")
      .fadeOut("slow", function () {
        $("#imageContainer").attr("src", imgArr[nextIndex]);
      })
      .fadeIn("slow");
  });
});
