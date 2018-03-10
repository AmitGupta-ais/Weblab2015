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

namespace WebLabMaster
{
    public partial class Forms_Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {  
            string Path = Server.MapPath("~\\" + txtDatabase.Text);
            string PathSecond = Server.MapPath("~\\" + txtDatabase.Text);
            //Common.AppPath = Path;
            //Common.AppPathSecond = PathSecond;
            if (System.IO.File.Exists(Path))
            {
                Session[Common.DATABASE] = "Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path;
                //Common.ConnectionNameUsingFile = Common.MyCStr(Session[Common.DATABASE]);
                //Common.ConnectionNameUsingFileSecond = Common.MyCStr(Session[Common.DATABASE]);
                
            }
            else
            {
                Response.Write("Input Lab Path not defined " + Path);
                return;
            }
            if (System.IO.File.Exists(PathSecond))
            {
                Session[Common.DATABASESECOND] = "Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path;
                //Common.ConnectionNameUsingFile =Common.MyCStr(Session[Common.DATABASE]);
                //Common.ConnectionNameUsingFileSecond = Common.MyCStr(Session[Common.DATABASE]);
            }
            else
            {
                Response.Write("Input Second Lab Path not defined " + Path);
                return;
            }

            lblDB.Text =Common.MyCStr(Session[Common.DATABASE]);
            lblSecDB.Text = Common.MyCStr(Session[Common.DATABASESECOND]);
            WebLabMaster.clsReport.SetRepoVal();

            Response.Redirect("frmHome.aspx");
        }
}
    
}