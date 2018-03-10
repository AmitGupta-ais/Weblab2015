using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using AISWebCommon;

/// <summary>
/// Summary description for clsLogin
/// </summary>
namespace WebLabMaster
{   
    public class clsLogin
    {
        public const string USERTYPE = "Patient";
        public const string MULTIPATIENT = "MultiPatient";
        public string Code, Name, Password, LoginID, Sex,patno;
        public Boolean Ispatno;
        public static bool ipd = true;
        public static bool RequiredCustomPass = false;
        public bool IsDefPass = false;
        public double Age,mons,days;
        Boolean isemailPwdFound;
        static string PatientTablename=clsTableNames.PatientTablename;
        static string LabemailTablename = clsTableNames.LabemailTablename;
        static string websettingTablename = clsTableNames.WebReportSettingsTablename;
        
        public clsLogin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<string> ValidateLogin(string strLoginID, string strPassword)
        {
            string qry;
            List<string> CollUser = new List<string>();
            DataTable dt;
            //CollUser = null;
            bool isValidUser = false;
            string PatPassword;
            isemailPwdFound = false;
            qry = "Select * From "+ LabemailTablename +" Where Code='" + strLoginID + "'";
            dt = Common.GetTableFromSessionSecond(qry, "User", null, null);
            if (dt.Rows.Count != 0)
            {
               PatPassword = Common.MyCStr(dt.Rows[0]["Password"]);
               if (Common.MyLen(PatPassword)!= 0)
               {
                   if (Common.AISCompareString(PatPassword, strPassword) == AISCompareStringResult.AISCompareEqual)
                   {
                       isemailPwdFound = true;
                       CollUser = CheckforMultipleUser(strLoginID, strPassword, isemailPwdFound);
                   }
               }
               else
               {
                   CollUser = CheckforMultipleUser(strLoginID, strPassword, isemailPwdFound);
               }
            }
            else
            {
                isValidUser = CheckForCodeAsLoginID(strLoginID, strPassword);
                if (isValidUser)
                {
                    CollUser.Add(strLoginID);
                }
            }
            return CollUser;
        }

        List<string> CheckforMultipleUser(string stremail, string strPassword, bool emailPwdFound)
        {
            List<string> CollPatID = new List<string>();
            string PatName;
            
            string qry = "Select * from " + PatientTablename + " Where Loginid='" + stremail + "'";
            DataTable dt = Common.GetTableFromSessionSecond(qry, PatientTablename, null, null);
            int icnt = dt.Rows.Count;
            if (icnt != 0)
            {
                for (int i = 0; i < icnt; i++)
                {
                    if (!emailPwdFound)
                    {
                        PatName = Common.MyCStr(dt.Rows[i]["Name"]);
                        emailPwdFound = CheckNameAsPassword(PatName, strPassword, Common.MyCStr(dt.Rows[i]["Code"]));
                    }
                    CollPatID.Add(Common.MyCStr(dt.Rows[i]["Code"]));
                }
                
            }
            if (emailPwdFound)
            {
                return CollPatID;
            }
            else
            {
                return null;
            }
        }

        bool CheckNameAsPassword(string strPatName, string strPassword,string PCODE)
        {
            bool isValid = false;
            if (RequiredCustomPass)
            {
                DataTable Dt = Common.GetTableFromSessionSecond("Select * From Pat_Reg Where PCODE='" + PCODE + "'", "", null, null);
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    IsDefPass = Common.MycInt(Dt.Rows[0]["DefPass"]) == 1 ? true : false;
                    strPatName = Common.MyCStr(Dt.Rows[0]["Pass"]).Trim();
                }
            }
            else
            {
                if (Common.MyLen(strPatName) > 4)
                {
                    strPatName = strPatName.Substring(0, 4);
                }
            }
            if (Common.AISCompareString(strPatName, strPassword) == AISCompareStringResult.AISCompareEqual)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }
            return isValid;
        }

        bool CheckForCodeAsLoginID(string strPatCode,string strPwd)
        {
            string qry;
            DataTable dt;
            string PatName;
            ipd = true;
            int logintype = 0;
            int hideipdrpt = 0;
            bool isipdpat = false;
            bool isValidPatCode = false;
            double Loginbased=0;

            qry = "Select * from " + websettingTablename;
            DataTable dw = Common.GetTableFromSession(qry, websettingTablename, null, null);
            if (dw !=null && dw.Rows.Count > 0)
            {
                logintype = Common.MycInt(dw.Rows[0]["Loginbased"]);
                hideipdrpt = Common.MycInt(dw.Rows[0]["hideipdr"]);
                RequiredCustomPass = Common.MycInt(dw.Rows[0]["UseCPass"]) == 1 ? true : false;
            }
            if (logintype == 1)
            {
                qry = "select typ from bills where patno='" + strPatCode + "' order by bdate desc";
                DataTable dtisipd = Common.GetTableFromSessionSecond(qry, "Bills", null, null);
                if (dtisipd.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtisipd.Rows)
                    {
                        if (Common.MyCStr(dr["typ"]) == "I")
                        {
                            isipdpat = true;
                            break;

                        }
                    }
                }
            }
            if (dw.Rows.Count != 0)
            {
                Loginbased = Common.MyCDbl(dw.Rows[0]["Loginbased"]);
            }
            if (Loginbased == 0 || Loginbased == 2)
            {
                qry = "Select * From " + PatientTablename + " Where Code='" + strPatCode + "'";
                dt = Common.GetTableFromSessionSecond(qry, "User", null, null);
                if (dt.Rows.Count != 0)
                {
                    PatName = Common.MyCStr(dt.Rows[0]["Name"]);
                    PatName = PatName.Replace(" ", "");
                    PatName = PatName.Replace(".", "");
                    PatName = PatName.Replace("/", "");
                    PatName = PatName.Replace("\\", "");
                    PatName = PatName.Replace(",", "");
                    PatName = PatName.Replace("&", "");
                    PatName = PatName.Replace("*", "");
                    isValidPatCode = CheckNameAsPassword(PatName, strPwd,strPatCode);
                    if (hideipdrpt == 1 && isipdpat && isValidPatCode==true )
                    {
                        isValidPatCode = false;
                        ipd = false;
                    }
                }
                else
                {
                    isValidPatCode = false;
                }
            }
            if ((Loginbased == 1 || Loginbased == 2) && isValidPatCode==false)
            { 
               qry ="select " + PatientTablename + ".name,bills.patno,bills.pcode from (bills left outer join " + PatientTablename+ " on bills.pcode="+ PatientTablename + ".code ) where bills.patno='" + strPatCode + "'";
               dt = Common.GetTableFromSessionSecond(qry, "Bills", null, null);
               if (dt!=null && dt.Rows.Count > 0)
               {
                   PatName = Common.MyCStr(dt.Rows[0]["Name"]);
                   PatName = PatName.Replace(" ", "");
                   PatName = PatName.Replace(".", "");
                   PatName = PatName.Replace("/", "");
                   PatName = PatName.Replace("\\", "");
                   PatName = PatName.Replace(",", "");
                   PatName = PatName.Replace("&", "");
                   PatName = PatName.Replace("*", "");
                   isValidPatCode = CheckNameAsPassword(PatName, strPwd, Common.MyCStr(dt.Rows[0]["PCODE"]));
                   if (hideipdrpt == 1 && isipdpat && isValidPatCode==true )
                   {
                       isValidPatCode = false;
                       ipd = false;
                   }
               }
               else
               {
                   isValidPatCode = false;
               }
            }


            return isValidPatCode;
        }

        public static clsLogin GetList(string strLoginID)
        {
            string qry;
            DataTable dt;
            qry = "Select " + PatientTablename + ".code," + PatientTablename + ".Name," + PatientTablename + ".Password," + PatientTablename + ".LoginID,";
            qry = qry + PatientTablename + ".Age," + PatientTablename + ".mons," + PatientTablename + ".days," + PatientTablename + ".sex,Bills.patno from (" + PatientTablename;
            qry = qry + " left outer join bills on " + PatientTablename + ".code=bills.pcode) where LoginID='" + strLoginID + "' OR Code='" + strLoginID + "' or Bills.patno='" + strLoginID + "'";
            //qry = "Select " + PatientTablename + ".code," + PatientTablename + ".Name,"+ PatientTablename + ".Password," + PatientTablename + ".LoginID,";
            //qry = qry + PatientTablename + ".Age," + PatientTablename + ".mons,"+ PatientTablename + ".days," + PatientTablename + ".sex,Bills.patno from (" + PatientTablename ;
            //qry= qry + " left outer join bills on " + PatientTablename + ".code=bills.pcode) where LoginID='" + strLoginID + "' OR Code='" + strLoginID + "' or Bills.patno='"+ strLoginID +"'";
            //qry = "Select " + PatientTablename + ".code," + PatientTablename + ".Name," + PatientTablename + ".Passwd," + PatientTablename + ".LoginID,";
            //qry = qry + PatientTablename + ".Age," + PatientTablename + ".mons," + PatientTablename + ".days," + PatientTablename + ".sex from " + PatientTablename;
            //qry = qry + "  where LoginID='" + strLoginID + "' OR Code='" + strLoginID + "'" ;
            dt = Common.GetTableFromSessionSecond(qry,"User", null, null);
            clsLogin objLogin = new clsLogin();
            if (dt.Rows.Count != 0)
            {
                objLogin.Code = Common.MyCStr(dt.Rows[0]["Code"]);
                objLogin.Name = Common.MyCStr(dt.Rows[0]["Name"]);
                objLogin.Password = Common.MyCStr(dt.Rows[0]["Password"]);
                objLogin.LoginID = Common.MyCStr(dt.Rows[0]["LoginId"]);
                objLogin.Age = Common.MyCDbl(dt.Rows[0]["Age"]);
                objLogin.mons = Common.MyCDbl(dt.Rows[0]["mons"]);
                objLogin.days = Common.MyCDbl(dt.Rows[0]["days"]);
                objLogin.Sex = Common.MyCStr(dt.Rows[0]["Sex"]);
                objLogin.patno = Common.MyCStr(dt.Rows[0]["Patno"]);
                if (Common.AISCompareString(Common.MyCStr(strLoginID).Trim().ToUpper(), Common.MyCStr(dt.Rows[0]["Patno"]).Trim().ToUpper()) == AISCompareStringResult.AISCompareEqual)
                {
                    objLogin.Ispatno = true;
                }
                else
                {
                    objLogin.Ispatno = false;
                }
            }
            return objLogin;
        }

        public DataTable GetMultiPatientList(string strLoginID)
        {
            string qry;
            DataTable dt;
            qry = "Select * from " + PatientTablename + " Where LoginID='" + strLoginID + "'";
            dt = Common.GetTableFromSessionSecond(qry, "User", null, null);
            return dt;
        }
    } 
}
