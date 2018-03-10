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
using System.IO;
using AISWebCommon;

namespace WebLabMaster
{
    public partial class Forms_frmLogin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Common.MyLen(Common.MyCStr(Request.Form["UId"]).Trim()) > 0)
                {
                    Session["PRPG"] = Request.UrlReferrer;
                }
                //if (System.Configuration.ConfigurationManager.ConnectionStrings["WebPatientDetails"] == null)
                //{
                //    labpath = Request.QueryString["labdbpath"];


                //    fllabdbpath = Server.MapPath("~");
                //    fllabdbpath = fllabdbpath + "\\labdbpath.txt";
                //    if (Common.MyLen(labpath) == 0)
                //    {
                //        if (System.IO.File.Exists(fllabdbpath))
                //        {
                //            string str1 = System.IO.File.ReadAllText(fllabdbpath);
                //            if (Common.MyLen(str1) > 0)
                //            {
                //                labpath = str1;
                //            }
                //        }
                //    }

                //    if ((Common.MyLen(labpath)) == 0)
                //    {
                //        Response.Write("Data Path not defined. Please try again");
                //        return;
                //    }
                //    Session["labdbpath"] = labpath;
                //    Common.ERRORPAGE = Server.MapPath("~\\Forms\\frmSessionEnd.aspx");
                //}
                //string Path = Server.MapPath("~\\" + labpath);
                //string PathSecond = Server.MapPath("~\\" + labpath);
                //if (System.IO.File.Exists(Path))
                //{
                    if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE]).Trim()) > 0)
                    {
                        
                    }
                //    else
                //    {
                //        Session[Common.PROVIDERNAME] = "System.Data.OleDb";
                //        Session[Common.DATABASE] = "Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path;
                //    }
                //}
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
                        //Response.Write("ProviderName is not given");
                        return;
                    }
                    if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE])) == 0)
                    {
                        //Response.Write("Input Lab Path not defined " + Path);
                        return;
                    }
                }

                //if (System.IO.File.Exists(PathSecond))
                //{
                    if (Common.MyLen(Common.MyCStr(Session[Common.DATABASESECOND]).Trim()) > 0)
                    {

                    }
                //    else
                //    {
                //        Session[Common.PROVIDERNAMESECOND] = "System.Data.OleDb";
                //        Session[Common.DATABASESECOND] = "Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path;
                //        Common.DBType = AisDBType.Access;
                //    }
                //}
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
                    //if (Common.MyLen(Common.MyCStr(Session[Common.DATABASESECOND])) == 0)
                    //{
                    //    Response.Write("Input Second Lab Path not defined " + Path);
                    //    return;
                    //}

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
            //else
            //{
            //    Response.Write("Input Lab Path not defined");

            //}
        }
    }

}

