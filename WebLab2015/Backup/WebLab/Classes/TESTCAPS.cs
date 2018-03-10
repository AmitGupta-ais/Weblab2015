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
using System.Collections;
using System.Collections.Generic;

namespace WebLab.Classes
{
    public class TESTCAPS
    {
        public int mvarTOP  {get;set;}
        public int mvarlft { get; set; }
        public string mvarfldname { get; set; }
        int mvarHEIGHT {get;set;}
        public int mvarWDTH { get; set; }
        public string mvartyp { get; set; }
        public string mvarTYPTEST { get; set; }
        public string mvarTVAL { get; set; }
        public string mvarLBND { get; set; }
        public string mvarUBND { get; set; }
        public string mvarLOWVAL { get; set; }
        public string mvarHIGHVAL { get; set; }
        public string mvarFORMULA { get; set; }
        public string mvarLABNO { get; set; }
        public string mvarcaption { get; set; }
        public string mvarlcode { get; set; }
        public string mvarbcode { get; set; }
        public int mvarNO { get; set; } //'local copy
        public string mvarDEFVAL { get; set; }
        public string mvarOPTION1 { get; set; }
        public string mvarOPTION2 { get; set; }
        public string mvarOPTION3 { get; set; }
        public string mvarOPTION4 { get; set; }
        public string mvarOPTION5 { get; set; }
        public string mvarINT1 { get; set; }
        public string mvarINT2 { get; set; }
        public string mvarINT3 { get; set; }
        public string mvarINT4 { get; set; }
        public string mvarINT5 { get; set; }
        public string mvarINT6 { get; set; }
        public string mvarISPERF { get; set; }
        public string fldName { get; set; }
        public string caption { get; set; }
        public string FORMULA { get; set; }
        public string HIGHVAL { get; set; }
        public string LOWVAL { get; set; }
        public string Labno { get; set; }
        public List<clsTableRowData> colltabulardata { get; set; }
        // mvarLABELCONTROL As Control
        // mvarHIGHCONTROL As Control
        // mvarLOWCONTROL As Control
        // mvarCURRENTCONTROL As Control
        public string Imagestring { get; set; }
        public string mvarcurrval { get; set; }
        public string mvarCOMMENTS { get; set; }
        public string mvarint7 { get; set; }
        public string mvarint8 { get; set; }
        public string mvarint9 { get; set; }
        public string mvarint10 { get; set; }
        public string mvarDeftable { get; set; }
        public string mvarismain { get; set; }
        public string mvarcondition { get; set; }
        public string mvarImagestring { get; set; }
        public int pgno { get; set; }
        public ArrayList collspec;
        public string SHORTRESULT {get;set;}
        public List<CultClass> cultureResult;
        public List<string> cultmedcoll;
        //public unitcontrol As Control
        public string unit {get;set;}
        public string wordfilename {get;set;}
        public bool hasWord {get;set;}
        public string file1 {get;set;}
        public string file2 {get;set;}
        public string file3 {get;set;}
        public string file4 {get;set;}
        public string file5 {get;set;}
        public string file6 {get;set;}
        public string tipfile1 {get;set;}
        public string tipfile2 {get;set;}
        public string tipfile3 {get;set;}
        public string tipfile4 {get;set;}
        public string tipfile5 {get;set;}
        public string tipfile6 {get;set;}
        public int inWorksheetViewRowno {get;set;}
        public int inWorksheetViewColno {get;set;}
        public string inWorksheetViewPatname {get;set;}
        public string inWorksheetViewTestName {get;set;}
        public bool inWorksheetViewisUpdated {get;set;}
        public string inWorksheetViewRefName {get;set;}
        public string shortname {get;set;}
        public string CreateUser {get;set;}
        public string moduser {get;set;}
        public  DateTime ModDate {get;set;}
        public bool labCancel {get;set;}
        public string remarkswithTEST {get;set;}
        public DateTime ExpectedReportingdate {get;set;}
        public string testlibmethod {get;set;}
        public string labtestmethod {get;set;}
        public double reportDays {get;set;}
        public DateTime bookingdate {get;set;}
        public string Groupunder {get;set;}
        public int IsAbNormalCase {get;set;}
        public bool IsCritical {get;set;}
        public string prvValueforTest {get;set;}
        public bool islong {get;set;}
        public int ISKIT {get;set;}
        public string KITCODE {get;set;}
        public int isMachPerf {get;set;}
        public bool machPerf {get;set;}
        public DateTime sampledate {get;set;}
        public bool issampledrawn {get;set;}
        public string pcode {get;set;}
        public string aptestcode {get;set;}
        public string TMPNARRFORWORKSHEET {get;set;}
        public bool isdoubleunit {get;set;}
        public string doubleunitformula {get;set;}
        public bool IsBold {get;set;}
        public bool ImageChanged {get;set;}
        public clsTableRowData List;

        public string LBND { get; set; }
        public string lcode { get; set; }
        public string UBND { get; set; }
        public string HBOUND { get; set; }

        public string ismain { get; set; }
        public string comments { get; set; }

        public string typtest { get; set; }
        public string KitCode { get; set; }
        public string typ {get;set;}
        
        public string Option1 {get;set;}
        public string Option2 {get;set;}
        public string Option3 {get;set;}
        public string Option4 {get;set;}
        public string Option5 {get;set;}
        public string int1 {get;set;}
        public string int2 {get;set;}
        public string int3 {get;set;}
        public string int4 {get;set;}
        public string int5 {get;set;}
        public string int6 {get;set;}
        public string int7 {get;set;}
        public string int8 {get;set;}
        public string int9 {get;set;}
        public string int10 {get;set;}
        public string DEFVAL {get;set;}
        public string ISPERF {get;set;}
        public string condition {get;set;}
        public int no {get;set;}
        public string bcode { get; set; }
        public string tval { get; set; }
        public string CURRVAL { get; set; }
        public string Deftable { get; set; }

                                          
    }
}
