using System;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.Common;
using System.Xml.Linq;
using AISWebCommon;
using System.IO;
using System.Security.AccessControl;
using System.EnterpriseServices;
using System.Reflection;
using System.Collections;
using System.Xml;
using WebLabMaster;

/// <summary>
/// Summary description for Logins
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

 [System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public class Logins : System.Web.Services.WebService {

    public Logins () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
      
    }
    
               
    }
    


