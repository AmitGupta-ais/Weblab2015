using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using AISWebCommon;
using System.Data;

namespace WebLab.Classes
{
    public class clsSubTest
    {
        public string LabID { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }
        public string Code { get; set; }
    }
    public class clsoptions
    {
        public string sno;
        public string opt;
        public string appFor; 
    }
    public class clsTestDetails
    {
        private string m_apServCode; //APSERVICE CODE FROM SERVICE
        private string m_serviceName; //TESTNAME FROM SERVICE
        private string m_testCode;
        private string m_testHead;
        private string m_testPrintName;
        private string m_testID;
        private string m_typeOftest;
        private string m_unit;
        private double m_rate;
        private string m_formula;
        private string m_comments;
        private string m_condition;
        private string m_lowval;
        private string m_highval;
        private string m_defval;
        private string m_rmk;
        private string m_lbound;
        private string m_ubound;
        private double m_rptime;
        private double m_rphrs;
        private string m_srchparam;
        private string m_prcomnt;
        private string m_method;
        private string m_presample;
        private double m_isword;
        private double m_isAppTo;
        private string m_rday;
        private double m_hideinPrint;
        private double m_hideinWS;
        private string m_grpunder;
        private bool m_ismain;
        private double m_sno;

        //html-rtf
        private string m_precaution;
        private string m_interpretation;
        //Detail Format html-rtf
        private string m_1_detFrmt;
        private string m_2_detFrmt;
        private string m_3_detFrmt;
        private string m_4_detFrmt;
        private string m_5_detFrmt;
        private string m_6_detFrmt;
        private string m_7_detFrmt;
        private string m_8_detFrmt;
        private string m_9_detFrmt;
        private string m_10_detFrmt;
        //
        private string m_1_detName;
        private string m_2_detName;
        private string m_3_detName;
        private string m_4_detName;
        private string m_5_detName;
        private string m_6_detName;
        private string m_7_detName;
        private string m_8_detName;
        private string m_9_detName;
        private string m_10_detName;

        

        private advBound m_adBund_A = new advBound();
        private advBound m_adBund_C = new advBound();
        private advBound m_adBund_O = new advBound();
        private advBound m_adBund_E = new advBound();
        private advBound m_adBund_F = new advBound();

        private advBound m_adBund_MON1 = new advBound();
        private advBound m_adBund_MON2 = new advBound();

        private advBound m_adBund_DAY1 = new advBound();
        private advBound m_adBund_DAY2 = new advBound();
        private List<clsoptions> m_options = new List<clsoptions>();


        private List<string> m_detailName = new List<string>();

        public bool isMain
        {
            get { return m_ismain; }
            set { m_ismain = value; }
        }
        public List<string> detailNames
        {
            get { return m_detailName; }
            set { m_detailName = value; }
        }

        public List<clsoptions> options
        {
            get { return m_options; }
            set { m_options = value; }
        }
        public double Sno 
        {
            get { return m_sno; }
            set { m_sno = value; }
        }


        private List<advBound> m_adBund = new List<advBound>();

        /// <summary>
        /// Insert Data in testlib 
        /// </summary>
        /// <param name="Sno">AisUpdateTableType(Insert/Update)</param>
        /// <param name="queryType">AisUpdateTableType(Insert/Update)</param>
        /// <param name="whereClause">Where Clause Applicable for Update</param>
        /// <returns></returns>
        public bool updateData(double Sno,AisUpdateTableType queryType , string whereClause)
        {
            bool retVal = false;
            DbConnection conn = null;
            DbTransaction trans = null;
            try
            {
                conn = Common.GetConnectionFromSession();
                trans = conn.BeginTransaction();
                ColumnDataCollection coll;
                coll = new ColumnDataCollection();
                coll.Add("SNO", Sno);



                if (m_testHead != null)
                {
                    coll.Add("CODE",m_testHead);
                }

                if (m_testPrintName != null)
                {
                    coll.Add("NAME",m_testPrintName);
                }

                if (m_testID != null)
                {
                    coll.Add("LCODE",m_testID);
                }

                if (m_typeOftest != null)
                {
                    coll.Add("TESTTYPE",m_typeOftest);
                }

                if (m_unit != null)
                {
                    coll.Add("UNITS",m_unit);
                }

                if (m_rate != null)
                {
                    coll.Add("RATE",m_rate);
                }

                if (m_formula != null)
                {
                    coll.Add("FORMULA", m_formula);
                }

                if (m_comments != null)
                {
                    coll.Add("COMMENTS", m_comments);
                }

                if (m_condition != null)
                {
                    coll.Add("CONDITION", m_condition);
                }

                if (m_lowval != null)
                {
                    coll.Add("LOWVAL", m_lowval);
                }

                if (m_highval != null)
                {
                    coll.Add("HIGHVAL", m_highval);
                }

                if (m_defval != null)
                {
                    coll.Add("DEFVAL", m_defval);
                }

                if (m_rmk != null)
                {
                    coll.Add("REMARKS", m_rmk);
                }

                if (m_lbound != null)
                {
                    coll.Add("LBOUND", m_lbound);
                }

                if (m_ubound != null)
                {
                    coll.Add("UBOUND", m_ubound);
                }

                if (m_rptime != null)
                {
                    coll.Add("RPTIME", m_rptime);
                }

                if (m_rphrs != null)
                {
                    coll.Add("RPHRS", m_rphrs);
                }

                if (m_srchparam != null)
                {
                    coll.Add("SEARCHPARAM", m_srchparam);
                }

                if (m_prcomnt != null)
                {
                    coll.Add("PRCOMNT", m_prcomnt);
                }

                if (m_method != null)
                {
                    coll.Add("METHOD", m_method);
                }

                if (m_presample != null)
                {
                    coll.Add("PRESAMPLE", m_presample);
                }

                if (m_isword != null)
                {
                    coll.Add("ISWORD", m_isword);
                }

                if (m_isAppTo != null)
                {
                    coll.Add("TESTAPPLICABLE", m_isAppTo);
                }

                if (m_rday != null)
                {
                    coll.Add("RDAY", m_rday);
                }
                if (hideinPrint != null)
                {
                    coll.Add("HIDENAMEINPRINT", m_hideinPrint);
                }

                if (hideinWS != null)
                {
                    coll.Add("HIDEINWS", m_hideinWS);
                }
                if (underGroup != null)
                {
                    coll.Add("GRP", m_grpunder);
                }


                if( m_adBund_A != null)
                {
                    if (m_adBund_A.AGE != null) {
                        coll.Add("AGEA", m_adBund_A.AGE);
                    }
                    if (m_adBund_A.MALE != null) {
                        if (m_adBund_A.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDMA", m_adBund_A.MALE.LOWERBOUND);
                        }
                        if (m_adBund_A.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDMA", m_adBund_A.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_A.FEMALE != null) {
                        if (m_adBund_A.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDFA", m_adBund_A.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_A.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDFA", m_adBund_A.FEMALE.UPPERBOUND);
                        }
                    }  
                }

                if ( m_adBund_C != null){
                    if (m_adBund_C.AGE != null) {
                        coll.Add("AGEC", m_adBund_C.AGE);
                    }
                    if (m_adBund_C.MALE != null) {
                        if (m_adBund_C.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDMC", m_adBund_C.MALE.LOWERBOUND);
                        }
                        if (m_adBund_C.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDMC", m_adBund_C.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_C.FEMALE != null) {
                        if (m_adBund_C.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDFC", m_adBund_C.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_C.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDFC", m_adBund_C.FEMALE.UPPERBOUND);
                        }
                    }
                }

                if ( m_adBund_O != null){
                    if (m_adBund_O.AGE != null) {
                        coll.Add("AGEO", m_adBund_O.AGE);
                    }
                    if (m_adBund_O.MALE != null) {
                        if (m_adBund_O.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDMO", m_adBund_O.MALE.LOWERBOUND);
                        }
                        if (m_adBund_O.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDMO", m_adBund_O.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_O.FEMALE != null) {
                        if (m_adBund_O.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDFO", m_adBund_O.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_O.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDFO", m_adBund_O.FEMALE.UPPERBOUND);
                        }
                    }
                }

                if ( m_adBund_E != null){
                    if (m_adBund_E.AGE != null) {
                        coll.Add("AGEE", m_adBund_E.AGE);
                    }
                    if (m_adBund_E.MALE != null) {
                        if (m_adBund_E.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDME", m_adBund_E.MALE.LOWERBOUND);
                        }
                        if (m_adBund_E.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDME", m_adBund_E.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_E.FEMALE != null) {
                        if (m_adBund_E.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDFE", m_adBund_E.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_E.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDFE", m_adBund_E.FEMALE.UPPERBOUND);
                        }
                    }
                }

                if ( m_adBund_F != null){
                    if (m_adBund_F.AGE != null) {
                        coll.Add("AGEF", m_adBund_F.AGE);
                    }
                    if (m_adBund_F.MALE != null) {
                        if (m_adBund_F.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDMF", m_adBund_F.MALE.LOWERBOUND);
                        }
                        if (m_adBund_F.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDMF", m_adBund_F.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_F.FEMALE != null) {
                        if (m_adBund_F.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBOUNDFF", m_adBund_F.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_F.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBOUNDFF", m_adBund_F.FEMALE.UPPERBOUND);
                        }
                    }
                }

                if ( m_adBund_MON1 != null){
                    if (m_adBund_MON1.AGE != null) {
                        coll.Add("AGEDAY1", m_adBund_MON1.AGE);
                    }
                    if (m_adBund_MON1.MALE != null) {
                        if (m_adBund_MON1.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBAGEINDAY1M", m_adBund_MON1.MALE.LOWERBOUND);
                        }
                        if (m_adBund_MON1.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEINDAY1M", m_adBund_MON1.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_MON1.FEMALE != null) {
                        if (m_adBund_MON1.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBAGEINDAY1F", m_adBund_MON1.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_MON1.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEINDAY1F", m_adBund_MON1.FEMALE.UPPERBOUND);
                        }
                    }
                }

                if ( m_adBund_MON2 != null){
                    if (m_adBund_MON2.AGE != null) {
                        coll.Add("AGEDAY2", m_adBund_MON2.AGE);
                    }
                    if (m_adBund_MON2.MALE != null) {
                        if (m_adBund_MON2.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBAGEINDAY2M", m_adBund_MON2.MALE.LOWERBOUND);
                        }
                        if (m_adBund_MON2.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEINDAY2M", m_adBund_MON2.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_MON2.FEMALE != null) {
                        if (m_adBund_MON2.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBAGEINDAY2F", m_adBund_MON2.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_MON2.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEINDAY2F", m_adBund_MON2.FEMALE.UPPERBOUND);
                        }
                    }
                }

                if ( m_adBund_DAY1 != null){
                    if (m_adBund_DAY1.AGE != null) {
                        coll.Add("AGEMON1", m_adBund_DAY1.AGE);
                    }
                    if (m_adBund_DAY1.MALE != null) {
                        if (m_adBund_DAY1.MALE.LOWERBOUND != null)
                        {
                            coll.Add("LBAGEMON1MALE", m_adBund_DAY1.MALE.LOWERBOUND);
                        }
                        if (m_adBund_DAY1.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEMON1MALE", m_adBund_DAY1.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_DAY1.FEMALE != null) {
                        if (m_adBund_DAY1.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBAGEMON1FEMALE", m_adBund_DAY1.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_DAY1.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEMON1FEMALE", m_adBund_DAY1.FEMALE.UPPERBOUND);
                        }
                    }
                }

                if ( m_adBund_DAY2 != null){
                    if (m_adBund_DAY2.AGE != null) {
                        coll.Add("AGEMON2", m_adBund_DAY2.AGE);
                    }
                    if (m_adBund_DAY2.MALE != null) {
                        if (m_adBund_DAY2.MALE.LOWERBOUND != null) {
                            coll.Add("LBAGEMON2MALE", m_adBund_DAY2.MALE.LOWERBOUND);
                        }
                        if (m_adBund_DAY2.MALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEMON2MALE", m_adBund_DAY2.MALE.UPPERBOUND);
                        }
                    }
                    if (m_adBund_DAY2.FEMALE != null) {

                        if (m_adBund_DAY2.FEMALE.LOWERBOUND != null)
                        {
                            coll.Add("LBAGEMON2FEMALE", m_adBund_DAY2.FEMALE.LOWERBOUND);
                        }
                        if (m_adBund_DAY2.FEMALE.UPPERBOUND != null)
                        {
                            coll.Add("UBAGEMON2FEMALE", m_adBund_DAY2.FEMALE.UPPERBOUND);
                        }
                    }
                    if (m_precaution != null)
                    {
                        coll.Add("PRECAUTIONS", m_precaution);
                    }
                    
                    if (m_interpretation != null)
                    {
                        coll.Add("DETAILS", m_interpretation);
                    }
                    if (m_1_detFrmt != null)
                    {
                        coll.Add("INT1", m_1_detFrmt);
                    }
                    if (m_2_detFrmt != null)
                    {
                        coll.Add("INT2", m_2_detFrmt);
                    }
                    if (m_3_detFrmt != null)
                    {
                        coll.Add("INT3", m_3_detFrmt);
                    }
                    if (m_4_detFrmt != null)
                    {
                        coll.Add("INT4", m_4_detFrmt);
                    }
                    if (m_5_detFrmt != null)
                    {
                        coll.Add("INT5", m_5_detFrmt);
                    }
                    if (m_6_detFrmt != null)
                    {
                        coll.Add("INT6", m_6_detFrmt);
                    }
                    if (m_7_detFrmt != null)
                    {
                        coll.Add("INT7", m_7_detFrmt);
                    }
                    if (m_8_detFrmt != null)
                    {
                        coll.Add("INT8", m_8_detFrmt);
                    }
                    if (m_9_detFrmt != null)
                    {
                        coll.Add("INT9", m_9_detFrmt);
                    }
                    if (m_10_detFrmt != null)
                    {
                        coll.Add("INT10", m_10_detFrmt);
                    }
                    if (m_1_detName != null)
                    {
                        coll.Add("NAME1", m_1_detName);
                    }
                    if (m_2_detName != null)
                    {
                        coll.Add("NAME2", m_2_detName);
                    }
                    if (m_3_detName != null)
                    {
                        coll.Add("NAME3", m_3_detName);
                    }
                    if (m_4_detName != null)
                    {
                        coll.Add("NAME4", m_4_detName);
                    }
                    if (m_5_detName != null)
                    {
                        coll.Add("NAME5", m_5_detName);
                    }
                    if (m_6_detName != null)
                    {
                        coll.Add("NAME6", m_6_detName);
                    }
                    if (m_7_detName != null)
                    {
                        coll.Add("NAME7", m_7_detName);
                    }
                    if (m_8_detName != null)
                    {
                        coll.Add("NAME8", m_8_detName);
                    }
                    if (m_9_detName != null)
                    {
                        coll.Add("NAME9", m_9_detName);
                    }
                    if (m_10_detName != null)
                    {
                        coll.Add("NAME10", m_10_detName);
                    }

                }
                Common.UpdateTable("testlib",coll, queryType , whereClause ,conn,trans);
                string delqry = "delete from TESTLINT where CODE ='" + m_testHead + "'";
                Common.AisExecuteQuery(delqry,conn,trans);
                ColumnDataCollection coll2 = null;
                //int k = Common.MyLen(options);
                for (int i = 0; i < options.Count; i++)
                {
                    if(Common.MyLen(options[i].opt)>0)
                    {
                        coll2 = new ColumnDataCollection();
                        coll2.Add("CODE", m_testHead);
                        coll2.Add("OPTIONID", options[i].opt);
                        coll2.Add("SNO", options[i].sno);
                        coll2.Add("TESTAPPLICABLE", options[i].appFor);
                        Common.UpdateTable("TESTLINT", coll2, AisUpdateTableType.Insert, "", conn, trans);
                    }
                }

                
                
                trans.Commit();
                retVal = true;

        }
            catch (Exception ex)
            {
                retVal = false;
                trans.Rollback();
                throw ex;
                
            }

            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    trans = null;
                    conn.Close();
                }
            }
            return retVal;
        }



        public string detailFrmt1
        {
            get { return m_1_detFrmt; }
            set { m_1_detFrmt = value; }
        }
        public string detailFrmt2
        {
            get { return m_2_detFrmt; }
            set { m_2_detFrmt = value; }
        }
        public string detailFrmt3
        {
            get { return m_3_detFrmt; }
            set { m_3_detFrmt = value; }
        }
        public string detailFrmt4
        {
            get { return m_4_detFrmt; }
            set { m_4_detFrmt = value; }
        }
        public string detailFrmt5
        {
            get { return m_5_detFrmt; }
            set { m_5_detFrmt = value; }
        }
        public string detailFrmt6
        {
            get { return m_6_detFrmt; }
            set { m_6_detFrmt = value; }
        }
        public string detailFrmt7
        {
            get { return m_7_detFrmt; }
            set { m_7_detFrmt = value; }
        }
        public string detailFrmt8
        {
            get { return m_8_detFrmt; }
            set { m_8_detFrmt = value; }
        }
        public string detailFrmt9
        {
            get { return m_9_detFrmt; }
            set { m_9_detFrmt = value; }
        }
        public string detailFrmt10
        {
            get { return m_10_detFrmt; }
            set { m_10_detFrmt = value; }
        }

        public string detailName1
        {
            get { return m_1_detName; }
            set { m_1_detName = value; }
        }
        public string detailName2
        {
            get { return m_2_detName; }
            set { m_2_detName = value; }
        }
        public string detailName3
        {
            get { return m_3_detName; }
            set { m_3_detName = value; }
        }
        public string detailName4
        {
            get { return m_4_detName; }
            set { m_4_detName = value; }
        }
        public string detailName5
        {
            get { return m_5_detName; }
            set { m_5_detName = value; }
        }
        public string detailName6
        {
            get { return m_6_detName; }
            set { m_6_detName = value; }
        }
        public string detailName7
        {
            get { return m_7_detName; }
            set { m_7_detName = value; }
        }
        public string detailName8
        {
            get { return m_8_detName; }
            set { m_8_detName = value; }
        }
        public string detailName9
        {
            get { return m_9_detName; }
            set { m_9_detName = value; }
        }
        public string detailName10
        {
            get { return m_10_detName; }
            set { m_10_detName = value; }
        }

        public string interpretation
        {
            get { return m_interpretation; }
            set { m_interpretation = value; }
        }
        public string precaution
        {
            get { return m_precaution; }
            set { m_precaution = value; }
        }
        public string underGroup
        {
            get { return m_grpunder; }
            set { m_grpunder = value; }
        }
        public double hideinPrint
        {
            get { return m_hideinPrint; }
            set { m_hideinPrint = value; }
        }
        public double hideinWS
        {
            get { return m_hideinWS; }
            set { m_hideinWS = value; }
        }
        public List<advBound> YRS
        {
            get { return m_adBund; }
            set { m_adBund = value; }
        }


        public advBound DAY1
        {
            get { return m_adBund_DAY1; }
            set { m_adBund_DAY1 = value; }
        }
        public advBound DAY2
        {
            get { return m_adBund_DAY2; }
            set { m_adBund_DAY2 = value; }
        }
        public advBound MON1
        {
            get { return m_adBund_MON1; }
            set { m_adBund_MON1 = value; }
        }
        public advBound MON2
        {
            get { return m_adBund_MON2; }
            set { m_adBund_MON2 = value; }
        }

        public advBound YRSC
        {
            get { return m_adBund_C; }
            set { m_adBund_C = value; }
        }
        public advBound YRSA
        {
            get { return m_adBund_A; }
            set { m_adBund_A = value; }
        }
        public advBound YRSO
        {
            get { return m_adBund_O; }
            set { m_adBund_O = value; }
        }
        public advBound YRSE
        {
            get { return m_adBund_E; }
            set { m_adBund_E = value; }
        }
        public advBound YRSF
        {
            get { return m_adBund_F; }
            set { m_adBund_F = value; }
        }


        public double testApplyTo   //0 fro All 1 For Male 2 For Female
        {
            get { return m_isAppTo; }
            set { m_isAppTo = value; }
        }
        public double isword
        {
            get { return m_isword; }
            set { m_isword = value; }
        }
        public string srchparam
        {
            get { return m_srchparam; }
            set { m_srchparam = value; }
        }
        public double time
        {
            get { return m_rptime; }
            set { m_rptime = value; }
        }
        public double hours
        {
            get { return m_rphrs; }
            set { m_rphrs = value; }
        }
        public string prntcomment
        {
            get { return m_prcomnt; }
            set { m_prcomnt = value; }
        }
        public string method
        {
            get { return m_method; }
            set { m_method = value; }
        }
        public string rday
        {
            get { return m_rday; }
            set { m_rday = value; }
        }
        public string presample
        {
            get { return m_presample; }
            set { m_presample = value; }
        }
        public string lowbound
        {
            get { return m_lbound; }
            set { m_lbound = value; }
        }
        public string upbound
        {
            get { return m_ubound; }
            set { m_ubound = value; }
        }
        public string defaultvalue
        {
            get { return m_defval; }
            set { m_defval = value; }
        }
        public string remark
        {
            get { return m_rmk; }
            set { m_rmk = value; }
        }
        public string highValue
        {
            get { return m_highval; }
            set { m_highval = value; }
        }
        public string lowValue
        {
            get { return m_lowval; }
            set { m_lowval = value; }
        }
        public string condition
        {
            get { return m_condition; }
            set { m_condition = value; }
        }
        public string comments
        {
            get { return m_comments; }
            set { m_comments = value; }
        }
        public string formula
        {
            get { return m_formula; }
            set { m_formula = value; }
        }
        public string apservcode
        {
            get { return m_apServCode; }
            set { m_apServCode = value; }
        }

        public string servname
        {
            get { return m_serviceName; }
            set { m_serviceName = value; }
        }
        public string testcode
        {
            get { return m_testCode; }
            set { m_testCode = value; }
        }
        public string testhead
        {
            get { return m_testHead; }
            set { m_testHead = value; }
        }
        public string printname
        {
            get { return m_testPrintName; }
            set { m_testPrintName = value; }
        }
        public string testid
        {
            get { return m_testID; }
            set { m_testID = value; }
        }
        public string testtyp
        {
            get { return m_typeOftest; }
            set { m_typeOftest = value; }
        }
        public string unit
        {
            get { return m_unit; }
            set { m_unit = value; }
        }
        
        public double rate
        {
            get { return m_rate; }
            set { m_rate = value; }
        }

    }

    public class person
    {
        private double _AGE;
        private string _lowerB;
        private string _upperB;
        public double AGE
        {
            get { return _AGE; }
            set { _AGE = value; }
        }
        public string LOWERBOUND
        {
            get { return _lowerB; }
            set { _lowerB = value; }
        }
        public string UPPERBOUND
        {
            get { return _upperB; }
            set { _upperB = value; }
        }
    }

    public class advBound
    {
        private double _AGE;
        private person _M = new person();
        private  person _F = new person();
        public double AGE
        {
            get { return _AGE; }
            set { _AGE = value; }
        }
        public person MALE
        {
            get { return _M; }
            set { _M = value; }
        }
        public person FEMALE
        {
            get { return _F; }
            set { _F = value; }
        }
    }

   
}