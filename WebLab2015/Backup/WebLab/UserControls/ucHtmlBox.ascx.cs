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

namespace WebLab.UserControls
{
    public partial class ucHtmlBox : System.Web.UI.UserControl
    {
        public string controlval { get; set; }
        //public string _TextVal
        //{
        //    set { _TextVal = value; TextBox2.Text = value; }
        //    get { return TextBox2.Text; }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox2.Text = controlval;
        }



    }
}