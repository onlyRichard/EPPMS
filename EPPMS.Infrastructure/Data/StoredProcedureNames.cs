using System;
using System.Collections.Generic;
using System.Text;

namespace EPPMS.Infrastructure.Data
{
    public static class StoredProcedureNames
    {
        public static class Lookup
        {
            public const string ActionType = "dbo.usp_ActionType_Get";
            public const string Complexity = "dbo.usp_Complexity_Get";
            public const string CurrentHealth = "dbo.usp_CurrentHealth_Get";
            public const string Priority = "dbo.usp_Priority_Get";
            public const string ReleaseStatus = "dbo.usp_ReleaseStatus_Get";
            public const string RequestType = "dbo.usp_RequestType_Get";
            public const string Severity = "dbo.usp_Severity_Get";
            public const string Status = "dbo.usp_Status_Get";
            public const string TechnologyArea = "dbo.usp_TechnologyArea_Get";
            public const string TestingStatus = "dbo.usp_TestingStatus_Get";
            public const string Type = "dbo.usp_Type_Get";
            public const string Application = "dbo.usp_Application_GetLookup";
        }
        public static class User
        {
            public const string Get = "dbo.usp_User_Get";
            public const string GetByMSID = "dbo.usp_User_GetByMSID";
            public const string Upsert = "dbo.usp_User_Upsert";
            public const string Update = "dbo.usp_User_Update";
            public const string Delete = "dbo.usp_User_Delete";
        }

        public static class Application
        {
            public const string Create = "dbo.usp_Application_Create";
            public const string Get = "dbo.usp_Application_Get";
            public const string GetById = "dbo.usp_Application_GetById";
            public const string Update = "dbo.usp_Application_Update";
            public const string Delete = "dbo.usp_Application_Delete";
        }

        public static class Feature
        {
            public const string Create = "dbo.usp_Feature_Create";
            public const string Get = "dbo.usp_Feature_Get";
            public const string GetById = "dbo.usp_Feature_GetById";
            public const string Update = "dbo.usp_Feature_Update";
            public const string Delete = "dbo.usp_Feature_Delete";
        }

        public static class TechnicalModule
        {
            public const string Create = "dbo.usp_Task_Create";
            public const string Get = "dbo.usp_Task_Get";
            public const string GetById = "dbo.usp_Task_GetById";
            public const string Update = "dbo.usp_Task_Update";
            public const string Delete = "dbo.usp_Task_Delete";
        }

        public static class Task
        {
            public const string Get = "usp_Task_Get";
            public const string GetById = "usp_Task_GetById";
            public const string Create = "usp_Task_Create";
            public const string Update = "usp_Task_Update";
            public const string Delete = "usp_Task_Delete";
        }

        public static class Bug
        {
            public const string Create = "dbo.usp_Bug_Create";
            public const string Get = "dbo.usp_Bug_Get";
            public const string GetById = "dbo.usp_Bug_GetById";
            public const string Update = "dbo.usp_Bug_Update";
            public const string Delete = "dbo.usp_Bug_Delete";
        }

        public static class OngoingTask
        {
            public const string Create = "dbo.usp_OngoingTask_Create";
            public const string Get = "dbo.usp_OngoingTask_Get";
            public const string GetById = "dbo.usp_OngoingTask_GetById";
            public const string Update = "dbo.usp_OngoingTask_Update";
            public const string Delete = "dbo.usp_OngoingTask_Delete";
        }
    }
}
