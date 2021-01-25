//console.log('episandbox.js');

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

// navbar dropdowns on mouse hover
$(window).on("load resize", function () {
    console.log("load resize");

    var dropdowns = $(".dropdown");
    var dropdownToggles = $(".dropdown-toggle");
    var dropdownMenus = $(".dropdown-menu");

    if (this.matchMedia("(min-width: 768px)").matches) {
        console.log("(min-width: 768px)");

        console.log(dropdowns.length);

        dropdowns.hover(
            function () {
                console.log("hover in");

                const $this = $(this);
                $this.addClass("show");
                $this.find(dropdownToggles).attr("aria-expanded", "true");
                $this.find(dropdownMenus).addClass("show");
            },
            function () {
                console.log("hover out");

                const $this = $(this);
                $this.removeClass("show");
                $this.find(dropdownToggles).attr("aria-expanded", "false");
                $this.find(dropdownMenus).removeClass("show");
            }
        );
    } else {
        dropdowns.off("mouseenter mouseleave");
    }
});