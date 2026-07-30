using EPPMS.Portal.Models.Navigation;

namespace EPPMS.Portal.Configuration;

public static class AdminNavigation
{
    public static readonly IReadOnlyList<NavigationItem> Items =
    [
        // Dashboard

        new()
        {
            Title = "Dashboard",
            Icon = "bi-house",
            Area = "Admin",
            Route = "/Dashboard/Index"
        },

        // Product Management

        new()
        {
            Title = "Applications",
            Icon = "bi-grid",
            Area = "Admin",
            Route = "/Applications/Index"
        },

        new()
        {
            Title = "Features & Epics",
            Icon = "bi-diagram-3",
            Area = "Admin",
            Route = "/Features/Index"
        },

        new()
        {
            Title = "Technical Modules",
            Icon = "bi-box-seam",
            Area = "Admin",
            Route = "/TechnicalModules/Index"
        },

        new()
        {
            Title = "Tasks",
            Icon = "bi-list-task",
            Area = "Admin",
            Route = "/Tasks/Index"
        },

        new()
        {
            Title = "Bugs & Issues",
            Icon = "bi-bug",
            Area = "Admin",
            Route = "/Bugs/Index"
        },

        new()
        {
            Title = "Ongoing Tasks",
            Icon = "bi-clock-history",
            Area = "Admin",
            Route = "/OngoingTasks/Index"
        },

        // Reports

        //new()
        //{
        //    DividerBefore = true,
        //    Title = "Reports",
        //    Icon = "bi-bar-chart-line",
        //    Area = "Admin",
        //    Route = "/Reports/Index"
        //}
    ];
}