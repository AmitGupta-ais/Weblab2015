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

namespace WebLab.Classes
{
    public class CultClass
    {
        public DateTime RDate {get;set;}
        public DateTime SDate {get;set;}
        public string oiso {get;set;}
        public string colcount {get;set;}
        public string sterile {get;set;}
        public string comments {get;set;}
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
        public string spec {get;set;}
        public string sampletype {get;set;}
        public string Bloodsource {get;set;}
        public string Identification {get;set;}
        public string ReortRatio {get;set;}
        public string Method {get;set;}
        public string Headerontop { get; set; }
    }
}
