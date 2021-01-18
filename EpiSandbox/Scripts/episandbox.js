console.log('episandbox.js');

function tryLogin() {
    console.log('tryLogin()');

    var data = {
        login: $('#tbLoginLogin').val(),
        password: $('#tbLoginPassword').val()
    };

    $.post("/login", data, function (response) {
        console.log("success");
        if (response.success) {
            location.reload(true);
        } else {
            $('#loginErrorMessage').html(response.message);
            $('#loginErrorMessage').show();
            $('#tbLoginPassword').val('');
        }
    })
        .fail(function () {
            console.log("error");
            $('#loginErrorMessage').html('Error occured during login');
            $('#loginErrorMessage').show();
        });
}

function trySignIn() {
    console.log('trySignIn()');

    var data = {
        login: $('#tbSigninLogin').val(),
        email: $('#tbSigninEmail').val(),
        password: $('#tbSigninPassword').val(),
        passwordconfirmation: $('#tbSigninConfirmPassword').val()
    };

    $.post("/register", data, function (response) {
        console.log("success");
        if (response.success) {
            location.reload(true);
        } else {
            $('#signinErrorMessage').html(response.message);
            $('#signinErrorMessage').show();
        }
    })
        .fail(function () {
            console.log("error");
            $('#signinErrorMessage').html('Error occured during login');
            $('#signinErrorMessage').show();
        });
}

function logoff() {
    console.log('logoff()');

    $.post("/logoff", function (response) {
    })
        .always(function () {
            location.reload(true);
        });
}

// navbar transition js
$(document).ready(function () {
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        if (scroll > 0) {
            $(".navbar").addClass("navbar-scroll");
        }
        else {
            $(".navbar").removeClass("navbar-scroll");
        }
        if (scroll > 200) {
            $(".navbar").addClass("nav-bg");
        }
        else {
            $(".navbar").removeClass("nav-bg");
        }
    })
});