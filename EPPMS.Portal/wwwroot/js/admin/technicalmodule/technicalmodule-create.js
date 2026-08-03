const TechnicalModuleCreate = {

    selectors: {
        form: "#technicalModuleForm",
        saveButton: "#btnSaveTechnicalModule",
        description: "#Description",
        reason: "#Reason",
        releaseImpact: "#ReleaseImpact",
        latestUpdate: "#LatestUpdate",
        notes: "#Notes",
        descriptionCounter: "#descriptionCounter",
        reasonCounter: "#reasonCounter",
        releaseImpactCounter: "#releaseImpactCounter",
        latestUpdateCounter: "#latestUpdateCounter",
        notesCounter: "#notesCounter"
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
            this.selectors.descriptionCounter,
            4000);

        this.updateCounter(
            this.selectors.reason,
            this.selectors.reasonCounter,
            1000);

        this.updateCounter(
            this.selectors.releaseImpact,
            this.selectors.releaseImpactCounter,
            1000);

        this.updateCounter(
            this.selectors.latestUpdate,
            this.selectors.latestUpdateCounter,
            1000);

        this.updateCounter(
            this.selectors.notes,
            this.selectors.notesCounter,
            1000);
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
                    self.selectors.descriptionCounter,
                    4000);
            });

        $(this.selectors.reason)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.reason,
                    self.selectors.reasonCounter,
                    1000);
            });

        $(this.selectors.releaseImpact)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.releaseImpact,
                    self.selectors.releaseImpactCounter,
                    1000);
            });

        $(this.selectors.latestUpdate)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.latestUpdate,
                    self.selectors.latestUpdateCounter,
                    1000);
            });

        $(this.selectors.notes)
            .on("input", function () {
                self.updateCounter(
                    self.selectors.notes,
                    self.selectors.notesCounter,
                    1000);
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
                '<i class="bi bi-floppy me-2"></i> Save Module');
    },

    updateCounter: function (
        textbox,
        counter,
        maxLength) {

        const length = $(textbox).val().length;

        $(counter).text(
            `${length} / ${maxLength}`);
    }

    //showNotifications: function () {
    //    const success = $("#SuccessMessage").val();
    //    if (success) {
    //        toastr.success(success);
    //    }
    //
    //    $(".server-error").each(function () {
    //        const message = $(this).val();
    //        if (message) {
    //            toastr.error(message);
    //        }
    //    });
    //
    //}

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

    TechnicalModuleCreate.initialize();
});