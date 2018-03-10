using System;
using System.Drawing;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AISWebCommon;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data.OracleClient;
using WebLab.DataSets;

/// <summary>
/// Summary description for clsReport
/// </summary>
namespace WebLabMaster
{
    public class clsReport
    {
        string LabTestTablename = clsTableNames.LabTestTablename;
        string TestLibTablename = clsTableNames.TestLibTablename;
        string TestlibHeadTablename = clsTableNames.TestlibHeadTablename;
        string PatientTablename = clsTableNames.PatientTablename;
        string DeptartmentTablename = clsTableNames.DeptartmentTablename;
        string LabMTablename = clsTableNames.LabMTablename;
        string WebsettingTablename = clsTableNames.WebReportSettingsTablename;
        static string RepoSettingsTablename = clsTableNames.RepoSettingsTablename;
        string ReportTunningTableName = clsTableNames.ReportTunningTableName;
        public const string CULTSPEC = "SPEC";
        public const string CULTREPORTDATE = "RD";
        public const string CULTSAMPLEDATE = "SD";
        public const string CULTMEDICINECODE = "MED";
        public const string CULTORGISO = "ORG";
        public const string CULTCOLCOUNT = "CC";
        public const string CULTSTERILE = "ST";
        public const string CULTADVICE = "AD";
        public const string CULTTYPEOFSAMPLE = "TS";
        public const string CULTBLOODSOURCE = "BS";
        public const string CULTIDENTIFICATION = "IDEN";
        public const string CULTREPORTSTAT = "RSTAT";
        public const string CULTMETHOD = "Meth";



        public enum AISPAYTYPE
        {

            APPAYMENT = 0,
            APREFUND = 1,
            APCREDITNOTE = 2,
            APDEBITNOTE = 3,
            APDEDUCTION = 4
        };
        
        string INTP, SIGN, EOR, COMM, IFCOMM, abnormal, highbound, lowbound, sex;

        int cultsno;
        double age, mons, days, getrepovaluelbabclr, getrepovaluelbabbold, getrepovaluelbabul, getrepovaluelbabast, getrepovaluelbnoclr, getrepovaluelbnoul, getrepovaluelbnobold, getrepovaluelbszeHName, getrepovaluelbclrHName, getrepovalueszeDetail;
        string getrepovaluelbfntHName, getrepovalefntDetail;
        bool isTValZero = false;
        string wcl = "";

        public static Hashtable CollRepoSetting;
        bool isBlank;

        string dcode;
        clsTableFontSetting objTableFontsettings = new clsTableFontSetting();
        bool reqStarinabnormal;
        bool ReqWebDigitalSign = false;

        ReportData objRepData = new ReportData();

        public clsReport()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string gettunvar(string varname)
        {
            string qry;
            if (WebLabMaster.clsdbtype.isOracle)
            {// if (Common.DBType==AisDBType.Oracle)
                qry = "select * from " + ReportTunningTableName + " where upper(code)='" + varname.ToUpper() + "'";
            }
            else
            {
                qry = "select * from " + ReportTunningTableName + " where code='" + varname + "'";
            }
            DataTable dt;
            string retVal = "";
            dt = Common.GetTableFromSession(qry, "Table", null, null);
            if (dt.Rows.Count != 0)
            {
                retVal = Common.MyCStr(dt.Rows[0]["Value"]);
            }
            retVal = retVal.Trim();
            return retVal;
        }

        public Boolean mycboolean(string varval)
        {
            Boolean RETVAL;
            RETVAL = false;
            varval = Common.MyCStr(varval).ToUpper().Trim();

            switch (varval)
            {
                case "Y":
                    RETVAL = true;
                    break;
                case "YES":
                    RETVAL = true;
                    break;
                case "TRUE":
                    RETVAL = true;
                    break;
                default:
                    break;
            }
            return (RETVAL);
        }
        public ReportData GetData(string LABNO, string testheadcode)
        {
            return  GetData(LABNO,testheadcode,false);
        }
        public ReportData GetData(string LABNO, string testheadcode,bool hideSomeTest)
        {
            bool IsOldStyle = true;
            DataTable HospDt = Common.GetTableFromSession("Select VALUE From SYSTUN Where Code='SHWSDC'", "", null, null);
            if (HospDt != null && HospDt.Rows.Count > 0)
            {
                IsOldStyle = !Common.mycboolean(Common.MyCStr(HospDt.Rows[0]["VALUE"]).Trim());
            }
            clsReport obj = new clsReport();
            SetRepoVal();
            int hCount = -1, mCount = -1, rCount = -1, sCount = -1;
            string[] hStr = new string[1000];
            string[] mStr = new string[1000];
            string[] rstr = new string[1000];
            string[] sStr = new string[1000];
            GetHeaderData(LABNO, testheadcode);
            GetReportFormat();
            Hashtable LowUpBound;
            int maxnoofcolsinTable = 0;
            DataTable dtData;

            string QUERY = "";

            QUERY = "select " + PatientTablename + ".age," + PatientTablename + ".mons," + PatientTablename + ".days," + PatientTablename + ".sex  from (" + PatientTablename + " left outer join " + LabMTablename + " on " + LabMTablename + ".pcode=" + PatientTablename + ".code ) where " + LabMTablename + ".labno='" + LABNO + "'";
            DataTable dpatient;
            dpatient = Common.GetTableFromSession(QUERY, PatientTablename, null, null);

            

            if (dpatient.Rows.Count > 0)
            {
                age = Common.MyCDbl(dpatient.Rows[0]["age"]);
                mons = Common.MyCDbl(dpatient.Rows[0]["mons"]);
                days = Common.MyCDbl(dpatient.Rows[0]["days"]);
                sex = Common.MyCStr(dpatient.Rows[0]["sex"]);
            }
            else
            {
                QUERY = "select patage,patmons,patdays,patsex,pcode,PATNO from " + LabMTablename + " where labno='" + LABNO + "'";
                dpatient = Common.GetTableFromSession(QUERY, LabMTablename, null, null);
                string pcode = "";
                string patno = "";
                if (dpatient.Rows.Count > 0)
                {
                    age = Common.MyCDbl(dpatient.Rows[0]["patage"]);
                    mons = Common.MyCDbl(dpatient.Rows[0]["patmons"]);
                    days = Common.MyCDbl(dpatient.Rows[0]["patdays"]);
                    sex = Common.MyCStr(dpatient.Rows[0]["patsex"]);
                    pcode = Common.MyCStr(dpatient.Rows[0]["Pcode"]);
                    patno = Common.MyCStr(dpatient.Rows[0]["PATNO"]);
                }
                if (age == 0 && mons == 0 && days == 0 && Common.MyCStr(patno).Trim().Length > 0)
                {
                    QUERY = "select * from bills where patno='" + patno + "'";
                    dpatient = Common.GetTableFromSessionSecond(QUERY, "Bills", null, null);
                    if (dpatient.Rows.Count > 0)
                    {
                        age = Common.MyCDbl(dpatient.Rows[0]["patage"]);
                        mons = Common.MyCDbl(dpatient.Rows[0]["patmons"]);
                        days = Common.MyCDbl(dpatient.Rows[0]["patdays"]);

                    }
                }
                if (((age == 0 && mons == 0 && days == 0) || (Common.MyCStr(sex).Trim().Length == 0)) && Common.MyCStr(pcode).Trim().Length > 0)
                {
                    QUERY = "select * from patient where code='" + pcode + "'";
                    dpatient = Common.GetTableFromSessionSecond(QUERY, PatientTablename, null, null);
                    if (dpatient.Rows.Count > 0)
                    {
                        if (age == 0 && mons == 0 && days == 0)
                        {
                            age = Common.MyCDbl(dpatient.Rows[0]["age"]);
                            mons = Common.MyCDbl(dpatient.Rows[0]["mons"]);
                            days = Common.MyCDbl(dpatient.Rows[0]["days"]);
                        }
                        sex = Common.MyCStr(dpatient.Rows[0]["sex"]);

                    }
                }


            }
            string Header1 = "", Header2 = "", Footer = "", SenstivityAs = "", MedicineAs = "", UpperHeader = "";
            Header1 = gettunvar("SHWHD1").Trim();
            Header2 = gettunvar("SHWHD2").Trim();
            Footer = gettunvar("SHWFR").Trim();
            UpperHeader = gettunvar("SHWUH").Trim();
            SenstivityAs = gettunvar("SHWSENS").Trim();
            MedicineAs = gettunvar("SHWMED").Trim();
            if (Common.MyLen(MedicineAs.Trim()) == 0)
            {
                MedicineAs = "ANTIMICROBIAL";
            }
            if (Common.MyLen(SenstivityAs.Trim()) > 0)
            {
                SenstivityAs = "SENSTIVITY";
            }
            if (Common.MyLen(testheadcode.Trim()) > 0)
            {
                QUERY = "Select " + LabTestTablename + ".gcode," + LabTestTablename + ".tcode," + LabTestTablename + ".booksno," + LabTestTablename + ".BCODE," + LabTestTablename + ".Remarks AS LabTestRemarks," + LabTestTablename + ".LABNO," + LabTestTablename + ".TVAL," + LabTestTablename + ".PwordFile," + LabTestTablename + ".Comments,";
                QUERY += TestLibTablename + ".dcode," + TestLibTablename + ".lcode," + TestLibTablename + ".ismain," + TestLibTablename + ".name AS TestLibName," + TestLibTablename + ".SNO as TestLibSNo," + TestLibTablename + ".units," + TestLibTablename + ".DETAILS," + TestLibTablename + ".testType ," + TestLibTablename + ".tableFont," + TestLibTablename + ".tableFontsize," + TestLibTablename + ".tableCaptionWidth," + TestLibTablename + ".tableColWidth," + TestLibTablename + ".DEFINETAB,";
                QUERY += TestlibHeadTablename + ".remarks AS TestlibheadRemarks," + TestlibHeadTablename + ".name AS TestlibheadName," + TestlibHeadTablename + ".UNITREQ," + TestlibHeadTablename + ".RANGEREQ," + TestlibHeadTablename + ".PRINTREQ," + TestlibHeadTablename + ".headreq," + TestlibHeadTablename + ".sno as TestlibHeadSNo,";
                QUERY += DeptartmentTablename + ".websignatory as signatory," + DeptartmentTablename + ".websignht," + DeptartmentTablename + ".webeor as EOR , " + DeptartmentTablename + ".REQDIGITSIGNONWEB WEBREQ ";
                QUERY += " ," + LabTestTablename + ".METHOD TESTMETHOD";
                QUERY += " From  (((" + LabTestTablename + " Left Outer Join " + TestLibTablename + " on " + LabTestTablename + ".tcode=" + TestLibTablename + ".code)";
                QUERY += " Left Outer Join " + TestlibHeadTablename + " on " + TestLibTablename + ".lcode=" + TestlibHeadTablename + ".code)";
                QUERY += " Left Outer Join " + DeptartmentTablename + " on " + TestlibHeadTablename + ".dcode=" + DeptartmentTablename + ".code) ";
                QUERY += " where " + LabTestTablename + ".labno='" + LABNO + "' AND TestLib.lcode='" + testheadcode + "' AND " + LabTestTablename + ".isperf='Y' " + wcl;
                QUERY += " AND " + TestLibTablename + ".ISPF='N' ";
                QUERY += " And (Dept.NotAllowWeb IS NULL OR Dept.NotAllowWeb<>1) ";
                if (hideSomeTest)
                {
                    QUERY += " AND " + LabTestTablename + ".TCODE Not IN (select ap_lab_hideTest_on_web.code from ap_lab_hideTest_on_web) ";
                }
                QUERY += " Order by " + TestlibHeadTablename + ".Name," + TestlibHeadTablename + ".code," + TestLibTablename + ".Sno," + TestLibTablename + ".Name," + LabTestTablename + ".booksno ";
            }
            else
            {
                QUERY = "Select " + LabTestTablename + ".gcode," + LabTestTablename + ".tcode," + LabTestTablename + ".booksno," + LabTestTablename + ".BCODE," + LabTestTablename + ".Remarks AS LabTestRemarks," + LabTestTablename + ".LABNO," + LabTestTablename + ".TVAL," + LabTestTablename + ".PwordFile," + LabTestTablename + ".Comments,";
                QUERY += TestLibTablename + ".dcode," + TestLibTablename + ".lcode," + TestLibTablename + ".ismain," + TestLibTablename + ".name AS TestLibName," + TestLibTablename + ".SNO as TestLibSNo," + TestLibTablename + ".units," + TestLibTablename + ".DETAILS," + TestLibTablename + ".testType ," + TestLibTablename + ".tableFont," + TestLibTablename + ".tableFontsize," + TestLibTablename + ".tableCaptionWidth," + TestLibTablename + ".tableColWidth," + TestLibTablename + ".DEFINETAB,";
                QUERY += TestlibHeadTablename + ".remarks AS TestlibheadRemarks," + TestlibHeadTablename + ".name AS TestlibheadName," + TestlibHeadTablename + ".UNITREQ," + TestlibHeadTablename + ".RANGEREQ," + TestlibHeadTablename + ".PRINTREQ," + TestlibHeadTablename + ".headreq," + TestlibHeadTablename + ".sno as TestlibHeadSNo,";
                QUERY += DeptartmentTablename + ".websignatory as signatory," + DeptartmentTablename + ".websignht," + DeptartmentTablename + ".webeor as EOR , "+DeptartmentTablename+".REQDIGITSIGNONWEB WEBREQ ";
                QUERY += " ," + LabTestTablename + ".METHOD TESTMETHOD";
                QUERY += " From  (((" + LabTestTablename + " Left Outer Join " + TestLibTablename + " on " + LabTestTablename + ".tcode=" + TestLibTablename + ".code)";
                QUERY += " Left Outer Join " + TestlibHeadTablename + " on " + TestLibTablename + ".lcode=" + TestlibHeadTablename + ".code)";
                QUERY += " Left Outer Join " + DeptartmentTablename + " on " + TestlibHeadTablename + ".dcode=" + DeptartmentTablename + ".code) ";
                QUERY += " where " + LabTestTablename + ".labno='" + LABNO + "' AND " + LabTestTablename + ".isperf='Y' " + wcl;
                QUERY += " AND " + TestLibTablename + ".ISPF='N' ";
                QUERY += " And (Dept.NotAllowWeb IS NULL OR Dept.NotAllowWeb<>1) ";
                if (hideSomeTest)
                {
                    QUERY += " AND " + LabTestTablename + ".TCODE Not IN (select ap_lab_hideTest_on_web.code from ap_lab_hideTest_on_web) ";
                }
                QUERY += " Order by " + TestlibHeadTablename + ".Name," + TestlibHeadTablename + ".code," + TestLibTablename + ".Sno," + TestLibTablename + ".Name," + LabTestTablename + ".booksno ";
            }
            dtData = Common.GetTableFromSession(QUERY, "Table", null, null);

            int z = dtData.Rows.Count;
            string PrevTestType = "";
            string NORMALRANGE = "", wherecondition = "", retstr = "";
            bool SelectiveTests = false;
            int pos;

            if (Common.MyLen(retstr) > 0)
            {
                if (SelectiveTests)
                {
                    wherecondition = " code ";
                }
                else
                {
                    wherecondition = " Lcode ";
                }
            }
            wherecondition = " AND " + wherecondition + " IN  " + retstr;

            string prvgrpcode;
            string WEBREQSIGN = "";
            clsSubHead lastsubhead;
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                string HCode = "";
                ReportData.Report_TableRow dr;
                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                pos = 0;

                if (i >= 1)
                {
                    PrevTestType = Common.MyCStr(dtData.Rows[i]["testType"]);
                }
                dr.PrevType = PrevTestType;
                prvgrpcode = Common.MyCStr(dtData.Rows[i]["gcode"]);
                WEBREQSIGN = Common.MyCStr(dtData.Rows[i]["WEBREQ"]);

                string hcodepos = "";

                abnormal = "N";

                dcode = Common.MyCStr(dtData.Rows[i]["dcode"]);
                
                EOR = Common.MyCStr(dtData.Rows[i]["EOR"]);
                if (Common.AISCompareString(obj.gettunvar("apprhdwis"), Common.AISUCase("N")) == AISCompareStringResult.AISCompareEqual)
                {
                    SIGN = Common.MyCStr(dtData.Rows[i]["signatory"]);
                }
                ////
                string isSign = Common.MyCStr(GetRepoVal("lbhidsigwe"));
                if (Common.AISCompareString(isSign, "Y") != AISCompareStringResult.AISCompareEqual)
                {
                    dr.SIGN = "";
                }
                else
                {


                    dr.SIGN = SIGN;

                }

                dr.EOR = EOR;
                dr.UnderLine = 0;

                string HEADVAL = "";
                string headrq = "";
                string tname = "";
                string HName = "";
                string Remarks = "";
                string Specimen = "", ReportingDate = "", SampleDate = "", Organism = "", SampleType = "", BloodSource = "", ColonyCount = "", Identification = "", ReportState = "", Method = "", Strile = "", Comment = "";
                HEADVAL = Common.MyCStr(dtData.Rows[i]["PRINTREQ"]) + Common.MyCStr(dtData.Rows[i]["UNITREQ"]) + Common.MyCStr(dtData.Rows[i]["RANGEREQ"]);
                headrq = Common.MyCStr(dtData.Rows[i]["headREQ"]);
                HName = Common.MyCStr(dtData.Rows[i]["TestlibheadName"]).Trim();
                Remarks = Common.MyCStr(dtData.Rows[i]["TestlibheadRemarks"]);
                pos = (int)Common.MyCDbl(dtData.Rows[i]["TestlibHeadSNo"]);

                tname = Common.MyCStr(dtData.Rows[i]["TestlibheadName"]).Trim();
                    if (Common.AISCompareString(Common.MyCStr(dtData.Rows[i]["ismain"]), "Y") == AISCompareStringResult.AISCompareEqual)
                    {
                        tname = Common.MyCStr(dtData.Rows[i]["TestLibName"]).Trim();
                    }
                    else
                    {
                        tname = Common.MyCStr(dtData.Rows[i]["TestLibName"]).Trim();
                        if (Common.mycboolean(Common.MyCStr(GetRepoVal("lbshsubtrt"))))
                        {
                            tname = Spac(5) + Common.MyCStr(dtData.Rows[i]["TestLibName"]).Trim();
                        }
                    }
                if (Common.AISCompareString(headrq, "N") == AISCompareStringResult.AISCompareEqual)
                {
                    tname = "";
                    HName = "";
                }



                dr.booksno =Common.MycInt(Common.MyCStr(dtData.Rows[i]["booksno"]));
                dr.Bookedunder = Common.MyCStr(dtData.Rows[i]["BCODE"]);
                dr.tname = tname;
               
                if (mycboolean(gettunvar("prnmthspl")))
                {
                    if (Common.MyLen(Common.MyCStr(dtData.Rows[i]["TestMethod"])) > 0)
                    {
                        string caps = Common.MyCStr(gettunvar("prnsplcap"));
                        dr.TestMethod = caps + Common.MyCStr(dtData.Rows[i]["TestMethod"]);
                    }
                }
                else
                {
                    dr.TestMethod = "";
                }
                
                dr.HEADVAL = HEADVAL;
                dr.Remrep = Common.MyCStr(dtData.Rows[i]["LabTestRemarks"]);
                dr.Remarks = Remarks;
                bool isptype;
                isptype = false;

                dr.labno = LABNO;
                dr.testcode = Common.MyCStr(dtData.Rows[i]["tcode"]);

                if (mycboolean(gettunvar("syrqhdnmprf")) == false)
                {
                    if (isProfileType(Common.MyCStr(dtData.Rows[i]["BCODE"])) == true)
                    {
                        isptype = true;
                    }
                }



                if (isptype == false)
                {
                    HCode = Common.MyCStr(dtData.Rows[i]["lcode"]).Trim();
                    dr.hcode = HCode;
                    dr.hcodesno = Common.MyCStr(pos);
                    //hcodepos = Common.myformat(pos, false, true, 3, true, 1, false) + HName;
                    hcodepos = Common.myformat(pos, false, true, 3, 1, true, false) + HName;
                    dr.hcodepos = hcodepos;
                    dr.HName = HName;
                    dr.sno = Common.MycInt(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]));
                }
                else
                {
                    HName = getMasterValue("Code", Common.MyCStr(dtData.Rows[i]["BCODE"]), "testlib", "Name").Trim();
                    HCode = Common.MyCStr(dtData.Rows[i]["BCODE"]).Trim();
                    dr.hcode = HCode;
                    dr.hcodesno = Common.MyCStr(dtData.Rows[i]["booksno"]);
                    //hcodepos = Common.myformat(Common.MyCDbl(getFirstBookno(Common.MyCStr(dtData.Rows[i]["LABNO"]), Common.MyCStr(dtData.Rows[i]["BCODE"]))), false, true, 3, true, 1, false) + HName;
                    hcodepos = Common.myformat(Common.MyCDbl(getFirstBookno(Common.MyCStr(dtData.Rows[i]["LABNO"]), Common.MyCStr(dtData.Rows[i]["BCODE"]))), false, true, 3, 1, true, false) + HName;
                    dr.hcodepos = hcodepos;
                    dr.HName = HName;
                    if (mycboolean(gettunvar("sylmTordpr")) == true)
                    {
                        dr.sno = Common.MycInt(Common.MyCStr(dtData.Rows[i]["booksno"]));
                    }
                    else
                    {

                        dr.sno = Common.MycInt(Common.MyCStr(dtData.Rows[i]["booksno"]));
                    }
                

                }
                dr.units = Common.MyCStr(dtData.Rows[i]["units"]).Trim();
                INTP = Common.MyCStr(dtData.Rows[i]["DETAILS"]).Trim();
                dr.INTP = INTP;
                if (Common.AISCompareString(Common.MyCStr(dtData.Rows[i]["testType"]), "PARAGRAPH") == AISCompareStringResult.AISCompareEqual || Common.AISCompareString(Common.MyCStr(dtData.Rows[i]["testType"]), "X") == AISCompareStringResult.AISCompareEqual)
                {
                    dr.TYP = "P";
                    isTValZero = true;
                    COMM = Common.MyCStr(dtData.Rows[i]["TVAL"]).Trim();
                    if (Common.MyLen(COMM) == 0)
                    {
                        IFCOMM = "N";
                    }
                    else
                    {
                        IFCOMM = "Y";
                    }
                    dr.comments = COMM;
                    dr.IFCOMM = IFCOMM;
                    dr.tval = "";
                    dr.NORANGE = "";
                    dr.LBOUND = "";
                    dr.UBOUND = "";
                }
                else if (Common.AISCompareString(Common.MyCStr(dtData.Rows[i]["testType"]), "SUB HEADING") == AISCompareStringResult.AISCompareEqual)
                {
                    lastsubhead = new clsSubHead();

                    lastsubhead.tname = tname;

                    dr.TYP = "S";
                    dr.comments = "";
                    dr.IFCOMM = "N";
                    dr.tval = "";
                    dr.NORANGE = "";
                    dr.LBOUND = "";
                    dr.UBOUND = "";
                    if (mycboolean(GetRepoVal("lbrqulsh")))
                    {
                        dr.UnderLine = 1;
                    }
                }
                else if (Common.AISCompareString(Common.MyCStr(dtData.Rows[i]["testType"]), "TABLE") == AISCompareStringResult.AISCompareEqual)
                {
                    isTValZero = true;
                    objTableFontsettings.FONTNAME = Common.MyCStr(dtData.Rows[i]["tableFont"]);
                    objTableFontsettings.fontsize = (int)Common.MyCDbl(dtData.Rows[i]["tableFontsize"]);
                    objTableFontsettings.capwidth = Common.MyCDbl(dtData.Rows[i]["tableCaptionWidth"]);
                    objTableFontsettings.colwisth = Common.MyCDbl(dtData.Rows[i]["tableColWidth"]);
                    dr.TYP = "T";
                    string row1, row2, row3, row4, row5, row6;

                    dr.Row1 = "";
                    dr.Row1val1 = strtok(7, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    dr.Row1val2 = strtok(8, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    dr.Row1val3 = strtok(9, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    dr.Row1val4 = strtok(10, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    dr.Row1val5 = strtok(11, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    dr.Row1val6 = strtok(12, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));

                    if (maxnoofcolsinTable == 0)
                    {
                        if (Common.MyLen(strtok(12, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]))) > 0)
                        {
                            maxnoofcolsinTable = 6;
                        }
                        else if (Common.MyLen(strtok(11, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]))) > 0)
                        {
                            maxnoofcolsinTable = 5;
                        }
                        else if (Common.MyLen(strtok(10, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]))) > 0)
                        {
                            maxnoofcolsinTable = 4;
                        }
                        else if (Common.MyLen(strtok(9, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]))) > 0)
                        {
                            maxnoofcolsinTable = 3;
                        }
                        else if (Common.MyLen(strtok(8, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]))) > 0)
                        {
                            maxnoofcolsinTable = 2;
                        }
                        else if (Common.MyLen(strtok(7, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]))) > 0)
                        {
                            maxnoofcolsinTable = 1;
                        }
                    }

                    row1 = strtok(1, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    if (Common.MyLen(row1) > 0)
                    {
                        dr.Row2 = row1;
                        dr.Row2val1 = strtok(1, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row2val2 = strtok(2, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row2val3 = strtok(3, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row2val4 = strtok(4, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row2val5 = strtok(5, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row2val6 = strtok(6, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                    }
                    row2 = strtok(2, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    if (Common.MyLen(row2) > 0)
                    {
                        dr.Row3 = row2;
                        dr.Row3val1 = strtok(7, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row3val2 = strtok(8, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row3val3 = strtok(9, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row3val4 = strtok(10, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row3val5 = strtok(11, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row3val6 = strtok(12, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                    }
                    row3 = strtok(3, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    if (Common.MyLen(row3) > 0)
                    {
                        dr.Row4 = row3;
                        dr.Row4val1 = strtok(13, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row4val2 = strtok(14, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row4val3 = strtok(15, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row4val4 = strtok(16, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row4val5 = strtok(17, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row4val6 = strtok(18, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                    }
                    row4 = strtok(4, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    if (Common.MyLen(row4) > 0)
                    {
                        dr.Row5 = row4;
                        dr.Row5val1 = strtok(19, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row5val2 = strtok(20, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row5val3 = strtok(21, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row5val4 = strtok(22, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row5val5 = strtok(23, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row5val6 = strtok(24, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                    }
                    row5 = strtok(5, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    if (Common.MyLen(row5) > 0)
                    {
                        dr.Row6 = row5;
                        dr.Row6val1 = strtok(25, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row6val2 = strtok(26, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row6val3 = strtok(27, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row6val4 = strtok(28, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row6val5 = strtok(29, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row6val6 = strtok(30, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                    }
                    row6 = strtok(6, Common.MyCStr(dtData.Rows[i]["DEFINETAB"]));
                    if (Common.MyLen(row6) > 0)
                    {
                        dr.Row6 = row5;
                        dr.Row7val1 = strtok(31, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row7val2 = strtok(32, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row7val3 = strtok(33, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row7val4 = strtok(34, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row7val5 = strtok(35, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                        dr.Row7val6 = strtok(36, Common.MyCStr(dtData.Rows[i]["TVAL"]));
                    }

                    dr.LBOUND = "";
                    dr.UBOUND = "";
                    dr.comments = "";
                    dr.IFCOMM = "";
                    dr.tval = "";

                    dr.NORANGE = "";
                }

                    //Start of CULTURE REPORT

                else if (Common.AISCompareString(Common.MyCStr(dtData.Rows[i]["testType"]), "CULTURE") == AISCompareStringResult.AISCompareEqual)
                {
                    isTValZero = true;
                    dr.TYP = "R";
                    List<CultureForSubRepo> Coll = new List<CultureForSubRepo>();
                    string qryCult = "select * from labtestcult where tcode='" + Common.MyCStr(dtData.Rows[i]["tcode"]) + "' and labno='" + Common.MyCStr(dtData.Rows[i]["Labno"]) + "' order by cultsno";

                    DataTable dtCult;
                    dtCult = Common.GetTableFromSession(qryCult, "Culture", null, null);

                    bool isMed = false;
                    bool medflag = false;
                    medflag = false;
                    int iCultCnt = dtCult.Rows.Count;
                    for (int iCult = 0; iCult < iCultCnt; iCult++)
                    {
                        isMed = false;
                        //medflag = false;
                        ////     collnew = new ArrayList();
                        ////    int ii;
                        ////    For ii = 1 To Coll.count
                        ////        Dim xx1 As Variant
                        ////        Set xx1 = Coll.Item(ii)
                        ////        If UCase(Trim(xx1.colname)) <> "CULTSNO" And UCase(Trim(xx1.colname)) <> "TNAME" And UCase(Trim(xx1.colname)) <> "HNAME" And UCase(Trim(xx1.colname)) <> "TVAL" And UCase(Trim(xx1.colname)) <> "HCODEPOS" And UCase(Trim(xx1.colname)) <> "INTP" Then

                        ////            addupdatedata collnew, xx1.colname, xx1.colval

                        ////        End If


                        ////}
                        isBlank = true;
                        switch (Common.MyCStr(dtCult.Rows[iCult]["cultfld"]).Trim())
                        {
                            case CULTSPEC:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) > 0)
                                {
                                    Specimen = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Specimen";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTREPORTDATE:
                                if (Common.MyCDate(dtCult.Rows[iCult]["cultval"]) > Common.MyCDate("12/31/1900"))
                                {
                                    ReportingDate = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Reporting Date";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTSAMPLEDATE:
                                if (Common.MyCDate(dtCult.Rows[iCult]["cultval"]) > Common.MyCDate("12/31/1900"))
                                {
                                    SampleDate = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Sample Date";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTORGISO:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    Organism = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Organism Isolated";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTCOLCOUNT:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    ColonyCount = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Colony Count";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTSTERILE:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    Strile = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "";
                                        dr.tval = "";
                                        INTP = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        //dr.INTP = INTP;
                                    }
                                }
                                break;
                            case CULTADVICE:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    Comment = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "";
                                        dr.tval = "";
                                        INTP = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        //dr.INTP = INTP;
                                    }
                                }
                                break;
                            case CULTTYPEOFSAMPLE:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    SampleType = Common.MyCStr(dtCult.Rows[iCult]["CULTVAL"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Sample Type";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["CULTVAL"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTBLOODSOURCE:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    BloodSource = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Blood Source";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTIDENTIFICATION:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    Identification = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Identification";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTREPORTSTAT:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    ReportState = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Report State";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;

                            case CULTMETHOD:
                                if (Common.MyLen(Common.MyCStr(dtCult.Rows[iCult]["cultval"])) != 0)
                                {
                                    Method = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                    if (IsOldStyle)
                                    {
                                        isBlank = false;
                                        dr.tname = "Method";
                                        dr.tval = Common.MyCStr(dtCult.Rows[iCult]["cultval"]);
                                        dr.INTP = "";
                                    }
                                }
                                break;
                            case CULTMEDICINECODE:
                                isMed = true;
                                medflag = true;
                                if (Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim().Length > 0)
                                {
                                    DataTable CultDt = Common.GetTableFromSession("select A.grpcode,A.NAME,G.SNO from ANTIBIOTICS A Left Outer Join Lab_medicen_group G ON A.GRPCODE=G.CODE where A.code='" + Common.MyCStr(dtCult.Rows[iCult]["MEDCODE"]) + "'", "Tab", null, null);
                                    if (CultDt != null && CultDt.Rows.Count > 0)
                                    {
                                        CultureForSubRepo Obj = new CultureForSubRepo();
                                        Obj.Name = Common.MyCStr(CultDt.Rows[0]["NAME"]).Trim();
                                        Obj.Group = Common.MyCStr(CultDt.Rows[0]["GRPCODE"]).Trim();
                                        Obj.SNO = Common.MycInt(CultDt.Rows[0]["SNO"]);
                                        Obj.Value = Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Trim();
                                        Coll.Add(Obj);
                                    }
                                    switch (Common.MyCStr(dtCult.Rows[iCult]["cultval"]).Substring(0, 1).ToUpper())
                                    {
                                        case "H":
                                            hCount = hCount + 1;
                                            hStr[hCount] = getMasterValue("Code", Common.MyCStr(dtCult.Rows[iCult]["medcode"]), "Antibiotics", "Name");
                                            break;
                                        case "M":
                                            mCount = mCount + 1;
                                            mStr[mCount] = getMasterValue("Code", Common.MyCStr(dtCult.Rows[iCult]["medcode"]), "Antibiotics", "Name");
                                            break;
                                        case "R":
                                            rCount = rCount + 1;
                                            rstr[rCount] = getMasterValue("Code", Common.MyCStr(dtCult.Rows[iCult]["medcode"]), "Antibiotics", "Name");
                                            break;
                                        case "S":
                                            sCount = sCount + 1;
                                            sStr[sCount] = getMasterValue("Code", Common.MyCStr(dtCult.Rows[iCult]["medcode"]), "Antibiotics", "Name");
                                            break;
                                        default:
                                            break;
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        if (isMed == false && isBlank == false)
                        {
                            cultsno = cultsno + 1;
                            if (Common.AISCompareString(Common.MyCStr(dtCult.Rows[iCult]["cultfld"]), CULTADVICE) == AISCompareStringResult.AISCompareEqual)
                            {
                                dr.cultsno = 1000;
                            }
                            else
                            {
                                dr.cultsno = cultsno;
                            }
                            //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                            dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                            dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]).Trim();
                            objRepData.Report_Table.Rows.Add(dr);
                            dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                            dr.HName = HName;
                            //dr.hcodepos = hcodepos;
                            dr.hcode = HCode;
                            dr.labno = LABNO;
                            dr.SIGN = SIGN;
                            dr.EOR = EOR;
                        }
                    }
                    int counter;
                    if (1==1)
                    {
                        if (medflag && IsOldStyle)
                        {
                            if (hCount > -1)
                            {
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                //                            dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.tname = "";
                                dr.senst = GetRepoVal("lbshowhias");
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]).Trim();
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;
                                dr.EOR = EOR;
                            }

                            for (counter = 0; counter <= hCount; counter = counter + 3)
                            {
                                if (hCount - counter >= 2)
                                {
                                    dr.med1 = hStr[counter];
                                    dr.med2 = hStr[counter + 1];
                                    dr.med3 = hStr[counter + 2];
                                }
                                else if (hCount - counter >= 1)
                                {
                                    dr.med1 = hStr[counter];
                                    dr.med2 = hStr[counter + 1];
                                }
                                else
                                {
                                    dr.med1 = hStr[counter];
                                }
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]).Trim();
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;
                                dr.EOR = EOR;
                            }

                            if (mCount > -1)
                            {
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]);
                                dr.tname = "";
                                dr.senst = GetRepoVal("lshowmodas");
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;
                                dr.EOR = EOR;
                            }
                            for (counter = 0; counter <= mCount; counter = counter + 3)
                            {
                                if (mCount - counter >= 2)
                                {
                                    dr.med1 = mStr[counter];
                                    dr.med2 = mStr[counter + 1];
                                    dr.med3 = mStr[counter + 2];
                                }
                                else if (mCount - counter >= 1)
                                {
                                    dr.med1 = mStr[counter];
                                    dr.med2 = mStr[counter + 1];
                                }
                                else
                                {
                                    dr.med1 = mStr[counter];
                                }
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]);
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;
                                dr.EOR = EOR;
                            }
                            if (sCount > -1)
                            {
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]);
                                dr.tname = "";
                                dr.senst = GetRepoVal("lshowsenas");
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;

                                dr.EOR = EOR;
                            }
                            for (counter = 0; counter <= sCount; counter = counter + 3)
                            {
                                if (sCount - counter >= 2)
                                {
                                    dr.med1 = sStr[counter];
                                    dr.med2 = sStr[counter + 1];
                                    dr.med3 = sStr[counter + 2];
                                }
                                else if (sCount - counter >= 1)
                                {
                                    dr.med1 = sStr[counter];
                                    dr.med2 = sStr[counter + 1];
                                }
                                else
                                {
                                    dr.med1 = sStr[counter];
                                }
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]);
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;
                                dr.EOR = EOR;
                            }
                            if (rCount > -1)
                            {
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]);
                                dr.tname = "";
                                dr.senst = GetRepoVal("lshowresas");
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;
                                dr.EOR = EOR;
                            }
                            for (counter = 0; counter <= rCount; counter = counter + 3)
                            {
                                if (rCount - counter >= 2)
                                {
                                    dr.med1 = rstr[counter];
                                    dr.med2 = rstr[counter + 1];
                                    dr.med3 = rstr[counter + 2];
                                }
                                else if (rCount - counter >= 1)
                                {
                                    dr.med1 = rstr[counter];
                                    dr.med2 = rstr[counter + 1];
                                }
                                else
                                {
                                    dr.med1 = rstr[counter];
                                }
                                cultsno = cultsno + 1;
                                dr.cultsno = cultsno;
                                dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, 1, true, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                //dr.hcodepos = hcodepos.TrimEnd() + Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 2, true, 1, true) + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                                dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]);
                                objRepData.Report_Table.Rows.Add(dr);
                                dr = (ReportData.Report_TableRow)objRepData.Report_Table.NewRow();
                                dr.HName = HName;
                                //dr.hcodepos = hcodepos;
                                dr.hcode = HCode;
                                dr.labno = LABNO;
                                dr.SIGN = SIGN;
                                dr.EOR = EOR;
                            }
                        }
                        else if (IsOldStyle==false)
                        {
                            dr.cultsno = cultsno;
                            string sttt = Common.myformat(Common.MyCStr(dtData.Rows[i]["TestLibSNo"]), false, true, 5, 0, true, true);

                            dr.hcodepos = hcodepos.TrimEnd() + sttt + Common.AISUCase(Common.MyCStr(dtData.Rows[i]["tcode"])).Trim();
                            dr.HName = Common.MyCStr(dtData.Rows[i]["TestLibName"]);
                            dr.tname = "";
                            dr.med1 = "";
                            int isanymedicine = 0;

                            int icNt, iCnt2, SNO = 0;
                            string PRVMED1 = "", PRVRESULT1 = "", PRVMED2 = "", PRVRESULT2 = "", prvgrp = "";
                            bool isanyrowaddedincurrent = false;
                            isanyrowaddedincurrent = false;
                            
                            for (icNt = 0; icNt < Coll.Count; icNt++)
                            {
                                
                                CultureForSubRepo Obj = Coll[icNt];
                                if (Obj.POS == 0)
                                {
                                    prvgrp = Obj.Group;
                                    int POSVAL = 0;
                                    PRVMED1 = "";
                                    PRVRESULT1 = "";
                                    PRVMED2 = "";
                                    PRVRESULT2 = "";
                                    for (iCnt2 = icNt; iCnt2 < Coll.Count; iCnt2++)
                                    {
                                        Obj = Coll[iCnt2];
                                        if (Obj.POS == 0 && Common.AISUCase(prvgrp.Trim()) == Common.AISUCase(Obj.Group.Trim()))
                                        {
                                            POSVAL++;
                                            Obj.POS = 1;
                                            if (POSVAL == 1)
                                            {
                                                PRVMED1 = Obj.Name;
                                                PRVRESULT1 = Obj.Value;
                                            }
                                            else if (POSVAL == 2)
                                            {
                                                PRVMED2 = Obj.Name;
                                                PRVRESULT2 = Obj.Value;
                                                DataTable rsgrpname = Common.GetTableFromSession("select name,SNO from Lab_medicen_group where code='" + prvgrp + "'", "Tab", null, null);
                                                if (rsgrpname != null && rsgrpname.Rows.Count > 0)
                                                {
                                                    if (Common.MyLen(Common.MyCStr(rsgrpname.Rows[0]["NAME"])) > 0)
                                                    {
                                                        prvgrp = Common.MyCStr(rsgrpname.Rows[0]["Name"]).Trim();
                                                        SNO = Common.MycInt(rsgrpname.Rows[0]["SNO"]);
                                                    }
                                                }
                                                isanymedicine = 1;
                                                ReportData.CultureResultRow CultDr = (ReportData.CultureResultRow)objRepData.CultureResult.NewRow();
                                                CultDr.MEDNAME1 = PRVMED1;
                                                CultDr.RESULT1 = PRVRESULT1;
                                                CultDr.MEDNAME2 = PRVMED2;
                                                CultDr.RESULT2 = PRVRESULT2;
                                                CultDr.GRPNAME = prvgrp;
                                                CultDr.SNO = SNO;
                                                CultDr.BloodSource = BloodSource;
                                                CultDr.ColonyCount = ColonyCount;
                                                CultDr.Comment = Comment;
                                                CultDr.Footer = Footer;
                                                CultDr.Header1 = Header1;
                                                CultDr.Header2 = Header2;
                                                CultDr.Identification = Identification;
                                                CultDr.MedicineAs = MedicineAs;
                                                CultDr.Method = Method;
                                                CultDr.Organism = Organism;
                                                CultDr.ReportingDate = ReportingDate;
                                                CultDr.ReportState = ReportState;
                                                CultDr.SampleDate = SampleDate;
                                                CultDr.SampleType = SampleType;
                                                CultDr.SenstivityAs = SenstivityAs;
                                                CultDr.Specimen = Specimen;
                                                CultDr.Strile = Strile;
                                                CultDr.UpperHeader = UpperHeader;

                                                

                                                CultDr.HASANYMEDICINE = 1;
                                                CultDr.TESTCODE = Common.MyCStr(dtData.Rows[i]["tcode"]);
                                                isanyrowaddedincurrent = true;
                                                objRepData.CultureResult.Rows.Add(CultDr);

                                                PRVMED1 = "";
                                                PRVRESULT1 = "";
                                                PRVMED2 = "";
                                                PRVRESULT2 = "";
                                                POSVAL = 0;

                                            }
                                        }
                                    }

                                    if (Common.MyLen(PRVMED1.Trim()) > 0)
                                    {
                                        DataTable rsgrpname = Common.GetTableFromSession("select name,SNO from Lab_medicen_group where code='" + prvgrp + "'", "Tab", null, null);
                                        if (rsgrpname != null && rsgrpname.Rows.Count > 0)
                                        {
                                            if (Common.MyLen(Common.MyCStr(rsgrpname.Rows[0]["Name"])) > 0)
                                            {
                                                prvgrp = Common.MyCStr(rsgrpname.Rows[0]["Name"]).Trim();
                                                SNO = Common.MycInt(rsgrpname.Rows[0]["SNO"]);
                                            }
                                        }
                                        isanymedicine = 1;
                                        ReportData.CultureResultRow CultDr = (ReportData.CultureResultRow)objRepData.CultureResult.NewRow();
                                        CultDr.MEDNAME1 = PRVMED1;
                                        CultDr.RESULT1 = PRVRESULT1;
                                        CultDr.MEDNAME2 = "";
                                        CultDr.RESULT2 = "";
                                        CultDr.GRPNAME = prvgrp;
                                        CultDr.SNO = SNO;
                                        CultDr.BloodSource = BloodSource;
                                        CultDr.ColonyCount = ColonyCount;
                                        CultDr.Comment = Comment;
                                        CultDr.Footer = Footer;
                                        CultDr.Header1 = Header1;
                                        CultDr.Header2 = Header2;
                                        CultDr.Identification = Identification;
                                        CultDr.MedicineAs = MedicineAs;
                                        CultDr.Method = Method;
                                        CultDr.Organism = Organism;
                                        CultDr.ReportingDate = ReportingDate;
                                        CultDr.ReportState = ReportState;
                                        CultDr.SampleDate = SampleDate;
                                        CultDr.SampleType = SampleType;
                                        CultDr.SenstivityAs = SenstivityAs;
                                        CultDr.Specimen = Specimen;
                                        CultDr.Strile = Strile;
                                        CultDr.UpperHeader = UpperHeader;
                                        CultDr.HASANYMEDICINE = 1;
                                        CultDr.TESTCODE = Common.MyCStr(dtData.Rows[i]["tcode"]);
                                        objRepData.CultureResult.Rows.Add(CultDr);
                                        isanyrowaddedincurrent = true;
                                    }
                                }

                            }
                            if (!isanyrowaddedincurrent )
                            {
                                ReportData.CultureResultRow CultDr = (ReportData.CultureResultRow)objRepData.CultureResult.NewRow();

                                if (Common.MyLen(BloodSource) != 0 ||
                                    Common.MyLen(ColonyCount) != 0 ||
                                    Common.MyLen(Comment) != 0 ||
                                    Common.MyLen(Header1) != 0 ||
                                    Common.MyLen(Header2) != 0 ||
                                    Common.MyLen(Identification) != 0 ||
                                    Common.MyLen(Method) != 0 ||
                                    Common.MyLen(Organism) != 0 ||
                                    Common.MyLen(ReportingDate) != 0 ||
                                    Common.MyLen(ReportState) != 0 ||
                                    Common.MyLen(SampleDate) != 0 ||
                                    Common.MyLen(SampleType) != 0 ||
                                    Common.MyLen(Specimen) != 0 ||
                                    Common.MyLen(Strile) != 0
                                    )
                                {

                                    CultDr.SNO = 0;
                                    CultDr.BloodSource = BloodSource;
                                    CultDr.ColonyCount = ColonyCount;
                                    CultDr.Comment = Comment;
                                    ////////CultDr.Footer = Footer;
                                    CultDr.Header1 = Header1;
                                    CultDr.Header2 = Header2;
                                    CultDr.Identification = Identification;
                                    CultDr.MedicineAs = MedicineAs;
                                    CultDr.Method = Method;
                                    CultDr.Organism = Organism;
                                    CultDr.ReportingDate = ReportingDate;
                                    CultDr.ReportState = ReportState;
                                    CultDr.SampleDate = SampleDate;
                                    CultDr.SampleType = SampleType;
                                    CultDr.SenstivityAs = SenstivityAs;
                                    CultDr.Specimen = Specimen;
                                    CultDr.Strile = Strile;
                                    CultDr.UpperHeader = UpperHeader;
                                    CultDr.HASANYMEDICINE = 1;
                                    CultDr.TESTCODE = Common.MyCStr(dtData.Rows[i]["tcode"]);
                                    objRepData.CultureResult.Rows.Add(CultDr);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }

                    dr.INTP = INTP;
                    hCount = -1;
                    mCount = -1;
                    rCount = -1;
                    sCount = -1;

                }
                //End of CULTURE REPORT.
                else
                {
                    dr.TYP = "N";

                    COMM = Common.MyCStr(dtData.Rows[i]["COMMENTS"]).Trim();
                    dr.comments = COMM;
                    if (COMM.Trim() == "")
                    {
                        IFCOMM = "N";
                    }
                    else
                    {
                        IFCOMM = "Y";
                    }
                    dr.IFCOMM = IFCOMM;

                    LowUpBound = getLowerUpperBound(Common.MyCStr(dtData.Rows[i]["tcode"]), sex, age, mons, days);

                    highbound = Common.MyCStr(LowUpBound["0"]);
                    lowbound = Common.MyCStr(LowUpBound["1"]);

                    if (Common.MyLen(lowbound) > 0 && Common.MyLen(highbound) > 0)
                    {
                        NORMALRANGE = "(" + lowbound.Trim() + "-" + highbound.Trim() + ")";
                    }
                    else if (Common.MyLen(lowbound) > 0)
                    {
                        NORMALRANGE = "(" + lowbound.Trim() + ")";
                    }
                    else if (Common.MyLen(highbound) > 0)
                    {
                        NORMALRANGE = "(" + (highbound).Trim() + ")";
                    }
                    else
                    {
                        NORMALRANGE = "";
                    }
                    if (Common.MyLen(highbound) > 0 && Common.MyLen(lowbound) > 0)
                    {
                        if (Common.MyCDbl(dtData.Rows[i]["TVAL"]) >= Common.MyCDbl(lowbound) && (Common.MyCDbl(dtData.Rows[i]["TVAL"]) <= Common.MyCDbl(highbound)))
                        {
                            abnormal = "N";
                        }
                        else if (Common.MyCDbl(lowbound) == 0 && Common.MyCDbl(highbound) == 0)
                        {
                            abnormal = "N";
                        }
                        else
                        {
                            abnormal = "Y";
                        }
                    }
                    else
                    {
                        abnormal = "N";
                    }
                }


                string testvalue;
                testvalue = Common.MyCStr(dtData.Rows[i]["TVAL"]);

                if (Common.AISCompareString(abnormal, "Y") == AISCompareStringResult.AISCompareEqual && reqStarinabnormal)
                {
                    testvalue = "*" + testvalue;
                }
                dr.abnormal = abnormal;
                if (isTValZero)
                {
                    dr.tval = "";
                }
                else
                {
                    dr.tval = testvalue;
                }
                isTValZero = false;
                
                if (Common.MyLen(testvalue) != 0)
                {
                    dr.LBOUND = lowbound;
                    dr.UBOUND = highbound;
                    dr.NORANGE = NORMALRANGE;
                }
                else
                {
                    dr.LBOUND = "";
                    dr.UBOUND = "";
                    dr.NORANGE = "";
                }
                dr.HighLowVal = "";
                if (Common.mycboolean(GetRepoVal("lbrqhghl")))
                {
                    if (Common.MyLen(dr.LBOUND ) > 0 && Common.MyCDbl(lowbound) > 0)
                    {
                        if (Common.MyCDbl(testvalue) < Common.MyCDbl(lowbound))
                        {
                            dr.HighLowVal = "Low";

                        }
                    }

                    if (Common.MyLen(dr.UBOUND) > 0 && Common.MyCDbl(highbound) > 0)
                    {
                        if (Common.MyCDbl(testvalue) > Common.MyCDbl(highbound))
                        {
                            dr.HighLowVal = "High";
                        }
                    }

                }

                objRepData.Report_Table.Rows.Add(dr);
            }
            DataTable dt22=objRepData.Report_Table.Copy();
            dt22.DefaultView.Sort = "booksno,hcodepos,sno,testcode";
            DataTable dt33 = dt22.DefaultView.ToTable();
            objRepData.Report_Table.Clear();
            for (int ij = 0; ij < dt33.Rows.Count; ij++)
            {
                DataRow drnew = objRepData.Report_Table.NewRow();
                drnew.ItemArray = dt33.Rows[ij].ItemArray;
                objRepData.Report_Table.Rows.Add(drnew);

            }
            


            if (Common.AISCompareString(WEBREQSIGN, "Y") == AISCompareStringResult.AISCompareEqual)
            {
                ReqWebDigitalSign = true;
            }

            return objRepData;
        }

        void GetHeaderData(string LabNo, string testheadcode)
        {
            

            ReportData.Report_HeaderRow dr;
            dr = objRepData.Report_Header.NewReport_HeaderRow();


           


            double  signtop=0, signleft=0, signwidth=0, signhieght=0;
            byte[] signimage;
            byte[] websignImage = null;
            byte[] InterimImg = null;
            bool isapproved = false;
            double WebSignTop = 0.0, webSignLeft = 0.0, WebSignHeight = 0.0, WebSignWidth = 0.0;

            string appuser = "";
            string appcons = "";
            clsReport obj = new clsReport();
            if (Common.AISCompareString(obj.gettunvar("apprhdwis"), Common.AISUCase("y")) == AISCompareStringResult.AISCompareEqual)
            {
                if ((Common.MyLen(testheadcode)) > 0)
                {

                    string qryapproove = "select * from LABHEADAPPROVAL where labno='" + LabNo + "' and lcode='" + testheadcode + "'";
                    DataTable dtapproove = Common.GetTableFromSession(qryapproove, "", null, null);
                    if (dtapproove.Rows.Count > 0)
                    {
                        if (Common.MyLen(Common.MyCStr(dtapproove.Rows[0]["conscode"]).Trim()) > 0)
                        {
                            appcons = Common.MyCStr(dtapproove.Rows[0]["conscode"]);
                        }
                        else
                        {
                            appuser = Common.MyCStr(dtapproove.Rows[0]["ApprovedBy"]);
                        }
                    }
                    dtapproove = null;
                    if (Common.MyLen(appuser) > 0)
                    {
                        qryapproove = "select * from AP_UserSign where usercode='" + appuser + "'";
                        dtapproove = Common.GetTableFromSession(qryapproove, "", null, null);
                        if (dtapproove.Rows.Count > 0)
                        {
                            signhieght = Common.MyCDbl((dtapproove.Rows[0]["ht"]));
                            signtop = Common.MyCDbl((dtapproove.Rows[0]["tp"]));
                            signwidth = Common.MyCDbl((dtapproove.Rows[0]["wid"]));
                            signleft = Common.MyCDbl((dtapproove.Rows[0]["lft"]));


                            if (Common.MyCDbl((dtapproove.Rows[0]["issepwebsett"])) == 1)
                            {
                                signhieght = Common.MyCDbl((dtapproove.Rows[0]["webht"]));
                                signtop = Common.MyCDbl((dtapproove.Rows[0]["webtp"]));
                                signwidth = Common.MyCDbl((dtapproove.Rows[0]["webwid"]));
                                signleft = Common.MyCDbl((dtapproove.Rows[0]["weblft"]));

                            }

                            if (Common.MyLen(Common.MyCStr(dtapproove.Rows[0]["digitalimage"])) > 0)
                            {
                                signimage = (byte[])(dtapproove.Rows[0]["digitalimage"]);
                                dr.SignImage = signimage;
                            }

                        }
                    }
                    if (Common.MyLen(appcons) > 0)
                    {

                        qryapproove = "select labsign from consmast where code='" + appcons + "'";
                        dtapproove = Common.GetTableFromSessionSecond(qryapproove, "", null, null);
                        if (dtapproove.Rows.Count > 0)
                        {
                            SIGN = Common.MyCStr(dtapproove.Rows[0]["labsign"]);

                        }
                    }

                    dr.SIht = Common.MyCStr(signhieght);
                    dr.SIlft = Common.MyCStr(signleft);
                    dr.SItop = Common.MyCStr(signtop);
                    dr.SIwd = Common.MyCStr(signwidth);
                    

                }
            }
            else  ///// approval on complete lab report
            {
                string qryapproove = "Select isapproved,approvedby,APPROVALDATE From " + LabMTablename + " where LABNO='" + LabNo + "'";
                DataTable dtTemp1;
                dtTemp1 = Common.GetTableFromSession(qryapproove, "table", null, null);
                if (dtTemp1.Rows.Count > 0)
                {

                    if (Common.MyCDbl(dtTemp1.Rows[0]["isapproved"]) == 1)
                    {
                        appuser = Common.MyCStr(dtTemp1.Rows[0]["approvedby"]);

                        qryapproove = "select * from AP_UserSign where usercode='" + appuser + "'";
                        DataTable dtapproove = Common.GetTableFromSession(qryapproove, "", null, null);
                        if (dtapproove.Rows.Count > 0)
                        {
                            signhieght = Common.MyCDbl((dtapproove.Rows[0]["ht"]));
                            signtop = Common.MyCDbl((dtapproove.Rows[0]["tp"]));
                            signwidth = Common.MyCDbl((dtapproove.Rows[0]["wid"]));
                            signleft = Common.MyCDbl((dtapproove.Rows[0]["lft"]));
                            if (Common.MyCDbl((dtapproove.Rows[0]["issepwebsett"])) == 1)
                            {
                                signhieght = Common.MyCDbl((dtapproove.Rows[0]["webht"]));
                                signtop = Common.MyCDbl((dtapproove.Rows[0]["webtp"]));
                                signwidth = Common.MyCDbl((dtapproove.Rows[0]["webwid"]));
                                signleft = Common.MyCDbl((dtapproove.Rows[0]["weblft"]));
                            
                            }


                            if (Common.MyLen(Common.MyCStr(dtapproove.Rows[0]["digitalimage"])) > 0)
                            {
                                signimage = (byte[])(dtapproove.Rows[0]["digitalimage"]);
                                dr.SignImage = signimage;
                            }
                            dr.SIht = Common.MyCStr(signhieght);
                            dr.SIlft = Common.MyCStr(signleft);
                            dr.SItop = Common.MyCStr(signtop);
                            dr.SIwd = Common.MyCStr(signwidth);
                            isapproved=true;
                        }



                        string strshowapprovedbyas = "Approved By : ";
                        string strshowapprovedtimeas = "Approval Date : ";
                        bool boolshowapprovedtime = true;
                        bool boolshowapprovedby = true;

                        int lftapprovedby = 0;
                        int topapprovedby = 0;

                        int lftapprovedtime = 4500;
                        int topapprovedtime = 0;
                        if (Common.AISCompareString(obj.gettunvar("custappset"), Common.AISUCase("y")) == AISCompareStringResult.AISCompareEqual)
                        {
                            if (Common.AISCompareString(obj.gettunvar("apprhdapby"), Common.AISUCase("y")) == AISCompareStringResult.AISCompareEqual)
                            {
                                boolshowapprovedby = false;
                            }
                            if (Common.AISCompareString(obj.gettunvar("hdtimby"), Common.AISUCase("y")) == AISCompareStringResult.AISCompareEqual)
                            {
                                boolshowapprovedtime = false;
                            }

                            strshowapprovedbyas = obj.gettunvar("apprshapby").Trim();
                            strshowapprovedtimeas = obj.gettunvar("aprstimby").Trim();

                            lftapprovedby = (int)Common.MyCDbl(obj.gettunvar("apprleftby")) * 1440;
                            topapprovedby = (int)Common.MyCDbl(obj.gettunvar("apprtopby")) * 1440;
                            lftapprovedtime = (int)Common.MyCDbl(obj.gettunvar("apprtiml")) * 1440;
                            topapprovedtime = (int)Common.MyCDbl(obj.gettunvar("apptimtop")) * 1440;

                            strshowapprovedbyas = strshowapprovedbyas + " " + Common.MyCStr((dtTemp1.Rows[0]["approvedby"]));
                            strshowapprovedtimeas = strshowapprovedtimeas + " " + Common.GetPrintDate(Common.MyCDate(dtTemp1.Rows[0]["APPROVALDATE"]), "dd/MMM/yyyy HH:mm");

                            dr.ApprovedBy = Common.MyCStr(strshowapprovedbyas);
                            dr.ApprovedTime = Common.MyCStr(strshowapprovedtimeas);
                            if (boolshowapprovedby)
                            {
                                dr.showAppr = 1;
                            }
                            if (boolshowapprovedtime)
                            {
                                dr.showApptime = 1;
                            }
                            dr.ApprovedbyLeft = lftapprovedby;
                            dr.ApprovedBytop = topapprovedby;
                            dr.ApptimeTop = topapprovedtime;
                            dr.AppTimeLeft = lftapprovedtime;
                        }
                    }
                }
            }

            dr.MethodBold = Common.MycInt(GetRepoVal("lbmbold"));
            dr.MethodFont = Common.MyCStr(GetRepoVal("lblmethf"));
            dr.MethodFS = Common.MyCDbl(GetRepoVal("lbmthsz"));

            string qry = "Select PCode,PatName,TDate,PatNo,ConsName From " + LabMTablename + " where LABNO='" + LabNo + "'";
            DataTable dtTemp;
            dtTemp = Common.GetTableFromSession(qry, "table", null, null);
            
            //HttpContext.Current.Server.MapPath()
            
            
            string patientname = "";
            if (dtTemp.Rows.Count != 0)
            {
                patientname = Common.MyCStr(dtTemp.Rows[0]["PatName"]);

                dr.PatCode = Common.MyCStr(dtTemp.Rows[0]["PCode"]);
                dr.RegnNo = Common.MyCStr(dtTemp.Rows[0]["PatNo"]);
                dr.ReferBy = Common.MyCStr(dtTemp.Rows[0]["ConsName"]);
                dr.Date = Common.GetPrintDate(Common.MyCDate(dtTemp.Rows[0]["TDate"]), AisPrintDateFormat.Default);

                string qryPatdetails;
                qryPatdetails = "Select * from patient where code='" + Common.MyCStr(dtTemp.Rows[0]["PCode"]) + "'";
                DataTable dtPatDetails;
                dtPatDetails = Common.GetTableFromSession(qryPatdetails, "PatientDetails", null, null);
                if (dtPatDetails.Rows.Count != 0)
                {
                    string agestr;
                    agestr = "";
                    agestr = GetageforReport(Common.MyCDbl(dtPatDetails.Rows[0]["age"]), Common.MyCDbl(dtPatDetails.Rows[0]["mons"]), Common.MyCDbl(dtPatDetails.Rows[0]["days"]));
                    agestr = agestr + "/" + Common.MyCStr(dtPatDetails.Rows[0]["Sex"]).Trim();
                    dr.Age = agestr;
                    if (Common.MyLen(Common.MyCStr(dtPatDetails.Rows[0]["pre"])) > 0)
                    {
                        patientname = Common.MyCStr(dtPatDetails.Rows[0]["pre"]).Trim() + " " + patientname;
                    }




                }
                else
                {

                    dtPatDetails = Common.GetTableFromSessionSecond(qryPatdetails, PatientTablename, null, null);
                    if (dtPatDetails.Rows.Count != 0)
                    {
                        string agestr;
                        agestr = "";
                        agestr = GetageforReport(Common.MyCDbl(dtPatDetails.Rows[0]["age"]), Common.MyCDbl(dtPatDetails.Rows[0]["mons"]), Common.MyCDbl(dtPatDetails.Rows[0]["days"]));

                        agestr = agestr + "/" + Common.MyCStr(dtPatDetails.Rows[0]["Sex"]).Trim();
                        dr.Age = agestr;
                        if (Common.MyLen(Common.MyCStr(dtPatDetails.Rows[0]["pre"])) > 0)
                        {
                            patientname = Common.MyCStr(dtPatDetails.Rows[0]["pre"]).Trim() + " " + patientname;
                        }
                    }
                }
                dr.PatName = patientname;

                dr.LabNo = LabNo;

            }
            qry = "Select * from labm where labno='" + LabNo + "'";
            DataTable dtlabm = Common.GetTableFromSession(qry,"Labm",null,null);
            if(dtlabm.Rows.Count>0)
            {
                if(Common.MycInt(dtlabm.Rows[0]["isinterim"])==1 && Common.MyLen(Common.MyCStr(dtlabm.Rows[0]["intmgenby"]))>0)
                {
                    string qry1 = "Select * from AP_UserSign where code='" + Common.MyCStr(dtlabm.Rows[0]["intmgenby"]).Trim() + "'";
                    DataTable dtsign= Common.GetTableFromSession(qry1, "Labm", null, null);
                    if(dtsign.Rows.Count>0)
                    {
                      if(dtsign.Rows[0]["DIGITALIMAGE"]!=DBNull.Value)
                        {
                            InterimImg = (byte[])dtsign.Rows[0]["DIGITALIMAGE"];
                            dr["InterimImg"] = InterimImg;
                            if(!isapproved)
                            {
                                dr["HeaderCaption"]=obj.gettunvar("shwintrpas").Trim();
                                string interimby= Common.MyCStr(dtlabm.Rows[0]["intmgenby"]).Trim();
                                if (Common.MyLen(interimby)>0)
                                {
                                    DataTable dt = Common.GetTableFromSession("Select USERDESCR from USERROLE","USERROLE",null,null);
                                    if(dt.Rows.Count>0)
                                    {
                                        if(Common.MyLen(Common.MyCStr(dt.Rows[0]["USERDESCR"]))>0)
                                        {
                                            interimby = Common.MyCStr(dt.Rows[0]["USERDESCR"]).Trim();
                                        }
                                    }

                                }
                                dr["InterimBy"] = obj.gettunvar("intmcap").Trim() + " " + interimby;

                            }
                        }
                    }
                }
            }
            string QUERY;
            QUERY = "Select " + LabTestTablename + ".gcode," + LabTestTablename + ".tcode," + LabTestTablename + ".booksno," + LabTestTablename + ".BCODE," + LabTestTablename + ".Remarks AS LabTestRemarks," + LabTestTablename + ".LABNO," + LabTestTablename + ".TVAL," + LabTestTablename + ".Comments,";
            QUERY = QUERY + TestLibTablename + ".dcode," + TestLibTablename + ".lcode," + TestLibTablename + ".ismain," + TestLibTablename + ".name AS TestLibName," + TestLibTablename + ".SNO as TestLibSNo," + TestLibTablename + ".units," + TestLibTablename + ".DETAILS," + TestLibTablename + ".testType ," + TestLibTablename + ".tableFont," + TestLibTablename + ".tableFontsize," + TestLibTablename + ".tableCaptionWidth," + TestLibTablename + ".tableColWidth," + TestLibTablename + ".DEFINETAB";
            QUERY = QUERY + " From  (" + LabTestTablename + " Left Outer Join " + TestLibTablename + " on " + LabTestTablename + ".tcode=" + TestLibTablename + ".code)";
            QUERY = QUERY + " where " + LabTestTablename + ".labno='" + LabNo + "' AND " + LabTestTablename + ".isperf='Y' " + wcl;
            QUERY = QUERY + " AND " + TestLibTablename + ".ISPF='N' ";
            DataTable dtData;
            dtData = Common.GetTableFromSession(QUERY, LabTestTablename, null, null);
            string dcode;
            dcode = "";
            Boolean multidepartment;
            multidepartment = true;
            if (dtData.Rows.Count > 0)
            {
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    if (Common.MyCStr(dcode).Trim().Length > 0)
                    {
                        if (Common.AISCompareString(Common.MyCStr(dcode).Trim().ToUpper(), Common.MyCStr(dtData.Rows[i]["dcode"]).Trim().ToUpper()) != AISCompareStringResult.AISCompareEqual)
                        {
                            multidepartment = false;
                        }

                    }
                    dcode = Common.MyCStr(dtData.Rows[i]["dcode"]);
                }
            }
            double hasspeieddept = 0;
            QUERY = "Select * from " + DeptartmentTablename + " where code='" + dcode + "'";
            dtData = Common.GetTableFromSession(QUERY, DeptartmentTablename, null, null);
            if (dtData.Rows.Count > 0)
            {
                hasspeieddept = Common.MyCDbl(dtData.Rows[0]["hasspecifysetting"]);
                if (Common.AISCompareString(Common.MyCStr(dtData.Rows[0]["REQDIGITSIGNONWEB"]), "Y") == AISCompareStringResult.AISCompareEqual)
                {
                    ReqWebDigitalSign = true;
                }
                dr.SignHeight = Common.MyCDbl(dtData.Rows[0]["websignht"]) * 1440;
            }
            
            string query = "Select * from " + WebsettingTablename;
            DataTable dtHeadFoot;
            dtHeadFoot = Common.GetTableFromSession(query, WebsettingTablename, null, null);
            int  reqlogo = 0;
            
                if (dtHeadFoot.Rows.Count > 0)
                {
                    reqlogo = Common.MycInt(dtHeadFoot.Rows[0]["islogo"]);
                  if (hasspeieddept != 1)
                   {
                    signimage = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~\\Logo.bmp"));
                    dr.LogoImage = signimage;
                    if (reqlogo == 1)
                    {
                        dr.logoht = Common.MyCStr(dtHeadFoot.Rows[0]["LOGOHT"]);
                        dr.logolft = Common.MyCStr(dtHeadFoot.Rows[0]["LOGOLFT"]);
                        dr.logotp = Common.MyCStr(dtHeadFoot.Rows[0]["LOGOTP"]);
                        dr.logowid = Common.MyCStr(dtHeadFoot.Rows[0]["LOGOWID"]);
                    }
                    else
                    {
                        dr.logoht = "0";
                        dr.logolft = "0";
                        dr.logotp = "0";
                        dr.logowid = "0";

                    }

                }
            }
           string  Dptlogo = "select * from deptwslogo where deptcode='" + dcode + "'";
           DataTable dtdptlogo = Common.GetTableFromSession(Dptlogo, "", null, null);
           if (hasspeieddept == 1)
           {
               if (dtdptlogo.Rows.Count > 0)
               {

                   signimage = (byte[])(dtdptlogo.Rows[0]["digtimage"]);
                   dr.LogoImage = signimage;

                   if (reqlogo == 1)
                   {
                       dr.logoht = Common.MyCStr(dtdptlogo.Rows[0]["imght"]);
                       dr.logolft = Common.MyCStr(dtdptlogo.Rows[0]["imgleft"]);
                       dr.logotp = Common.MyCStr(dtdptlogo.Rows[0]["imgtop"]);
                       dr.logowid = Common.MyCStr(dtdptlogo.Rows[0]["imgwid"]); 
                   }
                   else
                   {
                       dr.logoht = "0";
                       dr.logolft = "0";
                       dr.logotp = "0";
                       dr.logowid = "0";

                   }

               }
           }
            if (multidepartment == true)
            {
                
                    
                    if (hasspeieddept == 1)
                    {

                        dr.Header = Common.MyCStr(dtData.Rows[0]["webhead"]);
                        dr.Footer = Common.MyCStr(dtData.Rows[0]["webfoot"]);
                        dr.FooterHeight = Common.MyCStr(dtData.Rows[0]["webfooterH"]);
                       
                    }

            }


            
                

                if (dtHeadFoot.Rows.Count != 0)
                {
                    if (hasspeieddept != 1)
                    {
                        dr.Header = Common.MyCStr(dtHeadFoot.Rows[0]["HDR"]);
                        dr.Footer = Common.MyCStr(dtHeadFoot.Rows[0]["FTR"]);
                        dr.FooterHeight = GetRepoVal("lbftht");
                        //dr.logoht = Common.MyCStr(dtHeadFoot.Rows[0]["logoht"]);

                        //dr.logolft = Common.MyCStr(dtHeadFoot.Rows[0]["logolft"]);
                        //dr.logotp = Common.MyCStr(dtHeadFoot.Rows[0]["logotp"]);
                        //dr.logowid = Common.MyCStr(dtHeadFoot.Rows[0]["logowid"]);
                    }

                    
                }

                if (ReqWebDigitalSign)
                {
                    Dptlogo = "select * from AP_DigtSign where deptcode='WEB_" + dcode + "'";
                    dtdptlogo = Common.GetTableFromSession(Dptlogo, "", null, null);

                    if (dtdptlogo.Rows.Count > 0)
                    {

                        signimage = (byte[])(dtdptlogo.Rows[0]["DIGITALIMAGE"]);
                        dr.WEBSIGNIMAGE = signimage;

                        dr.WEBLOGOHT = Common.MyCStr(dtdptlogo.Rows[0]["HT"]);
                        dr.WEBLOGOLT = Common.MyCStr(dtdptlogo.Rows[0]["LFT"]);
                        dr.WEBLOGOTOP = Common.MyCStr(dtdptlogo.Rows[0]["TP"]);
                        dr.WEBLOGOWID = Common.MyCStr(dtdptlogo.Rows[0]["WID"]);
                    }
                }
                if (mycboolean(GetRepoVal("SYKIPWTN")))
                {
                    dr.KeepTogether = 1;
                }
                else
                {
                    dr.KeepTogether = 0;
                }


                string QUERYPanel = "select pn.name name from bills b left outer join panel pn on b.panel=pn.code  where b.patno='" + dr.RegnNo + "'";
                DataTable dtpnl = Common.GetTableFromSessionSecond(QUERYPanel, "PAnel", null, null);
                if (dtpnl.Rows.Count > 0)
                {
                    dr.Panel = Common.MyCStr(dtpnl.Rows[0]["name"]);
                }
                else
                {
                    dr.Panel = "";
                }
                if(Common.mycboolean(GetRepoVal("lbrqhghl")))
                {
                    dr.HIGHLOWCAPS = GetRepoVal("lbhglowcap");
                }
                else
                {
                    dr.HIGHLOWCAPS="";
                }
                if (Common.mycboolean(gettunvar("SYSRICOT")))
                {
                    dr.InvestOnTop = 1;
                }
                else
                {
                    dr.InvestOnTop = 0;
                }
            objRepData.Report_Header.Rows.Add(dr);
        }
        public string GetageforReport(double patage, double patmons, double patdays)
        {
            string strage;
            strage = "";
            if (Common.MyCDbl(patage) > 0)
            {
                if (Common.MyCDbl(patage) >= 3)
                {
                    strage = patage + " Yrs";
                    return (strage);
                }
                else if (Common.MyCDbl(patage) < 3)
                {
                    strage = patage + " Yrs ";
                    if (Common.MyCDbl(patmons) > 0)
                    {
                        strage = strage + patmons + " Mons ";
                    }
                    return (strage);
                }
            }
            else if (Common.MyCDbl(patmons) > 0)
            {
                if (Common.MyCDbl(patmons) > 6)
                {
                    strage = Common.MyCDbl(patmons) + " Mons";
                    return (strage);
                }
                else
                {
                    strage = Common.MyCDbl(patmons) + " Mons ";
                    if (Common.MyCDbl(patdays) > 0)
                    {
                        strage = strage + Common.MyCDbl(patdays) + " days";
                    }
                    return (strage);
                }

            }
            else if (Common.MyCDbl(patdays) > 0)
            {
                strage = Common.MyCDbl(patdays) + " days";
                return (strage);
            }
            return (strage);
        }

        void GetReportFormat()
        {

            ReportData.Report_FormatRow dr;
            dr = (ReportData.Report_FormatRow)objRepData.Report_Format.NewRow();
            getrepovaluelbabclr = Common.MyCDbl(GetRepoVal("lbabclr"));
            Color defColor = ColorTranslator.FromWin32((Int32)getrepovaluelbabclr);
            if (Common.MyCStr(GetRepoVal("lbabbold")) == "Y")
                getrepovaluelbabbold = 1;
            else
                getrepovaluelbabbold = 0;
            if (Common.MyCStr(GetRepoVal("lbabul")) == "Y")
                getrepovaluelbabul = 1;
            else
                getrepovaluelbabul = 0;
            if (Common.MyCStr(GetRepoVal("lbabast")) == "Y")
                getrepovaluelbabast = 1;
            else
                getrepovaluelbabast = 0;
            dr.Abnormal_Bold = (Int32)getrepovaluelbabbold;
            dr.Abnormal_UL = (Int32)getrepovaluelbabul;
            dr.Abnormal_Blue = defColor.B;
            dr.Abnormal_Red = defColor.R;
            dr.Abnormal_Green = defColor.G;
            dr.TestName_Print = "Test Title";
            dr.Abnormal_Asterik = (Int32)getrepovaluelbabast;

            getrepovaluelbnoclr = Common.MyCDbl(GetRepoVal("lbnoclr"));
            Color NoColor = ColorTranslator.FromWin32((Int32)getrepovaluelbnoclr);
            dr.Normal_Blue = NoColor.B;
            dr.Normal_Red = NoColor.R;
            dr.Normal_Green = NoColor.G;
            if (Common.MyCStr(GetRepoVal("lbnobold")) == "Y")
                getrepovaluelbnobold = 1;
            else
                getrepovaluelbnobold = 0;
            dr.Normal_Bold = (Int32)getrepovaluelbnobold;
            if (Common.MyCStr(GetRepoVal("lbnoul")) == "Y")
                getrepovaluelbnoul = 1;
            else
                getrepovaluelbnoul = 0;
            dr.Normal_UL = (Int32)getrepovaluelbnoul;

            getrepovaluelbfntHName = Common.MyCStr(GetRepoVal("lbfnttname"));
            dr.Headname_Font = getrepovaluelbfntHName;
            getrepovaluelbszeHName = Common.MyCDbl(GetRepoVal("lbszetname"));
            dr.Headname_Size = (Int32)getrepovaluelbszeHName;

            getrepovaluelbclrHName = Common.MyCDbl(GetRepoVal("lbclthname"));
            Color HNameClr = ColorTranslator.FromWin32((Int32)getrepovaluelbclrHName);

            dr.Headname_Red = HNameClr.R;
            dr.Headname_Green = HNameClr.G;
            dr.Headname_Blue = HNameClr.B;

            getrepovalefntDetail = Common.MyCStr(GetRepoVal("lbfntdt"));
            dr.Detail_Font = getrepovalefntDetail;

            //Common.MyCStr(GetRepoVal("lbhidsigwe"));  == Y 

            getrepovalueszeDetail = Common.MyCDbl(GetRepoVal("lbszedt"));
            dr.Detail_Size = (Int32)getrepovalueszeDetail;

            dr.RequireNewPageAfterHead = Convert.ToInt16((Common.AISCompareString(GetRepoVal("lbrqhdsppg"), "Y") == AISCompareStringResult.AISCompareEqual) ? 1 : 0);

            objRepData.Report_Format.Rows.Add(dr);

        }

        public string GetRepoVal(string Key)
        {
            string RepoVal = "";
            if (CollRepoSetting != null)
            {
                RepoVal = Common.MyCStr(CollRepoSetting[Key.ToUpper().Trim()]);
            }
            return RepoVal;
        }

        public static void SetRepoVal()
        {
            
            string qry = "select code,val from " + RepoSettingsTablename;
            DataTable dtTemp;
            //DbConnection con = Common.GetConnection("WebLabDetails");
            dtTemp = Common.GetTableFromSession(qry, "Table",null,null);
            clsRepoVal objRepoVal;
            CollRepoSetting = new Hashtable();
            if (dtTemp.Rows.Count != 0)
            {

                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    objRepoVal = new clsRepoVal();
                    objRepoVal.Code = Common.MyCStr(dtTemp.Rows[i]["Code"]).Trim();
                    objRepoVal.CodeVal = Common.MyCStr(dtTemp.Rows[i]["Val"]);
                    if (CollRepoSetting.ContainsKey(objRepoVal.Code) == false)
                    {
                        CollRepoSetting.Add(objRepoVal.Code, objRepoVal.CodeVal);
                    }
                }
            }
        }

        string getMasterValue(string colName, string colVal, string tabName, string retColName)
        {
            string qry = "Select " + retColName + " From " + tabName + " Where " + colName + "='" + colVal + "'";
            DataTable dt;
            string retVal = "";
            dt = Common.GetTableFromSession(qry, "Table", null, null);

            if (dt.Rows.Count != 0)
            {
                retVal = Common.MyCStr(dt.Rows[0][retColName]);
            }
            retVal = retVal.Trim();
            return retVal;
        }

        string Spac(int NoOfSpace)
        {
            string str = "";
            for (int i = 0; i < NoOfSpace; i++)
            {
                str = str + " ";
            }
            return str;
        }

        public bool isProfileType(string code)
        {
            DataTable dtTemp;
            string qry = "select * from " + TestLibTablename + " where code='" + code + "'";
            dtTemp = Common.GetTableFromSession(qry, "Table", null, null);
            bool isPType = false;
            if (dtTemp.Rows.Count != 0)
            {
                if ((Common.AISCompareString(Common.MyCStr(dtTemp.Rows[0]["ISPF"]), "Y") == AISCompareStringResult.AISCompareEqual || Common.AISCompareString(Common.MyCStr(dtTemp.Rows[0]["ispkg"]), "Y") == AISCompareStringResult.AISCompareEqual || Common.AISCompareString(Common.MyCStr(dtTemp.Rows[0]["isgrp"]), "Y") == AISCompareStringResult.AISCompareEqual))
                {
                    isPType = true;
                }
            }
            return isPType;
        }

        double getFirstBookno(string labno, string BCODE)
        {
            DataTable dtTemp;
            double FirstBookNo = 0;
            string qry = "select min(booksno) as minBookSNo from " + LabTestTablename + " where labno='" + labno + "' and bcode='" + BCODE + "'";
            dtTemp = Common.GetTableFromSession(qry, "Table", null, null);
            if (dtTemp.Rows.Count != 0)
            {
                FirstBookNo = Common.MyCDbl(dtTemp.Rows[0]["minBookSNo"]);
            }
            return FirstBookNo;
        }

        public string strtok(int POS, string strval)
        {
            char[] token = new char[1];
            token[0] = '`';
            string Value = "";
            string[] str = strval.Split(token);

            int rcnt = str.Length;

            if (rcnt >= POS)
            {
                Value = str[POS - 1];
            }
            else
            {
                Value = str[rcnt - 1];
            }

            return Value; ;
        }

        public double getpatnobalance(string patno, DateTime asondate)
        {
            double payment = 0, credit = 0, refund = 0, debit = 0, billded = 0, billamt = 0;

            string qry="";

            qry = "select bills.patno, (select sum(bb.amt) from Bills bb where bb.linktopatno=bills.patno and bb.entrytype= 1 and bb.bdate<= " + Common.GetDateString(asondate) + " ) as billDeduction , ";
            qry = qry + "(select sum(amt) from payment where payment.patno=bills.patno and payment.ptype in " + GetAISAPpaymentTCNew(AISPAYTYPE.APPAYMENT) + " and payment.bdate<= " + Common.GetDateString(asondate) + "  ) as Paid , ";
            qry = qry + "(select sum(amt) from payment where payment.patno=bills.patno and payment.ptype in " + GetAISAPpaymentTCNew(AISPAYTYPE.APCREDITNOTE) + " and payment.bdate<= " + Common.GetDateString(asondate) + " ) as Credit , ";
            qry = qry + "(select sum(amt) from payment where payment.patno=bills.patno and payment.ptype in " + GetAISAPpaymentTCNew(AISPAYTYPE.APREFUND) + " and payment.bdate<= " + Common.GetDateString(asondate) + " ) as Refund , ";
            qry = qry + "(select sum(amt) from payment where payment.patno=bills.patno and payment.ptype in " + GetAISAPpaymentTCNew(AISPAYTYPE.APDEBITNOTE) + " and payment.bdate<= " + Common.GetDateString(asondate) + " ) as DebitNote , ";
            qry = qry + "bills.taxamount,bills.amt ";
            qry = qry + " from bills where (bills.entrytype is null or bills.entrytype = 0) and bills.patno='" + patno + "'";

            DataTable ds = new DataTable();
            ds = Common.GetTableFromSessionSecond(qry, "Bills", null, null);
            if (ds.Rows.Count > 0)
            {

                billded = Common.MyCDbl(ds.Rows[0]["billDeduction"]);
                payment = Common.MyCDbl(ds.Rows[0]["paid"]);
                credit = Common.MyCDbl(ds.Rows[0]["credit"]);
                refund = Common.MyCDbl(ds.Rows[0]["refund"]);
                debit = Common.MyCDbl(ds.Rows[0]["Debitnote"]);
                billamt = Common.MyCDbl(ds.Rows[0]["amt"]);
            }

            double bal = billamt - payment - credit + refund + debit + billded;
            return (bal);
        }

        public string GetAISAPpaymentTCNew(AISPAYTYPE paytype)
        {
            string str1 = "";
            if (paytype == AISPAYTYPE.APPAYMENT)
            {
                str1 = "'PA','FI','AD','FS','BD','PV','AO'";
            }
            else if (paytype == AISPAYTYPE.APCREDITNOTE)
            {
                str1 = "'CR','BK','CA','PD','CT','BO','CX'";
            }
            else if (paytype == AISPAYTYPE.APDEBITNOTE)
            {
                str1 = "'DB','DK','CB' ";
            }
            else if (paytype == AISPAYTYPE.APREFUND)
            {
                str1 = "'RF','RX' ";
            }
            str1 = "(" + str1 + ")";
            return (str1);
        }

        Hashtable getLowerUpperBound(string TESTCODE, string sex, double age, double mons, double days)
        {
            Hashtable LowerUpperBound = new Hashtable();
            DataTable dtTemp;
            highbound = "";
            string LowerBound = "";
            string qry;
            qry = "select ageC,lboundMC,lboundFC,uboundMC,uboundFC,ageA,lboundMA,lboundFA,uboundMA,uboundFA,ageO,lboundMO,lboundFO,";
            qry = qry + "uboundMO,uboundFO,lbound,ubound,ageE,lboundME,lboundFE,uboundME,uboundFE,ageF,lboundMF,lboundFF,uboundMF,uboundFF,";
            qry = qry + "agemon1,lbagemon1male,ubagemon1male,ubagemon1female,lbagemon1female,";
            qry = qry + "agemon2,lbagemon2male,ubagemon2male,ubagemon2female,lbagemon2female";
            qry = qry + " from " + TestLibTablename + " where code='" + TESTCODE + "'";

            dtTemp = Common.GetTableFromSession(qry, TestLibTablename, null, null);

            if (dtTemp.Rows.Count != 0)
            {

                if (age == 0 && mons == 0 && days == 0)
                {
                    highbound = Common.MyCStr(dtTemp.Rows[0]["UBound"]);
                    LowerBound = Common.MyCStr(dtTemp.Rows[0]["lbound"]);
                    LowerUpperBound.Add("0", highbound);
                    LowerUpperBound.Add("1", LowerBound);
                    return LowerUpperBound;
                }
                if (age > 0)
                {
                    switch (sex.Trim().ToUpper())
                    {
                        case "M":
                            if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["agec"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundmc"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundmc"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["agea"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundma"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundma"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["ageo"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundmo"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundmo"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["ageE"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundmE"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundmE"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["ageF"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundmF"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundmF"]);
                            }
                            break;
                        case "F":
                            if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["agec"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundfc"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundfc"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["agea"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundfa"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundfa"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["ageo"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundfo"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundfo"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["ageE"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundfE"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundfE"]);
                            }
                            else if (Common.MyCDbl(age) <= Common.MyCDbl(dtTemp.Rows[0]["ageF"]))
                            {
                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundfF"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundfF"]);
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (sex.Trim().ToUpper())
                    {
                        case "M":
                            if (Common.MyCDbl(mons) <= Common.MyCDbl(dtTemp.Rows[0]["agemon1"]))
                            {

                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lbagemon1Male"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["Ubagemon1Male"]);
                            }
                            else if (Common.MyCDbl(mons) <= Common.MyCDbl(dtTemp.Rows[0]["agemon2"]))
                            {

                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lbagemon2Male"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["Ubagemon2Male"]);
                            }
                            else if (Common.MyCDbl(mons) > Common.MyCDbl(dtTemp.Rows[0]["agemon2"]) && Common.MyCDbl(mons) < (Common.MyCDbl(dtTemp.Rows[0]["agec"]) * 12))
                            {

                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundmc"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundmc"]);
                            }
                            break;
                        case "F":
                            if (Common.MyCDbl(mons) <= Common.MyCDbl(dtTemp.Rows[0]["agemon1"]))
                            {

                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lbagemon1Female"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["Ubagemon1Female"]);
                            }
                            else if (Common.MyCDbl(mons) <= Common.MyCDbl(dtTemp.Rows[0]["agemon2"]))
                            {

                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lbagemon2Female"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["Ubagemon2Female"]);
                            }
                            else if (Common.MyCDbl(mons) > Common.MyCDbl(dtTemp.Rows[0]["agemon2"]) && Common.MyCDbl(mons) < (Common.MyCDbl(dtTemp.Rows[0]["agec"]) * 12))
                            {

                                LowerBound = Common.MyCStr(dtTemp.Rows[0]["lboundfc"]);
                                highbound = Common.MyCStr(dtTemp.Rows[0]["uboundfc"]);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            if (Common.MyLen(LowerBound) == 0)
            {
                if (Common.MyLen(highbound) == 0)
                {
                    LowerBound = Common.MyCStr(dtTemp.Rows[0]["lbound"]);
                    highbound = Common.MyCStr(dtTemp.Rows[0]["UBound"]);
                }
            }
            LowerUpperBound.Add("0", highbound);
            LowerUpperBound.Add("1", LowerBound);
            return LowerUpperBound;
        }

    }

    public class CultureForSubRepo
    {
        public string Name = string.Empty;
        public string Value = string.Empty;
        public string Group = string.Empty;
        public int POS=default(int);
        public int SNO = default(int);
    }
}