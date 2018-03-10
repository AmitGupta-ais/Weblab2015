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
using AISWebCommon;

namespace WebLabMaster
{
    public static class clsdbtype
    {
        public static bool isOracle = false;
        public static void setforisOracle()
        {
            ConnectionStringSettingsCollection xxcoll= ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings xx in xxcoll)
            {
                if (Common.AISCompareString(xx.Name, "WebPatientDetails") == AISCompareStringResult.AISCompareEqual)
                {
                    string str11 = xx.ProviderName.ToUpper();
                    if (str11.Contains("ORACLE"))
                    {
                        isOracle = true;
                        Common.DBType = AisDBType.Oracle;
                    }
                }
            }
        }
    }
}
