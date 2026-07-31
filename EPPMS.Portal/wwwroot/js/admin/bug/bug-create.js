const BugCreate = {

    selectors: {
        form: "#bugForm",
        saveButton: "#btnSaveBug",

        description: "#Bug_Description",
        userBusinessImpact: "#Bug_UserBusinessImpact",
        rootCause: "#Bug_RootCause",
        reproductionSteps: "#Bug_ReproductionSteps",
        commentsUpdates: "#Bug_CommentsUpdates"
    },

    initialize: function () {
        this.initializeValidation();
        this.initializeCharacterCounters();
        this.registerEvents();
        //this.showNotifications();
    },

    initializeValidation: function () {
        $.validator.unobtrusive.parse(
            this.selectors.form);
    },

    initializeCharacterCounters: function () {

        this.updateCounter(
            this.selectors.description,
            4000);

        this.updateCounter(
            this.selectors.userBusinessImpact,
            1000);

        this.updateCounter(
            this.selectors.rootCause,
            1000);

        this.updateCounter(
            this.selectors.reproductionSteps,
            2000);

        this.updateCounter(
            this.selectors.commentsUpdates,
            4000);

    },

    registerEvents: function () {

        const self = this;

        $(this.selectors.saveButton)
            .on("click", function () {
                self.onSaveClicked();
            });

        $(this.selectors.description)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.description,
                    4000);
            });

        $(this.selectors.userBusinessImpact)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.userBusinessImpact,
                    1000);
            });

        $(this.selectors.rootCause)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.rootCause,
                    1000);
            });

        $(this.selectors.reproductionSteps)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.reproductionSteps,
                    2000);
            });

        $(this.selectors.commentsUpdates)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.commentsUpdates,
                    4000);
            });

    },

    onSaveClicked: function () {

        const form = $(this.selectors.form);

        if (!form.valid()) {

            toastr.warning(
                "Please complete all required fields.");

            return;

        }

        this.disableSaveButton();

        form.trigger("submit");

    },

    disableSaveButton: function () {

        $(this.selectors.saveButton)
            .prop("disabled", true)
            .html(
                '<i class="spinner-border spinner-border-sm me-2"></i>Saving...');

    },

    enableSaveButton: function () {

        $(this.selectors.saveButton)
            .prop("disabled", false)
            .html(
                '<i class="bi bi-floppy"></i> Save Bug');

    },

    updateCounter: function (
        textbox,
        maxLength) {

        const control = $(textbox);

        const length = control.val().length;

        control
            .closest(".col-md-6, .col-md-7, .col-12")
            .find(".char-count")
            .text(length);

    }

};

$(function () {

    toastr.options = {
        closeButton: true,
        progressBar: true,
        newestOnTop: true,
        positionClass: "toast-top-right",
        preventDuplicates: true,
        timeOut: 3000
    };

    BugCreate.initialize();

});