using AISWebCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WebLab.Classes;
using WebLabMaster;

namespace WebLab.Forms
{
    /// <summary>
    /// Summary description for webLabTest
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class webLabTest : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string isLabType(string Code,string Dept)
        {
            string response=String.Empty;
            string retval = Classes.CommonFunctions.getMasterValue("Code",Code,"Dept", "IsLabType", true);
            if (Common.AISCompareString(retval.ToUpper(), "Y") != AISCompareStringResult.AISCompareEqual & Common.AISCompareString(retval.ToUpper(), "N") != AISCompareStringResult.AISCompareEqual)
            {
                response = "Please Set isLabType in the department master to indicte whether this department is Lab Type or Imaging Type";
            }
            else if(Common.AISCompareString(retval.ToUpper(), "Y") != AISCompareStringResult.AISCompareEqual)
            {
                response = "true";
            }
            return response;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsTestDetails loadData(string apCode, string Code)
        {
            clsTestDetails retData = new clsTestDetails();

            string STRapUSERNAME="";
            clsReport tem = new clsReport();
            STRapUSERNAME = tem.gettunvar("APUSERNM").Trim();
            STRapUSERNAME += "..";
            tem = null;



            string qry1 = "select Code ,Name,grp,rate from " + STRapUSERNAME + "service where CODE = '" + apCode + "'";
            string qry2 = "select * from testlib where code='" + Code + "'";


            DataTable dt1 = Common.GetTableFromSession(qry1, "service", null, null);
            DataTable dt2 = Common.GetTableFromSession(qry2, "Data", null, null);
            if (Common.MyLen(apCode) == 0)
            {
                dt1 = null;
                string q1 = "select ACODE as Code,'' as Name from " + STRapUSERNAME + "maplab where LCODE = '" + Code.Trim() + "'";
                dt1 = Common.GetTableFromSession(q1, "", null, null);
            }


            {
                if (dt1.Rows.Count > 0)
                {
                    retData.apservcode = Common.MyCStr(dt1.Rows[0]["Code"]).Trim();
                    retData.servname = Common.MyCStr(dt1.Rows[0]["Name"]).Trim();
                }

                if (dt2.Rows.Count > 0)
                {

                    retData.rate = Common.MyCDbl(dt2.Rows[0]["rate"]);
                    retData.testid = Common.MyCStr(dt2.Rows[0]["CODE"]).Trim();
                    retData.testhead = Common.MyCStr(dt2.Rows[0]["LCODE"]).Trim();
                    retData.printname = Common.MyCStr(dt2.Rows[0]["NAME"]).Trim();
                    retData.testtyp = Common.MyCStr(dt2.Rows[0]["testtype"]).Trim();
                    retData.unit = Common.MyCStr(dt2.Rows[0]["units"]).Trim();
                    retData.lowValue = Common.MyCStr(dt2.Rows[0]["LOWVAL"]).Trim();
                    retData.highValue = Common.MyCStr(dt2.Rows[0]["HIGHVAL"]).Trim();
                    retData.defaultvalue = Common.MyCStr(dt2.Rows[0]["DEFVAL"]).Trim();
                    retData.remark = Common.MyCStr(dt2.Rows[0]["REMARKS"]).Trim();
                    retData.condition = Common.MyCStr(dt2.Rows[0]["CONDITION"]).Trim();
                    retData.comments = Common.MyCStr(dt2.Rows[0]["COMMENTS"]).Trim();
                    retData.formula = Common.MyCStr(dt2.Rows[0]["FORMULA"]).Trim();
                    retData.lowbound = Common.MyCStr(dt2.Rows[0]["LBOUND"]).Trim();
                    retData.upbound = Common.MyCStr(dt2.Rows[0]["UBOUND"]).Trim();
                    retData.time = Common.MyCDbl(dt2.Rows[0]["RPTIME"]);
                    retData.hours = Common.MyCDbl(dt2.Rows[0]["RPHRS"]);
                    retData.prntcomment = Common.MyCStr(dt2.Rows[0]["PRCOMNT"]).Trim();
                    retData.method = Common.MyCStr(dt2.Rows[0]["METHOD"]).Trim();
                    retData.presample = Common.MyCStr(dt2.Rows[0]["PRESAMPLE"]).Trim();
                    retData.srchparam = Common.MyCStr(dt2.Rows[0]["SEARCHPARAM"]).Trim();
                    retData.isword = Common.MyCDbl(dt2.Rows[0]["ISWORD"]);
                    retData.testApplyTo = Common.MyCDbl(dt2.Rows[0]["testapplicable"]);
                    retData.rday = Common.MyCStr(dt2.Rows[0]["rday"]);
                    retData.hideinPrint = Common.MyCDbl(dt2.Rows[0]["HIDENAMEINPRINT"]);
                    retData.hideinWS = Common.MyCDbl(dt2.Rows[0]["HIDEINWS"]);
                    retData.underGroup = Common.MyCStr(dt2.Rows[0]["GRP"]);




                    retData.YRSA.AGE = Common.MyCDbl(dt2.Rows[0]["AGEA"]);
                    retData.YRSA.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDMA"]).Trim();
                    retData.YRSA.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDMA"]).Trim();
                    retData.YRSA.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDFA"]).Trim();
                    retData.YRSA.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDFA"]).Trim();

                    retData.YRSC.AGE = Common.MyCDbl(dt2.Rows[0]["AGEC"]);
                    retData.YRSC.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDMC"]).Trim();
                    retData.YRSC.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDMC"]).Trim();
                    retData.YRSC.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDFC"]).Trim();
                    retData.YRSC.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDFC"]).Trim();

                    retData.YRSO.AGE = Common.MyCDbl(dt2.Rows[0]["AGEO"]);
                    retData.YRSO.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDMO"]).Trim();
                    retData.YRSO.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDMO"]).Trim();
                    retData.YRSO.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDFO"]).Trim();
                    retData.YRSO.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDFO"]).Trim();

                    retData.YRSE.AGE = Common.MyCDbl(dt2.Rows[0]["AGEE"]);
                    retData.YRSE.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDME"]).Trim();
                    retData.YRSE.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDME"]).Trim();
                    retData.YRSE.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDFE"]).Trim();
                    retData.YRSE.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDFE"]).Trim();

                    retData.YRSF.AGE = Common.MyCDbl(dt2.Rows[0]["AGEF"]);
                    retData.YRSF.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDMF"]).Trim();
                    retData.YRSF.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDMF"]).Trim();
                    retData.YRSF.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBOUNDFF"]).Trim();
                    retData.YRSF.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBOUNDFF"]).Trim();


                    retData.DAY1.AGE = Common.MyCDbl(dt2.Rows[0]["AGEDAY1"]);
                    retData.DAY1.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEINDAY1M"]).Trim();
                    retData.DAY1.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEINDAY1M"]).Trim();
                    retData.DAY1.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEINDAY1F"]).Trim();
                    retData.DAY1.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEINDAY1F"]).Trim();

                    retData.DAY2.AGE = Common.MyCDbl(dt2.Rows[0]["AGEDAY2"]);
                    retData.DAY2.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEINDAY2M"]).Trim();
                    retData.DAY2.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEINDAY2M"]).Trim();
                    retData.DAY2.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEINDAY2F"]).Trim();
                    retData.DAY2.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEINDAY2F"]).Trim();

                    retData.MON1.AGE = Common.MyCDbl(dt2.Rows[0]["AGEMON1"]);
                    retData.MON1.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEMON1MALE"]).Trim();
                    retData.MON1.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEMON1MALE"]).Trim();
                    retData.MON1.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEMON1FEMALE"]).Trim();
                    retData.MON1.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEMON1FEMALE"]).Trim();

                    retData.MON2.AGE = Common.MyCDbl(dt2.Rows[0]["AGEMON2"]);
                    retData.MON2.MALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEMON2MALE"]).Trim();
                    retData.MON2.MALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEMON2MALE"]).Trim();
                    retData.MON2.FEMALE.LOWERBOUND = Common.MyCStr(dt2.Rows[0]["LBAGEMON2FEMALE"]).Trim();
                    retData.MON2.FEMALE.UPPERBOUND = Common.MyCStr(dt2.Rows[0]["UBAGEMON2FEMALE"]).Trim();


                    retData.precaution = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["PRECAUTIONS"]).Trim());
                    retData.interpretation = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["DETAILS"]).Trim());

                    retData.detailFrmt1 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT1"]).Trim());
                    retData.detailFrmt2 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT2"]).Trim());
                    retData.detailFrmt3 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT3"]).Trim());
                    retData.detailFrmt4 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT4"]).Trim());
                    retData.detailFrmt5 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT5"]).Trim());
                    retData.detailFrmt6 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT6"]).Trim());
                    retData.detailFrmt7 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT7"]).Trim());
                    retData.detailFrmt8 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT8"]).Trim());
                    retData.detailFrmt9 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT9"]).Trim());
                    retData.detailFrmt10 = CommonFunctions.convertFromRtfToHtml(Common.MyCStr(dt2.Rows[0]["INT10"]).Trim());



                    retData.detailName1 = (Common.MyCStr(dt2.Rows[0]["NAME1"]).Trim());
                    retData.detailName2 = (Common.MyCStr(dt2.Rows[0]["NAME2"]).Trim());
                    retData.detailName3 = (Common.MyCStr(dt2.Rows[0]["NAME3"]).Trim());
                    retData.detailName4 = (Common.MyCStr(dt2.Rows[0]["NAME4"]).Trim());
                    retData.detailName5 = (Common.MyCStr(dt2.Rows[0]["NAME5"]).Trim());
                    retData.detailName6 = (Common.MyCStr(dt2.Rows[0]["NAME6"]).Trim());
                    retData.detailName7 = (Common.MyCStr(dt2.Rows[0]["NAME7"]).Trim());
                    retData.detailName8 = (Common.MyCStr(dt2.Rows[0]["NAME8"]).Trim());
                    retData.detailName9 = (Common.MyCStr(dt2.Rows[0]["NAME9"]).Trim());
                    retData.detailName10 = (Common.MyCStr(dt2.Rows[0]["NAME10"]).Trim());


                    for (int i = 1; i <= 10; i++)
                    {
                        string colName = "NAME" + i;
                        retData.detailNames.Add((Common.MyCStr(dt2.Rows[0][colName]).Trim()));
                    }





                    string query = "select  * from TESTLINT where CODE = '" + Code + "'";
                    DataTable tbl = Common.GetTableFromSession(query, "", null, null);
                    if (tbl.Rows.Count > 0)
                    {
                        foreach (DataRow dr in tbl.Rows)
                        {
                            clsoptions obj = new clsoptions();
                            obj.sno = Common.MyCStr(dr["SNO"]);
                            obj.opt = Common.MyCStr(dr["OPTIONID"]);
                            obj.appFor = Common.MyCStr(dr["TESTAPPLICABLE"]);
                            retData.options.Add(obj);
                        }
                    }


                }
            }

            return retData;
        }

        public  bool isSubTest(string Code)
        {
            bool isSubTest = false;
            string qry = "select scode,(select max(name)from testlib where CODE = '" + Code + "') Name,(select max(method) from testlib where CODE = '" + Code + "' ) METHOD from Testlibtr where MCODE = '" + Code + "' AND SCODE<> '" + Code + "' order by SNO";
            DataTable dt = Common.GetTableFromSession(qry, "", null, null);
            if (dt.Rows.Count > 0)
            {
                isSubTest = true;
            }
            return isSubTest;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<clsSubTest> subTest(string labid,string apServCode)
        {
            List<clsSubTest> coll = new List<clsSubTest>();
            string qry = "select SCODE,(select max(name)from testlib where CODE = '" + labid + "') NAME,(select max(method) from testlib where CODE = '" + labid + "' ) METHOD  from Testlibtr where MCODE = '" + labid + "' AND SCODE<> '" + labid + "' order by SNO";
            DataTable dt = Common.GetTableFromSession(qry, "", null, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                clsSubTest obj = new clsSubTest();
                obj.LabID = Common.MyCStr(dt.Rows[i]["SCODE"]);
                obj.Name = Common.MyCStr(dt.Rows[i]["NAME"]);
                obj.Method = Common.MyCStr(dt.Rows[i]["METHOD"]);
                obj.Code = apServCode;
                coll.Add(obj);
            }
            return coll;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public  string Validate(string apCode, string Dept, string Code)
        {
            string response = String.Empty;

            string retval = Classes.CommonFunctions.getMasterValue("Code", Dept, "Dept", "IsLabType", false);


            if (isSubTest(Code))
            {
                response = "2";
            }


            else
            {
                if (Common.AISCompareString(retval.ToUpper(), "Y") != AISCompareStringResult.AISCompareEqual & Common.AISCompareString(retval.ToUpper(), "N") != AISCompareStringResult.AISCompareEqual)
                {
                    response = "-1";//Please Set isLabType in the department master to indicte whether this department is Lab Type or Imaging Type
                }
                else if (Common.AISCompareString(retval.ToUpper(), "Y") != AISCompareStringResult.AISCompareEqual)
                {
                    response = "1";
                }
                else if (Common.AISCompareString(retval.ToUpper(), "Y") == AISCompareStringResult.AISCompareEqual)
                {
                    response = "0";
                }
            }
            return response;
        }

        public  string HTMLtoRTF(string sHTML)
        {

            RtfConverter.Converter converter = new RtfConverter.Converter();
            string result = converter.ConvertHtmlToRtf(sHTML);
            return result;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public  bool saveData(clsTestDetails obj)
        {
            bool retVal = false;
            string msg = String.Empty;
            if (obj == null)
            {
                retVal = false;
            }
            else
            {
                if (obj.interpretation != "<p><br data-mce-bogus=\"1\"></p>")///<p><br data-mce-bogus=\"1\"></p> is the case of blank data
                { obj.interpretation = HTMLtoRTF(obj.interpretation); }
                else { obj.interpretation = ""; }
                if (obj.precaution != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.precaution = HTMLtoRTF(obj.precaution); }
                else { obj.precaution = ""; }
                if (obj.detailFrmt1 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt1 = HTMLtoRTF(obj.detailFrmt1); }
                else { obj.detailFrmt1 = ""; }
                if (obj.detailFrmt2 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt2 = HTMLtoRTF(obj.detailFrmt2); }
                else { obj.detailFrmt2 = ""; }
                if (obj.detailFrmt3 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt3 = HTMLtoRTF(obj.detailFrmt3); }
                else { obj.detailFrmt3 = ""; }
                if (obj.detailFrmt4 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt4 = HTMLtoRTF(obj.detailFrmt4); }
                else { obj.detailFrmt4 = ""; }
                if (obj.detailFrmt5 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt5 = HTMLtoRTF(obj.detailFrmt5); }
                else { obj.detailFrmt5 = ""; }
                if (obj.detailFrmt6 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt6 = HTMLtoRTF(obj.detailFrmt6); }
                else { obj.detailFrmt6 = ""; }
                if (obj.detailFrmt7 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt7 = HTMLtoRTF(obj.detailFrmt7); }
                else { obj.detailFrmt7 = ""; }
                if (obj.detailFrmt8 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt8 = HTMLtoRTF(obj.detailFrmt8); }
                else { obj.detailFrmt8 = ""; }
                if (obj.detailFrmt9 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt9 = HTMLtoRTF(obj.detailFrmt9); }
                else { obj.detailFrmt9 = ""; }
                if (obj.detailFrmt10 != "<p><br data-mce-bogus=\"1\"></p>")
                { obj.detailFrmt10 = HTMLtoRTF(obj.detailFrmt10); }
                else { obj.detailFrmt10 = ""; }


                if (obj.isword == 1)
                {
                    if (Common.AISCompareString("PARAGRAPH", obj.testtyp.Trim()) == AISCompareStringResult.AISCompareEqual)
                    {

                    }
                    else
                    {
                        msg = "Word Editor can only be used in Paragraph type of tests";
                    }
                }

                if (Common.MyLen(obj.testhead) == 0)
                {
                    msg = "Test Head Cannot be blank";
                }
                if (Common.MyLen(obj.testid) == 0)
                {
                    msg = "Please Enter a Code";
                }



                try
                {
                    string qry = "SELECT MAX(SNO) SNO FROM TESTLIB WHERE LCODE='" + obj.testid + "'"; /*and GRP = '"+obj.testhead+"'"*/
                    DataTable dt = Common.GetTableFromSession(qry, "", null, null);
                    double SNO = Common.MyCDbl(dt.Rows[0]["SNO"]);
                    //SNO++;
                    string where = " Code='" + obj.testhead + "' ";
                    retVal = obj.updateData(SNO, AisUpdateTableType.Update, where);
                }
                catch (Exception ex)
                {
                    retVal = false;
                }
                finally
                {

                }
            }

            return retVal;
        }






        ///TEST HEAD CALLS
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public  List<clsTestHead> loadTestHead(string dept)
        {
            List<clsTestHead> testHead = new List<clsTestHead>();
            string qry = "select CODE,NAME,DCODE,SNO FROM TESTLIBHEAD WHERE DCODE = '" + dept.Trim() + "' order by SNO ";
            if (Common.AISCompareString(dept, "ALL") == AISCompareStringResult.AISCompareEqual)
            {
                qry = "select CODE,NAME,DCODE,SNO FROM TESTLIBHEAD  order by SNO";
            }
            DataTable dtTestHead = Common.GetTableFromSession(qry, "testHead", null, null);
            if (dtTestHead.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTestHead.Rows)
                {
                    clsTestHead obj = new clsTestHead();
                    obj.Sno = Common.MyCDbl(dr["SNO"]);
                    obj.Code = Common.MyCStr(dr["CODE"]);
                    obj.TestName = Common.MyCStr(dr["NAME"]);
                    testHead.Add(obj);
                }
            }
            return testHead;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public  List<clsTestDetails> loadTestUnderHead(string Code)
        {
            List<clsTestDetails> testsUnder = new List<clsTestDetails>();
            string qry = "select CODE,SNO,NAME,LBOUND,UBOUND,UNITS,ISMAIN from testlib where LCODE='" + Code + "'";
            DataTable dt = Common.GetTableFromSession(qry, "testUnderHead", null, null);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    clsTestDetails obj = new clsTestDetails();
                    obj.Sno = Common.MyCDbl(dr["SNO"]);
                    obj.testcode = Common.MyCStr(dr["CODE"]);
                    obj.testhead = Common.MyCStr(dr["NAME"]);
                    obj.lowbound = Common.MyCStr(dr["LBOUND"]);
                    obj.upbound = Common.MyCStr(dr["UBOUND"]);
                    obj.unit = Common.MyCStr(dr["UNITS"]);
                    obj.isMain = Common.mycboolean(Common.MyCStr(dr["ISMAIN"]));

                    testsUnder.Add(obj);
                }
            }


            return testsUnder;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsTestHead editTest(string Code)
        {
            clsTestHead obj = new Classes.clsTestHead();
            string qry = "SELECT * FROM TESTLIBHEAD WHERE CODE='" + Code + "'";
            DataTable dt = Common.GetTableFromSession(qry,"HeadTest",null,null);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj.Code = Common.MyCStr(dt.Rows[i]["CODE"]);
                    obj.TestName = Common.MyCStr(dt.Rows[i]["NAME"]);
                    obj.HNAME = Common.MyCStr(dt.Rows[i]["HNAME"]);
                    obj.numLINES = Common.MyCDbl(dt.Rows[i]["NOL"]);
                    obj.ISCULTTYPE = Common.MyCDbl(dt.Rows[i]["ISCULTURETYPE"]);
                    obj.FORMULA = Common.MyCStr(dt.Rows[i]["FORMULA"]);
                    obj.UNITREQ = Common.mycboolean(Common.MyCStr(dt.Rows[i]["UNITREQ"]));
                    //obj.SIGNATORY = 
                }
            }


            return obj;
        }

        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<clsTestHead> loadTestHead(string dept)
        //{
        //    List<clsTestHead> testHead = new List<clsTestHead>();
        //    string qry = "select CODE,NAME,DCODE,SNO FROM TESTLIBHEAD WHERE DCODE = '" + dept.Trim() + "' order by SNO ";
        //    DataTable dtTestHead = Common.GetTableFromSession(qry, "testHead", null, null);
        //    if (dtTestHead.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtTestHead.Rows)
        //        {
        //            clsTestHead obj = new clsTestHead();
        //            obj.Sno = Common.MyCDbl(dr["SNO"]);
        //            obj.Code = Common.MyCStr(dr["CODE"]);
        //            obj.TestName = Common.MyCStr(dr["NAME"]);
        //            testHead.Add(obj);
        //        }
        //    }
        //    return testHead;
        //}
        //[WebMethod(EnableSession = true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<clsTestDetails> loadTestUnderHead(string Code)
        //{
        //    List<clsTestDetails> testsUnder = new List<clsTestDetails>();
        //    string qry = "select CODE,SNO,NAME,LBOUND,UBOUND,UNITS,ISMAIN from testlib where LCODE='" + Code + "'";
        //    DataTable dt = Common.GetTableFromSession(qry, "testUnderHead", null, null);
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            clsTestDetails obj = new clsTestDetails();
        //            obj.Sno = Common.MyCDbl(dr["SNO"]);
        //            obj.testcode = Common.MyCStr(dr["CODE"]);
        //            obj.testhead = Common.MyCStr(dr["NAME"]);
        //            obj.lowbound = Common.MyCStr(dr["LBOUND"]);
        //            obj.upbound = Common.MyCStr(dr["UBOUND"]);
        //            obj.unit = Common.MyCStr(dr["UNITS"]);
        //            obj.isMain = Common.mycboolean(Common.MyCStr(dr["ISMAIN"]));

        //            testsUnder.Add(obj);
        //        }
        //    }


        //    return testsUnder;
        //}
    }
}
