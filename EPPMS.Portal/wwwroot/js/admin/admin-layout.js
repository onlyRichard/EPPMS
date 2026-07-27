document.addEventListener("DOMContentLoaded", () => {
    const layout = document.getElementById("adminLayout");
    const toggle = document.getElementById("sidebarToggle");

    if (!layout || !toggle) {
        return;
    }

    // Default = expanded
    const isCollapsed = localStorage.getItem("sidebar-collapsed") === "true";

    if (isCollapsed) {
        layout.classList.add("collapsed");
    } else {
        layout.classList.remove("collapsed");
    }

    toggle.addEventListener("click", () => {
        layout.classList.toggle("collapsed");

        localStorage.setItem(
            "sidebar-collapsed",
            layout.classList.contains("collapsed")
        );
    });
});