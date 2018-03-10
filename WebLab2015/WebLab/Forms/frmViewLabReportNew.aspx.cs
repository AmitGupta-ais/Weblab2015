using AISWebCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLab.DataSets;
using WebLabMaster;

namespace WebLab.Forms
{
    public partial class frmViewLabReportNew : System.Web.UI.Page
    {
        string patno = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["Patno"]!=null)
            {
                patno = Request.QueryString["Patno"].ToString();
            }
            bool isValid = false;
            if(Request.QueryString["Passcode"] != null)
            {
                if(decodePassCode(Request.QueryString["Passcode"].ToString(),patno))
                {
                    isValid = true;
                }
            }
            if(isValid)
            {
                // clsLabMTable objLabTable = new clsLabMTable();
                // dsReportMenu ds = objLabTable.GetData(patno, ReportMenuType.Datewise);
                // DataTable dt = ds.Tables[0];
                // TableRow tr = new TableRow();
                // TableCell tc = new TableCell();
                // tc.Text = "Date";
                // tc.Attributes.Add("class","headcol");
                // tr.Controls.Add(tc);
                // tc = new TableCell();
                // tc.Text = "Labno";
                // tc.Attributes.Add("class", "headcol");
                // tr.Controls.Add(tc);
                // tc = new TableCell();
                // tc.Text = "Test Name";
                // tc.Attributes.Add("class", "headcol");
                // tr.Controls.Add(tc);
                // tc = new TableCell();
                // tc.Text = "Download";
                // tc.Attributes.Add("class", "headcol");
                // tr.Controls.Add(tc);
                // table1.Controls.Add(tr);
                // if(dt.Rows.Count>0)
                // {
                //    var newdt= from myRow in dt.AsEnumerable() where myRow.Field<string>("IsApp") == "1" select myRow;
                //     DataTable newdata = newdt.CopyToDataTable();
                //     if (newdata.Rows.Count == 0)
                //     {
                //         ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "alert('No Data Found!')", true);
                //     }
                //     else if (newdata.Rows.Count == 1)
                //     {
                //         if (AllowDisplay(Common.MyCStr(newdata.Rows[0]["Labno"]).Trim()))
                //         {
                //             Response.Redirect("~/Forms/frmAisLabReportView.aspx?labno=" + Common.MyCStr(newdata.Rows[0]["Labno"]).Trim() + "&TestHeadCode=" + Common.MyCStr(newdata.Rows[0]["TestHeadCode"]));
                //         }
                //     }
                //     else
                //     {
                //         foreach (DataRow dr in newdata.Rows)
                //         {
                //             if (AllowDisplay(Common.MyCStr(dr["Labno"]).Trim()))
                //             {
                //                 tr = new TableRow();
                //                 tc = new TableCell();
                //                 tc.Text = Common.GetPrintDate(Common.MyCDate(dr["TestDate"]), "dd-MMM-yyyy HH:mm");
                //                 tc.Attributes.Add("class", "bodycol");
                //                 tr.Controls.Add(tc);
                //                 tc = new TableCell();
                //                 tc.Text = Common.MyCStr(dr["Labno"]).Trim();
                //                 tc.Attributes.Add("class", "bodycol");
                //                 tr.Controls.Add(tc);
                //                 tc = new TableCell();
                //                 tc.Text = Common.MyCStr(dr["TestName"]).Trim();
                //                 tc.Attributes.Add("class", "bodycol");
                //                 tr.Controls.Add(tc);
                //                 tc = new TableCell();
                //                 HyperLink hr = new HyperLink();
                //                 hr.Text = "Click Here";
                //                 hr.NavigateUrl = "~/Forms/frmAisLabReportView.aspx?labno=" + Common.MyCStr(dr["Labno"]).Trim() + "&TestHeadCode=" + Common.MyCStr(dr["TestHeadCode"]).Trim();
                //                 tc.Controls.Add(hr);
                //                 tc.Attributes.Add("class", "bodycol");
                //                 tr.Controls.Add(tc);
                //                 table1.Controls.Add(tr);
                //             }
                //         }
                //     }
                // }

                // //HyperLink hr = new HyperLink();
                // //hr.NavigateUrl = "~/Forms/frmAisLabReportView.aspx?labno=" + labNo + "&TestHeadCode=" + testhead+";

                //// Response.Redirect("~/Forms/frmAisLabReportView.aspx?labno=" + labNo + "&tcode=" + btnClicked.ID);
                ////bool IsDefPass = objLogin.IsDefPass;
                clsLogin objLogin = clsLogin.GetList(patno);
                Session[CRMConstants.LOGINUSER] = objLogin;
                Session[CRMConstants.USERTYPE] = clsLogin.USERTYPE;
                Response.Redirect("~/Forms/frmTestTable.aspx?Pcode=" + objLogin.Code, false);
                
            }
            else
            {
                Response.Redirect("~/Forms/frmLogin.aspx");
            }
        }
        public bool decodePassCode(string Passcode,string patno)
        {
            char[] chararr = patno.ToUpper().ToArray();
            int newPass = 0;
            foreach(char c in chararr)
            {
                newPass += (int)c;
            }
            newPass = newPass * 2;
            bool retval = false;
            if(Common.MycInt(Passcode)== newPass)
            {
                retval = true;
            }
            return retval;
        }
        protected bool AllowDisplay(string Labno)
        {
            string qry = "", patno = "";
            try
            {
                qry = "select hidereportpack FROM WebReportSettings";
                DataTable DT = new DataTable();

                DT = Common.GetTableFromSession(qry, "WebReportSettings", null, null);


                if (DT != null && DT.Rows.Count > 0)
                {
                    if (Common.MyCDbl(DT.Rows[0]["hidereportpack"]) == 1)
                    {


                        string package = "";

                        qry = "select code from HideReportPackage";
                        DT = new DataTable();
                        DT = Common.GetTableFromSession(qry, "HideReportPackage", null, null);
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            bool first = true;
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (first)
                                {
                                    package = "'" + Common.MyCStr(DT.Rows[i]["code"]) + "'";
                                    first = false;
                                }
                                else
                                {
                                    package = package + ",'" + Common.MyCStr(DT.Rows[i]["code"]) + "'";
                                }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        if (Common.MyCStr(package).Length > 0)
                        {
                            qry = "select patno from labm where labno='" + Labno + "'";
                            DT = new DataTable();
                            DT = Common.GetTableFromSession(qry, "labm", null, null);
                            if (DT != null && DT.Rows.Count > 0)
                            {
                                patno = Common.MyCStr(DT.Rows[0]["patno"]);
                                qry = "select COUNT(1) as total from billtr where PATNO='" + patno + "'" + " and (ICODE in (" + package + ") or packcode in (" + package + "))";
                                //conn = Common.GetSecondConnection("WebLabDetails");
                                DT = new DataTable();
                                DT = Common.GetTableFromSessionSecond(qry, "billtr", null, null);
                                if (DT != null && DT.Rows.Count > 0)
                                {
                                    if (Common.MyCDbl(DT.Rows[0]["total"]) > 0)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return true;
                            }


                        }
                        else
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                return false;
            }


        }
    } 
}