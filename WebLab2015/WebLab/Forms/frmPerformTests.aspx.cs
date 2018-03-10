using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AISWebCommon;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using WebLabMaster;
using System.Data.Common;
using System.Web.Services;

namespace WebLab.Forms
{
    public partial class frmPerformTests : System.Web.UI.Page 
    {
        public static string username = string.Empty;
        public static bool isapproval=false;
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                Response.Redirect("~/frmcommonlogin.aspx");
                username = Common.MyCStr(Session["lname"]);
            }

            #region Setting Hospital Name
            DataTable HospDt = Common.GetTableFromSessionSecond("Select VALUE From SYSTUN Where Code='SYHEAD1'", "", null, null);
            if (HospDt != null && HospDt.Rows.Count > 0)
            {
                HospName.InnerText = Common.MyCStr(HospDt.Rows[0]["VALUE"]).Trim();
            }
            if (Session["Username"] != null)
            {
                UserName.InnerText = Common.MyCStr(Session["Username"]).Trim();
                if (Common.MyLen(Common.MyCStr(Session["CONSCODE"])) > 0)
                {
                    HospDt = Common.GetTableFromSessionSecond("Select Name From ConsMAST Where Code='" + Common.MyCStr(Session["CONSCODE"]) + "'", "", null, null);
                    if (HospDt != null && HospDt.Rows.Count > 0)
                    {
                        UserName.InnerText += "[" + Common.MyCStr(HospDt.Rows[0]["Name"]).Trim() + "]";
                    }
                }
            }
            #endregion
            if (Common.MyCStr(Request.QueryString["M"]) == "Y")
            {
                processReport();
            }
            if (!IsPostBack)
            {
                DateTime Currdate = DateTime.Now;   ///Common.GetServerDate();
                txtFromDate.Text = Common.GetPrintDate(Currdate, "dd/MMM/yyyy");
                txtToDate.Text = Common.GetPrintDate(Currdate, "dd/MMM/yyyy");

                hdnIapprove.Value = Common.MyCStr(Common.MycInt(Session["HideUnAppRep"]));

                loadDepartments();

            }
        }

        private void loadDepartments()
        {
            int isIPDOPD = 0;
            int SelDept = 0;
            ddlOPDIPD.Enabled = true;

            DataTable dt = Common.GetTableFromSession("select depttype,opdipd from userrole where username = '" + Common.MyCStr(Session["lname"]) + "'", "Temp", null, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                isIPDOPD = Common.MycInt(dt.Rows[0]["opdIpd"]);
                if (isIPDOPD == 2)
                {
                    ddlOPDIPD.SelectedValue = "IPD";
                    ddlOPDIPD.Enabled = false;
                }
                else if (isIPDOPD == 1)
                {
                    ddlOPDIPD.SelectedValue = "OPD";
                    ddlOPDIPD.Enabled = false;
                }
                SelDept = Common.MycInt(dt.Rows[0]["depttype"]);
            }

            DataTable dt1 = Common.GetTableFromSession("select utr.deptbill as code,d.name from userroletr utr left outer join dept d on utr.deptbill = d.code  where utr.username = '" + Common.MyCStr(Session["lname"]) + "'", "Temp", null, null);

            if (SelDept != 2)
            {
                DataRow dr = dt1.NewRow();
                dr["Code"] = "";
                dr["Name"] = "All";
                dt1.Rows.InsertAt(dr, 0);
            }

            ddlDept.DataSource = dt1;
            ddlDept.DataTextField = "Name";
            ddlDept.DataValueField = "code";
            ddlDept.DataBind();

        }

        public void processReport()
        {
            string Query = "";
            DateTime FromDate = Common.MyCDate(Common.MyCStr(Request.QueryString["FROM"]));
            DateTime ToDate = Common.MyCDate(Common.MyCStr(Request.QueryString["TO"]));
            string Search = Common.MyCStr(Request.QueryString["SEARCH"]).Trim();
            string deptCode = Common.MyCStr(Request.QueryString["deptCode"]);
            string OPDIPD = Common.MyCStr(Request.QueryString["OPDIPD"]);

            string apDataBaseName;
            clsReport obj = new clsReport();

            apDataBaseName = obj.gettunvar("APUSERNM").Trim();
            if(Common.DBType==AisDBType.Sql)
            {
                apDataBaseName = apDataBaseName + "..";
            }
            else
            {
                apDataBaseName = apDataBaseName + ".";
            }

            Query = " select L.labNo,L.tdate TestDate,L.Patno RegNo,B.Pcode ,P.Name Name,P.Age,P.Sex,L.TestNames,L.IsApproved,(select count(1) from labtest lt1 where lt1.labno = L.labno and isperf ='Y') isPerform from LabM L ";
            Query += " left outer join " + apDataBaseName + "bills B on L.PATNO = B.PATNO ";
            Query += " left outer join " + apDataBaseName + "Patient p on B.pcode = P.code ";
            Query += " where L.TDATE >= " + Common.GetDateString(Common.GetDateStartTime(FromDate)) + " and L.TDATE <= " + Common.GetDateString(Common.GetDateEndTime(ToDate));
            if (Search.Trim().Length > 0)
            {
                Query += " and (B.Patno like '%" + Search + "%' or P.name like '%" + Search + "%') ";
            }
            if (Session["CONSCODE"] != null)
            {
                ArrayList ArrList = (ArrayList)(Session["CONSCODE"]);
                if (ArrList.Count > 0)
                {
                    Query += " and (B.consinch in " + Common.GetmultiCollString(ArrList) + " or B.cons in " + Common.GetmultiCollString(ArrList) + ") ";
                }
                ////if (Common.MyLen(Common.MyCStr(Session["CONSCODE"])) > 0)
                ////{
                ////    Query += " and (B.consinch='" + Common.MyCStr(Session["CONSCODE"]) + "' or B.consinch2='" + Common.MyCStr(Session["CONSCODE"]) + "'  or B.cons='" + Common.MyCStr(Session["CONSCODE"]) + "') ";
                ////}
            }

            if (Common.AISCompareString(OPDIPD, "OPD") == AISCompareStringResult.AISCompareEqual)
            {
                Query += " and (B.typ = 'O' or B.typ = 'G') ";
            }
            else if (Common.AISCompareString(OPDIPD, "IPD") == AISCompareStringResult.AISCompareEqual)
            {
                Query += " and B.typ = 'I' ";
            }
            if (Common.MyCStr(deptCode).Length > 0)
            {
                Query += " and Exists( select 1 from labtest left outer join testlib on labtest.tcode=testlib.code where labtest.labno = L.labno and testlib.dcode= '" + Common.MyCStr(deptCode) + "'  )";
            }



            DataTable Dt = Common.GetTableFromSession(Query, "Temp", null, null);




            ReturnData(Dt);
        }

        public void ReturnData(DataTable Dt)
        {
            try
            {
                Request.InputStream.Position = 0;
                StreamReader ipStRdr = new StreamReader(Request.InputStream);
                string json = ipStRdr.ReadToEnd();
                HttpContext context = Context;
                HttpRequest request = context.Request;
                string _search = request["_search"];
                string numberOfRows = request["rows"];
                string pageIndex = request["page"];
                string sortColumnName = request["sidx"];
                string sortOrderBy = request["sord"];
                JavaScriptSerializer jser = new JavaScriptSerializer();
                Dictionary<string, string> ss = new Dictionary<string, string>();
                string strData = getGridData(Common.MycInt(pageIndex), Common.MycInt(numberOfRows), Common.mycboolean(_search), sortOrderBy, Dt, true);
                Response.ContentType = "text/json";
                Response.Write(strData);
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
                Response.End();
            }
            catch (Exception ex)
            {
                Common.logException(ex);
                throw ex;
            }
        }

        public string getGridData(int page, int rows, bool _search, string sord, DataTable Main, bool RequiredPrintColumn)
        {
            try
            {

                if (rows == 0)
                {
                    rows = 1;
                }

                ////Fetch Record from database
                DataTable dtRecords = Main;
                int recordsCount = dtRecords.Rows.Count;

                ///// create a JQGrid data object to send as a JSON string
                JqGridData objJqGrid = new JqGridData();
                objJqGrid.page = page;
                objJqGrid.total = ((recordsCount + rows - 1) / rows);
                objJqGrid.records = recordsCount;
                objJqGrid.rows = ConvertDT(dtRecords, RequiredPrintColumn);

                /////create List of column that requires total
                List<string> arrcol_req_Total = new List<string>();
                Dictionary<object, object> hstbTotal = new Dictionary<object, object>();

                ///// Create a collection of model for JQGrid
                List<object> colcontetn = new List<object>();
                List<string> liob = new List<string>();
                List<string> col_forTotal = new List<string>();
                if (RequiredPrintColumn)
                {
                    JqGridDataHeading obj = new JqGridDataHeading();
                    obj.name = Common.MyCStr("Print");
                    obj.index = Common.MyCStr("Print");
                    obj.align = "center";
                    obj.width = 40;
                    obj.frozen = true;
                    obj.sortable = false;
                    obj.formatter = "function(){ return \"<span class='ui-icon ui-icon-print'></span>\"}";
                    colcontetn.Add(obj);
                    liob.Add("Print");
                    


                    obj = new JqGridDataHeading();
                    obj.name = "Attachment";
                    obj.index = "Attachment";
                    obj.align = "center";
                    obj.width = 50;
                    obj.frozen = true;
                    obj.sortable = false;
                    obj.formatter = "function(){ return \"<span class='ui-icon ui-icon-print'></span>\"}";
                    colcontetn.Add(obj);
                    liob.Add("Attachment");

                   
                }


                int i = 0;
                foreach (DataColumn column in dtRecords.Columns)
                {
                    JqGridDataHeading obj = new JqGridDataHeading();
                    obj.name = Common.MyCStr(column.ColumnName);
                    obj.index = Common.MyCStr(column.ColumnName);
                    obj.sorttype = column.DataType.Name;
                    obj.sortable = true;
                    obj.width = 100;

                    string HeadCaption = column.Caption;
                    if (Common.AISCompareString(column.ColumnName, "labNo") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 100;
                        HeadCaption = "Lab No.";
                    }
                    if (Common.AISCompareString(column.ColumnName, "TestDate") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 100;
                        HeadCaption = "Test Date";
                    }
                    if (Common.AISCompareString(column.ColumnName, "RegNo") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 100;
                        HeadCaption = "Reg No.";
                    }
                    if (Common.AISCompareString(column.ColumnName, "Pcode") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 100;
                        HeadCaption = "Patient ID";
                    }
                    if (Common.AISCompareString(column.ColumnName, "Name") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 200;
                        HeadCaption = "Patient Name";
                    }
                    if (Common.AISCompareString(column.ColumnName, "Age") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 30;
                    }
                    if (Common.AISCompareString(column.ColumnName, "Sex") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 30;
                    }
                    if (Common.AISCompareString(column.ColumnName, "TestNames") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 250;
                        HeadCaption = "Test Names";
                    }
                    if (Common.AISCompareString(column.ColumnName, "IsApproved") == AISCompareStringResult.AISCompareEqual)
                    {
                        continue;
                    }
                    if (Common.AISCompareString(column.ColumnName, "isPerform") == AISCompareStringResult.AISCompareEqual)
                    {
                        continue;
                    }


                    if (column.DataType == typeof(double) || column.DataType == typeof(decimal) || column.DataType == typeof(int) || column.DataType == typeof(float))
                    {
                        if (column.ColumnName != "Age")
                        {
                            obj.align = "right";
                            obj.summaryType = "sum";
                            double Total = default(double);
                            for (int ii = 1; ii < dtRecords.Rows.Count; ii++)
                            {
                                Total += Common.MyCDbl(dtRecords.Rows[ii][column.ColumnName]);
                            }

                            hstbTotal.Add(column.ColumnName, Common.myformat(Total));
                            col_forTotal.Add(column.ColumnName);
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            i++;
                            hstbTotal.Add(column.ColumnName, "Total:");
                            obj.frozen = true;
                        }
                        obj.align = "left";
                        
                    }
                    colcontetn.Add(obj);
                    liob.Add(HeadCaption);
                  
                }
                JqGridDataHeading obj1 = new JqGridDataHeading();
                obj1.name = "Options";
                obj1.index = "Options";
                obj1.align = "center";
                obj1.width = 100;
                obj1.frozen = true;
                obj1.sortable = false;
                obj1.formatter = "function(){ return \"<span class='ui-icon ui-icon-print'></span>\"}";
                colcontetn.Add(obj1);
                liob.Add("Options");
                objJqGrid.rowsHead = liob;
                objJqGrid.rowsM = colcontetn;
                objJqGrid.userdata = hstbTotal;
                objJqGrid.col_Fortotal = col_forTotal;
                JavaScriptSerializer jser = new JavaScriptSerializer();
                jser.MaxJsonLength = int.MaxValue;
                string strdata = jser.Serialize(objJqGrid);
                return strdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<object> ConvertDT(DataTable dsProducts, bool IsPrintButtonRequired)
        {
            try
            {
                List<object> objListOfEmployeeEntity = new List<object>();
                foreach (DataRow dRow in dsProducts.Rows)
                {
                    string Labno = Common.MyCStr(dRow["labno"]).Trim();
                    int IsApproved = Common.MycInt(dRow["IsApproved"]);
                    int isPerform = Common.MycInt(dRow["isPerform"]);
                    int attachment = 0;
                    string query = "  select count(1) files from aplab_linktoraw where labno='" + Labno + "' ";
                    DataTable dt = Common.GetTableFromSession(query, "SS", null, null);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        attachment = Common.MycInt(dt.Rows[0]["files"]);
                    }


                    Hashtable hashtable = new Hashtable();
                    if (IsPrintButtonRequired)
                    {

                        if (Common.MyLen(Labno) > 0)
                        {
                            hashtable.Add("Print", "<span title=\"Print Lab Report\" class='glyphicon glyphicon-print' onclick=\"PrintBill('" + Labno + "'," + IsApproved + "," + isPerform + ")\"></span>");
                        }
                        else
                        {
                            hashtable.Add("Print", "<span title=\"Print Lab Report\" class='glyphicon glyphicon-print' onclick=\"alert('Lab No. Not Exist')\"></span>");
                        }


                        if (attachment > 0)
                        {
                            hashtable.Add("Attachment", "<span title=\"View Attachments\" class='glyphicon glyphicon-pushpin' onclick=\"Showattachment('" + Labno + "')\"></span>");

                        }
                        else
                        {
                            hashtable.Add("Attachment", "<span title=\"Attachments Not Available\" onclick=\"alert('Attachments not Available at the moment')\"></span>");

                        }
                        clsReport obj = new clsReport();
                        string isheadwise= obj.gettunvar("apprhdwis").Trim();
                        hashtable.Add("Options", "<span title=\"Approve this test\" class='glyphicon glyphicon-ok' style='cursor:pointer' onclick=\"approveTest('" + Labno + "','"+Common.MyCStr(dRow["pcode"]) + "','"+ isheadwise + "')\"></span>&nbsp;&nbsp;<span title=\"Mark special sample collected\" class='glyphicon glyphicon-tags' style='cursor:pointer' onclick=\"sampleCollection('" + Labno + "','" + Common.MyCStr(dRow["pcode"]) + "')\"></span>&nbsp;&nbsp;<span title=\"Un-mark sample collected\" class='glyphicon glyphicon-pencil' style='cursor:pointer' onclick=\"unMarkSample('" + Labno + "')\"></span>");
                    }


                    foreach (DataColumn column in dsProducts.Columns)
                    {
                        if (column.DataType == typeof(int))
                        {
                            hashtable.Add(column.ColumnName, Common.MycInt(Common.MyCStr(dRow[column.ColumnName])));
                        }
                        else if (column.DataType == typeof(double) || column.DataType == typeof(decimal) || column.DataType == typeof(float))
                        {
                            hashtable.Add(column.ColumnName, Common.myformat(Common.MyCDbl(dRow[column.ColumnName])));
                        }
                        else if (column.DataType == typeof(DateTime))
                        {
                            hashtable.Add(column.ColumnName, Common.GetPrintDate(Common.MyCDate(dRow[column.ColumnName]), "dd/MMM/yyyy"));
                        }
                        else
                        {
                            if (Common.AISCompareString(column.ColumnName.ToUpper(), "LABNO") == AISCompareStringResult.AISCompareEqual)
                            {
                                hashtable.Add(column.ColumnName, "<a href=\"#\" onclick=\"return OpenLabTest('" + dRow[column.ColumnName].ToString() + "')\">" + dRow[column.ColumnName].ToString() + "</a>");
                            }
                            else
                            {
                                hashtable.Add(column.ColumnName, dRow[column.ColumnName].ToString());
                            }
                            
                        }
                    }
                    
                    objListOfEmployeeEntity.Add(hashtable);
                    attachment = 0;
                }
                return objListOfEmployeeEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/frmCommonLogin.aspx");
        }
        [WebMethod]
        public static string approveTest(string labno)
        {
            string retval = string.Empty;
            DbConnection conn = null;
            DbTransaction trans = null;
            int chkretval = 0;

            try
            {

                chkretval = Classes.CommonFunctions.GetApprovalUser(labno, out isapproval, username);
                if (chkretval == 1)
                {   if(Common.MycInt(Classes.CommonFunctions.getMasterValue("labno",labno,"labm", "isapproved",false)) ==1)
                    {
                        retval = "Already approved lab no  " + labno;
                    }
                    else
                    {
                        conn = Common.GetConnectionFromSession();
                        trans = conn.BeginTransaction();

                        ColumnDataCollection coll;
                        coll = new ColumnDataCollection();

                        coll.Add("Isapproved", 1);
                        coll.Add("ApprovedBy", username);
                        coll.Add("approvalDate", Common.GetServerDate(conn, trans));
                        Common.UpdateTable("labm", coll, AisUpdateTableType.Update, " labno='" + labno + "'", conn, trans);
                        trans.Commit();
                        retval = "Successfully Updated!";

                    }
                }
                else if (chkretval == 2)
                {
                    retval = "Can not approved multiple department lab no from here..";
                }
                else
                {
                    retval = "You are not authorized to approve/unapprove ";
                }
               

               
            }
            catch (Exception ex)
            {

                retval = ex.Message.ToString();

            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }

            return retval;
        }
        [WebMethod]
        public static string unMarkSample(string labno,string reason)
        {
            DbConnection conn = null;
            DbTransaction trans = null;
            string retval = string.Empty;
            DateTime prvSampleDate = new DateTime(1888, 8, 1, 0, 0, 0);
            
            if (Common.MycInt(Classes.CommonFunctions.getMasterValue("username",username,"userrole", "isAllowUserPerm",false))==1)
            {
                return "This user don't have permission to select sampe!";
            }
            try
            {
                
                DataTable dt = Common.GetTableFromSession("select labno,sampledate from labm where labno='" + labno + "'","templabm",null,null);
                if (dt.Rows.Count>0)
                {
                    prvSampleDate = Common.MyCDate(dt.Rows[0]["sampledate"]);
                }
                if (DateTime.Compare(prvSampleDate, new DateTime(2000, 8, 1, 0, 0, 0)) < 0)
                {
                    retval = "Sample collection not marked.";
                }
                else {
                    conn = Common.GetConnectionFromSession();
                    trans = conn.BeginTransaction();
                    ColumnDataCollection coll;
                    coll = new ColumnDataCollection();                    
                    coll.Add("SampleDate", "");
                    Common.UpdateTable("labm", coll, AisUpdateTableType.Update, " labno='" + labno + "'", conn, trans);

                    coll = new ColumnDataCollection();
                    coll.Add("labno", labno);
                    coll.Add("userid", username);
                    coll.Add("rmks", reason);
                    coll.Add("moddate", Common.GetServerDate(conn,trans));
                    Common.UpdateTable("lab_sample_seldesel", coll, AisUpdateTableType.Insert, "", conn, trans);
                    trans.Commit();
                    retval = "Successfully Un-remarked";               
                }
              
            
            }
            catch(Exception ex)
            {

                retval = ex.Message.ToString();
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }
            return retval;

        }


    }
}