using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WebLab.Classes
{
    public class labtestvalues
    {
        public string labno { get; set; }
        public string testcode { get; set; }
        public string testval { get; set; }
        public string controlid { get; set; }
        public string comment { get; set; }
    }
}
