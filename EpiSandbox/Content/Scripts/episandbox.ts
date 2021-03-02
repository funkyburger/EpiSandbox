/// <reference path="./jquery/jquery.d.ts"/>
/// <reference path="./Search/SearchResultDisplayer.ts"/>
/// <reference path="./Navbar/LoginHelper.ts"/>
/// <reference path="./Navbar/NavbarHelper.ts"/>

var loginHelper = new LoginHelper();

$(() => {
    var loginButton = $('#loginButton');
    loginButton.mouseup(eventObject => {
        loginHelper.tryLogin();
    });

    var tbLoginLogin = $('#tbLoginLogin');
    tbLoginLogin.keypress(eventObject => {
        if (eventObject.keyCode == 13) {
            loginHelper.tryLogin();
        }
    });

    var tbLoginPassword = $('#tbLoginPassword');
    tbLoginPassword.keypress(eventObject => {
        if (eventObject.keyCode == 13) {
            loginHelper.tryLogin();
        }
    });

    var signinButton = $('#signinButton');
    signinButton.mouseup(eventObject => {
        loginHelper.trySignIn();
    });

    var tbSigninLogin = $('#tbSigninLogin');
    tbSigninLogin.keypress(eventObject => {
        if (eventObject.keyCode == 13) {
            loginHelper.trySignIn();
        }
    });

    var tbSigninEmail = $('#tbSigninEmail');
    tbSigninEmail.keypress(eventObject => {
        if (eventObject.keyCode == 13) {
            loginHelper.trySignIn();
        }
    });

    var tbSigninPassword = $('#tbSigninPassword');
    tbSigninPassword.keypress(eventObject => {
        if (eventObject.keyCode == 13) {
            loginHelper.trySignIn();
        }
    });

    var tbSigninConfirmPassword = $('#tbSigninConfirmPassword');
    tbSigninConfirmPassword.keypress(eventObject => {
        if (eventObject.keyCode == 13) {
            loginHelper.trySignIn();
        }
    });

    var logoffLink = $('#logoffLink');
    logoffLink.mouseup(eventObject => {
        loginHelper.logoff();
    });

    var searchButton = $('#btnSearch');

    searchButton.mouseup(eventObject => {
        console.log('searchButton.mouseup');

        var query = $('#searchField').val();

        if (query.length > 0) {
            window.location.href = "/search?q=" + query;
        }
    });

    var searchField = $('#searchField');
    searchField.keypress(eventObject => {
        console.log('searchField.keypress');

        if (eventObject.keyCode == 13) {
            eventObject.preventDefault();

            var query = $('#searchField').val();

            if (query.length > 0) {
                window.location.href = "/search?q=" + query;
            }
        }
    });

    var resultTable = $('.result-table');

    var query = window.location.GetUrlParameterValue("q");

    if (resultTable.length > 0) {
        let resultDisplayer = new SearchResultDisplayer($('.result-table'), query);

        resultDisplayer.loadNextResults();

        window.addEventListener("scroll", function () {
            if (resultDisplayer.isTableSrolledToBottom()) {
                resultDisplayer.loadNextResults();
            }
        });
    }
});

// navbar transition js
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

function blah(): void {

}