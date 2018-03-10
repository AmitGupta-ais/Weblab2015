using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AISWebCommon;
/// <summary>
/// Summary description for clsConsultMenu
/// </summary>
namespace WebLabMaster
{
    public enum ConsultantMenuType
    {
        Datewise,
        Patientwise
    }
    public class clsConsultMenu
    {
        clsConsultantLogin objConsLogin = new clsConsultantLogin();
        string LabMTablename = clsTableNames.LabMTablename;
        string ConsMastTablename = clsTableNames.ConsultantTablename;
        public clsConsultMenu()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetData(string strConsCode,bool isDate, DateTime dtFrom,DateTime dtTo, ConsultantMenuType enmConsMenuType)
        {
            DataTable dt;
            string qry = "";

            string wherecl = "";
            if (isDate)
            {
                wherecl = wherecl + " and " + LabMTablename + ".TDate >=" + Common.GetDateString(dtFrom) + " and " + LabMTablename + ".TDate <=" + Common.GetDateString(dtTo);
            }
            if (enmConsMenuType == ConsultantMenuType.Patientwise)
            {
                qry = "select distinct " + LabMTablename + ".pcode, " + LabMTablename + ".patname from " + LabMTablename + " left outer join " + ConsMastTablename + " on " + ConsMastTablename + ".code=" + LabMTablename + ".refcon where " + LabMTablename + ".refcon='" + strConsCode + "'" + wherecl + " Order By " + LabMTablename + ".patname";
            }
            else if(enmConsMenuType== ConsultantMenuType.Datewise)
            {
                qry = "select distinct " + LabMTablename + ".LabNo," + LabMTablename + ".TDate," + LabMTablename + ".patname from " + LabMTablename + " left outer join " + ConsMastTablename + " on " + ConsMastTablename + ".code=" + LabMTablename + ".refcon where " + LabMTablename + ".refcon='" + strConsCode + "'" + wherecl + " Order By " + LabMTablename + ".TDate";
            }
            dt = Common.GetTableFromSession(qry, "Table", null, null);
            return dt;
        }
    } 
}
