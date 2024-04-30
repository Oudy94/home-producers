// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function confirmLogout() {
    if (confirm("Are you sure you want to log out?")) {
        // If the user confirms, submit the logout form
        document.getElementById("logoutForm").submit();
    }
}
