using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLabMaster;
using AISWebCommon;

public partial class Forms_loginchkform : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string pcode = "";
            string uid = "";
            string pwd = "";
            pcode = Common.MyCStr(Request.QueryString["pcode"]);
            uid = Common.MyCStr(Request.QueryString["uid"]);
            pwd = Common.MyCStr(Request.QueryString["pwd"]);
            if (Common.MyLen(uid.Trim()) > 0)
            {
                connect1();
                logincheck(uid, pwd);
            }
        }

    }
   
    public void connect1()
    {
        System.Configuration.ConnectionStringSettings objConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebPatientDetails"];
        string provider = objConnectionString.ProviderName;
        string ConStr = objConnectionString.ConnectionString;
        ConStr = ConStr + "Password=dfcnkbd78378hn";
        Session[Common.DATABASE] = ConStr;
        Session[Common.PROVIDERNAME] = provider;

        if (Common.MyLen(Common.MyCStr(Session[Common.PROVIDERNAME])) == 0)
        {
            Response.Write("ProviderName is not given");
            return;
        }
        
         objConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["WebLabDetails"];
         provider = objConnectionString.ProviderName;
         ConStr = objConnectionString.ConnectionString;
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
    public void logincheck(string uid, string pwd)
    {
        List<string> CollPatID = new List<string>();
        string msg = "";
        if (Common.MyLen(uid.Trim()) == 0)
        {
            msg = "Login ID is blank";
        }
        else if (Common.MyLen(pwd.Trim()) == 0)
        {
            msg = "Password is blank";
        }
        else
        {
            clsLogin objLogin = new clsLogin();
            CollPatID = objLogin.ValidateLogin(uid, pwd);
            if (CollPatID.Count == 1)
            {
                objLogin = clsLogin.GetList(uid);
                //Session[CRMConstants.LOGINUSER] = objLogin;
                //Session[CRMConstants.USERTYPE] = clsLogin.USERTYPE;
                //Response.Redirect("frmTestTable.aspx?Pcode=" + objLogin.Code, false);
                msg = "";
                Session[CRMConstants.LOGINUSER] = objLogin;
                Session[CRMConstants.USERTYPE] = clsLogin.USERTYPE;
                
                Session["PRPG"] = Request.UrlReferrer;
               // Response.Redirect("frmTestTable.aspx?Pcode=" + objLogin.Code, false);
            }
            else
            {
                if (clsLogin.ipd == false)
                {
                    msg = "Online investigation reports are available for OPD only";

                }
                else
                {

                    msg = "Either User Name/ Password incorrect";

                }



            }
        }
        Response.ContentType = "text/json";
        Response.Write(msg);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.End();
    }


}
