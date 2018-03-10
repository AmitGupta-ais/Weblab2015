using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebLab
{
    public partial class angularHTML : System.Web.UI.UserControl
    {
        public string htmlText { get; set; }
        public string headerText { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtHtml.Text = htmlText;
            txtHead.Text = headerText;
        }
    }
}