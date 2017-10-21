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
    public partial class frmDaybook : Form
    {
        string vtype_list1 = "";
        public frmDaybook()
        {
            InitializeComponent();
        }

        private void frmDaybook_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = System.DateTime.Today;//Global.gFromDt;
            dtpTo.Value = System.DateTime.Today;//Global.gToDt;

            label3.Text = "";//Global.branch;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0)
            {
                //All Voucher
                vtype_list1 = "";
            }
            else if (listBox1.SelectedIndex == 1)
            {
                //Payment
                vtype_list1 = "Y";
            }
            else if (listBox1.SelectedIndex == 2)
            {
                //Receipt
                vtype_list1 = "R";
            }
            else if (listBox1.SelectedIndex == 3)
            {
                //Journal
                vtype_list1 = "J";
            }
            else if (listBox1.SelectedIndex == 4)
            {
                //Contra
                vtype_list1 = "T";
            }
            else if (listBox1.SelectedIndex == 5)
            {
                //Sale
                vtype_list1 = "T";
            }
            else if (listBox1.SelectedIndex == 6)
            {
                //Purchase
                vtype_list1 = "P";
            }
        }

        private void FillData()
        {
            clsGLBAL GlBAL = new clsGLBAL();
            clsGLBO GlBO = new clsGLBO();
            clsSLBAL SlBAL = new clsSLBAL();
            clsSLBO SlBO = new clsSLBO();

            dataGridView1.Columns["Debit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Credit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.Columns["Date"].DefaultCellStyle.Font = style.Font;
            dataGridView1.Columns["Type"].DefaultCellStyle.Font = style.Font;
            dataGridView1.Columns["VNO"].DefaultCellStyle.Font = style.Font;

            //Date
            //Type
            //VNo
            //Particular
            //Debit
            //Credit
            //Glid
            //Slid
            //TrnType
            //V_No
            string QRY = "";
            if (listBox1.SelectedIndex != 0)
            {
                QRY = "Select * from LedgerView where BrCode='" + Global.branch + "' and TranType='" + vtype_list1 + "' and VDT>='" + dtpFrom.Value.ToString("dd/MM/yyyy") + "'  and VDT<'" + dtpTo.Value.AddDays(1).ToString("dd/MM/yyyy") + "'  order by VDT, TranType, Vno";
            }
            else
            {
                QRY = "Select * from LedgerView where BrCode='" + Global.branch + "'and VDT>='" + dtpFrom.Value.ToString("dd/MM/yyyy") + "'  and VDT<'" + dtpTo.Value.AddDays(1).ToString("dd/MM/yyyy") + "'  order by VDT, TranType, Vno";

            }
            SqlDataAdapter da = new SqlDataAdapter(QRY, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["DayBook"] != null)
                ds.Tables["DayBook"].Clear();
            da.Fill(ds, "DayBook");
            DataTable dt = ds.Tables["DayBook"];
            string lastvno = "";
            string lastvdt = "";
            string lastvtype = "";
            Int32 i = 0;
            foreach (DataRow row in dt.Rows)
            {

                dataGridView1.Rows.Add();
                if (lastvno == Convert.ToString(row["VNO"]) && lastvdt == Convert.ToString(row["VDT"]) && lastvtype == Convert.ToString(row["TRANTYPE"]))
                {
                    i = i + 1;
                }
                else
                {

                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Date"].Value = Convert.ToDateTime(row["VDT"]).ToString("dd/MM/yyyy");
                    if (Convert.ToString(row["TRANTYPE"]) == "Y")
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Type"].Value = "Payment";
                    }
                    else if (Convert.ToString(row["TRANTYPE"]) == "R")
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Type"].Value = "Receipt";
                    }
                    else if (Convert.ToString(row["TRANTYPE"]) == "J")
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Type"].Value = "Journal";
                    }
                    else if (Convert.ToString(row["TRANTYPE"]) == "T")
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Type"].Value = "Contra";
                    }
                    else if (Convert.ToString(row["TRANTYPE"]) == "P")
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Type"].Value = "Purchase";
                    }
                    else if (Convert.ToString(row["TRANTYPE"]) == "S")
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Type"].Value = "Sale";
                    }



                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["VNo"].Value = Convert.ToString(row["VNO"]);
                }




                if ((string.IsNullOrEmpty(row["SLID"].ToString()) ? "" : row["SLID"].ToString()) == "" || row["SLID"].ToString() == "0" || row["SLID"].ToString() == "00")
                {
                    GlBO = GlBAL.GetValue(Convert.ToString(row["GLID"]));
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Particular"].Value = GlBO.GetGL_L_NAME();
                }
                else
                {
                    SlBO = SlBAL.GetValue(Convert.ToString(row["SLID"]));
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Particular"].Value = SlBO.GetSL_L_NAME(); //Convert.ToString(row["SL_L_NAME"]);
                }
                if (Convert.ToString(row["AMTTYPE"]) == "D")
                {


                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Debit"].Value = Math.Round(Convert.ToDouble(row["AMT"]), 2);
                }
                else
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Credit"].Value = Math.Round(Convert.ToDouble(row["AMT"]), 2);
                }
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Glid"].Value = Convert.ToString(row["GLID"]);
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["Slid"].Value = (string.IsNullOrEmpty(row["SLID"].ToString()) ? "" : row["SLID"]);
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["TrnType"].Value = Convert.ToString(row["TRANTYPE"]);
                dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells["V_NO"].Value = Convert.ToString(row["VNO"]);

                lastvno = Convert.ToString(row["VNO"]);
                lastvdt = Convert.ToString(row["VDT"]);
                lastvtype = Convert.ToString(row["TRANTYPE"]);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            FillData();
            calculatetotal();
        }

        private void calculatetotal()
        {
            double dr = 0, cr = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    dr = dr + Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                }
                catch (Exception)
                {
                    dr += 0;
                }

                try
                {
                    cr = cr + Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                }
                catch (Exception)
                {
                    cr += 0;
                }


            }

            lbltotdr.Text = dr.ToString();
            lbltotalcr.Text = cr.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Clear();
            dt.Columns.Add("Date", System.Type.GetType("System.String"));
            dt.Columns.Add("Type", System.Type.GetType("System.String"));
            dt.Columns.Add("VNo", System.Type.GetType("System.String"));
            dt.Columns.Add("Particular", System.Type.GetType("System.String"));
            dt.Columns.Add("Debit", System.Type.GetType("System.Double"));
            dt.Columns.Add("Credit", System.Type.GetType("System.Double"));

            if (dataGridView1.Rows.Count > 1)
            {
                this.Cursor = Cursors.WaitCursor;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    DataRow dr = dt.NewRow();
                    try
                    {
                        dr[0] = dataGridView1.Rows[i].Cells["Date"].Value;
                    }
                    catch
                    {
                        dr[0] = "";
                    }
                    try
                    {
                        dr[1] = dataGridView1.Rows[i].Cells["Type"].Value;
                    }
                    catch
                    {
                        dr[1] = "";
                    }
                    try
                    {
                        dr[2] = dataGridView1.Rows[i].Cells["VNo"].Value;
                    }
                    catch
                    {
                        dr[2] = "";
                    }
                    try
                    {
                        dr[3] = dataGridView1.Rows[i].Cells["Particular"].Value;
                    }
                    catch
                    {
                        dr[3] = "";
                    }
                    try
                    {
                        dr[4] = dataGridView1.Rows[i].Cells["Debit"].Value;
                    }
                    catch
                    {
                        dr[4] = 0;
                    }
                    try
                    {
                        dr[5] = dataGridView1.Rows[i].Cells["Credit"].Value;
                    }
                    catch
                    {
                        dr[5] = 0;
                    }

                    dt.Rows.Add(dr);

                }

                ReportDocument rp = new ReportDocument();
                string rPath = Application.StartupPath.ToString() + "\\rptDayBook.rpt";
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

                crParameterDiscreteValue.Value = dtpFrom.Value.ToString("dd/MM/yyyy");
                crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["FrmDt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                crParameterDiscreteValue.Value = dtpTo.Value.ToString("dd/MM/yyyy");
                crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
                crParameterFieldDefinition = crParameterFieldDefinitions["ToDt"];
                crParameterValues = crParameterFieldDefinition.CurrentValues;

                crParameterValues.Clear();
                crParameterValues.Add(crParameterDiscreteValue);
                crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

                rpt.crystalReportViewer1.ReportSource = rp;
                rpt.Text = "Day Book Report";
                rpt.ShowDialog();
                rpt.WindowState = FormWindowState.Maximized;
                this.Cursor = Cursors.Default;
            }

        }


    }
}
