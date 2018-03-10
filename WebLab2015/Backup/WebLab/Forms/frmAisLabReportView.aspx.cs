using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using WebLabMaster;
using AISWebCommon;
using System.IO;
using WebLab.DataSets;
using System.Data.Common;

namespace WebLabMaster
{
    public partial class frmAisLabReportView : System.Web.UI.Page
    {
        clsReport objRepo = new clsReport();
        string labNo;
        string testheadcode;
        string Bcode;
        string Patname;
        bool isPDF = false;
        double IsWord = 0;

        int Preview = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (File.Exists(Server.MapPath("~") + "\\startdebug.txt"))
            {
                Common.ISINDEBUGMODE = true;
            }
            if (File.Exists(Server.MapPath("~") + "\\enddebug.txt"))
            {
                Common.ISINDEBUGMODE = false;
            }
            if (!IsPostBack)
            {
                Preview = Common.MycInt(Request.QueryString["PREVIEW"]); 
                IsWord = Common.MyCDbl(Request.QueryString["ISWORD"]);
                isPDF = true;
                ShowReport();

                if (Common.MyLen(Common.MyCStr(Request.Form["UId"]).Trim()) > 0)
                {
                    Session["PRPG"] = Request.UrlReferrer;
                }

                if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE]).Trim()) > 0)
                {

                }

                else
                {
                    System.Configuration.ConnectionStringSettings objConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebPatientDetails"];
                    string provider = objConnectionString.ProviderName;
                    string ConStr = objConnectionString.ConnectionString;
                    ConStr = ConStr + "Password=dfcnkbd78378hn";
                    Session[Common.DATABASE] = ConStr;
                    Session[Common.PROVIDERNAME] = provider;

                    if (Common.MyLen(Common.MyCStr(Session[Common.PROVIDERNAME])) == 0)
                    {
                        return;
                    }
                    if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE])) == 0)
                    {
                        return;
                    }
                }
                if (Common.MyLen(Common.MyCStr(Session[Common.DATABASESECOND]).Trim()) > 0)
                {

                }

                else
                {
                    System.Configuration.ConnectionStringSettings objConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebLabDetails"];
                    string provider = objConnectionString.ProviderName;
                    string ConStr = objConnectionString.ConnectionString;
                    ConStr = ConStr + "Password=dfcnkbd78378hn";
                    Session[Common.PROVIDERNAMESECOND] = provider;
                    Session[Common.DATABASESECOND] = ConStr;
                    Common.DBType = AisDBType.Sql;
                    if (Common.MyLen(Common.MyCStr(Session[Common.PROVIDERNAMESECOND])) == 0)
                    {
                        Response.Write("ProviderName is not given");
                        return;
                    }

                }

                if (File.Exists(Server.MapPath("~\\") + "debug.txt"))
                {
                    string OutputHTMFileName = Server.MapPath("~\\") + "testConn.txt";
                    FileStream fr = new FileStream(OutputHTMFileName, FileMode.Append, FileAccess.Write);
                    //fr.CanWrite = true;
                    StreamWriter sw = new StreamWriter(fr);
                    sw.Write("  Session[Common.DATABASESECOND] :- " + Session[Common.DATABASESECOND] + Environment.NewLine + "    Session[Common.DATABASE] :- " + Session[Common.DATABASE] + Environment.NewLine);

                    sw.Flush();
                    sw.Close();
                    fr.Close();
                }

                WebLabMaster.clsReport.SetRepoVal();

            }
        }

        protected void pdfload(object sendr, EventArgs e)
        {
            ShowReport();
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            ShowReport();
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
        }

        private void ShowReport()
        {

            
            //if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE]).Trim()) > 0)
            //{
               
            //}
            //else
            //{
            //    Conn = Common.GetConnectionFromSessionSecond();
            //}

            labNo = Common.MyCStr(Request.QueryString["LabNo"]);
            testheadcode = Common.MyCStr(Request.QueryString["TestHeadCode"]);

            if ((Common.MyCStr(labNo).Trim()).Length > 0)
            {
                DataTable ds = new DataTable();
                clsReport obj = new clsReport();
                string qry;
                qry = "select * from labm where labno='" + labNo + "'";
                ds = Common.GetTableFromSession(qry, "temp", null, null);
                Common.logquery(qry);
                if (ds.Rows.Count > 0)
                {
                    Patname = Common.MyCStr(ds.Rows[0]["patname"]);
                    string patno = Common.MyCStr(ds.Rows[0]["patno"]);
                    if (obj.mycboolean(obj.gettunvar("balcashrep")) == true)
                    {
                        if ((Common.AISCompareString(Common.MyCStr(ds.Rows[0]["Cashval"]).Trim().ToUpper(), "CASH") == AISCompareStringResult.AISCompareEqual) && (Common.AISCompareString(Common.MyCStr(ds.Rows[0]["MRDTYPE"]).Trim().ToUpper(), "O") == AISCompareStringResult.AISCompareEqual))
                        {
                            try
                            {
                                double bal = obj.getpatnobalance(patno, Common.GetServerDate());
                                if (bal > 1)
                                {

                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JavaScript:ShowMSG(\"This Report can not be printed, Because Cash OPD Patient has balance Amount\");", true);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                Common.logException(ex);
                            }
                        }
                    }
                    if (Common.MyCStr(Session["Username"]).Trim().Length == 0)
                    {
                        if (obj.mycboolean(obj.gettunvar("showapprep")) == true)
                        {
                            if (Common.MyCDbl(ds.Rows[0]["isapproved"]) == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "JavaScript:ShowMSG(\"This Report is not approved,So you Don't view the Report \");", true);
                                return;
                            }
                        }
                    }
                }
            }

            if (IsWord == 1)
            {
                DataTable dt = null;
                string tcode = Common.MyCStr(Request.QueryString["tcode"]);
                dt = TableOperation.LoadFromSession("labtest", "labno='" + labNo + "' and tcode='" + tcode + "'", "pwordfile", "");

                if (dt != null && dt.Rows.Count > 0)
                {
                    byte[] bytFile = (byte[])(dt.Rows[0]["pwordfile"]);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-word";
                    
                    {
                        Response.AddHeader("content-disposition", "attachment;filename=WordReport.doc");
                    }
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.Public);

                    Response.BinaryWrite(bytFile);

                    Response.End();
                }
                else
                {
                    lblInfo.Text = "Word Supporting File not Find for Lab Tests.";
                }
                isPDF = false;
            }
            else
            {

                ReportData objReport;

                if ((Common.MyCStr(labNo).Trim()).Length > 0)
                {
                    DataTable ds = new DataTable();
                    clsReport obj = new clsReport();
                    string qry;
                    qry = "select * from labm where labno='" + labNo + "'";
                    ds = Common.GetTableFromSession(qry, "labm", null, null);
                    Common.logquery(qry);
                    if (ds.Rows.Count > 0)
                    {
                        Patname = Common.MyCStr(ds.Rows[0]["patname"]);
                    }
                }

                ////////webreportlog
                ColumnDataCollection coll;
                coll = new ColumnDataCollection();
                coll.Add("labno", labNo);
                coll.Add("hcode", testheadcode);
                coll.Add("ip", Common.MyCStr(Request.UserHostAddress));
                coll.Add("downdate", Common.GetServerDate(Common.GetConnectionFromSession()));
                coll.Add("patname", Patname);

                Common.UpdateTable("webreportlog", coll, AisUpdateTableType.Insert, "", Common.GetConnectionFromSession(), null);



                objReport = objRepo.GetData(labNo, testheadcode);


                string sflname2 = Server.MapPath("~") + "\\test.xml";
                /////////////objReport = new ReportData();
                ////////string sflname3 = Server.MapPath("~") + "\\LB13-00858.xml";
                ////////objReport.ReadXml(sflname3);


                if (File.Exists(sflname2))
                {
                    if(labNo.Contains("\"\""))
                    {
                        labNo = labNo.Replace("\"\"", "_");
                    }
                    string sflname = Server.MapPath("~") + "\\" + labNo + ".xml";

                    objReport.WriteXml(sflname);
                }

                if (objReport.Tables[0].Rows.Count > 0)
                {

                    CrystalDecisions.CrystalReports.Engine.ReportDocument objrep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    objrep.Load(Server.MapPath("~\\aisLabreport.rpt"));

                    #region Show logo in report

                    //if (File.Exists(Server.MapPath("~\\Logo.bmp")))
                    //{
                    for (int ii = 0; ii < objReport.Report_Header.Rows.Count; ii++)
                    {
                        //objReport.Report_Header.Rows[ii]["LogoImage"] = File.ReadAllBytes(Server.MapPath("~\\Logo.bmp"));
                        //objReport.Report_Header.Rows[ii]["LogoImage"] = File.ReadAllBytes(Server.MapPath("~\\Logo.bmp"));
                    }
                    double LogoLeft = 0;// 280;
                    double LogoTop = 0;// 280;
                    double LogoHeight = 0;// 1440;
                    double LogoWidth = 0;// 1440;

                    LogoLeft = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["logolft"]) * 1440);
                    LogoTop = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["logotp"]) * 1440);
                    LogoHeight = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["logoht"]) * 1440);
                    LogoWidth = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["logowid"]) * 1440);
                    CrystalDecisions.CrystalReports.Engine.BlobFieldObject fldImg = (CrystalDecisions.CrystalReports.Engine.BlobFieldObject)objrep.ReportDefinition.Sections["PageHeaderSection1"].ReportObjects["LogoImage"];
                    if ((LogoHeight > 100) && (LogoWidth > 100))
                    {

                        fldImg.Left = (int)LogoLeft;
                        fldImg.Top = (int)LogoTop;
                        fldImg.Height = (int)LogoHeight;
                        fldImg.Width = (int)LogoWidth;
                        fldImg.ObjectFormat.EnableSuppress = false;
                    }
                    else
                    {
                        fldImg.ObjectFormat.EnableSuppress = true;
                    }

                    //}
                    #endregion
                    
                    #region Show Digital Sign in report

                    double DILeft = 0;// 280;
                    double DITop = 0;// 280;
                    double DIHeight = 0;// 1440;
                    double DIWidth = 0;// 1440;

                    DILeft = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["SIlft"]) * 1440);
                    DITop = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["SItop"]) * 1440);
                    DIHeight = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["SIht"]) * 1440);
                    DIWidth = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["SIwd"]) * 1440);
                    CrystalDecisions.CrystalReports.Engine.BlobFieldObject fldImg1 = (CrystalDecisions.CrystalReports.Engine.BlobFieldObject)objrep.ReportDefinition.Sections["Section5"].ReportObjects["DigitalSign"];
                    if ((DIHeight > 100) && (DIWidth > 100))
                    {

                        fldImg1.Left = (int)DILeft;
                        fldImg1.Top = (int)DITop;
                        fldImg1.Height = (int)DIHeight;
                        fldImg1.Width = (int)DIWidth;
                        fldImg1.ObjectFormat.EnableSuppress = false;
                    }
                    else
                    {
                        fldImg1.ObjectFormat.EnableSuppress = true;
                    }

                    #endregion

                    #region show Approval Captions
                    DILeft = 0;// 280;
                    DITop = 0;// 280;
                    DIHeight = 0;// 1440;
                    DIWidth = 0;// 1440;

                    DILeft = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["ApprovedbyLeft"]));
                    DITop = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["ApprovedbyTop"]));
                    CrystalDecisions.CrystalReports.Engine.FieldObject fldappr = (CrystalDecisions.CrystalReports.Engine.FieldObject)objrep.ReportDefinition.Sections["Section5"].ReportObjects["ApprovedBy1"];

                    if (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["ShowAppr"]) == 1)
                    {
                        fldappr.Left = (int)DILeft;
                        fldappr.Top = (int)DITop;
                    }
                    else
                    {
                        fldappr.ObjectFormat.EnableSuppress = true;
                    }

                    DILeft = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["Apptimeleft"]));
                    DITop = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["ApptimeTop"]));
                    fldappr = (CrystalDecisions.CrystalReports.Engine.FieldObject)objrep.ReportDefinition.Sections["Section5"].ReportObjects["ApprovedTime1"];

                    if (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["showApptime"]) == 1)
                    {
                        fldappr.Left = (int)DILeft;
                        fldappr.Top = (int)DITop;
                    }
                    else
                    {
                        fldappr.ObjectFormat.EnableSuppress = true;
                    }

                    #endregion show Approval Captions

                    #region From Here Digital Signature for WEB

                    
                    DILeft = 0;// 280;
                    DITop = 0;// 280;
                    DIHeight = 0;// 1440;
                    DIWidth = 0;// 1440;

                    DILeft = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["WEBLOGOLT"]) * 1440);
                    DITop = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["WEBLOGOTOP"]) * 1440);
                    DIHeight = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["WEBLOGOHT"]) * 1440);
                    DIWidth = (Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["WEBLOGOWID"]) * 1440);
                    fldImg1 = (CrystalDecisions.CrystalReports.Engine.BlobFieldObject)objrep.ReportDefinition.Sections["PageFooterSection4"].ReportObjects["WEBSIGNIMAGE1"];
                    if (DIHeight > 0 && DIWidth > 0)
                    {
                     
                        if ((DIHeight > 100) && (DIWidth > 100))
                        {

                            fldImg1.Left = (int)DILeft;
                            fldImg1.Top = (int)DITop;
                            fldImg1.Height = (int)DIHeight;
                            fldImg1.Width = (int)DIWidth;
                            fldImg1.ObjectFormat.EnableSuppress = false;
                        }
                        else
                        {
                            fldImg1.ObjectFormat.EnableSuppress = true;
                        }
                    }
                    else
                    {
                        fldImg1.ObjectFormat.EnableSuppress = true;
                    }

                    #endregion

                    if (Common.MycInt(Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["FooterHeight"]) * 1440) > Common.MycInt(objrep.ReportDefinition.Sections["Section5"].ReportObjects["Footer1"].Height))
                    {
                        objrep.ReportDefinition.Sections["Section5"].ReportObjects["Footer1"].Height = Common.MycInt(Common.MyCDbl(objReport.Tables["Report_Header"].Rows[0]["FooterHeight"]) * 1440);
                    }
                    objrep.SetDataSource(objReport);
                    CrystalDecisions.CrystalReports.Engine.ReportDocument SUBREP = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    SUBREP = objrep.OpenSubreport("Culture.rpt");
                    SUBREP.SetDataSource(objReport.Tables[4]);
                    if (objReport.Tables[4] == null || objReport.Tables[4].Rows.Count == 0)
                    {
                        
                    }


                    objrep.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    if (isPDF)
                    {
                        

                        if (Preview == 1)
                        {
                            Stream memoryStream =objrep.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                            byte[] buffer = new byte[16 * 1024];
                            MemoryStream ms = new MemoryStream();
                            {
                                int read;
                                while ((read = memoryStream.Read(buffer, 0, buffer.Length)) > 0)
                                {
                                    ms.Write(buffer, 0, read);
                                }
                            }

                            if (ms != null)
                            {

                                byte[] bytes = ms.ToArray();

                                string mimeType = string.Empty;

                                Response.ContentType = mimeType;
                                Response.AddHeader("content-disposition", "inline;filename=Report.pdf");
                                Response.BinaryWrite(bytes);
                                
                            }
                            else
                            {
                                Response.Write("Error Occured: Try with any other report.");
                            }
                            
                        }
                        else
                        {
                            objrep.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, labNo.Trim() + "-Report.pdf");


                        }


                    }
                    else
                    {
                        CRV1.ReportSource = objrep;
                    }
                    objrep.Refresh();
                }

                else
                {
                    Session["ShowReport"] = 1;


                    Response.Redirect("~/forms/frmTestTable.aspx");
                }
            }
        }

        private void wordReport()
        {
            //******************************By Suneel
            labNo = Common.MyCStr(Request.QueryString["LabNo"]);
            Bcode = Common.MyCStr(Request.QueryString["Bcode"]);
            clsWord cword = new clsWord();
            cword.checkWord(labNo);
            if (cword.wordCount > 0)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "openConfirmBox();", true);
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            isPDF = true;
            IsWord = 0;
            ShowReport();
        }

        void showWord(string code)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "", "alert('Value : " + code + "');", true);
        }

    }
}