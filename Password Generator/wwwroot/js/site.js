// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    function copyPassword() {
        var passwordText = document.getElementById("password-box").innerText;
    navigator.clipboard.writeText(passwordText).then(function() {
        alert("Password copied to clipboard!");
        }, function(err) {
        console.error("Could not copy text: ", err);
        });
    }
</script>

