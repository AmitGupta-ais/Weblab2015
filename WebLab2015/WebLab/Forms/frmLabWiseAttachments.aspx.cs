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
    public partial class frmLabWiseAttachments : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                if (Common.MyCStr(Session["USERTYPE"]).Trim().Length == 0)
                {
                    Response.Redirect("~/frmcommonlogin.aspx");
                }
            }

            DataTable HospDt = Common.GetTableFromSessionSecond("Select VALUE From SYSTUN Where Code='SYHEAD1'", "", null, null);
            if (HospDt != null && HospDt.Rows.Count > 0)
            {
                HospName.InnerText = Common.MyCStr(HospDt.Rows[0]["VALUE"]).Trim();
            }

            string Code = Common.MyCStr(Request.QueryString["LabNo"]);
            if (Common.MyLen(Code.Trim()) > 0)
            {
                processDownload(Code);
            }

            if (!IsPostBack)
            {
                string query = "  select Pcode,patname,patage,patsex from Labm where labno = '" + Common.MyCStr(Code) + "'";
                DataTable dt = Common.GetTableFromSession(query, "Temp", null, null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lbname.Text = Common.MyCStr(dt.Rows[0]["patname"]);
                    lbpatno.Text = Common.MyCStr(dt.Rows[0]["pcode"]);
                    lbage.Text = Common.MyCStr(dt.Rows[0]["patage"]) + "/" + Common.MyCStr(dt.Rows[0]["patsex"]);
                }

            }


        }

        public void processDownload(string Code)
        {
            string query = " select aplr.code, aplr.imgfl Name, Descr,filename from aplab_linktoraw left outer join aplab_Rawfiles aplr on aplab_linktoraw.code= aplr.code where aplab_linktoraw.labno='"+Common.MyCStr(Code)+"' "; 
            DataTable dt = Common.GetTableFromSession(query, "SS",null,null);
            if (dt != null && dt.Rows.Count > 0)
            {
                GVItemList.DataSource = dt;
                GVItemList.DataBind();
            }

            else
            {
                lbnofile.Visible = true;
                lbnofile.Text = "No attachments available for this test";

            }
            
        }

        protected void dwnld_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string code = grdrow.Cells[0].Text;
            
            
            string qry = " select ltr.filename,ltr.code, ltr.rawfileextenstion,arw.imgfl from aplab_Rawfiles arw left outer join aplab_linktoraw ltr on arw.code= ltr.code where ltr.code='" + Common.MyCStr(code) + "'";
            DataTable dt = Common.GetTableFromSession(qry, "temp", null, null);
            if (dt != null && dt.Rows.Count == 1)
            {
                download(dt);
            }

           
        }

        private void download(DataTable dt)
        {
            Byte[] bytes = (Byte[])dt.Rows[0]["imgfl"];
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = dt.Rows[0]["rawfileextenstion"].ToString();
            Response.AddHeader("content-disposition", "attachment;filename="
            + dt.Rows[0]["filename"].ToString());
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}
