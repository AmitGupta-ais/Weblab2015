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
using WebLab.DataSets;
using System.Data.Common;


/// <summary>
/// Summary description for clsLabMTable
/// </summary>
namespace WebLabMaster
{
    public enum ReportMenuType
    {
        Datewise,
        TestwiseHistory
    }
    public class clsLabMTable
    {
        DataTable dt;
        string LabMTablename = clsTableNames.LabMTablename;
        string LabTestTablename = clsTableNames.LabTestTablename;
        string TestLibTablename = clsTableNames.TestLibTablename;
        string TestlibHeadTablename = clsTableNames.TestlibHeadTablename;
        public clsLabMTable()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public dsReportMenu GetData(string strPatCode, ReportMenuType rptMenuType)
        {
            dsReportMenu objReportMenu = new dsReportMenu();
            clsReport obj = new clsReport();
            string qry;

            if (rptMenuType == ReportMenuType.Datewise)
            {

                if (Common.AISCompareString(obj.gettunvar("apprhdwis"), Common.AISUCase("y")) == AISCompareStringResult.AISCompareEqual)
                {
                    qry = "select LabM.labno Labno,max(LabM.tdate) TDate,max (TestlibHead.Name) Name,max(TestLib.lcode) TestHeadCode from LabTest  ";
                    qry += "left outer join LabM  on LabTest.labno=LabM.labno ";
                    qry += "left outer join TestLib   on LabTest.tcode=TestLib.code ";
                    qry += "left outer join TestlibHead    on TestLib.lcode=TestlibHead.code ";
                    qry += "Left Outer Join Dept On TestLib.DCode=Dept.Code ";
                    qry += "where (LabM.PCode='" + strPatCode + "' or LabM.patno='" + strPatCode + "') ";
                    if (obj.mycboolean(obj.gettunvar("shuptodate")))
                    {
                        if (Common.MyCDate(obj.gettunvar("uptodate")) > new DateTime(1900, 01, 01))
                        {
                            qry += " and " + LabMTablename + ".tdate>=" + Common.GetDateString(Common.MyCDate(obj.gettunvar("uptodate")));
                        }
                    }
                    qry += " And (Dept.NotAllowWeb IS NULL OR Dept.NotAllowWeb<>1) ";
                    qry += " group by Labm.labno,TestLib.lcode";

                }
                else
                {
                    qry = "Select Distinct " + LabMTablename + ".LabNo," + LabMTablename + ".TDate," + TestlibHeadTablename + ".Name from ";
                    qry += "((" + LabMTablename + " Left Outer Join " + LabTestTablename + " on " + LabMTablename + ".LabNo=" + LabTestTablename + ".LabNo)";
                    qry += " Left Outer Join " + TestLibTablename + " on " + LabTestTablename + ".tcode=" + TestLibTablename + ".code) ";
                    qry += " Left Outer Join " + TestlibHeadTablename + " on " + TestLibTablename + ".lcode=" + TestlibHeadTablename + ".code";
                    qry += " Left Outer Join Dept On TestLib.DCode=Dept.Code ";
                    qry += " Where (" + LabMTablename + ".PCode='" + strPatCode + "' or " + LabMTablename + ".patno='" + strPatCode + "')";

                    if (obj.mycboolean(obj.gettunvar("shuptodate")))
                    {
                        if (Common.MyCDate(obj.gettunvar("uptodate")) > new DateTime(1900, 01, 01))
                        {
                            qry += " and " + LabMTablename + ".tdate>=" + Common.GetDateString(Common.MyCDate(obj.gettunvar("uptodate")));
                        }
                    }
                    qry += " And (Dept.NotAllowWeb IS Null OR Dept.NotAllowWeb<>1) ";
                    qry += " Order by " + LabMTablename + ".TDate desc," + LabMTablename + ".labno desc";
                }
                dt = Common.GetTableFromSession(qry, "LabTest", null, null);
               
                

                int rcnt = dt.Rows.Count;

                dsReportMenu.DatewiseReportMenuRow dr;
                dsReportMenu.TestwiseHistoryReportMenuDataTable dtTestwiseHisTable = new dsReportMenu.TestwiseHistoryReportMenuDataTable();

                string Labno = "";
                if (Common.AISCompareString(obj.gettunvar("apprhdwis"), Common.AISUCase("y")) == AISCompareStringResult.AISCompareEqual)
                {


                    for (int i = 0; i < rcnt; i++)
                    {
                        string TestName = "";
                        
                        dr = (dsReportMenu.DatewiseReportMenuRow)objReportMenu.DatewiseReportMenu.NewRow();
                        
                        TestName = Common.MyCStr(dt.Rows[i]["Name"]);
                        dr.LabNo = Common.MyCStr(dt.Rows[i]["LabNo"]);
                        dr.TestDate = Common.GetPrintDate(Common.MyCDate(dt.Rows[i]["TDate"]));
                        dr.TestName = TestName;


                        dr.TestHeadCode = Common.MyCStr(dt.Rows[i]["TestHeadCode"]);

                        string qryapproove = "select * from LABHEADAPPROVAL where labno='" + dr.LabNo + "' and lcode='" + dr.TestHeadCode + "'";
                        DataTable dtapproove = Common.GetTableFromSession(qryapproove, "", null, null);
                        if (dtapproove.Rows.Count == 0)
                        {
                            dr.LabNo += "*";
                            dr.IsApp = "0";
                        }
                        else
                        {
                            dr.IsApp = "1";
                        }
                        objReportMenu.DatewiseReportMenu.Rows.Add(dr);
                    }


                }
                else
                {
                    for (int i = 0; i < rcnt; i++)
                    {
                        string TestName = "";
                        dr = (dsReportMenu.DatewiseReportMenuRow)objReportMenu.DatewiseReportMenu.NewRow();

                        Labno = Common.MyCStr(dt.Rows[i]["LabNo"]);
                        do
                        {
                            if (Common.MyLen(TestName) != 0)
                            {
                                TestName = TestName + ", ";
                            }
                            TestName = TestName + Common.MyCStr(dt.Rows[i]["Name"]);
                            i++;
                            if (i == rcnt)
                            {
                                break;
                            }

                        } while (Labno == Common.MyCStr(dt.Rows[i]["LabNo"]));

                        i--;
                        dr.LabNo = Common.MyCStr(dt.Rows[i]["LabNo"]);
                        dr.TestDate = Common.GetPrintDate(Common.MyCDate(dt.Rows[i]["TDate"]));
                        dr.TestName = TestName;
                        if (Common.AISCompareString(obj.gettunvar("apprhdwis"), Common.AISUCase("y")) == AISCompareStringResult.AISCompareEqual)
                        {
                            dr.TestHeadCode = Common.MyCStr(dt.Rows[i]["TestHeadCode"]);
                        }
                        else
                        {
                            dr.TestHeadCode = "";
                        }

                        objReportMenu.DatewiseReportMenu.Rows.Add(dr);
                    }
                }

                int IsNotAllowOPDCreditBalRep = Common.MycInt(HttpContext.Current.Session["IsNotAllowOPDCreditBalRep"]);
                if (IsNotAllowOPDCreditBalRep == 1)
                {
                    foreach (DataRow fdr in objReportMenu.DatewiseReportMenu.Rows)
                    {
                        if (Common.MyCStr(fdr["Labno"]).Trim().Length > 0)
                        {
                            bool tempRetVal = CheckOPDCreditBal(Common.MyCStr(fdr["Labno"]));
                            if (tempRetVal)
                            {
                                fdr["IsOPDCreditBal"] = "Y";
                            }
                        }
                    }
                }

            }
            else
            {

                qry = "Select Distinct " + LabMTablename + ".LabNo," + LabMTablename + ".TDate," + LabTestTablename + ".tcode," + TestlibHeadTablename + ".Name as TestHeadName," + TestLibTablename + ".Name," + TestLibTablename + ".GRP from ";
                qry += "((" + LabMTablename + " Left Outer Join " + LabTestTablename + " on " + LabMTablename + ".LabNo=" + LabTestTablename + ".LabNo)";
                qry += " Left Outer Join " + TestLibTablename + " on " + LabTestTablename + ".tcode=" + TestLibTablename + ".code) ";
                qry += " Left Outer Join " + TestlibHeadTablename + " on " + TestLibTablename + ".lcode=" + TestlibHeadTablename + ".code";
                qry += " Left Outer Join Dept On TestLib.DCode=Dept.Code ";
                qry += " Where (" + LabMTablename + ".PCode='" + strPatCode + "' or " + LabMTablename + ".patno='" + strPatCode + "')  and " + TestLibTablename + ".isMain='Y' ";
                if (obj.mycboolean(obj.gettunvar("shuptodate")))
                {
                    if (Common.MyCDate(obj.gettunvar("uptodate")) > new DateTime(1900, 01, 01))
                    {
                        qry += " and " + LabMTablename + ".tdate>=" + Common.GetDateString(Common.MyCDate(obj.gettunvar("uptodate")));
                    }
                }
                qry += " And (Dept.NotAllowWeb IS Null OR Dept.NotAllowWeb<>1) ";
                qry += " Order by " + LabMTablename + ".TDate desc," + LabMTablename + ".labno desc";

                dt = Common.GetTableFromSession(qry, "LabTest", null, null);

                

                int rcnt = dt.Rows.Count;


                DataTable tb = new DataTable();
                DataColumn dc = new DataColumn("TestGroup");
                tb.Columns.Add(dc);
                tb.PrimaryKey = new DataColumn[] { dc };

                dc = new DataColumn("TestCount", Type.GetType("System.Int32"));
                tb.Columns.Add(dc);

                dc = new DataColumn("TestHeadName");
                tb.Columns.Add(dc);

                dc = new DataColumn("TestName");
                tb.Columns.Add(dc);

                dc = new DataColumn("TestDate");
                tb.Columns.Add(dc);

                dc = new DataColumn("IsOPDCreditBal");
                tb.Columns.Add(dc);

       

                int IsNotAllowOPDCreditBalRep = Common.MycInt(HttpContext.Current.Session["IsNotAllowOPDCreditBalRep"]);

                for (int i = 0; i < rcnt; i++)
                {
                    string GRP = Common.MyCStr(dt.Rows[i]["GRP"]);

                    DataRow drCount = tb.Rows.Find(GRP);
                    if (drCount == null)
                    {
                        drCount = tb.NewRow();
                        drCount["TestGroup"] = GRP;

                        drCount["TestHeadName"] = Common.MyCStr(dt.Rows[i]["TestHeadName"]);
                        drCount["TestName"] = Common.MyCStr(dt.Rows[i]["Name"]);
                        drCount["TestCount"] = 1;
                        
                        drCount["TestDate"] = Common.MyCStr(dt.Rows[i]["TDate"]);

                        if (IsNotAllowOPDCreditBalRep == 1)
                        {
                            bool tempRetVal = CheckOPDCreditBal(Common.MyCStr(dt.Rows[i]["Labno"]));
                            if (tempRetVal)
                            {
                                drCount["IsOPDCreditBal"] = "Y";
                            }
                        }

                        tb.Rows.Add(drCount);
                    }
                    else
                    {
                        drCount["TestCount"] = (int)(Common.MyCDbl(drCount["TestCount"]) + 1);
                    }
                }
                dsReportMenu.TestwiseHistoryReportMenuRow drHistory;
                int tbCount = tb.Rows.Count;

                for (int i = 0; i < tbCount; i++)
                {
                    drHistory = (dsReportMenu.TestwiseHistoryReportMenuRow)objReportMenu.TestwiseHistoryReportMenu.NewRow();
                    drHistory.TestHeadName = Common.MyCStr(tb.Rows[i]["TestHeadName"]);
                    drHistory.TestCount = (Int32)Common.MyCDbl(tb.Rows[i]["TestCount"]);
                    drHistory.TestGroup = Common.MyCStr(tb.Rows[i]["TestGroup"]);
                    drHistory.TestName = Common.MyCStr(tb.Rows[i]["TestName"]);
                    drHistory.TestDate = Common.GetPrintDate(Common.MyCDate(tb.Rows[i]["TestDate"]));
                    drHistory.IsOPDCreditBal = Common.MyCStr(tb.Rows[i]["IsOPDCreditBal"]);
                   
                    objReportMenu.TestwiseHistoryReportMenu.Rows.Add(drHistory);
                }  
            }
    
  
            return objReportMenu;
        }

        public bool CheckOPDCreditBal(string strLabno)
        {
            bool retval = false;
            string patno = "";
            string qrybal = "select patno from Labm where labno = '" + strLabno + "'";
            DataTable TempDT = Common.GetTableFromSession(qrybal, "temp",null,null);
            if (TempDT != null && TempDT.Rows.Count > 0)
            {
                patno = Common.MyCStr(TempDT.Rows[0]["patno"]);
            }

            if (patno.Trim().Length > 0)
            {
                qrybal = "select typ,cashcredit,bdate,amt from Bills where patno = '" + patno + "'";
                TempDT = Common.GetTableFromSessionSecond(qrybal, "temp",null,null);
                if (TempDT != null && TempDT.Rows.Count > 0)
                {
                    string typ = Common.MyCStr(TempDT.Rows[0]["typ"]).Trim().ToUpper();
                    string cashcredit = Common.MyCStr(TempDT.Rows[0]["cashcredit"]).Trim().ToUpper();
                    if (((Common.AISCompareString(typ, "O") == AISCompareStringResult.AISCompareEqual) || (Common.AISCompareString(typ, "G") == AISCompareStringResult.AISCompareEqual)) && (Common.AISCompareString(cashcredit, "CREDIT") == AISCompareStringResult.AISCompareEqual))
                    {
                        clsReport tempOBJ = new clsReport();
                        DateTime currtime = DateTime.Now;
                        currtime.AddYears(10);
                        double AMTbalance = tempOBJ.getpatnobalance(patno, currtime);

                        if (AMTbalance > 1)
                        {
                            retval = true;
                        }
                    }

                }
            }

            return retval;
        }
    }
} 

