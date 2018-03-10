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
    public partial class Forms_mpLogin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////lblDB.Text = Common.MyCStr(Session[Common.DATABASE]);
            ////lblSecDB.Text = Common.MyCStr(Session[Common.DATABASESECOND]);
            string pth = "";
            
            pth = Server.MapPath("~") + "\\header.txt";
            if (System.IO.File.Exists(pth))
            {
                string data = System.IO.File.ReadAllText(pth);
                Literal1.Text = data;
            }

            pth = Server.MapPath("~") + "\\left.txt";
            if (System.IO.File.Exists(pth))
            {
                string data = System.IO.File.ReadAllText(pth);
                Literal2.Text = data;
            }

            pth = Server.MapPath("~") + "\\right.txt";
            if (System.IO.File.Exists(pth))
            {
                string data = System.IO.File.ReadAllText(pth);
                Literal3.Text = data;
            }

            pth = Server.MapPath("~") + "\\bottom.txt";
            if (System.IO.File.Exists(pth))
            {
                string data = System.IO.File.ReadAllText(pth);
                Literal4.Text = data;
            }


        }
    } 
}
