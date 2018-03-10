using AISWebCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using WebLab.Classes;

namespace WebLab.Forms
{
    [System.Web.Script.Services.ScriptService]
    public partial class frmTestHeadLib : System.Web.UI.Page
    {
        private string username;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDept();
            }
            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                Response.Redirect("~/frmcommonlogin.aspx");
                username = Common.MyCStr(Session["lname"]);
            }
        }
        public void loadDept()
        {
            string qry = "select * from Dept";
            DataTable dt = Common.GetTableFromSession(qry, "Dept", null, null);
            DataRow dr = dt.NewRow();
            dr["Name"] = "All";
            dr["Code"] = "ALL";
            dt.Rows.InsertAt(dr, 0);
            ddlDept.DataSource = dt;
            ddlDept.DataTextField = "Name";
            ddlDept.DataValueField = "Code";
            ddlDept.DataBind();
        }
       

    }
}