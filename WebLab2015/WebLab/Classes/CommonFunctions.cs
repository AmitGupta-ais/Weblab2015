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
using AISWebCommon;
using WebLabMaster;
using System.Collections;

namespace WebLab.Classes
{
    public class CommonFunctions
    {
        static string apDataBaseName;

        public string loadImageFileFor(string testcode,string Labno,TESTCAPS ASD)
        {
            string file_num;
            long file_length ;
            long BLOCK_SIZE ;
            long num_blocks ;
            long left_over ;
            long block_num ;
            long[] bytes;
            BLOCK_SIZE = 8192;
            bool wordfileloaded;
            wordfileloaded = false;
           
            DataTable dt=Common.GetTableFromSession("select pwordfile from labtest where labno='" + Labno + "' and  tcode='" + testcode + "'","Temptab", null,null);
            if(dt.Rows.Count>0)
            {
             //string wordfilename=string.Empty;
             //   if(Common.MyCStr(rs.Fields("pwordfile")!=null) 
             //   {             
             //       wordfilename = "pict_" +Labno.Trim() + "_" & testcode.Trim();
             //       if(Convert.ToByte(dt.Rows[0]["pwordfile"]) > 0 )
             //           file_num ="";
             //           Open wordfilename For Binary As #file_num
             //           file_length = rs.Fields("pwordfile").ActualSize
             //           num_blocks = file_length \ BLOCK_SIZE
             //           left_over = file_length Mod BLOCK_SIZE
                        
             //           For block_num = 1 To num_blocks
             //               bytes() = rs!pwordfile.GetChunk(BLOCK_SIZE)
             //               Put #file_num, , bytes()
             //           Next block_num
                
             //           If left_over > 0 Then
             //               bytes() = rs!pwordfile.GetChunk(CInt(left_over))
             //               ''bytes() = rs!photo.GetChunk(BLOCK_SIZE)
             //               Put #file_num, , bytes()
             //           End If

             //           Close #file_num
             //           wordfileloaded = True
             //       End If
             //       loadImageFileFor = wordfilename
             //   }
                    
             //   End If
 
            }
            return null;
        }
        public static string convertFromRtfToHtml(string rtfstring)
        {

            string newrtf = rtfstring;
            int ipos = newrtf.IndexOf("\\par");
            if (ipos > 0)
            {
                string tmprtf = newrtf.Substring(0, ipos);
                string tmprtf2 = newrtf.Substring(ipos);
                tmprtf2 = tmprtf2.Replace(" ", "&nbsp;");
                tmprtf = tmprtf + tmprtf2;
                newrtf = tmprtf;
            }
            RtfConverter.Converter converter = new RtfConverter.Converter();
            string result = converter.ConvertRtfToHtml(newrtf);
            result = result.Replace("&amp;nbsp;", "&nbsp;");
            return result;
        }

        public static string getMasterValue(string col, string colval, string tabname, string retcol,bool isap)
        { string result = string.Empty;
            clsReport obj = new clsReport();
            apDataBaseName = obj.gettunvar("APUSERNM").Trim();
            apDataBaseName = apDataBaseName + "..";
            if (!isap)
            {
                apDataBaseName = "";
            }
            try
            {
                DataTable dt = Common.GetTableFromSession("select " + retcol + " from "+ apDataBaseName + tabname + " where " + col + "='" + colval + "'", "temp",null,null);
                if (dt.Rows.Count > 0)
                {
                    result = Common.MyCStr(dt.Rows[0][retcol]);
                }
            }
            catch (Exception ex)
            {

            }
           
            return result;
        }

        public static bool UserAllowedtoEditAfterApproval()
        {
            bool retval = false;
            //Dim retval As Boolean
            //retval = False
            //Dim rs As ADODB.Recordset
            //Set rs = New ADODB.Recordset
            //Dim isallowedit As Double
            //isallowedit = 0
            //Dim qry As String
            //qry = "select * from userrole where username ='" & user & "' "

            //OpenRecordSet rs, qry
            //If Not rs.EOF() Then

            //    isallowedit = myCdbl(rs!alloweditapprove)
            //    If isallowedit = 1 Then
            //    retval = True
            //    End If


            //End If

            //UserAllowedtoEditAfterApproval = retval

            return retval;
        }
        public static int GetApprovalUser(string Labno,out bool isapproval,string username)
        {
            bool isapp = false;
            int retval = 0;
            bool isMultipleDept = false;
            string str = string.Empty;
            string prvdcode = string.Empty;
            clsAISGraphData clsgraphdata;
            DataTable dt = Common.GetTableFromSession("select distinct(tl.dcode) as Dcode from labtest lt left outer join testlib tl on lt.tcode = tl.code where lt.labno = '" + Labno + "'", "Temp",null,null);
            foreach (DataRow dr in dt.Rows)
            {
                if (Common.MyLen(Common.MyCStr(dr["Dcode"])) > 0)
                {
                    if (Common.MyLen(prvdcode)>0)
                    {
                        isMultipleDept = true;
                        break;
                    }
                    prvdcode = Common.MyCStr(dr["Dcode"]);
                }

            }
            if (isMultipleDept)
            {
                retval = 2;
            }
            else
            {
                str = prvdcode.Trim().ToUpper() + username.Trim().ToUpper();
                Hashtable ht = getApproveUserGraphData();
                if (ht.ContainsKey(str))
                {
                    clsgraphdata = new clsAISGraphData();
                    clsgraphdata =(clsAISGraphData) ht[str];
                    if (Common.AISCompareString(clsgraphdata.grpname.Trim().ToUpper(), prvdcode.Trim().ToUpper()) == AISCompareStringResult.AISCompareEqual && Common.AISCompareString(clsgraphdata.columnname.Trim().ToUpper(), username.Trim().ToUpper()) == AISCompareStringResult.AISCompareEqual)
                    {
                        retval = 1;
                    }
                    
                }
                ht = new Hashtable();
                ht = getIsReqApproveGraphData();
                if (ht.ContainsKey(prvdcode.Trim().ToUpper()))
                {
                    clsgraphdata = new clsAISGraphData();
                    clsgraphdata= (clsAISGraphData)ht[prvdcode];
                    if (clsgraphdata.colval == 1)
                    {
                        isapp = true;
                    }
                    else
                    {
                        isapp = false;
                    }
                }

            }
            //int isapp = Common.MycInt(getMasterValue("labno",Labno,"labm", "ISAPPROVED",false));
            //if (isapp == 1)
            //{
            //    retval = 2;
            //}
            //Dim obj As clsAisGraphData
            //Set obj = New clsAisGraphData
            //Dim retval As Boolean
            //retval = False
            //Dim str As String
            //str = UCase(Trim(prvdcode)) & UCase(Trim(user))
            //If chkCollItem(CollapprovalUser, UCase(str)) Then
            //     Set obj = CollapprovalUser(UCase(Trim(str)))
            //     If UCase(Trim(obj.grpname)) = UCase(Trim(prvdcode)) And UCase(Trim(obj.columnname)) = UCase(Trim(user)) Then
            //       retval = True
            //     End If
            //End If
            //If chkCollItem(collisreqappro, UCase(Trim(mycstr(prvdcode)))) Then
            //   Set obj = collisreqappro(UCase(Trim(mycstr(prvdcode))))
            //   If myCdbl(obj.colval) = 1 Then
            //     isreqapproval = True
            //   Else
            //     isreqapproval = False
            //   End If
            //End If
            //GetApprovalUser = retval
            isapproval = isapp;

            return retval;
        }
        public static Hashtable getIsReqApproveGraphData()
        {
            clsAISGraphData clsgraphdata;            
            Hashtable rethash = new Hashtable();
            DataTable dt = Common.GetTableFromSession("Select * from dept", "detptTemp", null, null);
            foreach (DataRow dr in dt.Rows)
            {
                clsgraphdata = new clsAISGraphData();
                
                if (rethash.ContainsKey(Common.MyCStr(dr["code"]).Trim()))
                {
                }
                else
                {
                    clsgraphdata.grpname = Common.MyCStr(dr["code"]);
                    clsgraphdata.colval = Common.MycInt(dr["isreqapproval"]);
                    rethash.Add(Common.MyCStr(dr["code"]).Trim(), clsgraphdata);
                }


            }
            return rethash;

        }
        public static Hashtable getApproveUserGraphData()
        {
            clsAISGraphData clsgraphdata;
            string str = string.Empty;
            Hashtable rethash = new Hashtable();
            DataTable dt = Common.GetTableFromSession("Select * from Approval_User","tempAppUer",null,null);
            foreach (DataRow dr in dt.Rows)
            {
                clsgraphdata = new clsAISGraphData();
                str = Common.MyCStr(dr["dept"]).Trim().ToUpper() + Common.MyCStr(dr["dept"]).Trim().ToUpper();
                if (rethash.ContainsKey(str))
                {
                }
                else
                {
                    clsgraphdata.grpname = Common.MyCStr(dr["dept"]);
                    clsgraphdata.columnname = Common.MyCStr(dr["username"]);                    
                    rethash.Add(str, clsgraphdata);
                }
                
                
            }
            return rethash;
        }
        

        public Hashtable getCollAppUser()
        {
            Hashtable ht = new Hashtable();
            try
            {
               
                DataTable dt = Common.GetTableFromSession("select * from dept", "tempDept", null, null);
                foreach (DataRow dr in dt.Rows)
                {


                }
            }
            catch(Exception)
            {


            }
            
            //OpenRecordSet rs, "select * from dept"
            //  If Not rs.EOF Then
            //    Do While Not rs.EOF
            //        If chkCollItem(collisreqappro, UCase(Trim(mycstr(rs!Code)))) Then
            //        Else
            //             Set obj = New clsAisGraphData
            //             obj.grpname = mycstr(rs!Code)
            //             obj.colval = myCdbl(rs!isreqapproval)
            //             collisreqappro.Add obj, UCase(Trim(mycstr(rs!Code)))
            //        End If
            //        rs.MoveNext
            //    Loop
            //  End If
            return ht;
        }
        public static string getApDb()
        {
            string apDataBaseName=string.Empty;
            clsReport obj = new clsReport();

            
            try
            {
                apDataBaseName = obj.gettunvar("APUSERNM").Trim();
                apDataBaseName = apDataBaseName + "..";
            }
            catch (Exception ex)
            {

            }
            

            return apDataBaseName;

        }

        public static string GetUnits(string KITCODE, string TESTCODE, string units22)
        {

            string units;
            units = "";
            string query;
            query = "select Lab_KitDetail.units as kitUnits,testlib.units as units from Lab_KitDetail left outer join testlib on Lab_KitDetail.tcode=testlib.code where  Lab_KitDetail.tcode='" + TESTCODE + "' and Lab_KitDetail.kcode='" + KITCODE + "'";

            DataTable rs1 = Common.GetTableFromSession(query, "UnitTab", null, null);
            if (rs1.Rows.Count > 0)
            {
                if (Common.MyLen(Common.MyCStr(rs1.Rows[0]["kitUnits"]).Trim()) > 0)
                {
                    units = Common.MyCStr(rs1.Rows[0]["kitUnits"]);
                }
                else
                {
                    units = units22;   //'''''''''''''mycstr(rs1!units)
                }
            }
            else
            {
                units = units22;//    '''''''''''''mycstr(rs1!units)
            }
            return units;
        }





    }
}
