// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function confirmLogout() {
    if (confirm("Are you sure you want to log out?")) {
        return true;
    } else {
        return false;
    }
}

function UpdateCartBadge() {
    var cartItemsCookie = document.cookie.split(';').find(cookie => cookie.trim().startsWith('CartItems='));

    if (cartItemsCookie) {
        var cartItemsJson = decodeURIComponent(cartItemsCookie.split('=')[1]);
        var cartItems = JSON.parse(cartItemsJson);

        var itemCount = cartItems.length;

        var countBadge = document.querySelector('.badge');

        countBadge.textContent = itemCount;
    } else {
        var countBadge = document.querySelector('.badge');
        countBadge.textContent = '0';
    }
}


UpdateCartBadge();