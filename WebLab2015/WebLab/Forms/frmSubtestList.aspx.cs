using AISWebCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLabMaster;

namespace WebLab.Forms
{
    public partial class frmSubtestList : System.Web.UI.Page
    {
        public string apcode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string labid = Common.MyCStr(Request.QueryString["LabID"]).Trim();
                string code = Common.MyCStr(Request.QueryString["apCode"]).Trim();
                apcode = code;
                loadData(labid);
            }
        }

        public void loadData(string labid)
        {
            string qry = "select scode,(select max(name)from testlib where CODE = '" + labid + "') Name,(select max(method) from testlib where CODE = '" + labid + "' ) METHOD  from Testlibtr where MCODE = '" + labid + "' AND SCODE<> '" + labid + "' order by SNO";
            DataTable dt = Common.GetTableFromSession(qry, "", null, null);
            DataTable temp = dt;
            if (temp.Rows.Count > 0)
            {
                qry = "select scode,(select max(name)from testlib where CODE = '" + labid + "') Name,(select max(method) from testlib where CODE = '" + labid + "' ) METHOD  from Testlibtr where MCODE = '" + labid + "'";
                dt = null;
                dt = Common.GetTableFromSession(qry, "", null, null);
            }
            bool isWebServiceCall = Common.mycboolean(Request.QueryString["SR"]);
            if (isWebServiceCall)
            {
                ReturnData(dt);                
            }
            else
            {
                txtCode.Text = labid;
                hdnapCode.Value = "";
            }
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
                string strData = getGridData(Common.MycInt(pageIndex), Common.MycInt(numberOfRows), Common.mycboolean(_search), sortOrderBy, Dt, false);
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
                    ///Print Button///

                    //JqGridDataHeading obj = new JqGridDataHeading();
                    //obj.name = Common.MyCStr("Print");
                    //obj.index = Common.MyCStr("Print");
                    //obj.align = "center";
                    //obj.width = 40;
                    //obj.frozen = true;
                    //obj.sortable = false;
                    //obj.formatter = "function(){ return \"<span class='ui-icon ui-icon-print'></span>\"}";
                    //colcontetn.Add(obj);
                    //liob.Add("Print");

                    ///Attachment Button///

                    //obj = new JqGridDataHeading();
                    //obj.name = "Attachment";
                    //obj.index = "Attachment";
                    //obj.align = "center";
                    //obj.width = 50;
                    //obj.frozen = true;
                    //obj.sortable = false;
                    //obj.formatter = "function(){ return \"<span class='ui-icon ui-icon-print'></span>\"}";
                    //colcontetn.Add(obj);
                    //liob.Add("Attachment");


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
                    if (Common.AISCompareString(column.ColumnName, "scode") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 150;
                        HeadCaption = "Code";
                    }
                    if (Common.AISCompareString(column.ColumnName, "Name") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 200;
                        HeadCaption = "Name";
                    }
                    if (Common.AISCompareString(column.ColumnName, "Method") == AISCompareStringResult.AISCompareEqual)
                    {
                        obj.width = 200;
                        HeadCaption = "Method";
                    }
                    


                    //if (column.DataType == typeof(double) || column.DataType == typeof(decimal) || column.DataType == typeof(int) || column.DataType == typeof(float))
                    //{
                    //    if (column.ColumnName != "Age")
                    //    {
                    //        obj.align = "right";
                    //        obj.summaryType = "sum";
                    //        double Total = default(double);
                    //        for (int ii = 1; ii < dtRecords.Rows.Count; ii++)
                    //        {
                    //            Total += Common.MyCDbl(dtRecords.Rows[ii][column.ColumnName]);
                    //        }

                    //        hstbTotal.Add(column.ColumnName, Common.myformat(Total));
                    //        col_forTotal.Add(column.ColumnName);
                    //    }
                    //}
                    //else
                    //{
                    //    if (i == 0)
                    //    {
                    //        i++;
                    //        hstbTotal.Add(column.ColumnName, "Total:");
                    //        obj.frozen = true;
                    //    }
                    //    obj.align = "left";

                    //}
                    colcontetn.Add(obj);
                    liob.Add(HeadCaption);

                }

                //Extra options///
                //JqGridDataHeading obj1 = new JqGridDataHeading();
                //obj1.name = "Options";
                //obj1.index = "Options";
                //obj1.align = "center";
                //obj1.width = 100;
                //obj1.frozen = true;
                //obj1.sortable = false;
                //obj1.formatter = "function(){ return \"<span class='ui-icon ui-icon-print'></span>\"}";
                //colcontetn.Add(obj1);
                //liob.Add("Options");
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
            
            string labid = Common.MyCStr(Request.QueryString["LabID"]).Trim();
            try
            {
                List<object> objListOfEmployeeEntity = new List<object>();
                foreach (DataRow dRow in dsProducts.Rows)
                {
                    string Labno = Common.MyCStr(dRow["scode"]).Trim();

                    Hashtable hashtable = new Hashtable();
                    if (IsPrintButtonRequired)
                    {

                        if (Common.MyLen(Labno) > 0)
                        {
                            hashtable.Add("Print", "<span title=\"Print Lab Report\" class='glyphicon glyphicon-print' onclick=\"PrintBill('" + Labno + ")\"></span>");
                        }
                        else
                        {
                            hashtable.Add("Print", "<span title=\"Print Lab Report\" class='glyphicon glyphicon-print' onclick=\"alert('Lab No. Not Exist')\"></span>");
                        }
                        clsReport obj = new clsReport();
                        string isheadwise = obj.gettunvar("apprhdwis").Trim();
                        //hashtable.Add("Options", "<span title=\"Approve this test\" class='glyphicon glyphicon-ok' style='cursor:pointer' onclick=\"approveTest('" + Labno + "','" + Common.MyCStr(dRow["pcode"]) + "','" + isheadwise + "')\"></span>&nbsp;&nbsp;<span title=\"Mark special sample collected\" class='glyphicon glyphicon-tags' style='cursor:pointer' onclick=\"sampleCollection('" + Labno + "','" + Common.MyCStr(dRow["pcode"]) + "')\"></span>&nbsp;&nbsp;<span title=\"Un-mark sample collected\" class='glyphicon glyphicon-pencil' style='cursor:pointer' onclick=\"unMarkSample('" + Labno + "')\"></span>");
                    }

                    int i = 0;
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
                            if (Common.AISCompareString(column.ColumnName.ToUpper(), "scode") == AISCompareStringResult.AISCompareEqual)
                            {
                                if (Common.MyCStr(dRow["scode"]).Trim() != labid)
                                {
                                    hashtable.Add(column.ColumnName, "<a href=\"#\"  ondblclick=\"return OpenTest('" + apcode.Trim() + "','" +/* Dept.Trim() +*/ "','" + Common.MyCStr(dRow["scode"]) + "',2)\">" + dRow[column.ColumnName].ToString().Trim() + "</a>");
                                }
                                else
                                {
                                    hashtable.Add(column.ColumnName, "<a href=\"#\"  ondblclick=\"return OpenTest('" + apcode.Trim() + "','" +/* Dept.Trim() +*/ "','" + Common.MyCStr(dRow["scode"]) + "',1)\">" + dRow[column.ColumnName].ToString().Trim()+"-Main Test" + "</a>");
                                }
                            }
                            else
                            {
                                hashtable.Add(column.ColumnName, dRow[column.ColumnName].ToString());
                            }

                        }
                        i++;
                    }
                    objListOfEmployeeEntity.Add(hashtable);
                }
                return objListOfEmployeeEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}