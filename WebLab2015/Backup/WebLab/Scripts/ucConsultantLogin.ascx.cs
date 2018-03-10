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
    public partial class UserControls_ucConsultantLogin : System.Web.UI.UserControl
    {   
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool isFound;
            clsConsultantLogin objConsLogin = new clsConsultantLogin();
            isFound = objConsLogin.ValidateConsultantLogin(txtLoginID.Text, txtPassword.Text);
            if (isFound)
            {
                //Session.RemoveAll();
                objConsLogin = clsConsultantLogin.GetList(txtLoginID.Text);
                Session[CRMConstants.LOGINUSER] = objConsLogin;
                Session[CRMConstants.USERTYPE] = clsConsultantLogin.USERTYPE;
                Response.Redirect("frmConsultMenuTable.aspx", false);
            }
        }
}
}