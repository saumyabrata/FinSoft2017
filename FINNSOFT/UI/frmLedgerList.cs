using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FINNSOFT
{
    public partial class frmLedgerList : MetroFramework.Forms.MetroForm
    {
        public frmLedgerList()
        {
            InitializeComponent();
        }

        private void frmLedgerList_Load(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            cmbMonth.Visible = false;
            cmbYear.Visible = false;

            fillList();
            fillyear();
            cmbViewType.Text = "Monthly";
            fillledgers();
        }

        public void fillledgers()
        {


            string qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and ANYSL=0";
            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["gl"] != null)
                ds.Tables["gl"].Clear();
            da.Fill(ds, "gl");

            qry = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "'";
            da = new SqlDataAdapter(qry, clsConnection.Conn);
            if (ds.Tables["sl"] != null)
                ds.Tables["sl"].Clear();

            da.Fill(ds, "sl");

            DataTable dt = new DataTable();
            dt.TableName = "ledgers";

            dt.Columns.Clear();
            dt.Columns.Add("ledgname");
            dt.Columns.Add("glsl");

            for (int i = 0; i < ds.Tables["gl"].Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = ds.Tables["gl"].Rows[i][0].ToString();
                dr[1] = ds.Tables["gl"].Rows[i][1].ToString();

                dt.Rows.Add(dr);
            }

            for (int i = 0; i < ds.Tables["sl"].Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = ds.Tables["sl"].Rows[i][0].ToString();
                dr[1] = ds.Tables["sl"].Rows[i][1].ToString();

                dt.Rows.Add(dr);
            }


            dt.DefaultView.Sort = "ledgname asc";

            cmbLedger.DataSource = null;
            cmbLedger.DataSource = dt;
            cmbLedger.DisplayMember = "ledgname";
            cmbLedger.ValueMember = "glsl";



            //listBox1.Items.Add("z");
            //listBox1.Items.Add("a");
            //listBox1.Items.Add("d");
            //listBox1.Items.Add("b");
            //listBox1.Sorted = true;

        }


        private void fillyear()
        {
            int yr = Convert.ToInt32(DateTime.Today.Year);

            for (int i = yr - 3; i <= yr + 5; i++)
            {
                cmbYear.Items.Add(i.ToString());
            }

        }

        private void fillList()
        {
            string QRY = "";

            QRY = "Select * from ListLedger where BrCode='" + Global.branch + "' and finyr='" + Global.finyr + "'";
            SqlDataAdapter da = new SqlDataAdapter(QRY, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["List"] != null)
                ds.Tables["List"].Clear();
            da.Fill(ds, "List");
            DataTable dt = ds.Tables["List"];
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToString(row["SL_L_NAME"]) == "")
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Ledger"].Value = row["GL_L_Name"];
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["GLID"].Value = row["GLID"];
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["SLID"].Value = row["SLID"];
                }
                else
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Ledger"].Value = row["SL_L_NAME"];
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["GLID"].Value = row["GLID"];
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["SLID"].Value = row["SLID"];
                }

            }
            //Select * from ListLedger where BrCode='" & GbrCode & "'
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //this.Hide();
            if (cmbViewType.Text == "Daily")
            {
                try
                {
                    Global.ledgyear = Convert.ToInt32(cmbYear.Text);
                }
                catch (Exception)
                {
                    Global.ledgyear = 0;
                }
                try
                {
                    Global.ledgmon = cmbMonth.SelectedIndex + 1;
                }
                catch (Exception)
                {
                    Global.ledgmon = 0;
                }
            }
            else
            {
                Global.ledgyear = 0;
                Global.ledgmon = 0;
            }
            frmLedger.mGlid = dataGridView1.Rows[e.RowIndex].Cells["GLID"].Value.ToString();
            frmLedger.mSlid = dataGridView1.Rows[e.RowIndex].Cells["SLID"].Value.ToString();
            frmLedger.mLedger = dataGridView1.Rows[e.RowIndex].Cells["LEDGER"].Value.ToString();
            frmLedger F = new frmLedger();
            F.ShowDialog();
        }

        private void cmbViewType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbViewType.Text == "Daily")
            {
                label3.Visible = true;
                label4.Visible = true;
                cmbMonth.Visible = true;
                cmbYear.Visible = true;
            }
            else
            {
                label3.Visible = false;
                label4.Visible = false;
                cmbMonth.Visible = false;
                cmbYear.Visible = false;
            }
        }

        private void cmbLedger_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string str = cmbLedger.SelectedValue.ToString();

                string glid = str.Substring(0, str.IndexOf("-"));
                string slid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));

                if (slid == "00")
                    slid = "";

                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (glid == dataGridView1.Rows[i].Cells["GLID"].Value.ToString() && slid == dataGridView1.Rows[i].Cells["SLID"].Value.ToString())
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = i;
                            //dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;

                        }
                        else
                        {
                            // dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }

            }
            catch (Exception)
            { }
        }
    }
}
