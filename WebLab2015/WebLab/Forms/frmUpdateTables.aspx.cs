using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using AISWebCommon;
using System.IO;

public partial class Forms_frmUpdateTables : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetTables();
        ////SetTestLibTables();
    }

    public void SetTables()
    {
        bool isTableCreate = false;
        string createdtable = "";
        DbConnection con = Common.GetConnectionFromSession();
        ArrayList col = new ArrayList();
        
        #region For WebReportSettings Table

        col.Add("HDR text null");
        col.Add("FTR text null");
        col.Add("isUploaded int null");
        col.Add("islogo int null");
        col.Add("logoht number null");
        col.Add("logolft number null");
        col.Add("logotp number null");
        col.Add("logowid number null");
        col.Add("hideTstOnly float null");
        isTableCreate = Common.changeTableACCESS(col, "WebReportSettings", con);

        con.Close();
        col.Clear();
        if (isTableCreate)
        {
            createdtable += "WebReportSettings";
        }
        #endregion

        if (Common.MyLen(createdtable) > 0)
        {
            createdtable = "Created table(s) are : " + createdtable;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", @"alert('" + createdtable + "');", true);
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", @"alert('No new table created');", true);
        }
    }

    ////public void SetTestLibTables()
    ////{
    ////    bool isTableCreate = false;
    ////    string createdtable = "";
    ////    DbConnection con = Common.GetConnectionFromSession();
    ////    ArrayList col = new ArrayList();

    ////    #region For Testlib Table

    ////    col.Add("agemon1 float null");
    ////    col.Add("lbagemon1male varchar(15) null");
    ////    col.Add("ubagemon1male varchar(15) null");
    ////    col.Add("lbagemon1female varchar(15) null");
    ////    col.Add("ubagemon1female varchar(15) null");
    ////    col.Add("agemon2 float null");
    ////    col.Add("lbagemon2male varchar(15) null");
    ////    col.Add("ubagemon2male varchar(15) null");
    ////    col.Add("lbagemon2female varchar(15) null");
    ////    col.Add("ubagemon2female varchar(15) null");

    ////    isTableCreate = Common.changeTableACCESS(col, "Testlib", con);

    ////    con.Close();
    ////    col.Clear();
    ////    if (isTableCreate)
    ////    {
    ////        createdtable += "TestLib";
    ////    }
    ////    #endregion

    ////    if (Common.MyLen(createdtable) > 0)
    ////    {
    ////        createdtable = "Created table(s) are : " + createdtable;
    ////        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", @"alert('" + createdtable + "');", true);
    ////    }
    ////    else
    ////    {
    ////        Page.ClientScript.RegisterStartupScript(this.GetType(), "Message", @"alert('No new table created');", true);
    ////    }
    ////}
}
