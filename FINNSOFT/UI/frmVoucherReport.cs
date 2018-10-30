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
    public partial class frmVoucherReport : MetroFramework.Forms.MetroForm
    {
        public frmVoucherReport()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void fillledgers()
        {
            string qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "'";
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

        private void frmVoucherReport_Load(object sender, EventArgs e)
        {
            fillledgers();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            string str = cmbLedger.SelectedValue.ToString();

            string glid = str.Substring(0, str.IndexOf("-"));
            string slid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));
            string qry = "";

            if (slid == "00" || slid == "0" || slid == "")
            {
                if (comboBox1.Text == "Only This Ledger")
                {
                    qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID  from TblLEDGER a, TblVOUCHER b " +
                        "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and a.glid = " + glid +
                        " and vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                        toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "'";
                }
                else if (comboBox1.Text == "Total Voucher")
                {
                    qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID  from TblLEDGER a, TblVOUCHER b " +
                       "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and (CONVERT(varchar, b.vno)+b.TRANTYPE) in (" +
                       "select (CONVERT(varchar,vno)+TRANTYPE) from TblLEDGER where glid = " + glid + ")" +
                       " and vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                        toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "'";
                }
            }
            else
            {
                if (comboBox1.Text == "Only This Ledger")
                {
                    qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID  from TblLEDGER a, TblVOUCHER b " +
                        "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and a.glid = " + glid +
                        " and a.slid = " + slid +
                        " and vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                        toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "'";
                }
                else if (comboBox1.Text == "Total Voucher")
                {
                    qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID  from TblLEDGER a, TblVOUCHER b " +
                       "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and (CONVERT(varchar, b.vno)+b.TRANTYPE) in (" +
                       "select (CONVERT(varchar,vno)+TRANTYPE) from TblLEDGER where glid = " + glid + " and slid = " + slid + ")" +
                       " and vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                        toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "'";
                }
            }

            this.Cursor = Cursors.WaitCursor;
            string companyname = Global.company;

            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["voucher1"] != null)
                ds.Tables["voucher1"].Clear();
            da.Fill(ds, "voucher1");

            DataTable dt = new DataTable();
            dt.TableName = "voucher";
            dt.Columns.Clear();
            dt.Columns.Add("Vno");
            dt.Columns.Add("vdt");
            dt.Columns.Add("TRANTYPE");
            dt.Columns.Add("InventoryVNo");
            dt.Columns.Add("CAMT");
            dt.Columns.Add("AMTTYPE");
            dt.Columns.Add("VName");
            dt.Columns.Add("DAMT");

            for (int i = 0; i < ds.Tables["voucher1"].Rows.Count; i++)
            {
                string glid1 = ds.Tables["voucher1"].Rows[i]["GLID"].ToString();
                string slid1 = ds.Tables["voucher1"].Rows[i]["SLID"].ToString();

                string tablename = "TblSLmast";
                string fieldname = "SL_L_NAME";
                string whereclause = " SLID = " + slid1 + " and GLID = " + glid1;

                if (slid1 == "00" || slid1 == "" || slid1 == "0")
                {
                    tablename = "TblGLmast";
                    fieldname = "GL_L_Name";
                    whereclause = " GLID = " + glid1;
                }

                qry = "select " + fieldname + " from " + tablename + " where " + whereclause;
                SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
                string name = Convert.ToString(com.ExecuteScalar());

                DataRow dr = dt.NewRow();
                dr[0] = ds.Tables["voucher1"].Rows[i]["vno"].ToString();
                dr[1] = ds.Tables["voucher1"].Rows[i]["vdt"].ToString();

                string trantype = ds.Tables["voucher1"].Rows[i]["TRANTYPE"].ToString();
                if (trantype == "J")
                    dr[2] = "Journal";
                else if (trantype == "Y")
                    dr[2] = "Payment";
                else if (trantype == "R")
                    dr[2] = "Receipt";
                else if (trantype == "T")
                    dr[2] = "Contra";
                
                //dr[2] = ds.Tables["voucher1"].Rows[i]["TRANTYPE"].ToString();
                
                string amttype = ds.Tables["voucher1"].Rows[i]["AMTTYPE"].ToString();
                if (amttype == "D")
                {
                    dr[7] = Math.Round(Convert.ToDouble(ds.Tables["voucher1"].Rows[i]["AMT"].ToString()),2);
                    dr[4] = 0;
                }
                else
                {
                    dr[7] = 0;
                    dr[4] = Math.Round(Convert.ToDouble(ds.Tables["voucher1"].Rows[i]["AMT"].ToString()),2);
                }

                dr[6] = name;
                dr[3] = ds.Tables["voucher1"].Rows[i]["InventoryVNo"].ToString();

                dt.Rows.Add(dr);
            }

            //Display Report

            ReportDocument rp = new ReportDocument();
            string rPath = Application.StartupPath.ToString() + "\\rptVoucher.rpt";
            rp.Load(rPath);

           // rp.DataDefinition.FormulaFields["LedgerName"].Text = cmbLedger.Text;
         //   rp.DataDefinition.FormulaFields["brname"].Text =  Global.branch;
           // rp.DataDefinition.FormulaFields["fin_period"].Text = "\"" + Global.gFromDt.ToString("dd/MM/yyyy") + " - " + Global.gToDt.ToString("dd/MM/yyyy") + "\"";

            rp.SetDataSource(dt);

            frmReportViewer rpt = new frmReportViewer();

            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            crParameterDiscreteValue.Value = companyname;
            crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["Company"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            rpt.crystalReportViewer1.ReportSource = rp;
            rpt.Text = "Voucher Report";
            rpt.ShowDialog();
            rpt.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }
    }
}
