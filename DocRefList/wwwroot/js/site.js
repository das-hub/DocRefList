// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".custom-file-input").on("change", function (e) {
    let filenames = [];
    let files = e.target.files;
    if (files.length > 1) {
        filenames.push("Выбрано (" + files.length + ")");
    } else {
        for (let i in files) {
            if (files.hasOwnProperty(i)) {
                filenames.push(files[i].name);
            }
        }
    }
    $(this).siblings(".custom-file-label").addClass("selected").html(filenames.join(","));
});

$("#openList").click(function () {
    $("#list").show();
});
$("#closeList").click(function () {
    $("#list").hide();
});
