$(document).ready(function () {
    $("#DivMsg").css("display", "none");
});
$(".icon").on("mouseover", function () {
    if ($(this).hasClass("green"))
        $(this).css("background-color", "lightgreen");
    if ($(this).hasClass("blue"))
        $(this).css("background-color", "aqua");
    if ($(this).hasClass("yellow"))
        $(this).css("background-color", "yellow");
    if ($(this).hasClass("orange"))
        $(this).css("background-color", "orange");
    if ($(this).hasClass("red"))
        $(this).css("background-color", "red");
});
$(".icon").on("mouseout", function () {
    $(this).css("background-color", "");
});
$(".icon").on("click", function (e) {
    rebindMouseOut();
    $(this).unbind("mouseout");
    if ($(this).hasClass("green")) {
        $(this).css("background-color", "lightgreen");
        $("#question1").val("5");
    }
    if ($(this).hasClass("blue")) {
        $(this).css("background-color", "aqua");
        $("#question1").val("4");
    }
    if ($(this).hasClass("yellow")) {
        $(this).css("background-color", "yellow");
        $("#question1").val("3");
    }
    if ($(this).hasClass("orange")) {
        $(this).css("background-color", "orange");
        $("#question1").val("2");
    }
    if ($(this).hasClass("red")) {
        $(this).css("background-color", "red");
        $("#question1").val("1");
    }
    $("#DivMsg").css("display", "block");
    $("#rating").text("Rating: " + $("#question1").val());
    $("#msg").text("");
    
    return false;
});
function rebindMouseOut() {
    $(".icon").css("background-color", "");
    $(".icon").bind("mouseout", function () {
        $(this).css("background-color", "");
    });
}

$("form").submit(function (event) {
    if ($("#question1").val() == '') {
        $("#DivMsg").css("display", "block");
        $("#msg").text("Question 1 is mandatory");
        event.preventDefault();
        return;
    }
    else if ($("#question1").val() < 3 && $.trim($('#question3').val()) == '') {
        $("#msg").text("Question 3 is mandatory");
        event.preventDefault();
        return;
    }   
    
});
