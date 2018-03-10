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
using System.Text;


 

namespace WebLabMaster
{
    public partial class UserControls_ucQryDetailTable : System.Web.UI.UserControl
    {
        //clsQueryDetail objQryDetail = new clsQueryDetail();
        //clsAisReport objReport = new clsAisReport();
        //public DataTable dt;
        //public ArrayList ReportArrList = new ArrayList();

        int startDataFrom = 0;
        int endDataTo = 0;
        int NoOfDataToShow = 20;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblPNo1.Text = Common.MyCStr((int)Common.MyCDbl(lblPNo1.Text) + 1);
                lblPNo.Text = Common.MyCStr((int)Common.MyCDbl(lblPNo.Text) + 1);
                startDataFrom = 0;
                ShowData(startDataFrom);
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            ArrayList HColl = new ArrayList();
            HColl.Add("");
            ArrayList FColl = new ArrayList();
            FColl.Add("");

            clsRptClass objRptClass;
            objRptClass = (clsRptClass)Session["RPT"];

            AISWebCommon.clsAisReportSetting.ShowReportOutputPDF("", "TEST", objRptClass.arr, objRptClass.dt, "", objRptClass.CollHeader, FColl, objRptClass.GroupField1, objRptClass.GroupDescriptionField1, null, null, objRptClass.GroupField2,objRptClass.GroupDescriptionField2, null, null, null, null, null, null, null, null, null, null,false,false);
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ArrayList HColl = new ArrayList();
            HColl.Add("");
            ArrayList FColl = new ArrayList();
            FColl.Add("");

            clsRptClass objRptClass;

            objRptClass = (clsRptClass)Session["RPT"];

            StringBuilder str;
            str = clsAisReportSetting.ShowReportOutput("", "TEST", objRptClass.arr, objRptClass.dt, "", objRptClass.CollHeader, FColl, objRptClass.GroupField1, objRptClass.GroupDescriptionField1, objRptClass.GroupField2, objRptClass.GroupDescriptionField2,"","", 0, objRptClass.dt.Rows.Count,false,false);
            AISWebCommon.clsShowExcel objXlsdrep = new clsShowExcel();
            objXlsdrep.ExcelReport = str.ToString();
            objXlsdrep.showReportinExcel();
        }

      void ShowData(int stPos)
        {
            btnLast.Enabled = true;
            btnPre.Enabled = true;
            btnNext.Enabled = true;
            btnFirst.Enabled = true;

            btnLast1.Enabled = true;
            btnPrev1.Enabled = true;
            btnNext1.Enabled = true;
            btnFirst1.Enabled = true;

            ArrayList HColl = new ArrayList();
            HColl.Add("");
            ArrayList FColl = new ArrayList();
            FColl.Add("");

            clsRptClass objRptClass;
            
            objRptClass = (clsRptClass)Session["RPT"];
            int icnt = objRptClass.dt.Rows.Count;
            lblCountofData.Text = Common.MyCStr(icnt);
            int TotalPage = (icnt / NoOfDataToShow) + 1;
            lblTotalPage.Text = Common.MyCStr(TotalPage);
            lblTotalPage1.Text = Common.MyCStr(TotalPage);
            startDataFrom = stPos;
            endDataTo = startDataFrom + NoOfDataToShow - 1;

            StringBuilder sb = clsAisReportSetting.ShowReportOutput("", "TEST", objRptClass.arr, objRptClass.dt, "", objRptClass.CollHeader, FColl, objRptClass.GroupField1, objRptClass.GroupDescriptionField1, objRptClass.GroupField2, objRptClass.GroupDescriptionField2,"","", startDataFrom, endDataTo,false,false);
            Literal1.Text = sb.ToString();

           
            lblStart.Text = Common.MyCStr(startDataFrom);
            lblEnd.Text = Common.MyCStr(endDataTo);
            CheckEnable();
        }
        protected void btnWord_Click(object sender, EventArgs e)
        {
            ArrayList HColl = new ArrayList();
            HColl.Add("");
            ArrayList FColl = new ArrayList();
            FColl.Add("");

            clsRptClass objRptClass;
            
            objRptClass = (clsRptClass)Session["RPT"];
            StringBuilder str;
            str = clsAisReportSetting.ShowReportOutput("", "TEST", objRptClass.arr, objRptClass.dt, "", objRptClass.CollHeader, FColl, objRptClass.GroupField1, objRptClass.GroupDescriptionField1, objRptClass.GroupField2, objRptClass.GroupDescriptionField2,"","", 0, objRptClass.dt.Rows.Count,false,false);
            AISWebCommon.clsShowWord objWordrep = new clsShowWord();
            objWordrep.wordReport = str.ToString();
            objWordrep.showReportinWord();

            
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            NextRecord();
        }
        protected void btnPre_Click(object sender, EventArgs e)
        {
            PrevRecord();
        }
        protected void btnLast_Click(object sender, EventArgs e)
        {
            LastRecord();
        }
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            FirstRecord();
        }
        void FirstRecord()
        {
            lblPNo.Text = Common.MyCStr(1);
            lblPNo1.Text = Common.MyCStr(1);
            startDataFrom = 0;
            ShowData(startDataFrom);
            CheckEnable();
        }
        void NextRecord()
        {
            lblPNo.Text = Common.MyCStr((int)Common.MyCDbl(lblPNo.Text) + 1);
            lblPNo1.Text = Common.MyCStr((int)Common.MyCDbl(lblPNo1.Text) + 1);
            startDataFrom = (int)(Common.MyCDbl(lblEnd.Text) + 1);
            ShowData(startDataFrom);
            CheckEnable();
        }

        void LastRecord()
        {
            int icnt = (int)Common.MyCDbl(lblCountofData.Text);
            int rem = (int)(icnt % NoOfDataToShow);
            lblPNo.Text = Common.MyCStr(icnt / NoOfDataToShow + 1);
            lblPNo1.Text = Common.MyCStr(icnt / NoOfDataToShow + 1);
            startDataFrom = icnt - rem;
            ShowData(startDataFrom);
            CheckEnable();
        }

        void PrevRecord()
        {
            lblPNo.Text = Common.MyCStr((int)Common.MyCDbl(lblPNo.Text) - 1);
            lblPNo1.Text = Common.MyCStr((int)Common.MyCDbl(lblPNo1.Text) - 1);
            startDataFrom = (int)(Common.MyCDbl(lblStart.Text) - NoOfDataToShow);
            ShowData(startDataFrom);
            CheckEnable();
        }

        void CheckEnable()
        {
            if (Common.AISCompareString(lblPNo.Text, "1") == AISCompareStringResult.AISCompareEqual)
            {
                btnPre.Enabled = false;
                btnPrev1.Enabled = false;
                btnFirst.Enabled = false;
                btnFirst1.Enabled = false;
            }
            else
            {
                btnPre.Enabled = true;
                btnPrev1.Enabled = true;
                btnFirst.Enabled = true;
                btnFirst1.Enabled = true;
            }
           if(Common.AISCompareString(lblPNo.Text, lblTotalPage.Text) == AISCompareStringResult.AISCompareEqual)
            {
                btnNext.Enabled = false;
                btnNext1.Enabled = false;
                btnLast.Enabled = false;
                btnLast1.Enabled = false;
            }
           else
           {
                btnNext.Enabled = true;
                btnNext1.Enabled = true;
                btnLast.Enabled = true;
                btnLast1.Enabled = true;
            }
        }
}
    
}