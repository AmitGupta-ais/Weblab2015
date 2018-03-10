using System;
using System.Data;
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

/// <summary>
/// Summary description for clsWord
/// </summary>
public class clsWord
{
    public clsWord()
    {

    }
    public int wordCount;
    public int pdfCount;
    public void checkWord(string labno)
    {
        
        string qry = "";
        qry = "Select * from labTest where labno='" + labno + "'";
        DataTable dt;
        dt = Common.GetTableFromSession(qry, "Table", null, null);
        foreach (DataRow dr in dt.Rows)
        {
            if (Common.MyLen(dr["Tval"]) > 0)
            {
                pdfCount++;
            }
            if (Common.MyLen(dr["PwordFile"]) > 0)
            {
                wordCount++;
            }
        }

    }
}
