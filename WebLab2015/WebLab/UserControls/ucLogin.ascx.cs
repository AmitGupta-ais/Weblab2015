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
using System.Collections.Generic;
using AISWebCommon;
using System.Web.Script.Serialization;

namespace WebLabMaster
{
    public partial class UserControls_ucLogin : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogin.Attributes.Add("OnClick", "return CheckOnClick()");

            if (!IsPostBack)
            {
                if (Common.MyLen(Common.MyCStr(Request.Form["UId"]).Trim()) > 0)
                {
                    txtLoginID.Text = Common.MyCStr(Request.Form["UId"]);
                    txtPassword.Text = Common.MyCStr(Request.Form["UPass"]);
                    url.Value = Common.MyCStr(Request.UrlReferrer);
                    btnLogin_Click(sender, e);
                }
            }
            if (Common.MyLen(Common.MyCStr(txtLoginID.Text).Trim()) > 0)
            {
               
            }
        }
        
       

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            List<string> CollPatID = new List<string>();
            if (!AISWebCommon.Common.CheckVersion("1.1.4.4"))
            {
                lblMsg.Text = "AisWebCommon version MisMatch";
                return;
            }
            clsLogin objLogin = new clsLogin();
            if (Common.MyLen(txtPassword.Text) == 0)
            {
                lblMsg.Text = "Please enter Password";
                if (Common.MyLen(Common.MyCStr(Request.Form["UId"]).Trim()) > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "", "javascript:backtobase('Please enter Password');", true);
                    
                }
                return;
            }
            CollPatID= objLogin.ValidateLogin(txtLoginID.Text, txtPassword.Text);
            if (CollPatID != null)
            {
                if (CollPatID.Count > 1)
                {
                    Session[CRMConstants.USERTYPE] = clsLogin.MULTIPATIENT;
                    Response.Redirect("frmMultiPatient.aspx?email=" + txtLoginID.Text, false);
                }
                else
                {
                    if (CollPatID.Count == 1)
                    {
                        bool IsDefPass = objLogin.IsDefPass;
                        objLogin = clsLogin.GetList(txtLoginID.Text);
                        Session[CRMConstants.LOGINUSER] = objLogin;
                        Session[CRMConstants.USERTYPE] = clsLogin.USERTYPE;
                        if (IsDefPass == true)
                        {
                            Response.Redirect("~/Change-Password.aspx?Pcode=" + objLogin.Code, false);
                        }
                        else
                        {
                            Response.Redirect("frmTestTable.aspx?Pcode=" + objLogin.Code, false);
                        }
                    }
                    else
                    {
                        if (clsLogin.ipd == false)
                        {
                            lblMsg.Text = "Online investigation reports are available for opd only";
                            if (Common.MyLen(Common.MyCStr(Request.Form["UId"]).Trim()) > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "", "javascript:backtobase('Online Lab Reports Are Not Available To IPD Patients.Please Collect Your Report From Lab.Thanks For Consulting');", true);
                                
                            }
                        }
                        else
                        {
                            if (Common.MyLen(Common.MyCStr(Request.Form["UId"]).Trim()) > 0)
                            {
                                var strerror = new JavaScriptSerializer().Serialize("backtobase('Either User Name')");
                                var script = string.Format("{0};", strerror);
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "javascript:backtobase('Either User Name/ Password incorrect');", true);
                                
                            }
                            lblMsg.Text = "Either User Name/ Password incorrect";
                        }
                        
                        
                        return;
                    }
                }
            }
        }
    } 
}
