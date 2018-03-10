using AISWebCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using WebLab.Classes;
using WebLabMaster;

namespace WebLab.Forms
{
    public class clsTest
    {
        public string Code { get; set; }
        public string LabID { get; set; }
        public string Method { get; set; }
        public double Rate { get; set; }
        public string Name { get; set; }
        public string LabName { get; set; }
        public bool valid { get; set; }
        public bool testValidity { get; set; }


    }
    [System.Web.Script.Services.ScriptService]
    public partial class frmDeptWiseTestLib : System.Web.UI.Page
    {
        private string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qry = "select * from Dept";
                DataTable dt = Common.GetTableFromSession(qry, "Dept", null, null);
                ddlDept.DataSource = dt;

                ddlDept.DataTextField = "Name";
                ddlDept.DataValueField = "Code";
                ddlDept.DataBind();
            }
            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                Response.Redirect("~/frmcommonlogin.aspx",false);
                username = Common.MyCStr(Session["lname"]);
            }
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static List<clsTest> loadData(string dept)
        {
            List<clsTest> retdata = new List<clsTest>();
            string qry1 = "SELECT apdept from labapdepttr where dept='" + Common.MyCStr(dept) + "'";

            DataTable dt = Common.GetTableFromSession(qry1, "LabDept", null, null);
            List<string> coll1 = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                coll1.Add(Common.MyCStr(dt.Rows[i]["apdept"]).Trim());
            }
            coll1.Add(Classes.CommonFunctions.getMasterValue("Code", Common.MyCStr(dept).Trim(), "Dept", "apdept", false));
            string STRapUSERNAME;
            clsReport obj = new clsReport();
            STRapUSERNAME = obj.gettunvar("APUSERNM").Trim();
            STRapUSERNAME += "..";
            string qry = String.Empty;
            if (STRapUSERNAME.Length > 0)
            {
                qry = "Select S.Code,S.Name,S.Rate, (select max(M.lcode) from " + STRapUSERNAME + "maplab M where M.acode=S.code) as labID  ";
                qry += " from " + STRapUSERNAME + "Service S where S.grp in " + Common.GetmultiCollString(coll1).Trim();
                qry += " and (S.notinuse is null or S.notinuse <>1) ";
                qry = "Select Final.Code,Final.Name,Final.Rate,Final.LabID,TL.Name TLName,TL.ispf,TL.ispkg from ( " + Environment.NewLine + qry + Environment.NewLine + " )   Final left outer join Testlib TL on Final.Labid=TL.code  order by Final.Name ";
                
               
            }
            else
            {
                qry = "Select Code,Name,Rate, (select max(lcode) from maplab where maplab.acode=service.code) as labID ,' ' TLNAME ";
                qry = qry + " from Service where grp in " + Common.GetmultiCollString(coll1);
                qry = qry + " and (notinuse is null or notinuse <>1) order by Name";
                
                
            }
            DataTable dtFinal = Common.GetTableFromSession(qry, "", null, null);
            if (dtFinal.Rows.Count > 0)
            {
                foreach (DataRow dr in dtFinal.Rows)
                {
                    clsTest ob = new clsTest();
                    ob.Code = Common.MyCStr(dr["CODE"]);
                    ob.Name = Common.MyCStr(dr["NAME"]);
                    ob.Rate = Common.MyCDbl(dr["RATE"]);
                    ob.LabID = Common.MyCStr(dr["labID"]);
                    ob.LabName = Common.MyCStr(dr["TLName"]);
                    if (Common.MyLen(Common.MyCStr(dr["labID"])) == 0)
                    {
                        ob.valid = false;
                    }
                    else
                    {
                        ob.valid = true;
                    }
                    if (ob.valid)
                    {
                        ob.testValidity = chkforValidityofTests(ob.LabID);
                    }
                    else
                    {
                        ob.testValidity = false;
                    }

                    retdata.Add(ob);
                }
            }

            return retdata;
        }
        public static bool chkforValidityofTests(string labID)
        {
            bool chkValidTest = false;
            string qry = " select mcode,count(1) from testlibtr where mcode = '" + labID + "' group by mcode";
            DataTable dt = Common.GetTableFromSession(qry, "", null, null);
            if (dt.Rows.Count > 0)
            {
                chkValidTest = true;
            }
            return chkValidTest;
        }
    }
}