using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FINNSOFT.UI;

namespace FINNSOFT.UI
{
    public partial class frmInvCorrection : Form
    {
        public frmInvCorrection()
        {
            InitializeComponent();
        }

        private void frmInvCorrection_Load(object sender, EventArgs e)
        {
            fillledgers();
        }

        private void fillledgers()
        {
            try
            {
                string qry = "select distinct Creditor_Name,GLID + '-' + SLID  from TblCreditorInvMast where finyr = '" + Global.finyr + "'" +
                    "and Brcode = '" + Global.branch + "' and rtrim(GLID) = 4 and payment_status='N'";
                SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(qry, clsConnection.Conn);
                if (ds.Tables["sl"] != null)
                    ds.Tables["sl"].Clear();
                da.Fill(ds, "sl");
                DataTable dt = new DataTable();
                dt.TableName = "ledgers";
                dt.Columns.Clear();
                dt.Columns.Add("ledgname");
                dt.Columns.Add("glsl");
                for (int i = 0; i < ds.Tables["sl"].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables["sl"].Rows[i][0].ToString();
                    dr[1] = ds.Tables["sl"].Rows[i][1].ToString();
                    dt.Rows.Add(dr);
                }
                dt.DefaultView.Sort = "ledgname asc";
                CreditorList.DataSource = null;
                CreditorList.DataSource = dt;
                CreditorList.DisplayMember = "ledgname";
                CreditorList.ValueMember = "glsl";
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
    }
}
