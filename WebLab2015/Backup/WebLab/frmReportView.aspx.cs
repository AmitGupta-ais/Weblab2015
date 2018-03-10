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

namespace WebLab
{
    public partial class frmReportView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE]).Trim()) > 0)
                {

                }

                else
                {
                    ConnectionStringSettings objConnectionString = ConfigurationManager.ConnectionStrings["WebPatientDetails"];
                    string provider = objConnectionString.ProviderName;
                    string ConStr = objConnectionString.ConnectionString;
                    ConStr = ConStr + "Password=dfcnkbd78378hn";
                    Session[Common.DATABASE] = ConStr;
                    Session[Common.PROVIDERNAME] = provider;

                    if (Common.MyLen(Common.MyCStr(Session[Common.PROVIDERNAME])) == 0)
                    {
                        Response.Write("ProviderName is not given in Patient Details Connections");
                        return;
                    }
                    if (Common.MyLen(Common.MyCStr(Session[Common.DATABASE])) == 0)
                    {
                        Response.Write("Database is not specified in Patient Details Connections");
                        return;
                    }
                }
                if (Common.MyLen(Common.MyCStr(Session[Common.DATABASESECOND]).Trim()) > 0)
                {

                }

                else
                {
                    ConnectionStringSettings objConnectionString = ConfigurationManager.ConnectionStrings["WebLabDetails"];
                    string provider = objConnectionString.ProviderName;
                    string ConStr = objConnectionString.ConnectionString;
                    ConStr = ConStr + "Password=dfcnkbd78378hn";
                    Session[Common.PROVIDERNAMESECOND] = provider;
                    Session[Common.DATABASESECOND] = ConStr;
                    Common.DBType = AisDBType.Sql;
                    if (Common.MyLen(Common.MyCStr(Session[Common.PROVIDERNAMESECOND])) == 0)
                    {
                        Response.Write("ProviderName is not given");
                        return;
                    }

                }

                if (CheckValidLogin())
                {



                    ////////string Qry = " select isperf and ISPF from LabTest where LabNo = '" + Common.MyCStr(Request.QueryString["LABNO"]) + "' ";
                    ////////DataTable dt = Common.GetTable(Qry, "Temp", Conn, trans);

                    ////////if (dt != null && dt.Rows.Count > 0)
                    ////////{

                    ////////    Conn.Close();
                    ////////}


                    ////////if ((Common.AISCompareString(Common.MyCStr(dt.Rows[0]["isperf"]), "Y") == AISCompareStringResult.AISCompareEqual)
                    ////////    ||
                    ////////   (Common.AISCompareString(Common.MyCStr(dt.Rows[0]["ISPF"]), "N") == AISCompareStringResult.AISCompareEqual))
                    {
                        string Preview = Common.MyCStr(Common.MycInt(Request.QueryString["PREVIEW"]));
                        string QryStrign = "LabNo=" + Common.MyCStr(Request.QueryString["LABNO"]);
                        if ((Common.MyLen(Common.MyCStr(Request.QueryString["WORD"])) > 0) && Common.MycInt(Request.QueryString["WORD"]) == 1)
                        {
                            QryStrign += "&ISWORD=1";
                        }
                        QryStrign += "&PREVIEW=" + Preview;
                        Response.Redirect("~/forms/frmAisLabReportView.aspx?" + QryStrign);
                    }
                    ////////else
                    ////////{
                    ////////    ///////Response.Write("Test Not Performed yet");
                    ////////}

                }
                else
                {
                    Response.Write("Parameters Not Valid. Please Check the Values");
                }
            }
            catch (Exception ex)
            {
               

            }
        }

        bool CheckValidLogin()
        {
            bool IsValid = false;
            string Labno = Common.MyCStr(Request.QueryString["LABNO"]);
            if (Common.MyLen(Labno) > 0)
            {
                string passVal = Common.MyCStr(Request.QueryString["KEY"]);
                if (Common.MyLen(passVal) > 0)
                {
                    string Qry = "Select Tdate from Labm where LABNO = '" + Labno + "'";
                    DataTable dt = Common.GetTableFromSession(Qry, "Temp", null, null);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows.Count == 1)
                        {
                            DateTime tdate = Common.MyCDate(dt.Rows[0][0]);
                            int Year = tdate.Year;
                            int Month = tdate.Month;
                            int day = tdate.Day;
                            string LabChar = Labno.Substring(Labno.Length - 3);

                            string CalculatedKey = Common.MyCStr(Year * 2) + Common.MyCStr(Month * 3) + Common.MyCStr(day * 4) + Common.MyCStr(Common.MyCDbl(LabChar) * 5);
                            if (Common.AISCompareString(passVal, CalculatedKey) == AISCompareStringResult.AISCompareEqual)
                            {
                                IsValid = true;

                                if (IsValid)
                                {
                                    

                                }


                            }
                        }
                    }
                }
            }
            return IsValid;
        }
    }
}
