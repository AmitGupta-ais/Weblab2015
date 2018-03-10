using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using AISWebCommon;

/// <summary>
/// Summary description for clsTestHistoryDetail
/// </summary>
namespace WebLabMaster
{
    public class clsTestHistoryDetail
    {
        public string TestCode, TestName, TestValue;
        public DateTime TestDate;
        
        string LabTestTablename=clsTableNames.LabTestTablename;
        string TestLibTablename=clsTableNames.TestLibTablename;
        
        public clsTestHistoryDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetData(string TestGroup,string PatientCode)
        {   
            string qry = "Select " + LabTestTablename + ".TDate," + LabTestTablename + ".TCode," + TestLibTablename + ".Name," + LabTestTablename + ".TVal from " + LabTestTablename + " Left Outer Join " + TestLibTablename + " on " + LabTestTablename + ".tcode=" + TestLibTablename + ".code Where PCode='" + PatientCode + "' and GRP='" + TestGroup + "' Order by " + LabTestTablename + ".TDate";
            DataTable dt=new DataTable();
            dt = Common.GetTableFromSession(qry, "Table", null, null);

            return dt;
        }
    } 
}
