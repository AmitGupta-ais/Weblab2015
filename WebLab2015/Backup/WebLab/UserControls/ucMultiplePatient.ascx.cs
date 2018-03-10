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

namespace WebLabMaster
{
    public partial class UserControls_ucMultiplePatient : System.Web.UI.UserControl
    {
        clsLogin objLogin = new clsLogin();
        string LoginID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loading();
            }
        }
        void Loading()
        {
            DataTable dt;
            LoginID = Request.QueryString["email"];
            dt=objLogin.GetMultiPatientList(LoginID);
            GVPatient.DataSource = dt;
            GVPatient.DataBind();
        }
    } 
}
