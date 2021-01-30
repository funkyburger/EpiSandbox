function tryLogin() {
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

function trySignIn() {
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

function logoff() {
    $.post("/logoff", function (response) {
    })
        .always(function () {
            location.reload(true);
        });
}

function onSearchBoxTyping() {
    var queryString = $('#searchField').val();

    if (queryString.length > 3) {
        $.get("api/search/?query=" + queryString, displaySearchSuggestions)
            .fail(function () {
                console.log("An error occured during search.");
                $('#searchSuggestion').hide();
            });
    }
    else {
        $('#searchSuggestion').hide();
    }
}

function appendSearchSuggestion(searchHit) {
    console.log('appendSearchSuggestion');

    var tr = $("<tr/>");
    var td = $("<td/>");
    var a = $("<a/>", { "href": searchHit.Link });
    var div = $("<div/>");
    div.html(searchHit.Headline);
    div.appendTo(a);
    a.appendTo(td);
    td.appendTo(tr);
    tr.appendTo($('#searchSuggestion > tbody'));
}

function displaySearchSuggestions(results) {
    $('#searchSuggestion > tbody > tr').remove();

    if (results.length > 0) {
        for (var i = 0; i < results.length; i++) {
            appendSearchSuggestion(results[i]);
        }

        $('#searchSuggestion').show();
    }
    else {
        $('#searchSuggestion').hide();
    }
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
    var dropdowns = $(".dropdown");
    var dropdownToggles = $(".dropdown-toggle");
    var dropdownMenus = $(".dropdown-menu");

    if (this.matchMedia("(min-width: 768px)").matches) {
        dropdowns.hover(
            function () {
                const $this = $(this);
                $this.addClass("show");
                $this.find(dropdownToggles).attr("aria-expanded", "true");
                $this.find(dropdownMenus).addClass("show");
            },
            function () {
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