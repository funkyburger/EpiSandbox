console.log('episandbox.js');

function tryLogin() {
    console.log('tryLogin()');

    var data = { login: $('#tbLoginLogin').val(), password: $('#tbLoginPassword').val() };

    $.post("/login", data, function (response) {
        console.log("success");
        if (response.success) {
            location.reload(true);
        } else {
            $('#loginErrorMessage').html(response.message);
            $('#loginErrorMessage').show();
        }
    })
        .fail(function () {
            console.log("error");
            $('#loginErrorMessage').html('Error occured during login');
            $('#loginErrorMessage').show();

        });
}

