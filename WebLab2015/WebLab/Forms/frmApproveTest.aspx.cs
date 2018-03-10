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
using System.Data.Common;
using WebLabMaster;
using WebLab.Classes;
using System.Collections.Generic;
using System.Web.Services;
using WebLab.UserControls;
using SautinSoft;
using System.IO;



namespace WebLab.Forms
{
    public partial class frmApproveTest : System.Web.UI.Page
    {
        string Labno = string.Empty;
        string Pcode = string.Empty;
        string headcode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Labno = Request.QueryString["labno"].ToString();
            Pcode = Request.QueryString["pcode"].ToString();
            labnohdn.Value = Labno;
            txtlabno.Value = Labno;
            DateTime Currdate = DateTime.Now;
            txtappdate.Value = Common.GetPrintDate(Currdate, "dd-MMM-yyyy");
            txtappby.Value = Common.MyCStr(Session["Lname"]);
            DataTable dt = Common.GetTableFromSession("select t.LCODE from labtest lt left outer join testlib t on t.code=lt.tcode where LABNO='LB16139424'", "tmp", null, null);
            if (dt.Rows.Count>0)
            {
                headcode = Common.MyCStr(dt.Rows[0]["lcode"]);
                txthead.Value = "";
            }
            txtname.Value = Classes.CommonFunctions.getMasterValue("code",Pcode,"patient","name",true);
            if (!IsPostBack)
            {
                
            }
        }



    }
}
