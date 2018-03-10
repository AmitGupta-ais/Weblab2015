using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for clsTableNames
/// </summary>
namespace WebLabMaster
{
    public static class clsTableNames
    {  
        public static string LabTestTablename = "LabTest";
        public static string TestLibTablename = "TestLib";
        public static string TestlibHeadTablename = "TestlibHead";
        public static string PatientTablename = "Patient";
        public static string DeptartmentTablename = "Dept";
        public static string LabMTablename = "LabM";
        public static string WebReportSettingsTablename = "WebReportSettings";
        public static string RepoSettingsTablename = "reposettings";
        public static string ConsultantTablename = "ConsMast";
        public static string LabemailTablename = "Labemail";
        public static string ReportTunningTableName = "Systun";
    }
    public static class CRMConstants
    {
        public static string LOGINUSER = "LOGINUSER";
        public static string USERTYPE = "USERTYPE";
        public static string PATIENTCODE = "PATIENTCODE";
        public static string DATABASE = "DATABASE";
    }
}
