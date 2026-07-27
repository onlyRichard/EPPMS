const ApplicationCreate = {

    selectors: {
        form: "#applicationForm",
        saveButton: "#btnSaveApplication",
        purpose: "#Application_Purpose",
        technicalDetails: "#Application_TechDetails",
        purposeCounter: "#purposeCounter",
        technicalCounter: "#technicalCounter"
    },

    initialize: function () {
        this.initializeValidation();
        this.initializeCharacterCounters();
        this.registerEvents();
        this.showNotifications();
    },

    initializeValidation: function () {
        $.validator.unobtrusive.parse(
            this.selectors.form);
    },

    initializeCharacterCounters: function () {
        this.updateCounter(
            this.selectors.purpose,
            this.selectors.purposeCounter,
            500);
        this.updateCounter(
            this.selectors.technicalDetails,
            this.selectors.technicalCounter,
            1000);
    },

    registerEvents: function () {
        const self = this;
        $(this.selectors.saveButton)
            .on("click", function () {
                self.onSaveClicked();
            });
        $(this.selectors.purpose)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.purpose,
                    self.selectors.purposeCounter,
                    500);
            });
        $(this.selectors.technicalDetails)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.technicalDetails,
                    self.selectors.technicalCounter,
                    1000);
            });
        $(this.selectors.technicalDetails)
            .on("keydown", function (e) {
                self.onTechnicalDetailsKeyDown(e);
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
                '<i class="bi bi-floppy"></i> Save Application');
    },

    updateCounter: function (
        textbox,
        counter,
        maxLength) {
        const length = $(textbox).val().length;
        $(counter).text(
            `${length} / ${maxLength}`);
    },

    showNotifications: function () {
        const success = $("#SuccessMessage").val();
        const error = $("#ErrorMessage").val();
        if (success) {
            toastr.success(success);
        }

        if (error) {
            toastr.error(error);
        }
    },

    onTechnicalDetailsKeyDown: function (e) {

        if (e.key !== "Enter") {
            return;
        }

        e.preventDefault();

        const textarea = e.target;

        let value = textarea.value.trimEnd();

        if (!value) {
            return;
        }

        // Prevent duplicate commas
        value = value.replace(/,+\s*$/, "");

        textarea.value = value + ", ";

        textarea.setSelectionRange(
            textarea.value.length,
            textarea.value.length);

        $(textarea).trigger("input");

    },


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
    ApplicationCreate.initialize();
});

