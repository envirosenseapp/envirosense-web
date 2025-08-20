// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function confirmLogout(event) {
    event.preventDefault(); 
    if (confirm("Are you sure you want to log out?")) {
        window.location.href = '@Url.Action("LogOut", "Accounts")';
    }
}
