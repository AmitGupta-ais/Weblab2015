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
using WebLab.DataSets;

namespace WebLabMaster
{
    public partial class UserControls_ucTestReports : System.Web.UI.UserControl
    {
        clsLogin objLogin = new clsLogin();
        clsLabMTable objLabTable = new clsLabMTable();
        Boolean ischkday = false;
        int days = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string LoginCode = "";
            string qrychk = "select * from webreportsettings";
            DataTable dtchk = Common.GetTableFromSession(qrychk, "webst", null, null);
            if (dtchk.Rows.Count > 0)
            {
                foreach (DataRow dr in dtchk.Rows)
                {
                    if (Common.MycInt(dr["shwrptld"]) == 1)
                    {
                        ischkday = true;
                        days = Common.MycInt(dr["txtrptld"]);
                    }

                    Session["IsNotAllowOPDCreditBalRep"] = Common.MycInt(dr["hideopdbalrep"]).ToString();
                }
            }
            string UserType = Common.MyCStr(Session[CRMConstants.USERTYPE]);
            if (Common.AISCompareString(UserType, clsConsultantLogin.USERTYPE) == AISCompareStringResult.AISCompareEqual)
            {
                LoginCode = Request.QueryString["PCode"];
                objLogin = clsLogin.GetList(LoginCode);
                Session[CRMConstants.PATIENTCODE] = objLogin;
                if (objLogin.Ispatno)
                {
                    LoginCode = objLogin.patno;
                }
                else
                {
                    LoginCode = objLogin.Code;
                }
            }
            else if (Common.AISCompareString(UserType, clsLogin.USERTYPE) == AISCompareStringResult.AISCompareEqual)
            {
                objLogin = (clsLogin)Session[CRMConstants.LOGINUSER];

                if (objLogin.Ispatno)
                {
                    LoginCode = objLogin.patno;
                }
                else
                {
                    LoginCode = objLogin.Code;
                }
            }
            else if (Common.AISCompareString(UserType, clsLogin.MULTIPATIENT) == AISCompareStringResult.AISCompareEqual)
            {
                LoginCode = Request.QueryString["PCode"];
                objLogin = clsLogin.GetList(LoginCode);

                Session[CRMConstants.LOGINUSER] = objLogin;

                if (objLogin.Ispatno)
                {
                    LoginCode = objLogin.patno;
                }
                else
                {
                    LoginCode = objLogin.Code;
                }
            }
            if (!IsPostBack)
            {
                Session["x1"] = objLabTable.GetData(LoginCode, ReportMenuType.Datewise);
                Session["x2"] = objLabTable.GetData(LoginCode, ReportMenuType.TestwiseHistory);
                Loading(ReportMenuType.Datewise);
            }

        }

        protected void lbtnDatewise_Click(object sender, EventArgs e)
        {
            Loading(ReportMenuType.Datewise);
        }

        protected void lbtnTestwise_Click(object sender, EventArgs e)
        {
            Loading(ReportMenuType.TestwiseHistory);
        }

        void Loading(ReportMenuType RepoMenu)
        {
            lblInfo.Visible = false;
            dsReportMenu objReportMenu = new dsReportMenu();
            string code = "";
            if (RepoMenu == ReportMenuType.Datewise)
            {
                objReportMenu = (dsReportMenu)Session["x1"];
                if (ischkday)
                {
                    for (int i = 0; i < objReportMenu.DatewiseReportMenu.Rows.Count; i++)
                    {
                        if (Common.GetDateEndTime(Common.MyCDate(Common.MyCStr(objReportMenu.DatewiseReportMenu.Rows[i]["TestDate"])).AddDays(Common.MyCDbl(days))) <= DateTime.Now)
                        {
                            objReportMenu.DatewiseReportMenu.Rows[i].Delete();
                            i--;
                        }
                    }
                }
                if (objReportMenu.DatewiseReportMenu.Rows.Count != 0)
                {
                    GV1.DataSource = objReportMenu.DatewiseReportMenu;
                    GV1.DataBind();
                    code = Common.MyCStr(objReportMenu.DatewiseReportMenu.Rows[0]["LabNo"]);
                }
                else
                {
                    if (ischkday == true)
                    {
                        lblInfo.Text = "Investigation Reports are availabe to download for " + days + "days only";
                    }
                    AisLabel1.Visible = false;
                    lblInfo.Visible = true;
                }
                GV2.DataSource = null;
                GV2.DataBind();
            }
            else
            {
                objReportMenu = (dsReportMenu)Session["x2"];
                if (ischkday)
                {
                    for (int i = 0; i < objReportMenu.TestwiseHistoryReportMenu.Rows.Count; i++)
                    {
                        if (Common.MyCDate(Common.MyCStr(objReportMenu.TestwiseHistoryReportMenu.Rows[i]["TestDate"])).AddDays(Common.MyCDbl(days)) <= DateTime.Now)
                        {
                            objReportMenu.TestwiseHistoryReportMenu.Rows[i].Delete();
                            i--;
                        }
                    }
                }
                if (objReportMenu.TestwiseHistoryReportMenu.Rows.Count != 0)
                {
                    GV2.DataSource = objReportMenu.TestwiseHistoryReportMenu;
                    GV2.DataBind();
                    code = Common.MyCStr(objReportMenu.TestwiseHistoryReportMenu.Rows[0]["LabNo"]);
                }
                else
                {
                    lblInfo.Visible = true;
                }

                GV1.DataSource = null;
                GV1.DataBind();
            }

            if (Common.MyLen(code.Trim()) > 0)
            {
                string qrywebreport = " select showattach from webreportsettings";
                DataTable dtweb = Common.GetTableFromSession(qrywebreport, "QRYWEB", null, null);
                if (dtweb != null && Common.MycInt(dtweb.Rows[0]["showattach"]) == 1)
                {
                    string query = " select aplr.code, aplr.imgfl Name, Descr,filename from aplab_linktoraw left outer join aplab_Rawfiles aplr on aplab_linktoraw.code= aplr.code where aplab_linktoraw.labno='" + Common.MyCStr(code) + "' ";
                    DataTable dt = Common.GetTableFromSession(query, "SS", null, null);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        GV1.Columns[3].Visible = true;
                    }
                    else
                    {
                        GV1.Columns[3].Visible = false;
                    }
                }
                else
                {
                    GV1.Columns[3].Visible = false;
                }
            }
        }

        protected void GV2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV2.PageIndex = e.NewPageIndex;
        }

        protected void GV1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV1.PageIndex = e.NewPageIndex;
        }

    }
}
