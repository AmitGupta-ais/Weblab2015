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
using System.Data.Common;
using AISWebCommon;
using System.Collections;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace WebLab
{
    public class JqGrid
    {
    }

    public class JqGridData
    {
        private int m_total;
        private int m_page;
        private int m_records;
        private List<object> m_rows;
        private List<string> m_rowshead;
        private List<string> m_coltotal;
        private Dictionary<object, object> a_userdata;
        private List<object> m_footerdata;
        public int total
        {
            get { return m_total; }
            set { m_total = value; }
        }

        public int page
        {
            get { return m_page; }
            set { m_page = value; }
        }

        public int records
        {
            get { return m_records; }
            set { m_records = value; }
        }

        public List<object> rows
        {
            get { return m_rows; }
            set { m_rows = value; }
        }

        public List<string> rowsHead
        {
            get { return m_rowshead; }
            set { m_rowshead = value; }
        }

        public List<string> col_Fortotal
        {
            get { return m_coltotal; }
            set { m_coltotal = value; }
        }

        public Dictionary<object, object> userdata
        {
            get { return a_userdata; }
            set { a_userdata = value; }
        }

        public List<object> footerdata
        {
            get { return m_footerdata; }
            set { m_footerdata = value; }
        }

        public List<object> rowsM
        {
            get { return m_rowsM; }
            set { m_rowsM = value; }
        }

        private List<object> m_rowsM;
    }

    public class JqGridDataHeading
    {
        public string name;
        public string index;
        public int width = default(int);
        public string align = default(string);
        public bool sortable = default(bool);
        public string sorttype = default(string);
        public string FooterValue = default(string);
        public string summaryType = default(string);
        public bool frozen = false;
        public string formatter = string.Empty;
    }

    public class JQGridFooter
    {
        public string[] FooterRow;
    }

    public class JQGridRow
    {
        public int id;
        public string[] cell;
    }
}
