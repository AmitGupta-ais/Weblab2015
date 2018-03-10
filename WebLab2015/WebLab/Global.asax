<%@ Application Language="C#" %>
<%@ Import Namespace = "AISWebCommon" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        ///  clsdbtype.setforisOracle();
        // Code that runs on application startup

        ////////AISWebCommon.clsShowWord objWordrep = new clsShowWord();
        ////////objWordrep.wordReport = "Welcome";
        ////////objWordrep.showReportinWord();
        /*
                string Path = Server.MapPath("~\\LabMaster.mdb");
                string PathSecond = Server.MapPath("~\\LabMaster.mdb");


                if (System.IO.File.Exists(Path))
                {
                    Common.AppPath = Path;
                    Common.ConnectionNameUsingFile = "Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path;
                }
                else
                {   
                    AISWebCommon.Common.ConnectionName = "WebLabMaster";
                }
                if (System.IO.File.Exists(PathSecond))
                {
                    Common.AppPathSecond = PathSecond;
                    Common.ConnectionNameUsingFileSecond = "Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + PathSecond;
                }
                else
                {
                    AISWebCommon.Common.ConnectionNameSecond = "WebLabMasterSecond";
                }
                ////////WebLabMaster.clsReport.SetRepoVal();*/
         WebLabMaster.clsdbtype.setforisOracle();
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
    }

    void Session_Start(object sender, EventArgs e)

    {
        // Code that runs when a new session is started
        Session["ShowReport"] = null;
        Session["labdbpaht"] = null;
        if (System.IO.File.Exists(Server.MapPath("~") + "\\StartDebug.txt"))
        {
            AISWebCommon.Common.ISINDEBUGMODE = true;
        }
        if (System.IO.File.Exists(Server.MapPath("~") + "\\EndDebug.txt"))
        {
            AISWebCommon.Common.ISINDEBUGMODE = false;
        }
       
    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
    }

</script>
