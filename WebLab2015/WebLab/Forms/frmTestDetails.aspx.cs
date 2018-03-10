using AISWebCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLab.Classes;
using WebLab.UserControls;
using WebLabMaster;

namespace WebLab.Forms
{
    [System.Web.Script.Services.ScriptService]
    public partial class frmTestDetails : System.Web.UI.Page
    {
        string username;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["lname"] == null) && (Common.MyCStr(Session["lname"]).Trim().Length == 0))
            {
                Response.Redirect("~/frmcommonlogin.aspx",false);
                username = Common.MyCStr(Session["lname"]);
            }
        }
        public static bool isSubTest(string Code)
        {
            bool isSubTest = false;
            string qry = "select scode,(select max(name)from testlib where CODE = '" + Code + "') Name,(select max(method) from testlib where CODE = '" + Code + "' ) METHOD from Testlibtr where MCODE = '" + Code + "' AND SCODE<> '"+Code+"' order by SNO";
            DataTable dt = Common.GetTableFromSession(qry, "", null, null);
            if (dt.Rows.Count > 0)
            {
                isSubTest = true;
            }
            return isSubTest;
        }
         public static string HTMLtoRTF(string sHTML)
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
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Validate(string apCode, string Dept,string Code)
        {
            string response = String.Empty;

            string retval = Classes.CommonFunctions.getMasterValue("Code", Dept, "Dept", "IsLabType", false);
            
            
                if (isSubTest(Code)) {
                    response = "sub";
                }
            

            else
            {
                if (Common.AISCompareString(retval.ToUpper(), "Y") != AISCompareStringResult.AISCompareEqual & Common.AISCompareString(retval.ToUpper(), "N") != AISCompareStringResult.AISCompareEqual)
                {
                    response = "false";//Please Set isLabType in the department master to indicte whether this department is Lab Type or Imaging Type
                }
                else if (Common.AISCompareString(retval.ToUpper(), "Y") != AISCompareStringResult.AISCompareEqual)
                {
                    response = "true";
                }
                else if (Common.AISCompareString(retval.ToUpper(), "Y") == AISCompareStringResult.AISCompareEqual)
                {
                    response = "new";
                }
            }
            return response;
        }

        public void load()
        {
            
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static clsTestDetails loadData(string apCode, string Dept, string Code)
        {
            clsTestDetails retData = new clsTestDetails();

            string STRapUSERNAME;
            clsReport tem = new clsReport();
            STRapUSERNAME = tem.gettunvar("APUSERNM").Trim();
            STRapUSERNAME += "..";
            tem = null;
            


            string qry1 = "select Code ,Name,grp,rate from "+STRapUSERNAME+"service where CODE = '" + apCode + "'";
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
                        foreach(DataRow dr in tbl.Rows)
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
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static bool saveData(clsTestDetails obj)
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
                { if (Common.AISCompareString("PARAGRAPH",obj.testtyp.Trim())==AISCompareStringResult.AISCompareEqual)
                    {
                        
                    }
                    else
                    { 
                        msg = "Word Editor can only be used in Paragraph type of tests";
                    }
                }
               
                if (Common.MyLen(obj.testhead) == 0)
                {
                    msg= "Test Head Cannot be blank";
                }
                if (Common.MyLen(obj.testid) == 0)
                {
                    msg= "Please Enter a Code";
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
    }
}