using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AISWebCommon;
using System.Data;
using System.Collections;
using System.Data.Common;

namespace WebLab
{
    public partial class Controller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool flag = false;
            DbConnection conAp = null;
            try
            {
                if (Common.MyLen(Common.MyCStr(Request.QueryString["SessionID"])) > 0)
                {
                    if (Common.MyLen(Common.MyCStr(Request.QueryString["FORMURL"])) > 0)
                    {
                        string SessionID = Common.MyCStr(Request.QueryString["SessionID"]);
                        conAp = Common.GetConnectionFromSessionSecond();
                        string query = "select code from AP_OneTimePassword where code='" + Common.MyCStr(Request.QueryString["TransID"]) + "'";
                        DataTable dtForTrans = Common.GetTable(query, "Trans", conAp, null);
                        if (dtForTrans != null && dtForTrans.Rows.Count > 0)
                        {
                            Common.AisExecuteQuery("Delete from AP_OneTimePassword where code='" + Common.MyCStr(Request.QueryString["TransID"]) + "'", conAp);
                            string Query = "Select * from progLoginStat where Code='" + SessionID + "'";
                            DataTable DtSession = Common.GetTable(Query, "", conAp, null);
                            if (DtSession != null && DtSession.Rows.Count > 0)
                            {
                                string UserName = Common.MyCStr(DtSession.Rows[0]["ProgLogID"]);

                                 Query = "Select * from userrole where username='" + UserName+"'";
                                DataTable Dt = Common.GetTableFromSession(Query, "UserRole", null, null);
                                if (Dt != null && Dt.Rows.Count > 0)
                                {
                                    if (Common.MycInt(Dt.Rows[0]["notinuse"]) == 1)
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "JavaScript:alert(\"" + UserName + " marked not in use.\");", true);
                                        flag = false;
                                    }
                                    else
                                    {
                                        Session["lname"] = Common.MyCStr(Dt.Rows[0]["username"]).Trim();
                                        Session["Username"] = Common.MyLen(Common.MyCStr(Dt.Rows[0]["UserDescr"]).Trim()) > 0 ? Common.MyCStr(Dt.Rows[0]["UserDescr"]).Trim() : Common.MyCStr(Dt.Rows[0]["Username"]).Trim();
                                        Session["HideUnAppRep"] = Common.MyCStr(Common.MycInt(Dt.Rows[0]["hideUnAPPRep"]));
                                        Session["CONSCODE"] = null;
                                        if (Common.MycInt(Dt.Rows[0]["IsConsultant"]) == 1)
                                        {

                                            Session["HideUnAppRep"] = "0";

                                            ArrayList ArrList = getconsultants(Common.MyCStr(Dt.Rows[0]["username"]));
                                            if (Common.MyCStr(Dt.Rows[0]["consultantCode"]).Trim().Length > 0)
                                            {
                                                ArrList.Add(Common.MyCStr(Dt.Rows[0]["consultantCode"]));
                                            }
                                            //consCode = Common.MyCStr(Dt.Rows[0]["consultantCode"]);
                                            Session["CONSCODE"] = ArrList;
                                        }
                                        flag = true;
                                        //Response.Redirect("~/Forms/frmPerformTests.aspx");

                                    }
                                }
                                else
                                {
                                    flag = false;

                                }

                            }


                            else
                            {
                                flag = false;
                            }
                            
                            
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
                if (flag)
                {
                    Response.Redirect(Common.MyCStr(Request.QueryString["FORMURL"]));
                }
                else
                {
                    Response.Redirect("~/frmCommonLogin.aspx");
                }
            }
            catch (Exception ex)
            {

                ///
                string strEx = ex.Message;
                if (flag == false)
                {
                    Response.Redirect("~/frmCommonLogin.aspx");
                }
            }
            finally
            {
                if (conAp != null && conAp.State == ConnectionState.Open)
                {
                    conAp.Close();
                }
            }
        }

        private ArrayList getconsultants(string UserCode)
        {
            ArrayList arrList = new ArrayList();

            DataTable dt = Common.GetTableFromSession("select consultantcode from userRoleconsTr where userName ='" + UserCode + "' ", "Temp", null, null);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    arrList.Add(Common.MyCStr(dr["consultantcode"]));
                }
            }

            return arrList;
        }

    }
}
