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
    public partial class UserControls_ucConsultMenuTable : System.Web.UI.UserControl
    {
        ConsultantMenuType enmConsMenuType;
        protected void Page_Load(object sender, EventArgs e)
        {
            GV1.DataSource = null;
            GV1.DataBind();

            GV2.DataSource = null;
            GV2.DataBind();
            lblInfo.Visible = false;
            if (!IsPostBack)
            {
                enmConsMenuType = ConsultantMenuType.Datewise;
                Loading(enmConsMenuType);
            }
        }

        void Loading(ConsultantMenuType ConsMenuType)
        {
            DataTable dt;
            clsConsultMenu objConMenu = new clsConsultMenu();
            clsConsultantLogin objConsLogin=new clsConsultantLogin();
            objConsLogin=(clsConsultantLogin) Session[CRMConstants.LOGINUSER];
            DateTime FromDate =Common.MyCDate(UcFromDate.GetDate());
            DateTime ToDate = Common.MyCDate(UcToDate.GetDate());
            
            dt = objConMenu.GetData(objConsLogin.Code, cbAll.Checked,FromDate , ToDate, ConsMenuType);

            if (dt.Rows.Count > 0)
            {
                if (ConsMenuType == ConsultantMenuType.Datewise)
                {
                    GV2.DataSource = dt;
                    GV2.DataBind();
                    GV1.DataSource = null;
                    GV1.DataBind();
                }
                else
                {
                    GV1.DataSource = dt;
                    GV1.DataBind();
                    GV2.DataSource = null;
                    GV2.DataBind();
                }
            }
            else
            {
                lblInfo.Visible = true;
            }
        }
        protected void GV2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        
        protected void lbtnDatewise_Click(object sender, EventArgs e)
        {
            enmConsMenuType = ConsultantMenuType.Datewise;
            Loading(enmConsMenuType);
        }
        protected void lbtnPatientwise_Click(object sender, EventArgs e)
        {
            enmConsMenuType = ConsultantMenuType.Patientwise;
            Loading(enmConsMenuType);
        }
} 
}
