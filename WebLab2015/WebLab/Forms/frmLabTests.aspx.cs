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
using System.Data.Common;
using WebLabMaster;
using WebLab.Classes;
using System.Collections.Generic;
using System.Web.Services;
using WebLab.UserControls;
using SautinSoft;
using System.IO;



namespace WebLab.Forms
{
    public partial class frmLabTests : System.Web.UI.Page
    {
        // DbConnection Labconn = Common.GetConnection("WebPatientDetails");
        //  DbConnection conn = Common.GetConnection("WebLabDetails");

        public const string cultReportDate = "RD";
        public const string cultSampleDate = "SD";
        public const string cultTypeofSample = "TS";
        public const string cultBloodSource = "BS";

        public const string cultMedicineCode = "MED";
        public const string CultOrgIso = "ORG";
        public const string CultColCount = "CC";
        public const string CultSterile = "ST";
        public const string CultADVICE = "AD";
        public const string CultSpec = "SPEC";
        public const string CultIdentification = "IDEN";
        public const string CultReportStat = "RSTAT";
        public const string CultReportHEADTOP = "RPDTP";
        public const string CultMethod = "Meth";
        public string prehead = string.Empty;
        public Double tqty;

        protected string Roomno = string.Empty;
        protected string DelBy = string.Empty;
        protected bool enableroomno;
        protected string permission = string.Empty;
        protected int maxpagno;
        protected string strcode = string.Empty;
        protected string changeReason = string.Empty;
        protected string actiontype = string.Empty;
        protected string actionlab = string.Empty;
        public bool exitform;
        protected int prvtop;
        public string reffbylab = string.Empty;
        //Dim FIRSTCNT As Control
        //Dim rs As ADODB.Recordset
        protected int i;
        protected int tcoun;
        public string clinicalInformation;

        protected bool isreqddoubleunit;
        protected string finalSampleReceiptDate = string.Empty;
        public string Labno = string.Empty;

        public string DNO = string.Empty;
        public bool isapproved;
        public string approvedbyUser = string.Empty;
        public string TypedbyUser = string.Empty;
        public string STRSELECTEDGROUP = string.Empty;
        public Dictionary<int, TESTCAPS> coll;
        int tindex;
        int cindex;
        int pindex;
        int timeindex;

        int tmultiindex;
        int pgno;
        int CNTFROM;
        int CNTTO;
        int a;
        int pagecnt = 15;
        int CNTTABINDEX;
        int headno, headno2;
        int headno3, prevhdno;
        bool flg;
        string remarks = string.Empty;
        string imageno = string.Empty;
        string performedby = string.Empty;
        string refferdby = string.Empty;
        string refferbyName = string.Empty;
        string ffbylab = string.Empty;
        string RepoTimeCap = string.Empty;
        string RepoTiMeVal = string.Empty;
        string ReturnString = string.Empty;
        string manrefno = string.Empty;
        string linicalInformation = string.Empty;
        string sampledate = string.Empty;
        string sampledrawnDate = string.Empty;
        string sampleqty = string.Empty;
        string sampletype = string.Empty;
        string Bloodsource = string.Empty;
        //Dim TESTTAG As Control
        string ldntype = string.Empty;
        int textindex;
        public bool isPrintAllTests;
        string imagefilename = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                Response.Redirect("~/frmcommonlogin.aspx");
              //  username = Common.MyCStr(Session["lname"]);
            }
            Labno = Request.QueryString["labno"].ToString();
            labnohdn.Value = Labno;
            Table1.Caption = "Performing test for LabNo.- " + Labno;
            if (!IsPostBack)
            {
                Pageload();
            }
        }

        public void Pageload()
        {
            string apDataBaseName;
            clsReport obj = new clsReport();


            apDataBaseName = obj.gettunvar("APUSERNM").Trim();
            apDataBaseName = apDataBaseName + "..";
            isreqddoubleunit = false;
            exitform = false;
            pgno = 1;
            maxpagno = 1;
            headno = 0;
            headno2 = 0;
            CNTTABINDEX = -1;
            //  lblApprovedby.caption = approvedbyUser;
            // lbltypedby.caption = TypedbyUser
            flg = false;
            changeReason = "";

            int MYNO;
            //Dim ASD As TESTCAPS
            string typcnt = string.Empty;
            Double age=0;
            Double mon=0;
            Double day=0;
            string sex = string.Empty;
            TESTCAPS ASD = new TESTCAPS();

            MYNO = 1;
            DbTransaction trans = null;
            try
            {

                string query = string.Empty;
                string wherecl = string.Empty;
                // trans=Labconn.BeginTransaction();

                if (Common.MyLen(DNO.Trim()) > 0)
                {
                    wherecl = " and labtest.dno='" + DNO + "'";
                }
                else
                {
                    wherecl = "";
                }

                if (Common.MyLen(STRSELECTEDGROUP.Trim()) > 0)
                {
                    wherecl = wherecl + " and testlib.lcode='" + STRSELECTEDGROUP + "'";
                }
                if (isPrintAllTests)
                {
                    STRSELECTEDGROUP = "";
                }

                //If AllowActionUser(OptPerformTest, Addaction) = False And AllowActionUser(OptPerformTest, Modiaction) = False Then
                //    Command1.caption = "Next"
                //    Command2.caption = "Back"
                //Else
                //    Command1.caption = "Save && &Next"
                //    Command2.caption = "Save && &Back"
                //End If

                //If isapproved = True And UserAllowedtoEditAfterApproval() = False Then
                //     Command1.caption = "Next"
                //     Command2.caption = "Back"
                //End If

                //If Not ModifyPeformTest(Labno) Then
                //    Command1.caption = "Next"
                //    Command2.caption = "Back"
                //End If

                string performorder = string.Empty;
                performorder = " dept.sno , TESTLIB.DCODE, TESTLIBHEAD.sno, TESTLIB.lcode, TESTLIB.sno ";

                if (Common.mycboolean(obj.gettunvar("syprordbook")) == true)
                {
                    performorder = " labtest.booksno,dept.sno , TESTLIB.DCODE, TESTLIBHEAD.sno, TESTLIB.lcode, TESTLIB.sno ";
                }

                if (Common.mycboolean(obj.gettunvar("rqdblunit")) == true)
                {
                    isreqddoubleunit = true;
                }

                string fldlist = string.Empty;
                fldlist = "labm.pcode,labm.patno,Labtest.shortres,labtest.tcode,labtest.labno, ";
                fldlist = fldlist + "labtest.comments as labtestcomments,labtest.tval,labtest.isperf,labtest.remarks,labtest.testImpression,labtest.method as labtestmethod, ";
                fldlist = fldlist + "testlib.ISPF,TESTLIB.DEFINETAB,testlib.name as testname,testlib.formula,testlib.highval,testlib.lowval,labtest.IsAbnormal,labtest.IsCritical,labtest.KITCODE AS LABTESTKITCODE,";
                fldlist = fldlist + "testlib.lcode,testlib.DefKit,testlib.units,testlib.ismain,testlib.testtype,testlib.isWord,testlib.defval, ";
                fldlist = fldlist + "testlib.comments as testlibcomments , testlib.testtype, testlib.Option1 , testlib.Option2, ";
                fldlist = fldlist + "testlib.Option3 , testlib.OPTION4, testlib.OPTION5 , testlib.int1, testlib.Int2 , testlib.int3, testlib.int4, testlib.int5,testlib.method as testlibmethod,";
                fldlist = fldlist + "testlib.int6,testlib.int7,testlib.int8,testlib.int9,testlib.int10 , testlib.condition, testlib.spec1 , testlib.spec2, testlib.spec3, testlib.spec4,testlib.spec5,testlib.spec6,";
                fldlist = fldlist + "testlib.spec7,testlib.spec8,testlib.spec9 ,testlib.spec10, testlib.spec11 , testlib.spec12, testlib.spec13 , testlib.spec14, testlib.spec15,testlib.spec16,Labtest.IsBold, ";
                if (isreqddoubleunit)
                {
                    fldlist = fldlist + "testlib.isAddunit,testlib.addunits,testlib.addunitformula,";
                }
                fldlist = fldlist + "testlib.spec17,testlib.spec18 , testlib.spec19, testlib.spec20,testlib.reqSignatory,testlib.Signatory,";
                fldlist = fldlist + "labtest.CreateUser,labtest.ModUser ,labtest.hideinDuplicate, labtest.ModDate,labtest.expecteddate,testlib.rptime,labtest.tdate,testlib.grp,labtest.bcode ,labtest.aptestcode,";
                fldlist = fldlist + " (SELECT MAX(LAB_KITDETAIL.KCODE)   FROM LAB_KITDETAIL WHERE LAB_KITDETAIL.TCODE=TESTLIB.CODE) KITCODE , labtest.isMachPerf,dept.isMachPerf MachPerf,dept.ispresample isdeptreqpresample";
                // ''''''''''check for testwise
                if (Common.mycboolean(obj.gettunvar("smpldwntst")) == true && Common.mycboolean(obj.gettunvar("reqtstwsmp")) == true)
                {
                    fldlist = fldlist + " , (SELECT MAX(lab_Sample_Coll_tstws.collecteddate)   FROM lab_Sample_Coll_tstws WHERE lab_Sample_Coll_tstws.labno=LABTEST.LABNO and lab_Sample_Coll_tstws.testcode=labtest.tcode) samplecolldate ";
                }
                else
                {
                    fldlist = fldlist + " , ''  samplecolldate ";
                }

                query = "select  " + fldlist + " from labtest left outer join TESTLIB on labtest.tcode=testlib.code ";
                query = query + " left outer join  TESTLIBHEAD on testlib.lcode=testlibhead.code  ";
                query = query + " left outer join DEPT on TESTLIB.DCODE=dept.code  ";
                query = query + " left outer join labm on labtest.labno=labm.labno ";

                query = query + " where  LABTEST.labno='" + Labno + "' " + wherecl;
                if (Common.mycboolean(obj.gettunvar("updhdottst")) == true)
                {
                    query = query + "  and (labtest.isoutsourced is null or labtest.isoutsourced=0 )";
                }

                query = query + " ORDER BY LABM.LABNO," + performorder;
                DataTable dt1 = Common.GetTableFromSession(query, "Labdetails", null, null);
                coll = new Dictionary<int, TESTCAPS>();
                if (dt1.Rows.Count > 0)
                {
                    DataTable dt2 = Common.GetTableFromSession("SELECT AGE,SEX,mons,days FROM " + apDataBaseName + "PATIENT WHERE CODE='" + Common.MyCStr(dt1.Rows[0]["pcode"]) + "'", "Patient", null, null);
                    if (dt2.Rows.Count > 0)
                    {
                        age = Common.MyCDbl(dt2.Rows[0]["age"]);
                        sex = Common.MyCStr(dt2.Rows[0]["sex"]);
                        mon = Common.MyCDbl(dt2.Rows[0]["mons"]);
                        day = Common.MyCDbl(dt2.Rows[0]["days"]);
                    }
                    else
                    {
                        //labconnstate=1
                        DataTable dt3 = Common.GetTableFromSession("SELECT AGE,SEX,mons,days FROM " + apDataBaseName + "PATIENT WHERE CODE='" + Common.MyCStr(dt1.Rows[0]["pcode"]) + "'", "patient", null, null);
                        if (dt3.Rows.Count > 0)
                        {
                            age = Common.MyCDbl(dt3.Rows[0]["age"]);
                            sex = Common.MyCStr(dt3.Rows[0]["sex"]);
                            mon = Common.MyCDbl(dt3.Rows[0]["mons"]);
                            day = Common.MyCDbl(dt3.Rows[0]["days"]);
                        }
                    }


                    // ''''''''''''''''''''''''' use DOB in case of new born
                    if (Common.mycboolean(obj.gettunvar("nwbrdob")))
                    {
                        DataTable dt4 = Common.GetTableFromSession("SELECT  ipdtype,DOB from " + apDataBaseName + "Bills   WHERE Patno='" + Common.MyCStr(dt1.Rows[0]["Patno"]) + "'", "billstab", null, null);
                        if (dt4.Rows.Count > 0)
                        {
                            if (Common.MyCStr(dt4.Rows[0]["IpdType"]).ToUpper() == "NEW BORN" && Common.MyCDate(dt4.Rows[0]["dob"]).Year > 2001)
                            {
                                DateTime patdob;
                                patdob = Common.MyCDate(dt4.Rows[0]["dob"]);
                                DateTime currdate;
                                currdate = Common.MyCDate(dt4.Rows[0]["tdate"]);
                                int daysdiff;
                                TimeSpan ts = currdate - patdob;
                                daysdiff = ts.Days;
                                if (daysdiff > 30)
                                {
                                    mon = daysdiff / 30;
                                    daysdiff = daysdiff % 30;
                                }
                                else
                                {
                                    age = 0;
                                    mon = 0;

                                }
                                day = daysdiff;
                            }
                        }

                    }



                    // Dim RSOPT As ADODB.Recordset

                    string HBOUND = string.Empty;

                    bool isanyvalueperformed;
                    isanyvalueperformed = false;
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt1.Rows)
                        {
                            ASD = new TESTCAPS();
                            if (Common.MyCStr(dr["ispf"]).Trim().ToUpper() == "N" && Common.MyCDbl(dr["hideinDuplicate"]) == 0)
                            {

                                ASD.SHORTRESULT = Common.MyCStr(dr["SHORTRES"]);
                                ASD.fldName = Common.MyCStr(dr["tcode"]);
                                strcode = Common.MyCStr(dr["tcode"]);
                                ASD.caption = Common.MyCStr(dr["TestName"]);
                                ASD.FORMULA = Common.MyCStr(dr["FORMULA"]);
                                ASD.HIGHVAL = Common.MyCStr(dr["HIGHVAL"]);
                                ASD.LOWVAL = Common.MyCStr(dr["LOWVAL"]);
                                ASD.Labno = Common.MyCStr(dr["Labno"]);
                                ASD.aptestcode = Common.MyCStr(dr["aptestcode"]);
                                if (Common.MyLen(Common.MyCStr(dr["LABTESTKITCODE"]).Trim()) > 0)
                                {
                                    ASD.KITCODE = Common.MyCStr(dr["LABTESTKITCODE"]);
                                }
                                else if (Common.MyLen(Common.MyCStr(dr["DefKit"]).Trim()) > 0)
                                {
                                    ASD.KITCODE = Common.MyCStr(dr["DefKit"]);
                                }
                                else
                                {
                                    ASD.KITCODE = Common.MyCStr(dr["KITCODE"]);
                                }
                                if (isreqddoubleunit)
                                {
                                    if (Common.MyCDbl(dr["isAddunit"]) == 1)
                                    {
                                        ASD.isdoubleunit = true;
                                        ASD.doubleunitformula = Common.MyCStr(dr["addunitformula"]);

                                    }
                                }
                                if (Common.MyCDbl(dr["isdeptreqpresample"]) == 1)
                                {
                                    ASD.issampledrawn = true;
                                }
                                else
                                {
                                    ASD.issampledrawn = false;
                                }

                                if (Common.MyCDate(dr["samplecolldate"]).Year > 1900)
                                {
                                    ASD.sampledate = Common.MyCDate(dr["samplecolldate"]);
                                }
                                else
                                {
                                    ASD.sampledate = new DateTime(1900, 1, 1);
                                }

                                ASD.pcode = Common.MyCStr(dr["pcode"]);
                                DataTable dtttt = new DataTable();
                                ASD.LBND = getLowerBound(ASD.fldName, sex, age, mon, day,ref HBOUND, dtttt, ASD.KitCode);
                                ASD.lcode = Common.MyCStr(dr["lcode"]);
                                ASD.UBND = HBOUND;
                                ASD.unit =CommonFunctions.GetUnits(Common.MyCStr(ASD.KITCODE), strcode, Common.MyCStr(dr["units"]));
                                ASD.ismain = Common.MyCStr(dr["ismain"]);
                                ASD.comments = Common.MyCStr(dr["labtestcomments"]);
                                ASD.remarkswithTEST = Common.MyCStr(dr["testImpression"]);
                                ASD.typtest = Common.MyCStr(dr["testtype"]);
                                ASD.ExpectedReportingdate = Common.MyCDate(dr["expecteddate"]);
                                ASD.bcode = Common.MyCStr(dr["bcode"]);
                                ASD.isMachPerf = Common.MycInt(dr["isMachPerf"]);
                                if (Common.MyCDbl(dr["machPerf"]) == 1)
                                {
                                    ASD.machPerf = true;
                                }
                                else
                                {
                                    ASD.machPerf = false;
                                }

                                ASD.reportDays = Common.MyCDbl(dr["rptime"]);
                                ASD.bookingdate = Common.MyCDate(dr["tdate"]);
                                ASD.Groupunder = Common.MyCStr(dr["grp"]);

                                ASD.CreateUser = Common.MyCStr(dr["CreateUser"]);
                                ASD.moduser = Common.MyCStr(dr["moduser"]);
                                ASD.ModDate = Common.MyCDate(dr["ModDate"]);
                                ASD.labtestmethod = Common.MyCStr(dr["labtestmethod"]);
                                ASD.testlibmethod = Common.MyCStr(dr["testlibmethod"]);
                                ASD.IsAbNormalCase = Common.MycInt(dr["Isabnormal"]);
                                if (Common.MyCDbl(dr["IsCritical"]) == 1)
                                {
                                    ASD.IsCritical = true;
                                }
                                else
                                {
                                    ASD.IsCritical = false;
                                }
                                if (Common.MyCDbl(dr["IsBold"]) == 1)
                                {
                                    ASD.IsBold = true;
                                }
                                else
                                {
                                    ASD.IsBold = false;
                                }
                                if (Common.MyCDbl(dr["isword"]) > 0)
                                {
                                    ASD.hasWord = true;
                                    //   ASD.wordfilename = loadWordFileFor(ASD.fldName, Labno, ASD);
                                }
                                if (Common.MyLen(Common.MyCStr(dr["tval"]).Trim()) > 0)
                                {
                                    isanyvalueperformed = true;
                                    ASD.tval = Common.MyCStr(dr["tval"]);
                                    ASD.CURRVAL = ASD.tval;
                                    ASD.prvValueforTest = ASD.tval;
                                }
                                else
                                {
                                    if (Common.mycboolean(Common.MyCStr(dr["tval"])) == true)
                                    {
                                        ASD.tval = Common.MyCStr(dr["DEFVAL"]);
                                        ASD.comments = Common.MyCStr(dr["testlibcomments"]);
                                    }
                                }
                                bool islong;
                                islong = false;
                                if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "PARAGRAPH" || Common.MyCStr(dr["testtype"]) == "X")
                                {
                                    typcnt = "P";
                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "SUB HEADING")
                                {
                                    typcnt = "S";
                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "TABLE")
                                {
                                    typcnt = "Q";
                                    ASD.Deftable = Common.MyCStr(dr["DEFINETAB"]);
                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "TABULAR")
                                {
                                    typcnt = "U";


                                    DataTable dt5 = Common.GetTableFromSession("Select * from labtest_tabdata where labno='" + Labno + "' and tcode='" + Common.MyCStr(dr["tcode"]) + "' order by sno ", "tecmp", null, null);
                                    foreach (DataRow dr1 in dt5.Rows)
                                    {
                                        ASD.colltabulardata = new List<clsTableRowData>();
                                        clsTableRowData xtabdata = new clsTableRowData();
                                        xtabdata.col1 = Common.MyCStr(dr1["col1"]);
                                        xtabdata.col2 = Common.MyCStr(dr1["col2"]);
                                        xtabdata.col3 = Common.MyCStr(dr1["col3"]);
                                        xtabdata.col4 = Common.MyCStr(dr1["col4"]);
                                        xtabdata.col5 = Common.MyCStr(dr1["col5"]);
                                        xtabdata.col5 = Common.MyCStr(dr1["COL6"]);
                                        xtabdata.col7 = Common.MyCStr(dr1["col7"]);
                                        xtabdata.col8 = Common.MyCStr(dr1["col8"]);
                                        xtabdata.col9 = Common.MyCStr(dr1["col9"]);
                                        xtabdata.col10 = Common.MyCStr(dr1["col10"]);
                                        ASD.colltabulardata.Add(xtabdata);

                                    }

                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "DATE")
                                {
                                    typcnt = "D";
                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "TIME")
                                {
                                    typcnt = "M";
                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "CULTURE")
                                {
                                    typcnt = "R";
                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "LONGRESULT")
                                {
                                    typcnt = "B";
                                    islong = true;
                                }
                                else if (Common.MyCStr(dr["testtype"]).Trim().ToUpper() == "IMAGE")
                                {
                                    typcnt = "I";
                                    //////////// ASD.Imagestring = loadImageFileFor(ASD.fldName, Labno, ASD)  ;                              
                                }
                                else if (Common.MyCStr(Common.MyCStr(dr["Option1"]).Trim()) == "" && Common.MyCStr(Common.MyCStr(dr["Option2"]).Trim()) == "" && Common.MyCStr(Common.MyCStr(dr["Option3"]).Trim()) == "" && Common.MyCStr(Common.MyCStr(dr["Option4"]).Trim()) == "" && Common.MyCStr(Common.MyCStr(dr["Option5"]).Trim()) == "")
                                {
                                    typcnt = "T";
                                }
                                else
                                {
                                    typcnt = "C";
                                }
                                DataTable dt6 = Common.GetTableFromSession("SELECT * FROM TESTLINT WHERE CODE='" + Common.MyCStr(dr["tcode"]) + "' ORDER BY SNO ", "Temp2", null, null);
                                if (dt6.Rows.Count > 0)
                                {
                                    if (typcnt != "B")
                                    {
                                        typcnt = "C";
                                    }
                                    else
                                    {
                                        if (Common.mycboolean(Common.MyCStr(dr["tval"])) == true)
                                        {
                                            ASD.tval = Common.MyCStr(dr["OPTIONID"]);
                                            ASD.comments = Common.MyCStr(dr["testlibcomments"]);
                                        }

                                    }
                                }

                                ASD.typ = typcnt;
                                ASD.islong = islong;
                                ASD.Option1 = Common.MyCStr(dr["Option1"]);
                                ASD.Option2 = Common.MyCStr(dr["Option2"]);
                                ASD.Option3 = Common.MyCStr(dr["Option3"]);
                                ASD.Option4 = Common.MyCStr(dr["Option4"]);
                                ASD.Option5 = Common.MyCStr(dr["Option5"]);
                                ASD.int1 = Common.MyCStr(dr["int1"]);
                                ASD.int2 = Common.MyCStr(dr["int2"]);
                                ASD.int3 = Common.MyCStr(dr["int3"]);
                                ASD.int4 = Common.MyCStr(dr["int4"]);
                                ASD.int5 = Common.MyCStr(dr["int5"]);
                                ASD.int6 = Common.MyCStr(dr["int6"]);
                                ASD.int7 = Common.MyCStr(dr["int7"]);
                                ASD.int8 = Common.MyCStr(dr["int8"]);
                                ASD.int9 = Common.MyCStr(dr["int9"]);
                                ASD.int10 = Common.MyCStr(dr["int10"]);
                                ASD.DEFVAL = Common.MyCStr(dr["DEFVAL"]);
                                ASD.ISPERF = Common.MyCStr(dr["ISPERF"]);
                                ASD.condition = Common.MyCStr(dr["condition"]);
                                ASD.no = MYNO;

                                if (Common.MyLen(Common.MyCStr(dr["KITCODE"]).Trim()) > 0)
                                {
                                    ASD.ISKIT = 1;
                                }
                                else
                                {
                                    ASD.ISKIT = 0;
                                }
                                if (typcnt == "R")
                                {
                                    var collmed = new List<string>();

                                    var xxcol = new ArrayList();
                                    int rowx;
                                    for (rowx = 1; rowx >= 20; rowx++)
                                    {
                                        if (Common.MyLen(Common.MyCStr(dt1.Rows[rowx]["spec"]).Trim()) > 0)//Fields("spec" & rowx).value
                                        {
                                            xxcol.Add(dt1.Rows[rowx]["spec"]);
                                        }
                                    }
                                    MedClass xxcultmed = new MedClass();
                                    ASD.collspec = xxcol;
                                    DataTable dt7 = Common.GetTableFromSession("select * from culturetest left outer join antibiotics on culturetest.abcode=antibiotics.code where tcode='" + Common.MyCStr(dr["tcode"]) + "'", "temp3", null, null);
                                    foreach (DataRow dr2 in dt7.Rows)
                                    {
                                        // MedClass xxcultmed = new MedClass();

                                        xxcultmed.Code = Common.MyCStr(dr2["Code"]);
                                        xxcultmed.Name = Common.MyCStr(dr2["Name"]);
                                        xxcultmed.grp = Common.MyCStr(dr2["grpcode"]).Trim();
                                        //bool x=collmed.Find(xxcultmed.Code);
                                        ;
                                        if (collmed.Contains(xxcultmed.Code))
                                        {
                                            collmed.Add(xxcultmed.Code);
                                        }

                                    }
                                    CultClass xxcult = new CultClass();

                                    string qry = string.Empty;
                                    qry = "Select DrawnTime,rptime from lab_Sample_Drawn_tstws left outer join testlib on lab_Sample_Drawn_tstws.testcode=testlib.code where lab_Sample_Drawn_tstws.labno='" + Labno + "' and lab_Sample_Drawn_tstws.testcode='" + Common.MyCStr(dr["tcode"]) + "'";
                                    DataTable dt8 = Common.GetTableFromSession(qry, "sampldrawntab", null, null);
                                    DateTime SdrawnDate;
                                    if (dt8.Rows.Count > 0)
                                    {
                                        SdrawnDate = Common.MyCDate(dt8.Rows[0]["DrawnTime"]);
                                        xxcult.SDate = SdrawnDate;
                                        xxcult.RDate = SdrawnDate.AddDays(Common.MyCDbl(dt8.Rows[0]["rptime"]));
                                    }

                                    DataTable dt9 = Common.GetTableFromSession("select * from labtestcult where labno='" + Labno + "' and tcode='" + Common.MyCStr(dr["tcode"]) + "'", "Temp", null, null);
                                    foreach (DataRow dr3 in dt9.Rows)
                                    {
                                        ASD.cultureResult = new List<CultClass>();
                                        switch (Common.MyCStr(dr3["cultfld"]).Trim().ToUpper())
                                        {
                                            case cultReportDate:
                                                xxcult.RDate = Common.MyCDate(dr3["CULTVAL"]);
                                                break;
                                            case cultSampleDate:
                                                xxcult.SDate = Common.MyCDate(dr3["CULTVAL"]);
                                                break;

                                            case CultOrgIso:
                                                xxcult.oiso = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case CultColCount:
                                                xxcult.colcount = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case CultSterile:
                                                xxcult.sterile = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case CultADVICE:
                                                xxcult.comments = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case cultMedicineCode:
                                                if (collmed.Contains(Common.MyCStr(dr3["MEDCODE"]).Trim().ToUpper()))
                                                {

                                                    xxcultmed.Code = Common.MyCStr(dr3["MEDCODE"]).Trim().ToUpper();
                                                    xxcultmed.tval = Common.MyCStr(dr3["CULTVAL"]);
                                                    xxcultmed.miccount = Common.MyCStr(dr3["miccount"]);
                                                }
                                                break;
                                            case CultSpec:
                                                xxcult.spec = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case cultBloodSource:
                                                xxcult.Bloodsource = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case cultTypeofSample:
                                                xxcult.sampletype = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case CultIdentification:
                                                xxcult.Identification = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                            case CultReportStat:
                                                xxcult.ReortRatio = Common.MyCStr(dr3["CULTVAL"]);
                                                break;
                                            case CultReportHEADTOP:
                                                xxcult.Headerontop = Common.MyCStr(dr3["CULTVAL"]);
                                                break;
                                            case CultMethod:
                                                xxcult.Method = Common.MyCStr(dr3["CULTVAL"]);
                                                break;

                                        }


                                    }


                                    ASD.cultureResult.Add(xxcult);
                                    ASD.cultmedcoll = collmed;

                                }
                                coll.Add(MYNO, ASD);
                                //coll.Add ASD, "" + MYNO;
                                MYNO = MYNO + 1;

                            }
                        }
                    }


                    if (isanyvalueperformed == false && Common.mycboolean(obj.gettunvar("savedefvlod")) == true)
                    {
                        for (int kkk = 1; kkk >= coll.Count; kkk++)
                        {
                            ASD = coll[kkk];
                            if (Common.MyLen(ASD.tval.Trim()) > 0)
                            {
                                ASD.ISPERF = "Y";
                            }
                        }
                    }


                    DataTable dt10 = Common.GetTableFromSession("select * from labm where labno='" + Labno + "'", "labm", null, null);
                    if (dt10.Rows.Count > 0)
                    {

                        refferdby = Common.MyCStr(dt10.Rows[0]["refcon"]);
                        refferbyName = Common.MyCStr(dt10.Rows[0]["consname"]);
                        reffbylab = Common.MyCStr(dt10.Rows[0]["refconslab"]);
                        performedby = Common.MyCStr(dt10.Rows[0]["performby"]);
                        imageno = Common.MyCStr(dt10.Rows[0]["imageno"]);
                        RepoTimeCap = Common.MyCStr(dt10.Rows[0]["RepoTimeCap"]);
                        RepoTiMeVal = Common.MyCStr(dt10.Rows[0]["RepoTiMeVal"]);
                        sampledate = Common.MyCStr(dt10.Rows[0]["sampledate"]);
                        sampletype = Common.MyCStr(dt10.Rows[0]["sampletype"]);
                        Bloodsource = Common.MyCStr(dt10.Rows[0]["Bloodsource"]);
                        manrefno = Common.MyCStr(dt10.Rows[0]["manualrefno"]).Trim();
                        clinicalInformation = Common.MyCStr(dt10.Rows[0]["clinicinform"]).Trim();
                        Roomno = Common.MyCStr(dt10.Rows[0]["Roomno"]).Trim();
                        DelBy = Common.MyCStr(dt10.Rows[0]["DelBy"]);
                        sampledrawnDate = Common.MyCStr(dt10.Rows[0]["sampledrawnDate"]);

                        if (Common.MyCDate(Common.MyCDate(dt10.Rows[0]["finalSampleDate"])) < DateTime.Parse("1/1/2002"))
                        {
                            finalSampleReceiptDate = "";
                        }
                        else
                        {
                            finalSampleReceiptDate = Common.MyCStr(dt10.Rows[0]["finalSampleDate"]);
                        }



                        sampleqty = Common.MyCStr(dt10.Rows[0]["sampleqty"]);

                        if (Common.MyLen(Roomno.Trim()) > 0)
                        {
                            enableroomno = false;
                        }
                        else
                        {
                            Roomno = Common.MyCStr(dt10.Rows[0]["manualroomno"]);
                            enableroomno = true;
                        }
                        remarks = Common.MyCStr(dt10.Rows[0]["remarks"]);
                    }

                    prvtop = 480;
                    tindex = 0;
                    cindex = 0;
                    pindex = 0;
                    tmultiindex = 0;
                    tcoun = 1;
                    timeindex = 0;
                    loadData();

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

        }

        public void loadData()
        {
            int cntno, prvhdno;
            int XXF, XXT;


            headno2 = 0;
            cntno = 0;
            XXF = CNTFROM + 1;
            XXT = XXF + pagecnt;
            CNTFROM = XXF;
            int ii;
            ii = 0;
            actionlab = "PT";
            actiontype = "U";

            // permission = getUserPermission(actionlab, actiontype);
            int cntNoofExtralong;
            cntNoofExtralong = 0;

            do
            {
                ii = ii + 1;
                prvhdno = headno;
                cntno = cntno + 1;

                if (ii > (pagecnt - cntNoofExtralong))
                {
                    break;
                }


                if ((CNTTO + 1) > coll.Count)
                {
                    break;
                }

                if (loaddatai(CNTTO + 1))
                {
                    cntNoofExtralong = cntNoofExtralong + 1;
                }
                CNTTO = CNTTO + 1;

                if (prvhdno != headno)
                {
                    ii = ii + 1;
                }
            } while (true);

        }

        public bool loaddatai(int kk)
        {
            clsReport obj = new clsReport();
            bool retval = false;
            bool en = true;

            try
            {
                if (Common.mycboolean(obj.gettunvar("syedittest")))
                {

                }
                else
                {
                    //if(ChkNotEditagain ==true)
                    //{
                    //    en=false;
                    //}
                }


                CNTTABINDEX = CNTTABINDEX + 1;
                TESTCAPS ldcnt = new TESTCAPS();
                ldcnt = coll[kk];
                ldcnt.pgno = pgno;

                if (kk == 1)
                {
                    //load lblheadname(kk)

                    //With lblheadname(kk)
                    //    .VISIBLE = True
                    //    .caption = Trim(getMasterName(ldcnt.lcode, "TESTLIBHEAD"))
                    //    .top = prvtop

                    //    prvtop = prvtop + 400
                    //    headno = headno + 1
                    //    headno2 = headno2 + 1
                    //End With
                }
                else
                {
                    //With lblheadname(kk)
                    //    On Error GoTo errrh
                    //    load lblheadname(kk)

                    //    .caption = Trim(getMasterName(ldcnt.lcode, "TESTLIBHEAD"))

                    //    If UCase(Trim(lblheadname(kk - 1).caption)) <> UCase(Trim(getMasterName(ldcnt.lcode, "TESTLIBHEAD"))) Then
                    //        .VISIBLE = True
                    //        .top = prvtop

                    //        prvtop = prvtop + 400
                    //        headno = headno + 1
                    //        headno2 = headno2 + 1
                    //    End If

                    //End With

                }


                ldcnt.CURRVAL = ldcnt.tval;
                //         load tname(kk)
                // load lval(kk)
                //''''''''''' surender 3 / 9
                // load Image1(kk)
                // load uval(kk)
                // load lunit(kk)




                // Set ldcnt.LABELCONTROL = tname(kk)
                // Set ldcnt.HIGHCONTROL = uval(kk)
                // Set ldcnt.LOWCONTROL = lval(kk)
                // Set ldcnt.unitcontrol = lunit(kk)
                // ''''''SURENDER 3 / 9
                //  With Image1(kk)
                //         .top = prvtop
                //         If ldcnt.ISKIT = 1 Then
                //             .VISIBLE = True
                //         Else
                //             .VISIBLE = False
                //         End If
                //         .width = 345
                //         .height = 345
                //         .tag = kk
                //         .tooltiptext = ldcnt.caption
                //  End With
                //  With tname(kk)
                //     .top = prvtop
                //     .VISIBLE = True
                //     .tag = kk
                //     .caption = ldcnt.caption
                //     .tooltiptext = ldcnt.caption

                //     If myCdbl(ldcnt.IsAbNormalCase) = 1 Then
                //         .caption = "**" & Trim(.tooltiptext)
                //     Else
                //         .caption = Trim(.tooltiptext)
                //     End If

                //     If ldcnt.ismain = "N" Then
                //         .left = .left + 100
                //         .ForeColor = vbBlue
                //     End If

                //     If ldcnt.typ = "S" Then
                //         '''making loaded subheadings as saved by default
                //         ldcnt.ISPERF = "Y"
                //         .ForeColor = vbBlue
                //         .Font.UNDERLINE = True
                //     End If

                // End With

                // With lunit(kk)
                //     .top = prvtop
                //     .VISIBLE = True
                //     .tag = kk
                //     .caption = ldcnt.unit
                // End With

                // With lval(kk)
                //     .top = prvtop
                //     .VISIBLE = True
                //     .tag = kk
                //     .caption = ldcnt.LBND
                // End With

                // With uval(kk)
                //     .top = prvtop
                //     .VISIBLE = True
                //     .tag = kk
                //     .caption = ldcnt.UBND
                // End With
                string currhead = string.Empty;
                if (Common.MyLen(ldcnt.lcode) > 0)
                {
                    DataTable dt = Common.GetTableFromSession("Select name from TESTLIBHEAD where code='" + ldcnt.lcode + "'", "temp", null, null);
                    if (dt.Rows.Count > 0)
                    {
                        currhead = Common.MyCStr(dt.Rows[0]["name"]);
                    }

                }

                ldntype = ldcnt.typ;
                if (Common.AISCompareString(prehead.Trim().ToUpper(), currhead.Trim().ToUpper()) != AISCompareStringResult.AISCompareEqual)
                {
                    TableRow tr1 = new TableRow();

                    TableCell labeltext1 = new TableCell();
                    Label lblhead = new Label();
                    lblhead.CssClass = "badge";
                    lblhead.Text = currhead.Trim();
                    prehead = currhead;
                    labeltext1.ColumnSpan = 5;
                    labeltext1.Controls.Add(lblhead);
                    tr1.Cells.Add(labeltext1);
                    Table1.Rows.Add(tr1);
                    tindex = tindex + 1;
                }

                TableRow tr = new TableRow();
                TableCell labeltext = new TableCell();
                labeltext.Text = ldcnt.caption.Trim();
                tr.Cells.Add(labeltext);
                TableCell cntcell = new TableCell();
                HiddenField hdfld = new HiddenField();
                hdfld.ID = "hdfld_" + tindex;
                hdfld.Value = ldcnt.fldName;
                cntcell.Controls.Add(hdfld);
                tr.Cells.Add(cntcell);


               
                switch (ldcnt.typ)
                {

                    case "C":
                        DropDownList drp = new DropDownList();
                        drp.Items.Add(ldcnt.CURRVAL);
                        cntcell.Controls.Add(drp);
                        tr.Cells.Add(cntcell);
                        tindex = tindex + 1;

                        if (Common.mycboolean(obj.gettunvar("updtmchtst")) == true && ldcnt.isMachPerf == 1)
                        {
                            if (Common.MyCDbl(ldcnt.isMachPerf) == 1)
                            {
                                drp.Enabled = true;
                            }
                            else
                            {
                                drp.Enabled = false;
                            }
                        }

                        //    ''''''''code for checking sample drawn condition
                        else if (Common.mycboolean(obj.gettunvar("smpldwntst")) == true && Common.mycboolean(obj.gettunvar("reqtstwsmp")) == true && ldcnt.issampledrawn == true)
                        {
                            if (Common.MyCDate(ldcnt.sampledate) > new DateTime(1990, 1, 1))
                            {
                                drp.Enabled = true;
                            }
                            else
                            {
                                drp.Enabled = false;
                            }
                        }
                        else
                        {
                            drp.Enabled = true;
                        }
                        if (Common.MyLen(ldcnt.Option1) > 0)
                        {
                            drp.Items.Add(ldcnt.Option1);
                        }

                        if (Common.MyLen(ldcnt.Option2.Trim()) > 0)
                        {
                            drp.Items.Add(ldcnt.Option2);
                        }

                        if (Common.MyLen(ldcnt.Option3) > 0)
                        {
                            drp.Items.Add(ldcnt.Option3);
                        }

                        if (Common.MyLen(ldcnt.Option4) > 0)
                        {
                            drp.Items.Add(ldcnt.Option4);
                        }

                        if (Common.MyLen(ldcnt.Option5) > 0)
                        {
                            drp.Items.Add(ldcnt.Option5);
                        }

                        // DataTable RSOPT = Common.GetTableFromSession("SELECT * FROM TESTLINT WHERE CODE='" + replapwithap(ldcnt.FLDNAME) + "'", "Temp5");
                        DataTable RSOPT = Common.GetTableFromSession("SELECT * FROM TESTLINT WHERE CODE='" + ldcnt.fldName + "'", "Temp5",null,null);
                        foreach (DataRow dr in RSOPT.Rows)
                        {
                            drp.Items.Add(Common.MyCStr(dr["OPTIONID"]));
                        }
                        if (permission.Trim().ToUpper() == "N" && Common.MyLen(ldcnt.tval) > 0)
                        {
                            drp.Enabled = false;
                        }

                        if (Common.MyCDbl(ldcnt.CURRVAL) < Common.MyCDbl(ldcnt.LBND))
                        {
                            //.ForeColor = vbRed
                            drp.ForeColor = System.Drawing.Color.Red;
                        }

                        if (Common.MyCDbl(ldcnt.CURRVAL) > Common.MyCDbl(ldcnt.UBND))
                        {
                            drp.ForeColor = System.Drawing.Color.Red;
                        }

                        if (Common.MyCDbl(ldcnt.LBND) == 0 && Common.MyCDbl(ldcnt.UBND) == 0)
                        {
                            drp.ForeColor = System.Drawing.Color.Black;
                        }

                         

                                    //If FIRSTCNT Is Nothing Then
                                    //    Set FIRSTCNT = Combo1(pindex)
                                    //End If
                        break;
                    /// ''''''''''''''''''''for multiline Textbox
                    case "B":
                        //tmultiindex = tmultiindex + 1
                        //load TxtMulti(tmultiindex)
                        //Set ldcnt.CURRENTCONTROL = TxtMulti(tmultiindex)

                        //With TxtMulti(tmultiindex)
                        //    .top = prvtop
                        //    .VISIBLE = True
                        //    .tag = kk
                        //    .tabIndex = CNTTABINDEX
                        //    .enabled = en
                        //    .Text = ldcnt.tval

                        //   '''''  .enabled = False
                        //    If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                        //      .enabled = False
                        //    End If
                        //    ''''''suren 10/11
                        //     If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                        //        If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //            .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    ''''''''code for checking sample drawn condition
                        //    ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //         If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //             .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    Else
                        //        .enabled = True
                        //    End If

                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = TxtMulti(tmultiindex)
                        //End If
                        break;
                    case "P":
                        HyperLink btnedit = new HyperLink();
                        btnedit.CssClass = "hpyebutton";
                        btnedit.Text = "EDIT";
                        btnedit.Attributes.Add("onclick", "showCommentModal('" + "uchtml_" + tindex + "_htmlboxmodal" + "','uchtml_" + tindex + "_close_" + tindex + "',''," + timeindex + ",'uchtml_" + tindex + "_ok_" + tindex + "')");
                        //btnedit.OnClientClick = "";
                        cntcell.Controls.Add(btnedit);
                        tr.Cells.Add(cntcell);


                        Image img = new Image();
                        img.ID = "ok_" + tindex;
                        img.Attributes.Add("src", "../Images/okbutton.png");
                        img.Attributes.Add("onclick", "closeModal('" + "uchtml_" + tindex + "_htmlboxmodal" + "',this.id,'uchtml_" + tindex + "_close_" + tindex + "')");
                        img.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:55%;top:0;background-color:white;border:4px white solid;border-radius:50%;");

                        Image img4 = new Image();
                        img4.ID = "close_" + tindex;
                        img4.Attributes.Add("src", "../Images/closebtn.png");
                        img4.Attributes.Add("onclick", "closeModal('" + "uchtml_" + tindex + "_htmlboxmodal" + "',this.id,'uchtml_" + tindex + "_ok_" + tindex + "')");
                        img4.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:45%;top:0;background-color:white;border:4px white solid;border-radius:50%;");

                        ucHtmlBox uc1 = (ucHtmlBox)Page.LoadControl("../UserControls/ucHtmlBox.ascx");
                        if(ldcnt.CURRVAL!=null)
                        { 
                        uc1.controlval =CommonFunctions.convertFromRtfToHtml(ldcnt.CURRVAL);
                            
                        }
                        uc1.testcode = ldcnt.fldName;
                        uc1.Controls.Add(img);
                        uc1.Controls.Add(img4);
                        uc1.ID = "uchtml_" + tindex;


                        //cntcell.Controls.Add(uc1);
                        uspanel.Controls.Add(uc1);
                        tindex = tindex + 1;


                        //cindex = cindex + 1
                        //load edt(cindex)
                        //Set ldcnt.CURRENTCONTROL = edt(cindex)

                        //With edt(cindex)
                        //    .top = prvtop
                        //    .VISIBLE = True
                        //    .tabIndex = CNTTABINDEX
                        //    .tag = kk
                        //    .enabled = en
                        //    ''''''suren 10/11
                        //   ''''''  .enabled = False
                        //    If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                        //      .enabled = False
                        //    End If
                        //    ''''''suren 10/11
                        //    If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                        //        If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //            .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //     ''''''''code for checking sample drawn condition
                        //    ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //         If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //             .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    Else
                        //        .enabled = True
                        //    End If

                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = edt(cindex)
                        //End If

                        break;
                    case "I":

                        HyperLink btnimage = new HyperLink();
                        btnimage.CssClass = "hpyebutton";
                        btnimage.Text = "IMAGE";
                        //btnedit.OnClientClick = "";
                        cntcell.Controls.Add(btnimage);
                        tr.Cells.Add(cntcell);
                        tindex = tindex + 1;
                        //cindex = cindex + 1
                        //load edt(cindex)
                        //Set ldcnt.CURRENTCONTROL = edt(cindex)

                        //With edt(cindex)
                        //    .top = prvtop
                        //    .VISIBLE = True
                        //    .tabIndex = CNTTABINDEX
                        //    .tag = kk
                        //    .caption = "&IMAGE"
                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = edt(cindex)
                        //End If

                        //''       imglblindex = imglblindex + 1
                        //load lblimagestring(cindex)
                        //Set ldcnt.CURRENTCONTROL = lblimagestring(cindex)

                        //With lblimagestring(cindex)
                        //    .VISIBLE = True
                        //    .caption = Trim(ldcnt.Imagestring)
                        //    .top = prvtop
                        //    .tag = kk
                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = lblimagestring(cindex)
                        //End If

                        //load IMGPREV(cindex)
                        //Set ldcnt.CURRENTCONTROL = IMGPREV(cindex)

                        //With IMGPREV(cindex)
                        //    .top = prvtop
                        //    .VISIBLE = True
                        //    .tabIndex = CNTTABINDEX
                        //    .tag = kk
                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = IMGPREV(cindex)
                        //End If
                        break;
                    case "T":
                        TextBox txt = new TextBox();
                        txt.Text = "";
                        txt.ID = "txt_" + tindex;
                        txt.Attributes.Add("onFocus", "getPreviousValue('" + ldcnt.fldName + "')");
                        txt.Attributes.Add("onkeyup", "checkTestNormalRamge(this.id,this.value,"+ tindex + ")");
                        string style = "float:left;width:90%";
                        
                        if (Common.MyCDbl(ldcnt.CURRVAL) < Common.MyCDbl(ldcnt.LBND))
                        {
                            style = "float:left;width:90%;color:red";
                        }
                        if (Common.MyCDbl(ldcnt.CURRVAL) > Common.MyCDbl(ldcnt.UBND))
                        {
                            style = "float:left;width:90%;color:red";
                        }
                        if (Common.MyCDbl(ldcnt.LBND) == 0 && Common.MyCDbl(ldcnt.UBND) == 0)
                        {
                            style= "float:left;width:90%;color:black";
                        }
                        txt.Attributes.Add("style", style);
                        txt.Text = ldcnt.CURRVAL;
                        cntcell.Controls.Add(txt);
                        Image img3 = new Image();
                        img3.ID = "contexMenu" + tindex;
                        img3.Attributes.Add("src", "../Images/uparrow.png");
                        img3.Attributes.Add("onclick", "return openContextMenu(this," + tindex + ")");
                        img3.Attributes.Add("style", "height:25px;width:25px;cursor:pointer;float:left;");
                        cntcell.Controls.Add(img3);
                        ucCommentHtmlBox ucComment = (ucCommentHtmlBox)Page.LoadControl("../UserControls/ucCommentHtmlBox.ascx");

                        ucComment.controlval =CommonFunctions.convertFromRtfToHtml(ldcnt.comments);

                        Image img5 = new Image();
                        img5.ID = "ok_" + tindex;
                        img5.Attributes.Add("src", "../Images/okbutton.png");
                        img5.Attributes.Add("onclick", "closeModal('" + "uccommenthtml_" + tindex + "_htmlboxmodal" + "',this.id,'uccommenthtml_" + tindex + "_close_" + tindex + "')");
                        img5.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:55%;top:0;background-color:white;border:4px white solid;border-radius:50%;");

                        Image img6 = new Image();
                        img6.ID = "close_" + tindex;
                        img6.Attributes.Add("src", "../Images/closebtn.png");
                        img6.Attributes.Add("onclick", "closeModal('" + "uccommenthtml_" + tindex + "_htmlboxmodal" + "',this.id,'uccommenthtml_" + tindex + "_ok_" + tindex + "')");
                        img6.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:45%;top:0;background-color:white;border:4px white solid;border-radius:50%;");

                        ucComment.Controls.Add(img5);
                        ucComment.Controls.Add(img6);
                        ucComment.ID = "uccommenthtml_" + tindex;
                        cntcell.Controls.Add(ucComment);

                        tr.Cells.Add(cntcell);

                        tindex = tindex + 1;

                        //    If UCase(Trim(getYN(permission))) = "N" And Len(ldcnt.tval) > 0 Then
                        //      .enabled = False
                        //    End If

                        
                        //                    ''''''suren 10/11
                        //    If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.machPerf) Then
                        //        If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //            .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //     ''''''''code for checking sample drawn condition
                        //    ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //        If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //             .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    Else
                        //        .enabled = True
                        //    End If


                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = Text1(tindex)
                        //End If
                        break;

                    case "M":

                        //timeindex = timeindex + 1
                        //load Text2(timeindex)
                        //Set ldcnt.CURRENTCONTROL = Text2(timeindex)

                        //With Text2(timeindex)
                        //    .top = prvtop
                        //    .tabIndex = CNTTABINDEX
                        //    .VISIBLE = True
                        //    .tag = kk
                        //    .Text = ldcnt.tval
                        //    .Text = Format(.Text, "##Min:##Sec")
                        //    '''''
                        //    .enabled = en
                        //    If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                        //      .enabled = False
                        //    End If
                        //                    ''''''suren 10/11
                        //    If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                        //        If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //            .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //     ''''''''code for checking sample drawn condition
                        //    ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //         If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //             .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    Else
                        //        .enabled = True
                        //    End If


                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = Text2(timeindex)
                        //End If
                        break;

                    case "D":
                        //tindex = tindex + 1
                        //load akDate1(tindex)
                        //Set ldcnt.CURRENTCONTROL = akDate1(tindex)

                        //With akDate1(tindex)
                        //    .top = prvtop
                        //    .tabIndex = CNTTABINDEX
                        //    .VISIBLE = True
                        //    .tag = kk
                        //    .enabled = en
                        //    If mycdate(ldcnt.tval) < #1/2/1900# Then
                        //        .Value = Format(Now, "dd/MMM/yyyy")
                        //        ldcnt.CURRVAL = Now
                        //    Else
                        //        .Value = mycdate(ldcnt.tval)
                        //        ldcnt.CURRVAL = Format(mycdate(ldcnt.tval), "dd/MMM/yyyy")
                        //    End If

                        //    If myCdbl(ldcnt.CURRVAL) < myCdbl(ldcnt.LBND) Then
                        //        ' .ForeColor = vbRed
                        //    End If

                        //    If myCdbl(ldcnt.CURRVAL) > myCdbl(ldcnt.UBND) Then
                        //        '.ForeColor = vbRed
                        //    End If

                        //    If myCdbl(ldcnt.LBND) = 0 And myCdbl(ldcnt.UBND) = 0 Then
                        //        ' .ForeColor = vbBlack
                        //    End If
                        //    ''''''suren 10/11
                        //    If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                        //        If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //            .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //     ''''''''code for checking sample drawn condition
                        //    ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //         If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //             .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    Else
                        //        .enabled = True
                        //    End If

                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = akDate1(tindex)
                        //End If
                        break;
                    case "Q":
                        HyperLink btnedit1 = new HyperLink();
                        btnedit1.CssClass = "hpyebutton";
                        btnedit1.Text = "Table";
                        btnedit1.Attributes.Add("onclick", "showCommentModal('" + "uctable_" + tindex + "_htmlboxmodal" + "','uctable_" + tindex + "_close_" + tindex + "',''," + timeindex + ",'uctable_" + tindex + "_ok_" + tindex + "')");
                        //btnedit.OnClientClick = "";
                        cntcell.Controls.Add(btnedit1);
                        tr.Cells.Add(cntcell);
                        Image img1 = new Image();
                        img1.ID = "ok_" + tindex;
                        img1.Attributes.Add("src", "../Images/okbutton.png");
                        img1.Attributes.Add("onclick", "closeModal('" + "uctable_" + tindex + "_htmlboxmodal" + "',this.id,'uctable_" + tindex + "_close_" + tindex + "')");
                        img1.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:55%;top:0;background-color:white;border:4px white solid;border-radius:50%;");

                        Image img7 = new Image();
                        img7.ID = "close_" + tindex;
                        img7.Attributes.Add("src", "../Images/closebtn.png");
                        img7.Attributes.Add("onclick", "closeModal('" + "uctable_" + tindex + "_htmlboxmodal" + "',this.id,'uctable_" + tindex + "_ok_" + tindex + "')");
                        img7.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:45%;top:0;background-color:white;border:4px white solid;border-radius:50%;");
                        UcTable uc2 = (UcTable)Page.LoadControl("../UserControls/UcTable.ascx");
                        // uc2.controlval = ldcnt.CURRVAL;
                        uc2.Controls.Add(img1);
                        uc2.Controls.Add(img7);
                        uc2.ID = "uctable_" + tindex;


                        //cntcell.Controls.Add(uc1);
                        uspanel.Controls.Add(uc2);
                        tindex = tindex + 1;


                        //load edt(cindex)
                        //Set ldcnt.CURRENTCONTROL = edt(cindex)

                        //With edt(cindex)
                        //    .top = prvtop
                        //    .VISIBLE = True
                        //    .tabIndex = CNTTABINDEX
                        //    .tag = kk
                        //    .caption = "&TABLE"
                        //    .enabled = en

                        //    If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                        //      .enabled = False
                        //    End If
                        //                                    ''''''suren 10/11
                        //    If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                        //        If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //            .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //     ''''''''code for checking sample drawn condition
                        //    ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //         If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //             .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    Else
                        //        .enabled = True
                        //    End If
                        break;
                    case "U":
                        HyperLink btntabular = new HyperLink();
                        btntabular.Text = "TABULAR";
                        //btntabular.OnClientClick = "";
                        btntabular.CssClass = "hpyebutton";
                        btntabular.Attributes.Add("onclick", "showCommentModal('" + "uctabular_" + tindex + "_htmlboxmodal" + "','uctabular_" + tindex + "_close_" + tindex + "',''," + timeindex + ",'uctabular_" + tindex + "_ok_" + tindex + "')");
                        cntcell.Controls.Add(btntabular);
                        tr.Cells.Add(cntcell);
                        Image img2 = new Image();
                        img2.ID = "ok_" + tindex;
                        img2.Attributes.Add("src", "../Images/okbutton.png");
                        img2.Attributes.Add("onclick", "closeModal('" + "uctabular_" + tindex + "_htmlboxmodal" + "',this.id,'uctabular_" + tindex + "_close_" + tindex + "')");
                        img2.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:55%;top:0;background-color:white;border:4px white solid;border-radius:50%;");

                        Image img8 = new Image();
                        img8.ID = "close_" + tindex;
                        img8.Attributes.Add("src", "../Images/closebtn.png");
                        img8.Attributes.Add("onclick", "closeModal('" + "uctabular_" + tindex + "_htmlboxmodal" + "',this.id,'uctabular_" + tindex + "_ok_" + tindex + "')");
                        img8.Attributes.Add("style", "position:fixed;z-index:1050;height:50px;width:50px;display:none;cursor:pointer;right:45%;top:0;background-color:white;border:4px white solid;border-radius:50%;");
                        ucTabular uc3 = (ucTabular)Page.LoadControl("../UserControls/ucTabular.ascx");
                        // uc2.controlval = ldcnt.CURRVAL;
                        uc3.Controls.Add(img2);
                        uc3.Controls.Add(img8);
                        uc3.ID = "uctabular_" + tindex;


                        //cntcell.Controls.Add(uc1);
                        uspanel.Controls.Add(uc3);

                        tindex = tindex + 1;
                        //cindex = cindex + 1
                        //load edt(cindex)
                        //Set ldcnt.CURRENTCONTROL = edt(cindex)

                        //With edt(cindex)
                        //    .top = prvtop
                        //    .VISIBLE = True
                        //    .tabIndex = CNTTABINDEX
                        //    .tag = kk
                        //    .caption = "&TABULAR"
                        //    .enabled = en

                        //    If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                        //      .enabled = False
                        //    End If
                        //                                    ''''''suren 10/11
                        //    If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                        //        If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //            .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //     ''''''''code for checking sample drawn condition
                        //    ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //         If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //             .enabled = True
                        //        Else
                        //            .enabled = False
                        //        End If
                        //    Else
                        //        .enabled = True
                        //    End If

                        //End With

                        //If FIRSTCNT Is Nothing Then
                        //    Set FIRSTCNT = edt(cindex)
                        //End If
                        break;
                    case "R":
                        HyperLink btnculture = new HyperLink();
                        btnculture.Text = "CULTURE";
                        //btntabular.OnClientClick = "";
                        btnculture.CssClass = "hpyebutton";
                        cntcell.Controls.Add(btnculture);
                        tr.Cells.Add(cntcell);
                        tindex = tindex + 1;
                        //            cindex = cindex + 1
                        //            load edt(cindex)
                        //            Set ldcnt.CURRENTCONTROL = edt(cindex)

                        //            With edt(cindex)
                        //                .top = prvtop
                        //                .VISIBLE = True
                        //                .tabIndex = CNTTABINDEX
                        //                .tag = kk
                        //                .caption = "&CULTURE"
                        //                .enabled = en
                        //                If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                        //                  .enabled = False
                        //                End If
                        //                                                ''''''suren 10/11
                        //                If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                        //                    If myCdbl(ldcnt.isMachPerf) = 1 Then
                        //                        .enabled = True
                        //                    Else
                        //                        .enabled = False
                        //                    End If
                        //                 ''''''''code for checking sample drawn condition
                        //                ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                        //                     If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                        //                         .enabled = True
                        //                    Else
                        //                        .enabled = False
                        //                    End If
                        //                Else
                        //                    .enabled = True
                        //                End If

                        //            End With

                        //            If FIRSTCNT Is Nothing Then
                        //                Set FIRSTCNT = edt(cindex)
                        //            End If
                        break;
                    case "S":
                        HyperLink btnperf = new HyperLink();
                        
                        if (ldcnt.ISPERF.ToUpper() == "Y")
                        {
                            btnperf.Text = "Unperform";

                        }
                        else
                        {
                            btnperf.Text = "Perform";

                        }
                        //btntabular.OnClientClick = "";
                        btnperf.CssClass = "hpyebutton";
                        cntcell.Controls.Add(btnperf);
                        tr.Cells.Add(cntcell);
                        tindex = tindex + 1;

                        break;


                }
                TableCell lblrange = new TableCell();
                lblrange.Text = ldcnt.unit.Trim();
                tr.Cells.Add(lblrange);
                lblrange = new TableCell();
                lblrange.Text = ldcnt.LBND.Trim();
                tr.Cells.Add(lblrange);
                lblrange = new TableCell();
                lblrange.Text =ldcnt.UBND.Trim();
                tr.Cells.Add(lblrange);
                Table1.Rows.Add(tr);





                //            If FIRSTCNT Is Nothing Then
                //                Set FIRSTCNT = edt(cindex)
                //            End If

                //        Case "U"
                //            cindex = cindex + 1
                //            load edt(cindex)
                //            Set ldcnt.CURRENTCONTROL = edt(cindex)

                //            With edt(cindex)
                //                .top = prvtop
                //                .VISIBLE = True
                //                .tabIndex = CNTTABINDEX
                //                .tag = kk
                //                .caption = "&TABULAR"
                //                .enabled = en

                //                If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                //                  .enabled = False
                //                End If
                //                                                ''''''suren 10/11
                //                If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                //                    If myCdbl(ldcnt.isMachPerf) = 1 Then
                //                        .enabled = True
                //                    Else
                //                        .enabled = False
                //                    End If
                //                 ''''''''code for checking sample drawn condition
                //                ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                //                     If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                //                         .enabled = True
                //                    Else
                //                        .enabled = False
                //                    End If
                //                Else
                //                    .enabled = True
                //                End If

                //            End With

                //            If FIRSTCNT Is Nothing Then
                //                Set FIRSTCNT = edt(cindex)
                //            End If

                //        Case "R"
                //            cindex = cindex + 1
                //            load edt(cindex)
                //            Set ldcnt.CURRENTCONTROL = edt(cindex)

                //            With edt(cindex)
                //                .top = prvtop
                //                .VISIBLE = True
                //                .tabIndex = CNTTABINDEX
                //                .tag = kk
                //                .caption = "&CULTURE"
                //                .enabled = en
                //                If UCase(Trim(permission)) = "N" And Len(ldcnt.tval) > 0 Then
                //                  .enabled = False
                //                End If
                //                                                ''''''suren 10/11
                //                If mycboolean(gettunvar("updtmchtst", "N")) = True And myCbool(ldcnt.isMachPerf) Then
                //                    If myCdbl(ldcnt.isMachPerf) = 1 Then
                //                        .enabled = True
                //                    Else
                //                        .enabled = False
                //                    End If
                //                 ''''''''code for checking sample drawn condition
                //                ElseIf mycboolean(gettunvar("smpldwntst", "Y")) = True And mycboolean(gettunvar("reqtstwsmp", "Y")) = True And myCbool(ldcnt.issampledrawn) Then
                //                     If mycdate(ldcnt.sampledate) > #1/1/1900# Then
                //                         .enabled = True
                //                    Else
                //                        .enabled = False
                //                    End If
                //                Else
                //                    .enabled = True
                //                End If

                //            End With

                //            If FIRSTCNT Is Nothing Then
                //                Set FIRSTCNT = edt(cindex)
                //            End If

                //    End Select



                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return retval;
        }

        protected void saveAllData(List<labtestvalues> lt)
        {

            DbConnection conn = null;
            DbTransaction trans = null;

            try
            {

                conn = Common.GetConnectionFromSession();
                trans = conn.BeginTransaction();
                foreach (var item in lt)
                {

                    ColumnDataCollection coll;
                    coll = new ColumnDataCollection();
                    string retval = "";
                    retval = item.testval;
                    if (Common.AISCompareString(item.controltype, "htmlbox") == AISCompareStringResult.AISCompareEqual)
                    {
                        coll.Add("tval", HTMLtoRTF(item.testval));
                    }
                    else
                    {
                        coll.Add("tval", item.testval);
                    }

                    if (Common.MyLen(item.comment) > 0)
                    {
                        coll.Add("comments", HTMLtoRTF(item.comment));
                    }
                    coll.Add("isperf", "Y");
                    //coll.Add("hcode", testheadcode);
                    //coll.Add("ip", Common.MyCStr(Request.UserHostAddress));
                    // coll.Add("downdate", Common.GetServerDate(Common.GetConnectionFromSession()));
                    //coll.Add("patname", Patname);
                    Common.UpdateTable("labtest", coll, AisUpdateTableType.Update, " LABNO='" + item.labno + "' and TCODE='" + item.testcode + "' ", conn, trans);
                    // updateDatatable "LABTEST", "U", colly, " LABNO='" & ldcnt.Labno & "' and TCODE='" & ldcnt.FLDNAME & "' ", , , True
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;

            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

            }


        }
        [WebMethod]
        public static bool saveData(List<labtestvalues> lt)
        {

            frmLabTests labt = new frmLabTests();
            labt.saveAllData(lt);
            return true;
        }
        [WebMethod]
        public static List<labtestvalues> getPreviousValueOfTest(string tescode)
        {
            List<labtestvalues> labtestpreval = new List<labtestvalues>();
            DataTable dt = Common.GetTableFromSession("Select * from gridvals where code='" + tescode + "'", "Preval", null, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    labtestpreval.Add(new labtestvalues { testval = Common.MyCStr(row["VAL"]) });
                }
            }
            return labtestpreval;
        }

       

        public string HTMLtoRTF(string sHTML)
        {

            RtfConverter.Converter converter = new RtfConverter.Converter();
            string result = converter.ConvertHtmlToRtf(sHTML);
            //#region
            //string[] ColorTable = new string[0];
            //string[] FontTable = new string[0];
            //string htmltortf = "";
            //int FontNumber = 0, ColorCombNumber = 0;
            //int lStart = 0, lEnd = 0, lTStart = 0, lTEnd = 0;
            //bool bFound = false, bFaceFound = false;
            //string sFontTable = "", sColorTable = "";
            //string PreviousFontFace = "", DefaultFace = ""; int PreviousFontSize = 0;
            //string sHead = "", sRTF = "", sText = "";
            //int lLen = 0, lCurrentToken = 0;
            //string sToken = "", sTemp = "";
            //int I = 0;
            //bool bUseDefaultFace = false;
            //int lHyperLink;
            //lHyperLink = 0;
            //string[] gsHyperLink = new string[0];
            //try
            //{
            //    //Fix the HTML
            //    sHTML.Trim();

            //    sHTML = sHTML.Replace("<strong>", "<STRONG>");
            //    sHTML = sHTML.Replace("</strong>", "</STRONG>");
            //    sHTML = sHTML.Replace("<em>", "<EM>");
            //    sHTML = sHTML.Replace("</em>", "</EM>");


            //    sHTML = sHTML.Replace("<span", "<SPAN");
            //    sHTML = sHTML.Replace("/span", "/SPAN");
            //    sHTML = sHTML.Replace("font-family:", "FONT-FAMILY:");
            //    sHTML = sHTML.Replace("font-size:", "FONT-SIZE:");
            //    sHTML = sHTML.Replace("font-weight:", "FONT-WEIGHT:");
            //    sHTML = sHTML.Replace("text-decoration:", "TEXT-DECORATION:");
            //    sHTML = sHTML.Replace("font-style:", "FONT-STYLE:");
            //    sHTML = sHTML.Replace("color:", "COLOR:");

            //    sHTML = sHTML.Replace("<STRONG>", "<B>");
            //    sHTML = sHTML.Replace("</STRONG>", "</B>");
            //    sHTML = sHTML.Replace("<EM>", "<I>");
            //    sHTML = sHTML.Replace("</EM>", "</I>");
            //    sHTML = sHTML.Replace("\r", "");
            //    sHTML = sHTML.Replace("&nbsp;", "\\~");
            //    //Debug.WriteLine("siva " + sHTML);
            //    //Initialize
            //    lLen = sHTML.Length;
            //    lStart = 0;
            //    lEnd = 0;
            //    //Add some default fonts
            //    Array.Resize(ref FontTable, 2);
            //    FontTable[0] = "Times New Roman";
            //    FontNumber = FontNumber + 1;
            //    FontTable[1] = "MS Sans Serif";
            //    FontNumber = FontNumber + 1;
            //    PreviousFontSize = 12; //12 default font size
            //                           //Add some default color
            //    Array.Resize(ref ColorTable, 4);
            //    ColorTable[0] = "000000";
            //    //Black
            //    ColorCombNumber = ColorCombNumber + 1;
            //    ColorTable[1] = "ff0000";
            //    //Red
            //    ColorCombNumber = ColorCombNumber + 1;
            //    ColorTable[2] = "00ff00";
            //    //Green
            //    ColorCombNumber = ColorCombNumber + 1;
            //    ColorTable[3] = "0000ff";
            //    //Blue
            //    ColorCombNumber = ColorCombNumber + 1;
            //    //Parse the HTML
            //    for (lCurrentToken = 0; lCurrentToken < lLen; lCurrentToken++)
            //    {
            //        lStart = sHTML.IndexOf("<", lEnd);
            //        //Debug.WriteLine("lStart " + lStart);
            //        if (lStart < 0)
            //            goto Completed;
            //        lEnd = sHTML.IndexOf(">", lStart);
            //        //Debug.WriteLine("lEnd " + lEnd);
            //        sToken = sHTML.Substring(lStart, lEnd - lStart + 1).ToUpper();
            //        //Debug.WriteLine("string  :" + sToken);
            //        //Take action
            //        switch (sToken)
            //        {
            //            case "<B>":
            //            case "<b>":
            //                sRTF = sRTF + "\\b1";
            //                break;
            //            case "</B>":
            //            case "</b>":
            //                sRTF = sRTF + "\\b0";
            //                break;
            //            case "<I>":
            //            case "<i>":
            //                sRTF = sRTF + "\\i1";
            //                break;
            //            case "</I>":
            //            case "</i>":
            //                sRTF = sRTF + "\\i0";
            //                break;
            //            case "<U>":
            //            case "<u>":
            //                sRTF = sRTF + "\\ul1";
            //                break;
            //            case "</U>":
            //            case "</u>":
            //                sRTF = sRTF + "\\ul0";
            //                break;
            //            case "<TR>":
            //            case "<tr>":
            //                sRTF = sRTF + "\\intbl";
            //                break;
            //            case "</TR>":
            //            case "</tr>":
            //                sRTF = sRTF + "\\row";
            //                break;
            //            case "<TD>":
            //            case "</TD>":
            //            case "<td>":
            //            case "</td>":
            //                sRTF = sRTF + "\\cell ";
            //                break;
            //            case "<BR/>":
            //            case "<cr/>":
            //            case "</P>":
            //            case "</p>":
            //                sRTF = sRTF + "\\r" + "\\par";
            //                break;
            //            case "</SPAN>":
            //            case "</span>":
            //                bUseDefaultFace = true;
            //                sRTF = sRTF + "\\plain\\cf1";
            //                break;
            //            default:
            //                //if (sToken.Substring(0, 6).ToUpper() == "<TABLE")
            //                //{
            //                sRTF = sRTF + "\\plain";
            //                //sRTF = sRTF + GetTableStructure(sHTML, lStart);
            //                // }
            //                break;
            //        }

            //        //Set font and color table
            //        //Check for fonts
            //        if (sToken.IndexOf("SPAN", 0) > 0 && sToken.Substring(1, 1) != "/")
            //        {
            //            // Debug.WriteLine("span: " + sToken);
            //            bUseDefaultFace = false;
            //            lTStart = sToken.IndexOf("FONT-FAMILY: ", 0);
            //            //Debug.WriteLine("lTStart: " + lTStart);
            //            if (lTStart > 0)
            //            {
            //                bFaceFound = true;
            //                lTEnd = sToken.IndexOf("\\\"\\\"", lTStart + ("FONT-FAMILY: ").Length + 1);
            //                if (lTEnd < 0)
            //                {
            //                    lTEnd = sToken.IndexOf(" ", lTStart + ("FONT-FAMILY: ").Length);
            //                }
            //                if (lTEnd < 0)
            //                {
            //                    lTEnd = sToken.IndexOf(">", lTStart + ("FONT-FAMILY: ").Length);
            //                }
            //                //Debug.WriteLine("lTEnd: " + lTEnd);
            //                sTemp = sToken.Substring(lTStart + ("FONT-FAMILY: ").Length, lTEnd - (lTStart + ("FONT-FAMILY: ").Length));
            //                //Debug.WriteLine("Font: " + sTemp);
            //                sTemp = sTemp.Replace("\\\"\\\"", "");
            //                if (sTemp != "")
            //                {
            //                    bFaceFound = true;
            //                    //Check if it is already in the table
            //                    if (FontTable.Length != 0)
            //                    {
            //                        for (I = 0; I < FontTable.Length; I++)
            //                        {
            //                            if (sTemp == FontTable[I])
            //                            {
            //                                bFound = true;
            //                                break;
            //                            }
            //                        }
            //                    }
            //                    else
            //                        bFound = false;
            //                }
            //                //If not found add it
            //                if (bFound == false)
            //                {
            //                    Array.Resize(ref FontTable, FontNumber + 1);
            //                    FontTable[FontNumber] = sTemp;
            //                    FontNumber = FontNumber + 1;
            //                    PreviousFontFace = sTemp;
            //                    if (DefaultFace == "")
            //                        DefaultFace = sTemp;
            //                }
            //                sRTF = sRTF + "\\f" + GetIndex(FontTable, sTemp);
            //                sTemp = "";
            //                bFound = false;
            //            }
            //            else
            //                bFaceFound = false;
            //        }
            //        lTStart = sToken.IndexOf("FONT-SIZE: ", 0);

            //        if (lTStart > 0)
            //        {
            //            lTEnd = sToken.IndexOf("P", lTStart + ("FONT-SIZE: ").Length);
            //            if (lTEnd >= 0)
            //            {
            //                if (lTEnd == 0)
            //                {
            //                    lTEnd = sToken.IndexOf(">", lTStart + ("FONT-SIZE: ").Length);
            //                }
            //                sTemp = sToken.Substring(lTStart + ("FONT-SIZE: ").Length, lTEnd - (lTStart + ("FONT-SIZE: ").Length)).Replace("\\\"\\\"", "");
            //                sTemp.Trim();
            //                if (sTemp != "")
            //                {
            //                    sTemp = GetFontSize(sTemp);
            //                    if (Convert.ToInt64(sTemp) < 0)
            //                        sTemp = Convert.ToString(PreviousFontSize);
            //                    PreviousFontSize = Convert.ToInt32(sTemp);
            //                    //Debug.WriteLine("size: " + PreviousFontSize);
            //                    sTemp = "";
            //                }
            //            }
            //            bFound = false;
            //        }
            //        lTStart = sToken.IndexOf("FONT-WEIGHT:", 0);
            //        if (lTStart > 0)
            //        {
            //            lTEnd = sToken.IndexOf(";", lTStart + ("FONT-WEIGHT:").Length);
            //            if (lTEnd == 0)
            //            {
            //                lTEnd = sToken.IndexOf(">", lTStart + ("FONT-WEIGHT:").Length);
            //            }
            //            sTemp = sToken.Substring(lTStart + ("FONT-WEIGHT:").Length, lTEnd - (lTStart + ("FONT-WEIGHT:").Length)).Replace("\\\"\\\"", "");
            //            sTemp.Trim();
            //            if (sTemp != string.Empty)
            //            {
            //                //Debug.WriteLine("bold  " + sTemp);
            //                if (sTemp.ToLower() == "bold" || sTemp.ToLower() == "bolder" || Convert.ToInt32(sTemp) > 500)
            //                {
            //                    sRTF = sRTF + "\\b1";
            //                }
            //                sTemp = string.Empty;
            //            }
            //            bFound = false;
            //        }
            //        lTStart = sToken.IndexOf("TEXT-DECORATION:", 0);
            //        if (lTStart > 0)
            //        {
            //            lTEnd = sToken.IndexOf(";", lTStart + ("TEXT-DECORATION:").Length);
            //            if (lTEnd == 0)
            //            {
            //                lTEnd = sToken.IndexOf(">", lTStart + ("TEXT-DECORATION:").Length);
            //            }
            //            sTemp = sToken.Substring(lTStart + ("TEXT-DECORATION:").Length, lTEnd - (lTStart + ("TEXT-DECORATION:").Length)).Replace("\\\"\\\"", "");
            //            sTemp.Trim();
            //            if (sTemp != string.Empty)
            //            {
            //                //Debug.WriteLine("string " + sTemp);
            //                //if (sTemp.ToLower() == "underline")
            //                if (Common.AISCompareString(sTemp.ToLower(), "underline") == AISCompareStringResult.AISCompareEqual)
            //                {
            //                    sRTF = sRTF + "\\ul1";
            //                }
            //                sTemp = string.Empty;
            //            }
            //            bFound = false;
            //        }
            //        lTStart = sToken.IndexOf("FONT-STYLE:", 0);
            //        if (lTStart > 0)
            //        {
            //            lTEnd = sToken.IndexOf(";", lTStart + ("FONT-STYLE:").Length);
            //            if (lTEnd == 0)
            //            {
            //                lTEnd = sToken.IndexOf(">", lTStart + ("FONT-STYLE:").Length);
            //            }
            //            sTemp = sToken.Substring(lTStart + ("FONT-STYLE:").Length, lTEnd - (lTStart + ("FONT-STYLE:").Length)).Replace("\\\"\\\"", "");
            //            sTemp.Trim();
            //            if (sTemp != string.Empty)
            //            {
            //                //Debug.WriteLine("string " + sTemp);
            //                //if (sTemp.ToLower() == "underline")
            //                if (Common.AISCompareString(sTemp.ToLower(), "underline") == AISCompareStringResult.AISCompareEqual)
            //                {
            //                    sRTF = sRTF + "\\i1";
            //                }
            //                sTemp = string.Empty;
            //            }
            //            bFound = false;
            //        }
            //        //Check for colors
            //        lTStart = sToken.IndexOf("COLOR: ", 0);
            //        if (lTStart > 0)
            //        {
            //            lTEnd = sToken.IndexOf(" ", lTStart + ("COLOR: ").Length);
            //            if (lTEnd == 0)
            //            {
            //                lTEnd = sToken.IndexOf(">", lTStart + ("COLOR: ").Length);
            //            }
            //            sTemp = sToken.Substring(lTStart + ("COLOR: ").Length, lTEnd - (lTStart + ("COLOR: ").Length));
            //            sTemp = sTemp.Replace("\\\"\\\"", "");
            //            sTemp = sTemp.Replace("#", "");
            //            //Debug.WriteLine("color: " + sTemp);
            //            if (sTemp != "")
            //            {
            //                //Check if it is already in the table
            //                if (ColorTable.Length != 0)
            //                {
            //                    for (I = 0; I < ColorTable.Length; I++)
            //                    {
            //                        if (sTemp == ColorTable[I])
            //                        {
            //                            bFound = true;
            //                            break;
            //                        }
            //                    }
            //                }
            //                else
            //                    bFound = false;
            //            }
            //            //If not found add it
            //            if (bFound == false)
            //            {
            //                Array.Resize(ref ColorTable, ColorCombNumber + 1);
            //                ColorTable[ColorCombNumber] = sTemp;
            //                ColorCombNumber = ColorCombNumber + 1;
            //            }
            //            int c = GetIndex(ColorTable, sTemp);
            //            c = c + 1;
            //            //Debug.WriteLine("Color: " + c);
            //            sRTF = sRTF + "\\cf" + c;
            //            sTemp = "";
            //            bFound = false;
            //        }
            //        //Debug.WriteLine("Color: " + sRTF);
            //        //check for hyperlink
            //        //<A href="http://www.microsoft.com">Friendly name</A>
            //        // Debug.WriteLine("sToken" + sToken);
            //        lTStart = sToken.IndexOf("HREF=", 0);
            //        // Debug.WriteLine("lTStart" + lTStart);
            //        if (lTStart > 0)
            //        {
            //            lTEnd = sToken.IndexOf(">", lTStart + ("HREF=").Length + 1);
            //            //Debug.WriteLine("lTEnd" +  lTEnd);
            //            if (lTEnd > 0)
            //            {
            //                sTemp = sToken.Substring(lTStart + ("HREF=").Length, lTEnd - (lTStart + ("HREF=").Length + 1));
            //                //Debug.WriteLine("sTemp " +sTemp);
            //                if (sTemp != string.Empty)
            //                {
            //                    Array.Resize(ref gsHyperLink, lHyperLink + 1);
            //                    gsHyperLink[lHyperLink] = sTemp.ToLower();
            //                    //Get the text
            //                    lStart = sHTML.IndexOf(">", lEnd);
            //                    //Debug.WriteLine("lStart: " + lStart);
            //                    if (lStart < 0)
            //                        goto Completed;
            //                    lEnd = sHTML.IndexOf("<", lStart);
            //                    //Debug.WriteLine("lEnd: " + lEnd);
            //                    if (lEnd < 0)
            //                        goto Completed;
            //                    sText = sHTML.Substring(lStart, (lEnd - lStart) + 1);
            //                    sText = sText.Replace("<", "");
            //                    sText = sText.Replace(">", "");
            //                    if ((sText).Length > 0 && sText != string.Empty)
            //                    {
            //                        sText = sText.Substring(0, (sText).Length);
            //                        //Debug.WriteLine("sText: " + sText);
            //                        //check out for special characters
            //                        sText = sText.Replace("\\", "\\\\");
            //                        sText = sText.Replace("{", "\\{");
            //                        sText = sText.Replace("}", "\\}");
            //                        if (bFaceFound = false && bUseDefaultFace)
            //                        {
            //                            sTemp = Convert.ToString(GetIndex(FontTable, DefaultFace)); //PreviousFontFace)
            //                            if (Convert.ToInt32(sTemp) < 0)
            //                                sRTF = sRTF + "\\f0";
            //                            else
            //                                sRTF = sRTF + "\\f" + sTemp;
            //                            sTemp = "";
            //                        }
            //                    }
            //                    sText.Trim();
            //                    int c = GetIndex(ColorTable, "0000ff");
            //                    c = c + 1;
            //                    sRTF = sRTF + "{\\field{\\*\\fldinst HYPERLINK " + sTemp.ToLower() + "\"}{\\fldrslt " + sText + "}}";
            //                    //sRTF = sRTF + "{ \\hl  { \\hlloc " + sTemp.ToLower() + " } { \\hlsrc "+ sText + " } { \\hlfr "+ sText + " } }";
            //                    //Debug.WriteLine("link : " + sTemp.ToLower());
            //                    sTemp = "";
            //                    bFound = false;
            //                    lHyperLink = lHyperLink + 1;
            //                }
            //            }
            //        }


            //        //Get the text
            //        lStart = sHTML.IndexOf(">", lEnd);
            //        //Debug.WriteLine("lStart: " + lStart);
            //        if (lStart < 0)
            //            goto Completed;
            //        lEnd = sHTML.IndexOf("<", lStart);
            //        //Debug.WriteLine("lEnd: " + lEnd);
            //        if (lEnd < 0)
            //            goto Completed;
            //        sText = sHTML.Substring(lStart, (lEnd - lStart) + 1);
            //        sText = sText.Replace("<", "");
            //        sText = sText.Replace(">", "");
            //        sText.Trim();
            //        if ((sText).Length > 0 && sText != string.Empty)
            //        {
            //            sText = sText.Substring(0, (sText).Length);
            //            //Debug.WriteLine("sText: " + sText);
            //            //check out for special characters
            //            sText = sText.Replace("\\", "\\\\");
            //            sText = sText.Replace("{", "\\{");
            //            sText = sText.Replace("}", "\\}");
            //            if (bFaceFound = false && bUseDefaultFace)
            //            {
            //                sTemp = Convert.ToString(GetIndex(FontTable, DefaultFace)); //PreviousFontFace)
            //                if (Convert.ToInt32(sTemp) < 0)
            //                    sRTF = sRTF + "\\f0";
            //                else
            //                    sRTF = sRTF + "\\f" + sTemp;
            //                sTemp = "";
            //            }
            //            sRTF = sRTF + "\\fs" + PreviousFontSize * 2 + " " + sText;

            //        }
            //        //Debug.WriteLine("sText: " + sRTF);
            //    }
            //    Completed:
            //    //Generate Font Table
            //    sFontTable = "\\deff0{\\fonttbl";
            //    if (FontTable.Length != 0)
            //    {
            //        for (I = 0; I < FontTable.Length; I++)
            //            sFontTable = sFontTable + "{\\f" + I + "\\fnil\\fcharset0 " + FontTable[I] + ";}";
            //        sFontTable = sFontTable + "}";
            //    }
            //    else
            //    {
            //        sFontTable = sFontTable + "{\\f0\\froman\\fcharset0 Times New Roman;}}";
            //        Array.Resize(ref FontTable, 0);
            //        FontTable[0] = "Times New Roman";
            //        //FontTable(0).SIZE = "18"
            //    }

            //    //Generate Color Table
            //    sColorTable = "{\\colortbl;";
            //    if (ColorTable.Length != 0)
            //    {
            //        for (I = 0; I < ColorTable.Length; I++)
            //        {
            //            //Debug.WriteLine("Color: "+ColorTable[I]);
            //            sColorTable = sColorTable + "\\red" + GetRed(ColorTable[I]) + "\\green" + GetGreen(ColorTable[I]) + "\\blue" + GetBlue(ColorTable[I]) + ";";
            //        }
            //        sColorTable = sColorTable + "}";
            //    }
            //    else
            //    {
            //    }
            //    //Generate head
            //    sHead = "{\\rtf1\\ansi" + sFontTable + "\\r" + sColorTable;
            //    sHead = sHead + "\\r" + "\\pard\\plain\\tx0";

            //    htmltortf = sHead + sRTF + "}";
            //}
            //catch(Exception ex)
            //{ }
            //#endregion
            //Debug.WriteLine("htmltortf: " + htmltortf);
            return result;
        }
        public int GetRed(string HexString)
        {
            string sTemp = "";
            sTemp = HexString.Substring(0, 2);
            //Debug.WriteLine("" + Convert.ToInt32(sTemp,16));
            return Convert.ToInt32(sTemp, 16);
        }
        public int GetGreen(string HexString)
        {
            string sTemp = "";
            sTemp = HexString.Substring(2, 2);
            //Debug.WriteLine("" + Convert.ToInt32(sTemp,16));
            return Convert.ToInt32(sTemp, 16);
        }
        public int GetBlue(string HexString)
        {
            string sTemp = "";
            sTemp = HexString.Substring(4, 2);
            //Debug.WriteLine("" + Convert.ToInt32(sTemp,16));
            return Convert.ToInt32(sTemp, 16);
        }
        string GetFontSize(string sTemp)
        {
            string getfontsize = "";
            try
            {
                switch (sTemp)
                {
                    case "1":
                        getfontsize = "30";//"7.5"
                        break;
                    case "2":
                        getfontsize = "40";// '"10"
                        break;
                    case "3":
                        getfontsize = "48";// '"12"
                        break;
                    case "4":
                        getfontsize = "54";// '"13.5"
                        break;
                    case "5":
                        getfontsize = "72";// '"18"
                        break;
                    case "6":
                        getfontsize = "96";// '"24"
                        break;
                    case "7":
                        getfontsize = "144";// '"36"
                        break;
                    default:
                        getfontsize = Convert.ToString((Math.Round(Convert.ToDouble(sTemp))));
                        break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return getfontsize;
        }
        public int GetIndex(string[] Table, string Code)
        {
            int I = 0, getindex = 0;
            bool bFound = false;
            try
            {

                //Get the index
                if (Table.Length != 0)
                {
                    for (I = 0; I < Table.Length; I++)
                    {
                        if (Code == Table[I])
                        {
                            bFound = true;
                            break;
                        }
                    }
                }
                else
                    bFound = false;

                //return it
                if (bFound == false)
                    getindex = 0;
                else
                    getindex = I;

            }
            catch (Exception e)
            {
                throw e;
            }
            return getindex;
        }
        public string getLowerBound(string TESTCODE, string sex, Double age, double agemon, double ageday, ref string HighBound, DataTable RSTESTLIBONTEST = null, string KITCODE = "")
        {
            string lowbound;
            lowbound = "";
            HighBound = "";
            string retval = string.Empty;

            bool isusepassdata;
            isusepassdata = false;
            try
            {
                if (RSTESTLIBONTEST.Rows.Count > 0)
                {
                    isusepassdata = true;
                }

                if (Common.MyLen(KITCODE) > 0)
                {
                    string str1KitLbound = string.Empty;
                    string str1KitUbound = string.Empty;
                    //str1KitLbound = getkitLowerBound(TESTCODE, KITCODE, sex, age, agemon, ageday, str1KitUbound)


                    if (Common.MyLen(str1KitLbound) > 0 || Common.MyLen(str1KitUbound) > 0)
                    {
                        retval = str1KitLbound;
                        HighBound = str1KitUbound;
                        return retval;
                    }
                }
                DataTable rs = new DataTable();
                if (isusepassdata == false)
                {
                    rs = new DataTable();
                    string rangeqry;
                    rangeqry = "select ageC,lboundMC,lboundFC,uboundMC,uboundFC,ageA,lboundMA,lboundFA,uboundMA,uboundFA,ageO,lboundMO,lboundFO,uboundMO,uboundFO,lbound,ubound,ageE,lboundME,lboundFE,uboundME,uboundFE,ageF,lboundMF,lboundFF,uboundMF,uboundFF,agemon1,agemon2,agemon3,agemon4,lbagemon1male,lbagemon2male,lbagemon3male,lbagemon4male,lbagemon1female,lbagemon2female,lbagemon3female,lbagemon4female,ubagemon1male,ubagemon2male,ubagemon3male,ubagemon4male,ubagemon1female,ubagemon2female,ubagemon3female,ubagemon4female, ";
                    rangeqry = rangeqry + " ageday1,ageday2,ageday3,ageday4,lbageinday1M,lbageinday1F,lbageinday2M,lbageinday2F,lbageinday3M,lbageinday3F,lbageinday4M,lbageinday4F,Ubageinday1M,Ubageinday1F,Ubageinday2M,Ubageinday2F,Ubageinday3M,Ubageinday3F,Ubageinday4M,Ubageinday4F from testlib where code='" + TESTCODE + "'";
                    rs = Common.GetTableFromSession(rangeqry, "BoundTab",null,null);
                }


                bool isanyrangeondays;
                isanyrangeondays = false;

                if (rs.Rows.Count > 0)
                {
                    if (age == 0 && agemon == 0 && ageday == 0)
                    {
                        retval = Common.MyCStr(rs.Rows[0]["lbound"]);
                        HighBound = Common.MyCStr(rs.Rows[0]["UBound"]);
                        return retval;

                    }
                    int maxdayinage = 0;


                    if (Common.MyCDbl(rs.Rows[0]["ageday1"]) > 0)
                    {
                        isanyrangeondays = true;
                    }
                    if (Common.MyCDbl(rs.Rows[0]["ageday1"]) > 0)
                    {
                        maxdayinage = Common.MycInt(rs.Rows[0]["ageday1"]);
                    }
                    if (Common.MyCDbl(rs.Rows[0]["ageday2"]) > 0)
                    {
                        maxdayinage = Common.MycInt(rs.Rows[0]["ageday2"]);
                    }
                    if (Common.MyCDbl(rs.Rows[0]["ageday3"]) > 0)
                    {
                        maxdayinage = Common.MycInt(rs.Rows[0]["ageday3"]);
                    }
                    if (Common.MyCDbl(rs.Rows[0]["ageday4"]) > 0)
                    {
                        maxdayinage = Common.MycInt(rs.Rows[0]["ageday"]);
                    }


                    bool isfoundrange = false;
                    if (isanyrangeondays == true && (ageday <= maxdayinage) && age == 0 && (Common.MyCDbl(agemon) == 0))
                    {
                        switch (sex.Trim().ToUpper())
                        {
                            case "M":

                                if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday1"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday1M"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday1M"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday2"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday2M"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday2M"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday3"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday3M"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday3M"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday4"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday4M"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday4M"]);
                                    isfoundrange = true;
                                }
                                break;
                            case "F":

                                if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday1"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday1F"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday1F"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday2"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday2F"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday2F"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday3"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday3F"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday3F"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(ageday) <= Common.MyCDbl(rs.Rows[0]["ageday4"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbageinday4F"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubageinday4F"]);
                                    isfoundrange = true;
                                }
                                break;

                        }

                    }

                    if (isfoundrange == false && agemon > 0 && age == 0)
                    {
                        switch (sex.Trim().ToUpper())
                        {
                            case "M":
                                if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon1"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon1Male"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon1Male"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon2"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon2Male"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon2Male"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon3"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon3Male"]);

                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon3Male"]);

                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon4"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon4Male"]);

                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon4Male"]);

                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) > Common.MyCDbl(rs.Rows[0]["agemon4"]) && Common.MyCDbl(agemon) < (Common.MyCDbl(rs.Rows[0]["agec"]) * 12))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundmc"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundmc"]);
                                    isfoundrange = true;
                                }
                                break;
                            case "F":

                                if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon1"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon1FeMale"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon1FeMale"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon2"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon2FeMale"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon2FeMale"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon3"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon3FeMale"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon3FeMale"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) <= Common.MyCDbl(rs.Rows[0]["agemon4"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lbagemon4FeMale"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["Ubagemon4FeMale"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(agemon) > Common.MyCDbl(rs.Rows[0]["agemon4"]) && Common.MyCDbl(agemon) < (Common.MyCDbl(rs.Rows[0]["agec"]) * 12))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundfc"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundfc"]);
                                    isfoundrange = true;
                                }
                                break;

                        }

                    }

                    double tmpage;
                    tmpage = age;
                    if (isfoundrange == false || age > 0)
                    {
                        age = age + Common.MycInt(agemon / 100);
                        switch (sex.Trim().ToUpper())
                        {
                            case "M":
                                if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["agec"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundmc"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundmc"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["agea"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundma"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundma"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["ageo"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundmo"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundmo"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["ageE"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundmE"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundmE"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["ageF"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundmF"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundmF"]);
                                    isfoundrange = true;
                                }
                                break;
                            case "F":
                                if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["agec"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundfc"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundfc"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["agea"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundfa"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundfa"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["ageo"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundfo"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundfo"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["ageE"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundfE"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundfE"]);
                                    isfoundrange = true;
                                }
                                else if (Common.MyCDbl(age) <= Common.MyCDbl(rs.Rows[0]["ageF"]))
                                {
                                    lowbound = Common.MyCStr(rs.Rows[0]["lboundfF"]);
                                    HighBound = Common.MyCStr(rs.Rows[0]["uboundfF"]);
                                    isfoundrange = true;
                                }
                                break;

                        }
                        age = tmpage;
                    }



                    if (Common.MyLen(lowbound) == 0)
                    {
                        if (Common.MyLen(HighBound.Trim()) == 0)
                        {
                            lowbound = Common.MyCStr(rs.Rows[0]["lbound"]);
                            HighBound = Common.MyCStr(rs.Rows[0]["UBound"]);
                        }

                    }

                    retval = lowbound;

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
            return retval;
        }
    }
}
