/// <reference path="../jquery/jquery.d.ts"/>
/// <reference path="../es6-promise/es6-promise.d.ts"/>

class LoginHelper {
    constructor() {
    }

    public tryLogin(): void {
        var data = {
            login: $('#tbLoginLogin').val(),
            password: $('#tbLoginPassword').val()
        };

        $.post("/login", data, function (response) {
            if (response.success) {
                location.reload(true);
            } else {
                $('#loginErrorMessage').html(response.message);
                $('#loginErrorMessage').show();
                $('#tbLoginPassword').val('');
            }
        })
            .fail(function () {
                $('#loginErrorMessage').html('Error occured during login');
                $('#loginErrorMessage').show();
            });
    }

    public trySignIn(): void {
        var data = {
            login: $('#tbSigninLogin').val(),
            email: $('#tbSigninEmail').val(),
            password: $('#tbSigninPassword').val(),
            passwordconfirmation: $('#tbSigninConfirmPassword').val()
        };

        $.post("/register", data, function (response) {
            if (response.success) {
                location.reload(true);
            } else {
                $('#signinErrorMessage').html(response.message);
                $('#signinErrorMessage').show();
            }
        })
            .fail(function () {
                $('#signinErrorMessage').html('Error occured during login');
                $('#signinErrorMessage').show();
            });
    }

    public logoff() {
        $.post("/logoff", function (response) {
        })
            .always(function () {
                location.reload(true);
            });
    }
}
