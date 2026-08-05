const ErrorPage = {

    selectors: {
        goBackButton: "#btnGoBack",
        dashboardButton: "#btnDashboard"
    },

    initialize: function () {

        this.registerEvents();

    },

    registerEvents: function () {

        const self = this;

        $(this.selectors.goBackButton)
            .on("click", function () {

                self.goBack();

            });

        $(this.selectors.dashboardButton)
            .on("click", function (e) {

                self.navigateToDashboard(e);

            });

    },

    goBack: function () {

        if (window.history.length > 1) {

            window.history.back();

            return;

        }

        window.location.href = "/";

    },

    navigateToDashboard: function (e) {

        const dashboardUrl =
            $(e.currentTarget).attr("href");

        if (!dashboardUrl) {

            return;

        }

        window.location.href = dashboardUrl;

    }

};

$(function () {

    ErrorPage.initialize();

});