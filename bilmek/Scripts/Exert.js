$(document).ready(function () {

    var acik = false;
    $("#toggle").bind("click", function () {
        if (!acik) {
            $(".carousel-cell, #hakkimizdaBaslik").animate({
                paddingTop: "+=148"
            }, 500, function () { acik = true; });
        }
        else {
            $(".carousel-cell, #hakkimizdaBaslik").animate({
                paddingTop: "-=148"
            }, 500, function () { acik = false; });
        }
        $("#menu").slideToggle();

        return false;
    });
})