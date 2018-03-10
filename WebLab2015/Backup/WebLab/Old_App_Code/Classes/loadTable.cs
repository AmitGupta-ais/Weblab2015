using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using AISWebCommon;
using System.Data.SqlClient;


public class TableOperation
{
    public TableOperation()
    {


        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable getTable(string tableName, string whereCon, string columnName, string orderBy)
    {
        DbConnection conn = null;
        try
        {
            string qry = null;
            conn = Common.GetConnection("WebPatientDetails");
            if (Common.MyLen(columnName) > 0)
            {
                qry = "select " + columnName + " from " + tableName;
            }
            else
            {
                qry = "select * from " + tableName;
            }
            if (Common.MyLen((whereCon.Trim())) > 0)
            {
                qry = qry + " where " + whereCon;
            }
            if (Common.MyLen((orderBy.Trim())) > 0)
            {
                qry = qry + " order By " + orderBy;
            }
            DataTable dt = Common.GetTable(qry, tableName, conn, null);
            return dt;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    public static DataTable LoadFromSession(string tableName, string whereCon, string columnName, string orderBy)
    {
        try
        {
            string qry = null;
            
            if (Common.MyLen(columnName) > 0)
            {
                qry = "select " + columnName + " from " + tableName;
            }
            else
            {
                qry = "select * from " + tableName;
            }
            if (Common.MyLen((whereCon.Trim())) > 0)
            {
                qry = qry + " where " + whereCon;
            }
            if (Common.MyLen((orderBy.Trim())) > 0)
            {
                qry = qry + " order By " + orderBy;
            }
            DataTable dt = Common.GetTableFromSession(qry, "Labtest", null, null);
            return dt;
        }
        catch
        {
            return null;
        }
        finally
        {
            
        }
    }
    public static bool deleteRows(string tableName, string whrcolumn, string deleteIN)
    {
        DbConnection conn = null;
        try
        {
            conn = Common.GetConnection("conn");
            if (deleteIN != "")
            {

                string qry = "";
                qry = "Delete From " + tableName + " ";
                if (whrcolumn != "")
                {
                    qry = qry + " where " + whrcolumn + " in(" + deleteIN + ")";
                }
                Common.AisExecuteQuery(qry, conn);
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();
        }

    }

}
