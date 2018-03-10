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
    public partial class UserControls_ucDate : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDate();
            }
        }
        void FillDate()
        {
            for (int i = 1; i <= 31; i++)
            {  
                ddlDay.Items.Add(Common.MyCStr(i));
            }
            ddlDay.SelectedIndex = 0;
            for (int i = 1900; i <= 9999; i++)
            {
                ddlYear.Items.Add(Common.MyCStr(i));
            }
            ddlYear.SelectedIndex = 107;

            ddlMonth.Items.Add("January");
            ddlMonth.Items.Add("February");
            ddlMonth.Items.Add("March");
            ddlMonth.Items.Add("April");
            ddlMonth.Items.Add("May");
            ddlMonth.Items.Add("June");
            ddlMonth.Items.Add("July");
            ddlMonth.Items.Add("August");
            ddlMonth.Items.Add("September");
            ddlMonth.Items.Add("October");
            ddlMonth.Items.Add("November");
            ddlMonth.Items.Add("December");
            
            ddlMonth.SelectedIndex = 0;
        }
        public string GetDate()
        {
            string Date = "";
            string day=Common.MyCStr(ddlDay.SelectedItem);
            string Month=Common.MyCStr(ddlMonth.SelectedIndex + 1);
            string Year=Common.MyCStr(ddlYear.SelectedItem);
            Date = day + "/" + Month + "/" + Year;
            return Date;
        }
        public bool DateValidation()
        {
            int SelectedYr;
            SelectedYr = (int)Common.MyCDbl(ddlYear.SelectedItem);
            int Remainder1;
            int Remainder2;
            Remainder1 = SelectedYr % 4;
            Remainder2 = SelectedYr % 400;
            if (ddlDay.SelectedIndex == 30 && (ddlMonth.SelectedIndex == 1 || ddlMonth.SelectedIndex == 3 || ddlMonth.SelectedIndex == 5 || ddlMonth.SelectedIndex == 8 || ddlMonth.SelectedIndex == 10))
            {
                return false;
            }
            else
            {
                if (ddlMonth.SelectedIndex == 1 && ddlDay.SelectedIndex > 27)
                {
                    if (Remainder1 != 0 && ddlDay.SelectedIndex == 28)
                    {
                        return false;
                    }
                    else
                    {
                        if (Remainder2 == 0 && ddlDay.SelectedIndex == 28)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
            
        }
    } 
}
