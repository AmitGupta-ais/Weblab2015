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

/// <summary>
/// Summary description for clsConsultantLogin
/// </summary>
namespace WebLabMaster
{
    public class clsConsultantLogin
    {
        public const string USERTYPE = "Consultant";
        public string Name, Code, Addr1, Addr2, Addr3, Phone1, Phone2, Mobile, LoginID, Password;
        static string ConsultantTablename = clsTableNames.ConsultantTablename;
        public clsConsultantLogin()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public bool ValidateConsultantLogin(string strLoginID, string strPassword)
        {
            string qry;
            DataTable dt;
            string ConsName;
            string ConsPassword;
            bool isValidUser = false;
            qry = "Select * From " + ConsultantTablename + " Where LoginID='" + strLoginID + "'";
            dt = Common.GetTableFromSessionSecond(qry, "User", null, null);
            
            if (dt.Rows.Count != 0)
            {
                ConsName = Common.MyCStr(dt.Rows[0]["Name"]);
                ConsPassword = Common.MyCStr(dt.Rows[0]["Passwd"]);
                if (Common.MyLen(ConsPassword) != 0)
                {
                    if (Common.AISCompareString(ConsPassword, strPassword) == AISCompareStringResult.AISCompareEqual)
                    {
                        isValidUser = true;
                    }
                    else
                    {
                        isValidUser = false;
                    }
                }
                else
                {
                    isValidUser = CheckforNameAsPassword(ConsName, strPassword);
                }
            }
            else
            {
                isValidUser = CheckForCodeAsLoginID(strLoginID, strPassword);
            }
            return isValidUser;
        }

        bool CheckforNameAsPassword(string strName, string strPassword)
        {
            bool isValid = false;
            if (Common.MyLen(strName) > 4)
            {
                strName = strName.Substring(0, 4);
            }
            if (Common.AISCompareString(strName, strPassword) == AISCompareStringResult.AISCompareEqual)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        bool CheckForCodeAsLoginID(string strPatCode, string strPwd)
        {
            string qry;
            DataTable dt;
            string ConsName;
            bool isValidConsCode = false;
            qry = "Select * From " + ConsultantTablename + " Where Code='" + strPatCode + "'";
            dt = Common.GetTableFromSessionSecond(qry, "User", null, null);
            if (dt.Rows.Count != 0)
            {
                ConsName = Common.MyCStr(dt.Rows[0]["Name"]);
                isValidConsCode = CheckforNameAsPassword(ConsName, strPwd);
            }
            else
            {
                isValidConsCode = false;
            }
            return isValidConsCode;
        }
        public static clsConsultantLogin GetList(string strLoginID)
        {
            string qry;
            DataTable dt;
            qry = "Select * from " + ConsultantTablename + " where LoginID='" + strLoginID + "' OR Code='" + strLoginID + "'";
            dt = Common.GetTableFromSessionSecond(qry, "User", null, null);
            clsConsultantLogin objConsLogin = new clsConsultantLogin();
            if (dt.Rows.Count != 0)
            {
                objConsLogin.Code = Common.MyCStr(dt.Rows[0]["Code"]);
                objConsLogin.Name = Common.MyCStr(dt.Rows[0]["Name"]);
                objConsLogin.Password = Common.MyCStr(dt.Rows[0]["Passwd"]);
                objConsLogin.LoginID = Common.MyCStr(dt.Rows[0]["LoginId"]);
                objConsLogin.Addr1 = Common.MyCStr(dt.Rows[0]["Addr1"]);
                objConsLogin.Addr2 = Common.MyCStr(dt.Rows[0]["Addr2"]);
                objConsLogin.Addr3 = Common.MyCStr(dt.Rows[0]["Addr3"]);
                objConsLogin.Phone1 = Common.MyCStr(dt.Rows[0]["Phone1"]);
                objConsLogin.Phone2 = Common.MyCStr(dt.Rows[0]["Phone2"]);
                objConsLogin.Mobile = Common.MyCStr(dt.Rows[0]["Cell"]);
            }
            return objConsLogin;
        }
    } 
}
