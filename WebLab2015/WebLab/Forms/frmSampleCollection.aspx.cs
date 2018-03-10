using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AISWebCommon;
using System.Data.Common;
using WebLabMaster;
using WebLab.Classes;
using System.Collections.Generic;
using System.Web.Services;
using WebLab.UserControls;
using SautinSoft;
using System.IO;



namespace WebLab.Forms
{
    public partial class frmSampleCollection : System.Web.UI.Page
    {
        string Labno = string.Empty;
        string Pcode = string.Empty;
        string headcode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Labno = Request.QueryString["labno"].ToString();
            Pcode = Request.QueryString["pcode"].ToString();
            pcodehn.Value = Pcode;
            txtlabno.Value = Labno;
            string iscons = Common.MyCStr(Session["IsConsultant"]);
            DateTime Currdate = DateTime.Now;
            txtappdate.Value = Common.GetPrintDate(Currdate, "dd-MMM-yyyy");
            txtappby.Value = Common.MyCStr(Session["Lname"]);
            DataTable dt2 = Common.GetTableFromSession("select t.LCODE from labtest lt left outer join testlib t on t.code=lt.tcode where LABNO='LB16139424'", "tmp", null, null);
            if (dt2.Rows.Count>0)
            {
                headcode = Common.MyCStr(dt2.Rows[0]["lcode"]);
              
            }

            if (Common.MycInt(Session["IsConsultant"]) == 0)
            {
               
                string Qry = "select code,name from "+Classes.CommonFunctions.getApDb()+"consmast order by name";
                try
                {
                    DataTable dt = Common.GetTableFromSession (Qry, "Temp",null,null);
                    if (dt != null || dt.Rows.Count > 0)
                    {

                        lstConsultant.DataSource = dt;
                        lstConsultant.DataValueField = "code";
                        lstConsultant.DataTextField = "name";
                        lstConsultant.DataBind();


                    }



                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {


                }
            }
            else
            {


                string query = "Select name from  " + Classes.CommonFunctions.getApDb() + "CONSMAST  where code='" + Common.MyCStr(Session["code"]) + "'";
                string consname = String.Empty;
                try
                {

                    DataTable dt = Common.GetTableFromSession(query, "Temp2",null,null);
                    if (dt.Rows.Count > 0)
                    {
                        consname = Common.MyCStr(dt.Rows[0]["name"]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                DataTable dt1 = new DataTable();
                dt1.Columns.AddRange(new DataColumn[2] { new DataColumn("code"), new DataColumn("name") });
                dt1.Rows.Add(Common.MyCStr(Session["code"]), consname);
                lstConsultant.DataSource = dt1;
                lstConsultant.DataValueField = "code";
                lstConsultant.DataTextField = "name";
                lstConsultant.DataBind();
                lstConsultant.SelectedIndex = 0;


            }

        
            txtname.Value = Classes.CommonFunctions.getMasterValue("code",Pcode,"patient","name",true);
            if (!IsPostBack)
            {
                
            }
        }
        [WebMethod]
        public static string markSampleCollected(string labno, string pcode,string user,string conscode,string urgnorm)
        {
            string retval = string.Empty;
            string countertype = string.Empty;
            DateTime prvSampleDate=new DateTime(1888, 8, 1, 0, 0, 0);
            string manualrefno = string.Empty;
            DbConnection conn = null;
            DbTransaction trans = null;
            if (Common.MyLen(conscode)==0)
            {
                return "Please Select Collected By Consultant!";
            }
            if (Common.MycInt(Classes.CommonFunctions.getMasterValue("username", user, "userrole", "isAllowUserPerm", true)) == 1)
            {
                retval = "This user don't have permission to select sampe!";
            }
            else
            {
                if (Common.MyLen(labno) > 0)
                {
                    DataTable dt = Common.GetTableFromSession("select labno,sampledate,MANUALREFNO from labm where labno='" + labno + "'","Temp",null,null);
                    if (dt.Rows.Count > 0)
                    {
                        prvSampleDate = Common.MyCDate(dt.Rows[0]["sampledate"]);
                        manualrefno= Common.MyCStr(dt.Rows[0]["MANUALREFNO"]);

                    }
                    DateTime date1 = new DateTime(2000, 8, 1, 0, 0, 0);
                    if (DateTime.Compare(prvSampleDate, date1) > 0)
                    {
                        retval = "Sample collection for this has already been marked at " + Common.GetPrintDate(prvSampleDate, "dd/MMM/yyyy hh:mm") + " So it cannot be remarked";
                    }
                    else
                    {
                        try
                        {
                            conn = Common.GetConnectionFromSession();
                            trans = conn.BeginTransaction();

                            ColumnDataCollection coll;
                            coll = new ColumnDataCollection();                            
                            coll.Add("performby", user);
                            coll.Add("sampleCollDoct", conscode);
                            coll.Add("SampleDate", Common.GetServerDate(conn, trans));
                            //If mycboolean(gettunvar("UseMRN", "N")) Then
                            if (Common.AISCompareString(Common.gettunvar("UseMRN",conn,trans).ToUpper(),"Y")==AISCompareStringResult.AISCompareEqual)
                            {
                                if (Common.MyLen(manualrefno) == 0)
                                {
                                    DataTable mnrefdt = Common.GetTableFromSession("select b.typ from " + Classes.CommonFunctions.getApDb() + "bills b  where b.patno=(select PATNO from labm where LABNO='" + labno + "') ", "Temp", null, null);
                                    if (mnrefdt.Rows.Count > 0)
                                    {
                                        if (Common.AISCompareString(Common.MyCStr(mnrefdt.Rows[0]["typ"]).Trim().ToUpper(), "O") == AISCompareStringResult.AISCompareEqual || Common.AISCompareString(Common.MyCStr(mnrefdt.Rows[0]["typ"]).Trim().ToUpper(), "G") == AISCompareStringResult.AISCompareEqual)
                                        {
                                            countertype = "OMN";
                                        }
                                        else if (Common.AISCompareString(Common.MyCStr(mnrefdt.Rows[0]["typ"]).Trim().ToUpper(), "I") == AISCompareStringResult.AISCompareEqual)
                                        {
                                            countertype = "IMN";
                                        }
                                        if (Common.MyLen(countertype) == 0)
                                        {
                                            retval = "Counter Type To Be Generated For The Patient Cannot Be Defined.";
                                        }
                                        manualrefno =countertype+"00000";
                                       

                                    }

                                }
                                coll.Add("MANUALREFNO",Common.incval(manualrefno));



                            }
                            coll.Add("urgent", Common.MycInt(urgnorm));
                            Common.UpdateTable("labm", coll, AisUpdateTableType.Update, " labno='" + labno + "'", conn, trans);
                            trans.Commit();
                            retval = "Generated Manual Reference No. " + manualrefno.Trim();
                            if (Common.MyLen(retval) == 0)
                            {
                                retval = "Successfully marked!";
                            }
                            else
                            {
                                retval = "Successfully marked!"+retval;
                            }
                            

                        }
                        catch(Exception ex)
                        {
                            retval = ex.Message.ToString();
                        }
                        finally
                        {
                            if (conn != null && conn.State == ConnectionState.Open)
                            {
                                conn.Close();
                            }

                        }
                          

                    }
                }
              
            }

            return retval;

        }



    }
}
