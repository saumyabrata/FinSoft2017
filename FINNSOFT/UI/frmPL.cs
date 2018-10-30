using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions;
using CrystalDecisions.Shared;


namespace FINNSOFT
{
    public partial class frmPL : Form
    {
        public static Boolean zoomin;

        public frmPL()
        {
            InitializeComponent();
        }

        private void frmPL_Load(object sender, EventArgs e)
        {
            lblCompany.Text = Global.branch;
            lblAsAt.Text = "As on Date  " + Convert.ToString(System.DateTime.Today.ToString("dd/MM/yyyy"));//Global.gToDt.ToString("dd/MM/yyyy"));
            lblDt.Text = Convert.ToString(System.DateTime.Today.ToString("dd/MM/yyyy"));//Global.gToDt.ToString("dd/MM/yyyy"));
            mskFrom.Text = Global.gFromDt.ToString("dd/MM/yyyy");
            mskTo.Text = Global.gToDt.ToString("dd/MM/yyyy");

            if (zoomin == true)
            {
                btnRefresh_Click(null, null);
                zoomin = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            FILL_TABLE();
            FILL_GRID();
        }

        private void FILL_TABLE() 
        {
            string qry = "";
            Int32 qry_row = 0;
            SqlCommand cmd = new SqlCommand();

            //delete rcord from TblrptLEDGER table
            cmd.CommandText = "delete from TblrptLEDGER";
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            //Insert Sub TblLEDGER Openning Debit Balance into TblRptLEDGER Table ( General TblLEDGER that has SubTblLEDGER)
            qry = "INSERT INTO TblRptLEDGER ( GLID, gl_l_name, SLID, SL_L_NAME, op_dr_bal ) ";
            qry = qry + "SELECT TblGLmast.GLID, TblGLmast.GL_L_Name, TblSLmast.SLID, TblSLmast.SL_L_NAME, TblSLmast.op_bal ";
            qry = qry + "FROM TblGLmast INNER JOIN TblSLmast ON TblGLmast.GLID = TblSLmast.GLID and TblGLmast.BrCode=TblSLmast.BrCode and  TblGLmast.finyr=TblSLmast.finyr ";
            qry = qry + "WHERE (TblGLmast.pl_bs_id=3 Or TblGLmast.pl_bs_id=4) and (TblSLmast.op_bal>=0) and TblGLmast.finyr='" + Global.finyr + "' and TblGLmast.BrCode='" + Global.branch + "'";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            qry = "INSERT INTO TblRptLEDGER (GLID, gl_l_name, SLID, SL_L_NAME, op_cr_bal ) ";
            qry = qry + "SELECT TblGLmast.GLID, TblGLmast.GL_L_Name, TblSLmast.SLID, TblSLmast.SL_L_NAME, abs(TblSLmast.op_bal) ";
            qry = qry + "FROM TblGLmast INNER JOIN TblSLmast ON TblGLmast.GLID = TblSLmast.GLID and TblGLmast.BrCode=TblSLmast.BrCode and  TblGLmast.finyr=TblSLmast.finyr ";
            qry = qry + "WHERE (TblGLmast.pl_bs_id=3 Or TblGLmast.pl_bs_id=4) and (TblSLmast.op_bal<0) and TblGLmast.finyr='" + Global.finyr + "' and TblGLmast.BrCode='" + Global.branch + "'";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            //Insert General TblLEDGER Openning Debit Balance into TblRptLEDGER Table
            qry = "INSERT INTO TblRptLEDGER (GLID, gl_l_name, pl_bs_id, op_dr_bal ) ";
            qry = qry + " SELECT TblGLmast.GLID, TblGLmast.GL_L_Name, TblGLmast.PL_BS_ID, TblGLmast.op_bal ";
            qry = qry + " From TblGLmast ";
            qry = qry + " WHERE (TblGLmast.PL_BS_ID=3 Or TblGLmast.PL_BS_ID=4) AND ";
            qry = qry + " (TblGLmast.op_bal>=0)  and (TblGLmast.finyr='" + Global.finyr + "') and (TblGLmast.BrCode='" + Global.branch + "')";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            //Insert General TblLEDGER Openning Credit Balance into TblRptLEDGER Table
            qry = "INSERT INTO TblRptLEDGER ( GLID, gl_l_name, pl_bs_id, op_cr_bal ) ";
            qry = qry + " SELECT TblGLmast.GLID, TblGLmast.GL_L_Name, TblGLmast.PL_BS_ID, abs(TblGLmast.op_bal) ";
            qry = qry + " From TblGLmast ";
            qry = qry + " WHERE (TblGLmast.PL_BS_ID=3 Or TblGLmast.PL_BS_ID=4) AND ";
            qry = qry + " (TblGLmast.op_bal<0) and (TblGLmast.finyr='" + Global.finyr + "') and (TblGLmast.BrCode='" + Global.branch + "')";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            //Update SubTblLEDGER Openning Debit Balance into TblRptLEDGER Table
            //qry = "Update TblGLmast INNER JOIN (TblSLmast INNER JOIN TblRptLEDGER ON (TblSLmast.SLID = TblRptLEDGER.SLID) ";
            //qry = qry + " AND (TblSLmast.GLID = TblRptLEDGER.GLID)) ON (TblRptLEDGER.GLID = TblGLmast.GLID) AND (TblGLmast.GLID = TblSLmast.GLID) ";
            //qry = qry + " Set TblRptLEDGER.op_dr_bal = [TblSLmast].[OP_BAL] ";
            //qry = qry + " WHERE ((TblSLmast.OP_BAL>=0) AND (TblGLmast.PL_BS_ID=1 Or TblGLmast.PL_BS_ID=2)) and TblGLmast.finyr='" + Global.finyr +"' and TblGLmast.BrCode='"+ Global.branch +"'";
            //cmd.CommandText = qry;
            //cmd.Connection = clsConnection.ConnItem;
            //qry_row = cmd.ExecuteNonQuery();
            //cmd.Cancel();

            //Update SubTblLEDGER Openning Credit Balance into TblRptLEDGER Table
            //qry = "Update TblGLmast INNER JOIN (TblSLmast INNER JOIN TblRptLEDGER ON (TblSLmast.SLID = TblRptLEDGER.SLID) ";
            //qry = qry + " AND (TblSLmast.GLID = TblRptLEDGER.GLID)) ON (TblRptLEDGER.GLID = TblGLmast.GLID) AND (TblGLmast.GLID = TblSLmast.GLID) ";
            //qry = qry + "SET TblRptLEDGER.op_cr_bal = abs([TblSLmast].[OP_BAL]) ";
            //qry = qry + "WHERE ((TblGLmast.pl_bs_id=1 Or TblGLmast.pl_bs_id=2) AND TblSLmast.OP_BAL<0) and TblGLmast.finyr='" + Global.finyr +"' and TblGLmast.BrCode='"+ Global.branch +"'";
            //cmd.CommandText = qry;
            //cmd.Connection = clsConnection.ConnItem;
            //qry_row = cmd.ExecuteNonQuery();
            //cmd.Cancel();

            DateTime dt1 = Convert.ToDateTime(mskTo.Text);

            DateTime addt = dt1.AddDays(1);           

                     //Update Periodical Balance into TblRptLEDGER table from TblLEDGER table
            qry = "SELECT TblLEDGER.GLID, TblLEDGER.SLID, Sum(TblLEDGER.AMT) AS SumAMT, TblLEDGER.AMTTYPE";
            qry = qry + " FROM TblGLmast INNER JOIN (TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
            qry = qry + " (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE and TblVOUCHER.brcode=TBlLedger.brcode and TblVOUCHER.finyr=TBlLedger.finyr)) ON TblGLmast.GLID = TblLEDGER.GLID";
            qry = qry + " Where (TblVOUCHER.vdt >='" + mskFrom.Text + "' and TblVOUCHER.vdt < '" + addt + "') ";
            qry = qry + " And (TblGLmast.PL_BS_ID = 3 Or TblGLmast.PL_BS_ID = 4) and tblledger.BrCode = TblGLmast.brcode and TblLedger.BrCode='" + Global.branch + "' and TblGLmast.finyr='" + Global.finyr + "' ";
            qry = qry + " GROUP BY TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE ";
            qry = qry + " ORDER BY TblLEDGER.GLID, TblLEDGER.SLID ";
            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["Trn"] != null)
                ds.Tables["Trn"].Clear();
            da.Fill(ds, "Trn");
            DataTable dt = ds.Tables["Trn"];
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToString(row["slid"]) == "" || Convert.ToString(row["slid"]) == null || Convert.ToString(row["slid"])=="00")
                {
                    if (Convert.ToString(row["amttype"]) == "D")
                    {
                        qry = "Update TblRptLEDGER  set dr_bal=" + row["SumAmt"] + " where glid ='" + Convert.ToString(row["glid"]) + "' ";
                    }
                    else
                    {
                        qry = "Update TblRptLEDGER  set cr_bal=" + row["SumAmt"] + " where glid ='" + Convert.ToString(row["glid"]) + "' ";
                    }
                }
                else
                {
                    if (Convert.ToString(row["amttype"]) == "D")
                    {
                        qry = "Update TblRptLEDGER  set dr_bal=" + row["SumAmt"] + " where glid ='" + Convert.ToString(row["glid"]) + "' and slid='" + Convert.ToString(row["slid"]) + "' ";
                    }
                    else
                    {
                        qry = "Update TblRptLEDGER  set cr_bal=" + row["SumAmt"] + " where glid ='" + Convert.ToString(row["glid"]) + "' and slid='" + Convert.ToString(row["slid"]) + "' ";
                    }
                }
                cmd.CommandText = qry;
                cmd.Connection = clsConnection.ConnItem;
                qry_row = cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            qry = "update tblrptledger set dr_bal=0 where dr_bal is NULL";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            qry = "update tblrptledger set cr_bal=0 where cr_bal is NULL";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            qry = "update tblrptledger set op_dr_bal=0 where op_dr_bal is NULL";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            qry = "update tblrptledger set op_cr_bal=0 where op_cr_bal is NULL";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            //qry = "UPDATE TblRptLEDGER SET TblRptLEDGER.clbal = (op_dr_bal+DR_BAL)- abs(op_cr_bal)+abs(CR_BAL))";
            qry = "UPDATE TblRptLEDGER SET TblRptLEDGER.clbal = abs(DR_BAL)- abs(CR_BAL)";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();

            cmd.CommandText = "delete from TblRptLEDGER where clbal=0";
            cmd.Connection = clsConnection.ConnItem;
            qry_row = cmd.ExecuteNonQuery();
            cmd.Cancel();


        }

        private void FILL_GRID()
        {
            Int32 i = 1;
            Int32 crcount = 0;
            Int32 dbcount = 0;
            Double cre = 0; //Credit
            Double deb = 0; //Debit
            string qry = "";
            //fill data(Credit Side)
            i = 0;
            qry = "SELECT TblRptLEDGER.GLID, TblRptLEDGER.gl_l_name, TblRptLEDGER.SLID, TblRptLEDGER.SL_L_NAME, TblRptLEDGER.clbal ";
            qry = qry + "FROM TblRptLEDGER WHERE TblRptLEDGER.clbal>=0";
            qry = qry + " order by glid,slid,clbal ";
            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["Lia"] != null)
                ds.Tables["Lia"].Clear();
            da.Fill(ds, "Lia");
            DataTable dt = ds.Tables["Lia"];
            crcount = dt.Rows.Count;
            dataGridView1.Rows.Add(crcount);
            //dataGridView1.Rows.Count = crcount;
            foreach (DataRow row in dt.Rows)
            {
                dataGridView1.Rows[i].Cells[0].Value = row["glid"];
                dataGridView1.Rows[i].Cells[1].Value = row["slid"];
                if (Convert.ToString(row["sl_l_name"]) == "" || Convert.ToString(row["sl_l_name"]) == null)
                {
                    dataGridView1.Rows[i].Cells[2].Value = row["gl_l_name"];
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Value = row["sl_l_name"];
                }
                dataGridView1.Rows[i].Cells[3].Value = Math.Round(Math.Abs((Double)row["clbal"]), 2);
                cre = cre + Math.Abs((Double)row["clbal"]);
                i = i + 1;

            }


            //fill data(Debit  Side)
            i = 0;

            qry = "SELECT TblRptLEDGER.GLID, TblRptLEDGER.gl_l_name, TblRptLEDGER.SLID, TblRptLEDGER.SL_L_NAME, TblRptLEDGER.clbal ";
            qry = qry + "FROM TblRptLEDGER WHERE TblRptLEDGER.clbal<0";
            qry = qry + " order by glid,slid,clbal ";
            SqlDataAdapter da_ase = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds_ase = new DataSet();
            if (ds_ase.Tables["Ase"] != null)
                ds_ase.Tables["Ase"].Clear();
            da_ase.Fill(ds_ase, "Ase");
            DataTable dt_ase = ds_ase.Tables["Ase"];
            dbcount = dt_ase.Rows.Count;
            dataGridView2.Rows.Add(dbcount);
            //dataGridView1.Rows.Count = crcount;
            foreach (DataRow row in dt_ase.Rows)
            {
                dataGridView2.Rows[i].Cells[0].Value = row["glid"];
                dataGridView2.Rows[i].Cells[1].Value = row["slid"];
                if (Convert.ToString(row["sl_l_name"]) == "" || Convert.ToString(row["sl_l_name"]) == null)
                {
                    dataGridView2.Rows[i].Cells[2].Value = row["gl_l_name"];
                }
                else
                {
                    dataGridView2.Rows[i].Cells[2].Value = row["sl_l_name"];
                }



                dataGridView2.Rows[i].Cells[3].Value = Math.Round(Math.Abs((Double)row["clbal"]), 2);
                deb = deb + Math.Abs((Double)row["clbal"]);
                i = i + 1;

            }

            if (cre < deb)
            {
                //dataGridView2.Rows.Add();
                //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = "Net Profit Transferred to Balance Sheet";
                //dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = Convert.ToString(Math.Round(Math.Abs(cre - deb), 2));
                //lblase.Text = Convert.ToString(Math.Abs(Math.Round(cre, 2)));
                //lbllia.Text = Convert.ToString(Math.Abs(Math.Round(cre, 2)));

                double totcnt = 0; 

                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = "Net Profit Transferred to Balance Sheet";
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = Convert.ToString(Math.Round(Math.Abs(deb - cre), 2));
                totcnt = Math.Round(cre, 2) + Math.Round(Math.Abs(deb - cre), 2);
                lblase.Text = Convert.ToString(Math.Abs(Math.Round(deb, 2)));
                lbllia.Text = Convert.ToString(totcnt);

            }
            else if (deb < cre)
            {
                //dataGridView1.Rows.Add();
                //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = "Net Loss Transfer to Balance Sheet";
                //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = Convert.ToString(Math.Round(Math.Abs(deb - cre), 2));
                //lblase.Text = Convert.ToString(Math.Abs(Math.Round(deb, 2)));
                //lbllia.Text = Convert.ToString(Math.Abs(Math.Round(deb, 2)));

                dataGridView2.Rows.Add();
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = "Net Loss Transfer to Balance Sheet";
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = Convert.ToString(Math.Round(Math.Abs(cre - deb), 2));
                lblase.Text = Convert.ToString(Math.Abs(Math.Round(deb, 2)));
                lbllia.Text = Convert.ToString(Math.Abs(Math.Round(cre, 2)));
            }
            else if (deb == cre)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = "No Profit No Loss";
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = Convert.ToString("0.00");
                lblase.Text = Convert.ToString(Math.Abs(Math.Round(deb, 2)));
                lbllia.Text = Convert.ToString(Math.Abs(Math.Round(cre, 2)));
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            dt.TableName = "Debit";
            dt.Columns.Clear();

            int cnt = dataGridView1.Rows.Count;
            int cnt1 = dataGridView2.Rows.Count;

            int finalcnt = 0;
            if (cnt > cnt1)
                finalcnt = cnt;
            else if (cnt1 > cnt)
                finalcnt = cnt1;
            else
                finalcnt = cnt;

            dt.Columns.Add("DrLedger", Type.GetType("System.String"));
            dt.Columns.Add("DrAmount", Type.GetType("System.Double"));
            dt.Columns.Add("CrLedger", Type.GetType("System.String"));
            dt.Columns.Add("CrAmount", Type.GetType("System.Double"));
            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < finalcnt; i++)
            {
                DataRow dr = dt.NewRow();
                try
                {
                    dr[0] = dataGridView1.Rows[i].Cells["Column2"].Value;
                }
                catch
                {
                    dr[0] = "";
                }
                try
                {
                    dr[1] = dataGridView1.Rows[i].Cells["Column3"].Value;
                }
                catch
                {
                    dr[1] = 0;
                }
                try
                {
                    dr[2] = dataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn3"].Value;
                }
                catch
                {
                    dr[2] = "";
                }
                try
                {
                    dr[3] = dataGridView2.Rows[i].Cells["dataGridViewTextBoxColumn4"].Value;
                }
                catch
                {
                    dr[3] = 0;
                }

                dt.Rows.Add(dr);

            }
            ReportDocument rp = new ReportDocument();
            string rPath = Application.StartupPath.ToString() + "\\rptPL.rpt";
            rp.Load(rPath);
            rp.SetDataSource(dt);

            frmReportViewer rpt = new frmReportViewer();

            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            string companyname = Global.company;

            crParameterDiscreteValue.Value = companyname;
            crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["Company"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            crParameterDiscreteValue.Value = System.DateTime.Today.ToString("dd/MM/yyyy");
            crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["ToDt"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            rpt.crystalReportViewer1.ReportSource = rp;
            rpt.Text = "PL Report";
            rpt.ShowDialog();
            rpt.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;


        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
