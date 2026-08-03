const TaskIndex = {

    selectors: {
        board: ".task-board",
        search: "#taskSearch",
        taskCards: ".task-card",
        columnBodies: ".task-column-body",
        filterButton: ".task-filter-button",
        moreActionsButton: ".task-more-actions-button"
    },

    initialize: function () {
        this.registerEvents();
        this.initializeDragAndDrop();
    },

    registerEvents: function () {
        const self = this;

        $(this.selectors.search)
            .on("keyup", function () {
                self.searchTasks($(this).val());
            });

        $(this.selectors.filterButton)
            .on("click", function () {
                self.openFilters();
            });

        $(this.selectors.moreActionsButton)
            .on("click", function () {
                self.openMoreActions();
            });
    },

    initializeDragAndDrop: function () {
        const self = this;

        document.querySelectorAll(this.selectors.taskCards)
            .forEach(function (card) {

                card.addEventListener("dragstart", function (e) {
                    self.onDragStart(e);
                });

                card.addEventListener("dragend", function (e) {
                    self.onDragEnd(e);
                });

            });

        document.querySelectorAll(this.selectors.columnBodies)
            .forEach(function (column) {

                column.addEventListener("dragover", function (e) {
                    self.onDragOver(e);
                });

                column.addEventListener("dragleave", function (e) {
                    self.onDragLeave(e);
                });

                column.addEventListener("drop", function (e) {
                    self.onDrop(e);
                });

            });
    },

    searchTasks: function (searchText) {
        searchText = searchText.toLowerCase();

        $(this.selectors.taskCards).each(function () {

            const card = $(this);

            const value = card.text().toLowerCase();

            card.toggle(value.indexOf(searchText) > -1);

        });
    },

    onDragStart: function (e) {
        e.dataTransfer.setData(
            "taskId",
            e.target.dataset.taskId);

        e.target.classList.add("dragging");
    },

    onDragEnd: function (e) {
        e.target.classList.remove("dragging");

        document
            .querySelectorAll(this.selectors.columnBodies)
            .forEach(function (column) {
                column.classList.remove("drag-over");
            });
    },

    onDragOver: function (e) {
        e.preventDefault();

        e.currentTarget.classList.add("drag-over");
    },

    onDragLeave: function (e) {
        e.currentTarget.classList.remove("drag-over");
    },

    onDrop: function (e) {
        e.preventDefault();

        const taskId =
            e.dataTransfer.getData("taskId");

        const status =
            e.currentTarget.dataset.status;

        const card =
            document.querySelector(
                `[data-task-id="${taskId}"]`);

        if (!card) {
            return;
        }

        e.currentTarget.appendChild(card);

        e.currentTarget.classList.remove("drag-over");

        this.updateTaskStatus(
            taskId,
            status);
    },

    updateTaskStatus: function (
        taskId,
        status) {

        console.log(
            "Update Task:",
            taskId,
            status);

        // TODO:
        // AJAX POST
        // /Admin/Tasks/UpdateStatus

        // Parameters
        // TaskId
        // Status

    },

    openFilters: function () {

        toastr.info(
            "Filter panel will be available soon.");

    },

    openMoreActions: function () {

        toastr.info(
            "More actions will be available soon.");

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

    TaskIndex.initialize();

});