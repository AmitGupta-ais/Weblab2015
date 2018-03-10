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
using System.Reflection;
using AISWebCommon;
using System.Data.Common;

namespace WebLab
{
    public partial class Change_Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session.Abandon();
                lblversion.InnerText = " [" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "]";
                HospName.InnerText = "WebLab";//Common.gettunvar("SYHEAD1").Trim();
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            bool Changed = false;
            DbConnection Conn=Common.GetConnectionFromSessionSecond();
            try
            {
                ColumnDataCollection Coll = new ColumnDataCollection();
                Coll.Add("DefPass", 0);
                Coll.Add("Pass", Common.MyCStr(txt_pswd1.Text));
                Changed = Common.UpdateTable("Pat_Reg", Coll, AisUpdateTableType.Update, "PCODE='" + Common.MyCStr(Request.QueryString["PCODE"]) + "'", Conn, null);
            }
            catch (Exception ex)
            {
                Changed = false;
                string strEx = ex.Message;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "", "JavaScript:alert(\" " + strEx + "  \");", true);
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
            if (Changed)
            {
                Response.Redirect("Forms/frmTestTable.aspx?Pcode=" + Common.MyCStr(Request.QueryString["PCODE"]), false);
            }
        }
    }
}
