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
using System.Reflection;
using System.Web.Services;
using System.Data.Common;
using System.IO;

namespace WebLab
{
    public partial class frmCommonLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                

                if (Common.MyLen(Common.MyCStr(Request.Form["UId"]).Trim()) > 0)
                {
                    Session["PRPG"] = Request.UrlReferrer;
                }
             

                if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE]).Trim()) ==0)
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
                if (Common.MyLen(Common.MyCStr(Session[Common.DATABASESECOND]).Trim()) == 0)
                {
                    System.Configuration.ConnectionStringSettings objConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebLabDetails"];
                    string provider = objConnectionString.ProviderName;
                    string ConStr = objConnectionString.ConnectionString;
                    ConStr = ConStr + "Password=dfcnkbd78378hn";
                    Session[Common.PROVIDERNAMESECOND] = provider;
                    Session[Common.DATABASESECOND] = ConStr;
                    //Common.DBType = AisDBType.Sql;
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
                    StreamWriter sw = new StreamWriter(fr);
                    sw.Write("  Session[Common.DATABASESECOND] :- " + Session[Common.DATABASESECOND] + Environment.NewLine + "    Session[Common.DATABASE] :- " + Session[Common.DATABASE] + Environment.NewLine);

                    sw.Flush();
                    sw.Close();
                    fr.Close();
                }
                WebLabMaster.clsReport.SetRepoVal();
            }


            if (!IsPostBack)
            {
                //Session.Abandon();
                lblversion.InnerText = " [" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "]";
                HospName.InnerText = "WebLab";//Common.gettunvar("SYHEAD1").Trim();
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string UserName = txt_userName.Text;           
                string Password = txt_pswd.Text;                         
                string Query = "Select * from userrole where username='" + UserName + "' And Lpass='" + Password + "' ";
                DataTable Dt = Common.GetTableFromSession(Query, "UserRole",null,null);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    if (Common.MycInt(Dt.Rows[0]["notinuse"]) == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "JavaScript:alert(\"" + UserName + " marked not in use.\");", true);
                    }
                    else
                    {
                        Session["lname"] = Common.MyCStr(Dt.Rows[0]["username"]).Trim();
                        Session["Username"] = Common.MyLen(Common.MyCStr(Dt.Rows[0]["UserDescr"]).Trim()) > 0 ? Common.MyCStr(Dt.Rows[0]["UserDescr"]).Trim() : Common.MyCStr(Dt.Rows[0]["Username"]).Trim();
                        Session["HideUnAppRep"] = Common.MyCStr(Common.MycInt(Dt.Rows[0]["hideUnAPPRep"]));
                        Session["CONSCODE"] = null;
                        if (Common.MycInt(Dt.Rows[0]["IsConsultant"]) == 1)
                        {

                            Session["HideUnAppRep"] = "0";

                            ArrayList ArrList = getconsultants(Common.MyCStr(Dt.Rows[0]["username"]));
                            if (Common.MyCStr(Dt.Rows[0]["consultantCode"]).Trim().Length > 0)
                            {
                                ArrList.Add(Common.MyCStr(Dt.Rows[0]["consultantCode"]));
                            }
                            //consCode = Common.MyCStr(Dt.Rows[0]["consultantCode"]);
                            Session["CONSCODE"] = ArrList;
                        }
                        Response.Redirect("~/Forms/frmPerformTests.aspx");
                        
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "JavaScript:alert(\"User Name or Password Are not Matched.\");", true);
                }
            }
            catch (Exception ex)
            {

                ///
                string strEx = ex.Message;

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "", "JavaScript:alert(\" " + strEx + "  \");", true);
            }
        }

        private ArrayList getconsultants(string UserCode)
        {
            ArrayList arrList = new ArrayList();

            DataTable dt = Common.GetTableFromSession("select consultantcode from userRoleconsTr where userName ='" + UserCode + "' ", "Temp", null, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    arrList.Add(Common.MyCStr(dr["consultantcode"]));
                }
            }

            return arrList;
        }
    }
}
