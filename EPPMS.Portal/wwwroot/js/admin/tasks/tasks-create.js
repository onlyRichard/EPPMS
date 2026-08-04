const TaskCreate = {

    selectors: {

        form: "#taskForm",

        saveButton: "#btnSaveTask",

        relationshipRadios: "input[name='LinkType']",

        featureContainer: "#featureLookupContainer",
        technicalModuleContainer: "#technicalModuleLookupContainer",
        bugContainer: "#bugLookupContainer",

        featureSelect: "#Task_FeatureId",
        technicalModuleSelect: "#Task_TechModuleId",
        bugSelect: "#Task_BugId"

    },

    initialize: function () {

        this.initializeValidation();
        this.initializeRelationship();
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

        // Handle click immediately
        $(this.selectors.relationshipRadios)
            .on("input click", function () {

                self.onRelationshipChanged(
                    this.value);

            });

        // Handle keyboard navigation (Tab + Space/Arrow keys)
        $(this.selectors.relationshipRadios)
            .on("change", function () {

                self.onRelationshipChanged(
                    this.value);

            });

    },

    initializeRelationship: function () {

        this.hideAllLookups();

        const selectedRelationship =
            $(this.selectors.relationshipRadios + ":checked");

        if (selectedRelationship.length > 0) {

            this.onRelationshipChanged(
                selectedRelationship.val());

        }

    },

    onRelationshipChanged: function (relationship) {

        this.hideAllLookups();

        switch (relationship) {

            case "Feature":

                $(this.selectors.featureContainer).show();

                break;

            case "TechnicalModule":

                $(this.selectors.technicalModuleContainer).show();

                break;

            case "Bug":

                $(this.selectors.bugContainer).show();

                break;

        }

    },

    hideAllLookups: function () {

        $(this.selectors.featureContainer).hide();
        $(this.selectors.technicalModuleContainer).hide();
        $(this.selectors.bugContainer).hide();

        $(this.selectors.featureSelect).val("");
        $(this.selectors.technicalModuleSelect).val("");
        $(this.selectors.bugSelect).val("");

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
                '<i class="bi bi-floppy"></i> Save Task');

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

    TaskCreate.initialize();

});