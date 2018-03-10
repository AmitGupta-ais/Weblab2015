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

namespace WebLab.Classes
{
    public class CommonFunctions
    {
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


    }
}
