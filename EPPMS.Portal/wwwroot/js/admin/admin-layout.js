document.addEventListener("DOMContentLoaded", () => {

    const layout = document.getElementById("adminLayout");
    const toggle = document.getElementById("sidebarToggle");

    if (layout && toggle) {

        // Default = Expanded
        const isCollapsed = localStorage.getItem("sidebar-collapsed") === "true";

        if (isCollapsed) {
            layout.classList.add("collapsed");
        }
        else {
            layout.classList.remove("collapsed");
        }

        toggle.addEventListener("click", () => {

            layout.classList.toggle("collapsed");

            localStorage.setItem(
                "sidebar-collapsed",
                layout.classList.contains("collapsed")
            );
        });
    }

    // ============================================
    // Global Notifications
    // ============================================

    const success = document.getElementById("SuccessMessage")?.value;
    const error = document.getElementById("ErrorMessage")?.value;
    const warning = document.getElementById("WarningMessage")?.value;
    const info = document.getElementById("InfoMessage")?.value;

    if (success) {
        toastr.success(success);
    }

    if (error) {
        toastr.error(error);
    }

    if (warning) {
        toastr.warning(warning);
    }

    if (info) {
        toastr.info(info);
    }

});