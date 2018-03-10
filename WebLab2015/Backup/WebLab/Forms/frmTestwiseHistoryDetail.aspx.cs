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
using System.Collections.Generic;


namespace WebLabMaster
{
    public partial class Forms_frmTestwiseHistoryDetail : System.Web.UI.Page
    {
        string TestGroup;
        clsTestHistoryDetail objTestHistory = new clsTestHistoryDetail();
        clsLogin objLogin = new clsLogin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Loading();
            }
        }

        void Loading()
        {
            ////////////Common.ConnectionNameUsingFile = Common.MyCStr(Session[Common.DATABASE]);
            ////////////Common.ConnectionNameUsingFileSecond = Common.MyCStr(Session[Common.DATABASE]);
            DataTable dt;
           
            clsTestHistoryDetail objTestHistory = new clsTestHistoryDetail();
            string UserType = Common.MyCStr(Session[CRMConstants.USERTYPE]);
            TestGroup = Request.QueryString["TestGroup"];
            if (Common.AISCompareString(UserType, clsConsultantLogin.USERTYPE) == AISCompareStringResult.AISCompareEqual)
            {
                objLogin = (clsLogin)Session[CRMConstants.PATIENTCODE];
            }
            else
            {
                objLogin = (clsLogin)Session[CRMConstants.LOGINUSER];
            }
            dt = objTestHistory.GetData(TestGroup, objLogin.Code);
            ArrayList arr = new ArrayList();
            clsAisReportSetting.AddRep(arr, "Test Date", "TDate", 25, false);
            clsAisReportSetting.AddRep(arr, "Test Code", "TCode", 25, false);
            clsAisReportSetting.AddRep(arr, "Test Name", "Name", 25, false);
            clsAisReportSetting.AddRep(arr, "Test Value", "TVal", 25, false);
            
            clsRptClass objRptClass = new clsRptClass();
            objRptClass.CollHeader = new ArrayList();
            objRptClass.CollHeader.Add("Name : " + objLogin.Name);
            objRptClass.CollHeader.Add("Patient No. : " + objLogin.Code);

            objRptClass.arr = arr;
            objRptClass.dt = new DataTable();
            objRptClass.dt = dt;

            Session.Add("RPT", objRptClass);
            
            objRptClass = (clsRptClass)Session["RPT"];

            //ArrayList HColl = new ArrayList();
            //HColl.Add("");
            //ArrayList FColl = new ArrayList();
            //FColl.Add("");

            //clsAisReportSetting.ShowReportOutputPDF("", "TEST", objRptClass.arr, objRptClass.dt, "", objRptClass.CollHeader, FColl, objRptClass.GroupField1, objRptClass.GroupDescriptionField1, null, null, objRptClass.GroupField2,objRptClass.GroupDescriptionField2, null, null, null, null, null, null, null, null, null, null,false,false);
        }
    }
    
}