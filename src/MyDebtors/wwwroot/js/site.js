jQuery(document).ready(function ($) {
    $(".actionLink").click(function () {
        window.document.location = $(this).attr("href");
    });
});