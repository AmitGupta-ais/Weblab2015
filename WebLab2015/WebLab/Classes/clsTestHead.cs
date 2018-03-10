using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLab.Classes
{
    public class clsTestHead
    {
        private string _code;
        private string _testname;
        private string _hname;
        private string _frmula;
        private string _condition;
        private string _preComm;
        private string _SIGNATORY;
        private string _REMARKS;


        private double _ept;
        private double _sno;
        private double _NOL;
        private double _isCulTyp;


        private bool _UNITREQ;
        private bool _RANGEREQ;
        private bool _PRINTREQ;
        private bool _HEADREQ;
        private bool _ISUPLOADED;
        private bool _REQMETHOD;
        private bool _REQAPPROVAL;
        private bool _TESTINBOX;
        private bool _ISMODIFIED;
        private bool _REQPREVRES;
        private bool _ISLOWHIGH;
        private bool _REQBKEDTEST;


        public string Code{ get { return _code; }set { _code = value; } }
        public string TestName { get { return _testname; } set { _testname = value; } }
        public string HNAME { get { return _hname; } set { _hname = value; } }
        public string CONDITION { get { return _condition; }set{ _condition = value; } }
        public string PRECOMMENT { get { return _preComm; } set { _preComm = value; } }
        public string SIGNATORY { get { return _SIGNATORY; } set { _SIGNATORY = value; } }
        public string REMARKS { get { return _REMARKS; } set { _REMARKS = value; } }
        public string FORMULA { get { return _frmula; } set { _frmula = value; } }



        public double ESTIMATE { get { return _ept; } set { _ept = value; } }
        public double numLINES { get { return _NOL; } set { _NOL = value; } }
        public double Sno { get { return _sno; } set { _sno = value; } }
        public double ISCULTTYPE { get { return _isCulTyp; } set { _isCulTyp = value; } }



        public bool UNITREQ { get { return _UNITREQ; } set { _UNITREQ = value; } }
        public bool REFRANGE { get { return _RANGEREQ; } set { _RANGEREQ = value; } }
        public bool REQPRINT { get { return _PRINTREQ; } set { _PRINTREQ = value; } }
        public bool REQHEAD { get { return _HEADREQ; } set { _HEADREQ = value; } }
        public bool isUPLD { get { return _ISUPLOADED; } set { _ISUPLOADED = value; } }
        public bool REQMETHOD { get { return _REQMETHOD; } set { _REQMETHOD = value; } }
        public bool REQAPPROV { get { return _REQAPPROVAL; } set { _REQAPPROVAL = value; } }
        public bool TEXTINBOX { get { return _TESTINBOX; } set { _TESTINBOX = value; } }
        public bool ISMOD { get { return _ISMODIFIED; } set { _ISMODIFIED = value; } }
        public bool REQPREV { get { return _REQPREVRES; } set { _REQPREVRES = value; } }
        public bool ISLOWHI { get { return _ISLOWHIGH; } set { _ISLOWHIGH = value; } }
        public bool REQBOOKED { get { return _REQBKEDTEST; } set { _REQBKEDTEST = value; } }






    }
}