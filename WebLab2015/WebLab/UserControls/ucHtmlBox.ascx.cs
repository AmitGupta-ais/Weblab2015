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
using WebLab.Classes;

namespace WebLab.UserControls
{
    public partial class ucHtmlBox : System.Web.UI.UserControl
    {
        public string controlval { get; set; }
        public string testcode { get; set; }
        //public string _TextVal
        //{
        //    set { _TextVal = value; TextBox2.Text = value; }
        //    get { return TextBox2.Text; }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add("code",typeof(System.String));
            dtnew.Columns.Add("value", typeof(System.String));


            TextBox2.Text = controlval;
           
            try
            {
                dt = Common.GetTableFromSession("select * from testlib where code='" + testcode + "'", "tabtemp", null, null);
                if(dt.Rows.Count>0)
                {
                    DataRow dr = null;
                    dr = dtnew.NewRow();
                    dr["code"] = "Current";
                    dr["value"] = controlval;
                    dtnew.Rows.Add(dr);
                    for (int i=1;i<=10;i++)
                    {
                        if(Common.MyLen(Common.MyCStr(dt.Rows[0]["name"+i]))>0)
                        {
                            dr = dtnew.NewRow();
                            dr["code"] = Common.MyCStr(dt.Rows[0]["name"+i]).Trim();
                            dr["value"] = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt.Rows[0]["int"+i]));
                            dtnew.Rows.Add(dr);

                        }

                    }
                   


                }
                templst.DataSource = dtnew;
                templst.DataTextField = "code";
                templst.DataValueField = "value";
                templst.DataBind();
            }
            catch(Exception ex)
            {

            }
           // templst.
        }

      
    }
}