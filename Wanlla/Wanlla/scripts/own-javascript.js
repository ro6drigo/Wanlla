/* JS Document */

/* Favorite Icon
-------------------------------------------------- */

$(".favorite-icon-inactive").hover(function () {
    $(this).removeClass('fa-star-o');
    $(this).addClass('fa-star');
}, function () {
    $(this).removeClass('fa-star');
    $(this).addClass('fa-star-o');
});

$(".favorite-icon-active").hover(function () {
    $(this).removeClass('fa-star');
    $(this).addClass('fa-star-o');
}, function () {
    $(this).removeClass('fa-star-o');
    $(this).addClass('fa-star');
});

/* ----------------------------------------------- */