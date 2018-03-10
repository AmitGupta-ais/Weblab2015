using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AISWebCommon;
using System.Data.Common;
namespace WebLabMaster
{

    public partial class Forms_frmTestTable : System.Web.UI.Page
    {
        string labNo;
        string isapp = "";
        string IsOPDCreditBal = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                if (Common.MyCStr(Session["USERTYPE"]).Trim().Length == 0)
                {
                    Response.Redirect("~/frmcommonlogin.aspx");
                }
            }


            labNo = Common.MyCStr(Request.QueryString["Labno"]);
            isapp = Common.MyCStr((Request.QueryString["IsApp"]));
            //panelWord.Visible = false;

            // Check Credit Balance for OPD
            IsOPDCreditBal = Common.MyCStr(Request.QueryString["IsOPDCreditBal"]);
            string IsPerformTest = Common.MyCStr((Request.QueryString["FromPerformTest"]));
            if (IsPerformTest == "Y")
            {
                showReportPanel();
                if (Session["ShowReport"] != null)
                {
                    if (Session["ShowReport"].ToString() == "1")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", @"alert('No data found to print report');", true);
                    }
                    Session["ShowReport"] = null;
                }
            }
            else if (Common.AISCompareString(IsOPDCreditBal,"Y") == AISCompareStringResult.AISCompareEqual)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", @"alert(' OPD credit balance at labno :-" + labNo.Trim() + " ');", true);
            }
            else if (isapp == "0" )
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", @"alert('This test report is under process.Please try later on');", true);
            }
            else
            {
                showReportPanel();
                if (Session["ShowReport"] != null)
                {
                    if (Session["ShowReport"].ToString() == "1")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorMessage", @"alert('No data found to print report');", true);
                    }
                    Session["ShowReport"] = null;
                }
            }
        }
        private void showReportPanel()
        {
            int i = 1;
            int j = 1;
            string tcode = "";
            DataTable dt = null;
            if (Common.MyLen(labNo) > 0)
            {
                dt = TableOperation.LoadFromSession("labtest", "labno='" + labNo + "'", "", "");


                foreach (DataRow dr in dt.Rows)
                {
                    
                    if (Common.MyLen(Common.MyCStr(dr["pwordfile"]).Trim()) > 0)
                    {
                        LinkButton lb = new LinkButton();
                        tcode = Common.MyCStr(dr["tcode"]).Trim();
                        lb.ID = tcode + "&isWord=1";

                        DataTable dtt = TableOperation.LoadFromSession("testlib", "code='" + tcode + "'", "name", "");
                        lb.Text = Common.MyCStr(dtt.Rows[0]["name"]);
                        lb.Click += new EventHandler(lb_Click);

                        ImageMap img = new ImageMap();
                        img.ID = "img" + i;
                        img.ImageUrl = "~/images/RTF_icon.jpg";

                        panelWord.Controls.Add(img);
                        panelWord.Controls.Add(new LiteralControl("&nbsp;"));
                        panelWord.Controls.Add(lb);
                        panelWord.Controls.Add(new LiteralControl("<br/>"));
                        i++;
                    }

                }
                if (i > 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Common.MyLen(Common.MyCStr(dr["Tval"]).Trim()) > 0 && j == 1)
                        {
                            LinkButton lb = new LinkButton();
                            lb.ID = Common.MyCStr(dr["Bcode"]).Trim() + "&isWord=0";
                            DataTable dtt = TableOperation.LoadFromSession("testlib", "code='" + Common.MyCStr(dr["Bcode"]).Trim() + "'", "name", "");
                            lb.Text = "Report";
                            lb.Click += new EventHandler(lb_Click);

                            ImageMap img2 = new ImageMap();
                            img2.ID = "img2" + j;
                            img2.ImageUrl = "~/images/pdf-ico.gif";

                            panelWord.Controls.Add(img2);
                            panelWord.Controls.Add(new LiteralControl("&nbsp;"));
                            panelWord.Controls.Add(lb);
                            panelWord.Controls.Add(new LiteralControl("<br/>"));
                            j++;
                            
                        }
                    }
                }
                else
                {
                    if (AllowDisplay())
                    {
                        string testhead = Common.MyCStr(Request.QueryString["TestHeadCode"]);
                        Response.Redirect("~/Forms/frmAisLabReportView.aspx?labno=" + labNo + "&TestHeadCode=" + testhead);
                        panelWord.Visible = false;
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "alert('This report cannot be displayed here. It can only be collected from Hospital')", true);
                    }
                }
                panelWord.Height = 100 * dt.Rows.Count;
                panelWord.Visible = !panelWord.Visible;
            }
            else
            {
                panelWord.Visible = false;
            }
            if (i == 2 && j == 1)
            {
                if (AllowDisplay())
                {
                    panelWord.Visible = false;
                    Response.Redirect("~/Forms/frmAisLabReportView.aspx?labno=" + labNo + "&tcode=" + tcode + "&isword=1");
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "alert('This report cannot be displayed here. It can only be collected from Hospital')", true);
                }
            }
        }
        private void lb_Click(object sender, EventArgs e)
        {
            if (AllowDisplay())
            {
                LinkButton btnClicked = (LinkButton)sender;
                Response.Redirect("~/Forms/frmAisLabReportView.aspx?labno=" + labNo + "&tcode=" + btnClicked.ID);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "alert('This report cannot be displayed here. It can only be collected from Hospital')", true);
            }
        }

        protected bool AllowDisplay()
        {
            string qry = "",patno="";
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
                            qry = "select patno from labm where labno='" + labNo + "'";
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
