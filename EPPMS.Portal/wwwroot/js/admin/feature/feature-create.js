const FeatureCreate = {

    selectors: {
        form: "#featureForm",
        saveButton: "#btnSaveFeature"
    },

    initialize: function () {

        this.initializeValidation();
        this.registerEvents();

    },

    initializeValidation: function () {

        $.validator.unobtrusive.parse(
            this.selectors.form);

    },

    registerEvents: function () {

        const self = this;

        $(this.selectors.saveButton)
            .on("click", function () {

                self.onSaveClicked();

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
                '<i class="bi bi-floppy"></i> Save Feature');

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

    FeatureCreate.initialize();

});