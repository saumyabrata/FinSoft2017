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
    public partial class frmCashbook : Form
    {
        public frmCashbook()
        {
            InitializeComponent();
        }

        private void frmCashbook_Load(object sender, EventArgs e)
        {
            fillGrid();
        }

        public void fillGrid()
        {
            dataGridView1.Rows.Add();
            int i = 0;
            i = dataGridView1.Rows.Count - 1;
            i--;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            this.dataGridView1.Columns["Particular"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView1.Columns["Debit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dataGridView1.Columns["Credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string qry2 = "Select * from TblSlMast where finyr='" + Global.finyr + "' and BrCode='" + Global.branch + "' and GLID='00001' order by SL_L_Name";
            SqlDataAdapter da2 = new SqlDataAdapter(qry2, clsConnection.Conn);
            DataSet ds2 = new DataSet();
            if (ds2.Tables["Bank"] != null)
                ds2.Tables["Bank"].Clear();
            da2.Fill(ds2, "Bank");
            DataTable dt2 = ds2.Tables["Bank"];
            foreach (DataRow row2 in dt2.Rows)
            {
                dataGridView1.Rows[i].Cells["Particular"].Value = row2["SL_L_NAME"];
                dataGridView1.Rows[i].Cells["GLID"].Value = row2["GLID"];
                dataGridView1.Rows[i].Cells["SLID"].Value = row2["SLID"];
                dataGridView1.Rows.Add();
                i = i + 1;
            }
            Int32 r = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToString(row.Cells["Particular"].Value) != "")
                {
                    fill_grid_detail(r);
                }
                r = r + 1;
            }
        }
        public void fill_grid_detail(Int32 mRow)
        {
            string QRY = "";
            double OpBal = 0;
            if (Convert.ToString(dataGridView1.Rows[mRow].Cells["SLID"].Value) != "")
            {
                QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, Month([VDT]) AS mon ";
                QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.slid = '" + Convert.ToString(dataGridView1.Rows[mRow].Cells["SLID"].Value) + "') ";
                QRY = QRY + "AND (TblLEDGER.BrCode = '" + Global.branch + "') AND (TblLEDGER.finyr = '" + Global.finyr + "')  AND (TblVOUCHER.vdt >='" + Global.gFromDt.ToString("dd/MM/yyyy") + "' and TblVOUCHER.vdt <= '" + Global.gToDt.ToString("dd/MM/yyyy") + "') ";//<='" + System.DateTime.Today.ToString("dd/MM/yyyy") + "' ";
                QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, Month(TblVOUCHER.VDT) ";
                QRY = QRY + "ORDER BY Month(TblVOUCHER.VDT)";
                SqlDataAdapter da = new SqlDataAdapter(QRY, clsConnection.Conn);
                DataSet ds = new DataSet();
                if (ds.Tables["TrnDtl"] != null)
                    ds.Tables["TrnDtl"].Clear();
                da.Fill(ds, "TrnDtl");
                DataTable dt = ds.Tables["TrnDtl"];
                foreach (DataRow row in dt.Rows)
                {
                    if (row["AMTTYPE"].ToString() == "D")
                    {
                        OpBal = OpBal + Convert.ToDouble(row["SumAmt"]);
                    }
                    else
                    {
                        OpBal = OpBal - Convert.ToDouble(row["SumAmt"]);
                    }
                }
            }
            QRY = "Select * from TblSlMast where Slid='" + Convert.ToString(dataGridView1.Rows[mRow].Cells["SLID"].Value) + "' and BrCode='" + Global.branch + "' and finyr='" + Global.finyr + "'";
            SqlDataAdapter da2 = new SqlDataAdapter(QRY, clsConnection.Conn);
            DataSet ds2 = new DataSet();
            if (ds2.Tables["Opening"] != null)
                ds2.Tables["Opening"].Clear();
            da2.Fill(ds2, "Opening");
            DataTable dt2 = ds2.Tables["Opening"];
            foreach (DataRow row2 in dt2.Rows)
            {
                OpBal = OpBal + Convert.ToDouble(row2["op_bal"]);
            }
            if (OpBal > 0)
            {
                dataGridView1.Rows[mRow].Cells["Debit"].Value = OpBal;
            }
            else if (OpBal < 0)
            {
                dataGridView1.Rows[mRow].Cells["Credit"].Value = Math.Abs(OpBal);
            }
            else
            {
                //dataGridView1.Rows[mRow].Cells["Debit"].Value = OpBal;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Global.ledgyear = 0;
            Global.ledgmon = 0;

            frmLedger.mSlid = dataGridView1.Rows[e.RowIndex].Cells["SLID"].Value.ToString();
            
            try
            {
                frmLedger.mSlid = dataGridView1.Rows[e.RowIndex].Cells["SLID"].Value.ToString();
            }
            catch (Exception)
            {
                frmLedger.mSlid = "";
            }
            frmLedger.mLedger = dataGridView1.Rows[e.RowIndex].Cells["Particular"].Value.ToString();
            frmLedger F = new frmLedger();
            F.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
