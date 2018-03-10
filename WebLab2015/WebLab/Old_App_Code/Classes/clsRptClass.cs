using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AISWebCommon;
using System.Collections;

/// <summary>
/// Summary description for clsRptClass
/// </summary>
namespace WebLabMaster
{
    public class clsRptClass
    {
        public ArrayList arr;
        public DataTable dt;
        public string GroupField1;
        public string GroupDescriptionField1;
        public string GroupField2;
        public string GroupDescriptionField2;
        public ArrayList CollCriteria;
        public ArrayList CollHeader;
        public clsRptClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
    
}