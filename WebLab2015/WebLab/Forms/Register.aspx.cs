using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using AISWebCommon;
using System.Drawing;

namespace WebLabMaster
{
    public partial class Forms_Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            string DrName = txtName.Text;
            string AppPath = Server.MapPath("~\\");
            System.Security.AccessControl.DirectorySecurity objSecurity=new System.Security.AccessControl.DirectorySecurity();
            objSecurity=null;
            string DirAppPath=AppPath+ DrName;
            string InputHTMFilename = AppPath + "frmBlankWelcome.htm";
            string OutputHTMFileName = DirAppPath + "\\Welcome.htm";
            string InputMDBFilename = AppPath + "LabMaster.mdb";
            string OutputMDBFilename = DirAppPath + "\\LabMaster.mdb";

            if (Directory.Exists(DirAppPath))
            {
                lblMsg.Visible = true;
                //lblMsg.ForeColor = Color.BurlyWood;
                lblMsg.Text = "The name '" + DrName + "' has been assigned. Please try with unique name";
            }
            else
            {
                Directory.CreateDirectory(DirAppPath, objSecurity);
                if (!System.IO.File.Exists(OutputMDBFilename))
                {
                    File.Copy(InputMDBFilename, OutputMDBFilename);
                    File.SetAttributes(OutputMDBFilename, FileAttributes.Normal);
                }
                FileStream fs = new FileStream(InputHTMFilename, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string data = sr.ReadToEnd();
                data = data.Replace("[XNAME]", DrName);
                fs.Close();
                if (!System.IO.File.Exists(OutputHTMFileName))
                {
                    FileStream fr = new FileStream(OutputHTMFileName, FileMode.CreateNew, FileAccess.ReadWrite);
                    //fr.CanWrite = true;
                    StreamWriter sw = new StreamWriter(fr);
                    sw.Write(data);
                    sw.Flush();
                    sw.Close();
                    fr.Close();
                }

                Response.Redirect("~/" + DrName + "/Welcome.htm");
            }
        }
} 
}
