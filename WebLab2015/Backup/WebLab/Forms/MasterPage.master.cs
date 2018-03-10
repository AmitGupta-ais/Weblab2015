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
using AISWebCommon;
using System.Reflection;
namespace WebLabMaster
{
    public partial class Forms_MasterPage : System.Web.UI.MasterPage
    {
        clsLogin objLogin = new clsLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblVersion.Text = "Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
                if (Common.AISCompareString(Common.MyCStr(Session[CRMConstants.USERTYPE]), clsLogin.USERTYPE) == AISCompareStringResult.AISCompareEqual)
                {
                    objLogin = (clsLogin)Session[CRMConstants.LOGINUSER];
                    lblShowName.Text = objLogin.Name;
                    lblShowPatNo.Text = objLogin.Code;
                    lblShowAgeSex.Text = Common.MyCStr(objLogin.Age) + " / " + objLogin.Sex;
                }
                else
                {
                    lblShowName.Visible=false;
                    lblShowPatNo.Visible=false;
                    lblShowAgeSex.Visible = false;
                    lblAgeSex.Visible = false;
                    lblPatNo.Visible = false;
                    lblName.Visible = false;
                }
            }
            lblDB.Text = Common.MyCStr(Session[Common.DATABASE]);
            lblSecDB.Text = Common.MyCStr(Session[Common.DATABASESECOND]);

            string pth = Server.MapPath("~");
            pth = pth + "\\header.txt";
            if (System.IO.File.Exists(pth))
            {
                string data = System.IO.File.ReadAllText(pth);
                Literal1.Text = data;
            }
        }
        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            #region Code for disabling the back button of IE
            //Session.Abandon();
            //Response.Buffer = true;
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            //Response.Expires = -1500;
            //Response.CacheControl = HttpCacheability.NoCache.ToString();
            #endregion
            string addlpath = "";
            if (Session["labdbpath"] != null)
            {
                addlpath = "?labdbpath=" + Session["labdbpath"].ToString();
            }
            string finalurl;
            if (Common.MyLen(Common.MyCStr(Session["PRPG"]).Trim()) > 0)
            {
                finalurl = Common.MyCStr(Session["PRPG"]).Trim();
            }
            else
            {
                finalurl = "frmLogin.aspx";
                finalurl = finalurl + addlpath;
            }
            Session.Abandon();
            Response.Redirect(finalurl);
            
        }
    } 
}
