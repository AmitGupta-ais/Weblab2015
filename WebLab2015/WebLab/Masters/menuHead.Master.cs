using AISWebCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebLab.Masters
{
    public partial class menuHead : System.Web.UI.MasterPage
    {
        public static string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                Response.Redirect("~/frmcommonlogin.aspx");
                username = Common.MyCStr(Session["lname"]);
            }

            #region Hospital Name
            DataTable HospDt = Common.GetTableFromSessionSecond("Select VALUE From SYSTUN Where Code='SYHEAD1'", "", null, null);
            if (HospDt != null && HospDt.Rows.Count > 0)
            {
                HospName.InnerText = Common.MyCStr(HospDt.Rows[0]["VALUE"]).Trim();
                lblversion.InnerText = " [" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "]";

            }
            if (Session["Username"] != null)
            {
                UserName.Text = Common.MyCStr(Session["Username"]).Trim();
                if (Common.MyLen(Common.MyCStr(Session["CONSCODE"])) > 0)
                {
                    HospDt = Common.GetTableFromSessionSecond("Select Name From ConsMAST Where Code='" + Common.MyCStr(Session["CONSCODE"]) + "'", "", null, null);
                    if (HospDt != null && HospDt.Rows.Count > 0)
                    {
                        UserName.Text += "[" + Common.MyCStr(HospDt.Rows[0]["Name"]).Trim() + "]";
                    }
                }
            }
            #endregion
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/frmCommonLogin.aspx");
        }
    }
}