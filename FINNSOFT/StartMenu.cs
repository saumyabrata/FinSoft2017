using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FINNSOFT.UI;
using LiveCharts; //Core of the library
using LiveCharts.Wpf; //The WPF controls
using LiveCharts.WinForms; //the WinForm wrappers
using System.Data.SqlClient;
using System.Globalization;


namespace FINNSOFT
{
    public partial class StartMenu : MetroFramework.Forms.MetroForm
    {
        private int childFormNumber = 0;

        public StartMenu()
        {
            InitializeComponent();

            
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void StartMenu_Load(object sender, EventArgs e)
        {
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    if (ctl is MdiClient)
                    {
                        ctl.BackColor = Color.White;
                    }
                }
                catch (Exception a)
                {
                }
            }
            lblUser.Text = Global.username;
            lblBranchName.Text = Global.branch;
            lblFinYear.Text = Global.finyr;
            Cursor.Current = Cursors.WaitCursor;
            RefreshAnalysisChart();
            PieChart();
            FillBalance();
            MapChart();
            VolumeChart();
            CashChart();
            Cursor.Current = Cursors.Default;
        }

        private void RefreshAnalysisChart()
        {
            SqlCommand command1 = new SqlCommand("usp_salesanalysis", clsConnection.Conn);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.Add(new SqlParameter("@brcode", SqlDbType.Char, 0, "BrCode"));
            command1.Parameters["@brcode"].Value = Global.branch.Trim();
            command1.ExecuteNonQuery();
            command1.Dispose();

            SqlCommand command2 = new SqlCommand("usp_volumeanalysis", clsConnection.Conn);
            command2.CommandType = CommandType.StoredProcedure;
            command2.Parameters.Add(new SqlParameter("@brcode", SqlDbType.Char, 0, "BrCode"));
            command2.Parameters["@brcode"].Value = Global.branch.Trim();
            command2.ExecuteNonQuery();
            command2.Dispose();


        }
        private void FillBalance()
        {
            SqlCommand command1 = new SqlCommand("pCollectionView", clsConnection.Conn);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.Add(new SqlParameter("@cash_curbal", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 2, "@cash_curbal", DataRowVersion.Default, null));
            command1.Parameters.Add(new SqlParameter("@chq_curbal", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 2, "@chq_curbal", DataRowVersion.Default, null));
            command1.Parameters.Add(new SqlParameter("@card_curbal", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 2, "@card_curbal", DataRowVersion.Default, null));
            command1.Parameters.Add(new SqlParameter("@cust_curbal", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 2, "@cust_curbal", DataRowVersion.Default, null));

            command1.Parameters.Add(new SqlParameter("@targetdate", SqlDbType.DateTime, 0, "VDT"));
            command1.Parameters.Add(new SqlParameter("@mbrcode", SqlDbType.Char, 0, "BrCode"));
            command1.Parameters["@targetdate"].Value = System.DateTime.Today.ToString("dd/MM/yyyy");
            command1.Parameters["@mbrcode"].Value = Global.branch.Trim();
            command1.UpdatedRowSource = UpdateRowSource.OutputParameters;
            command1.ExecuteNonQuery();
            metroTile8.Text = command1.Parameters["@cash_curbal"].Value.ToString();
            metroTile12.Text = command1.Parameters["@chq_curbal"].Value.ToString();
            metroTile13.Text = command1.Parameters["@card_curbal"].Value.ToString();
            metroTile14.Text = command1.Parameters["@cust_curbal"].Value.ToString();

        }

        private void PieChart()
        {
            Func<ChartPoint, string> labelPoint = chartPoint =>
            string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            pieChart1.Series = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Gold",
                Values = new ChartValues<double> {3},
                PushOut = 15,
                DataLabels = true,
                LabelPoint = labelPoint
            },
            new PieSeries
            {
                Title = "Silver",
                Values = new ChartValues<double> {4},
                DataLabels = true,
                LabelPoint = labelPoint
            },
            new PieSeries
            {
                Title = "Gem",
                Values = new ChartValues<double> {6},
                DataLabels = true,
                LabelPoint = labelPoint
            },
            new PieSeries
            {
                Title = "Diamond",
                Values = new ChartValues<double> {2},
                DataLabels = true,
                LabelPoint = labelPoint
            }
        };

            pieChart1.LegendLocation = LegendLocation.Bottom;
        }

        private void MapChart()
        {
            cmbometal.SelectedIndex = 0;
            string sMonth = DateTime.Now.ToString("MMMM");
            int index = cmbomonth.Items.IndexOf(sMonth);
            cmbomonth.SelectedValue = sMonth;
            cmboyear.SelectedIndex = 0;
            string mon = null;
            string myear = null;
            string chartsql = null;
            int i;

            chartsql = "select yearh,month,revenue from TBLSALESANALYSIS where month= '" + index + "' and Category='" + cmbometal.Items[0] + "' group by month,yearh,revenue order by yearh,month";
            SqlCommand CMD = new SqlCommand(chartsql,clsConnection.Conn);
            SqlDataReader RDR = CMD.ExecuteReader();
            ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<Double>(), LabelPoint = point => point.Y.ToString() };
            Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };

            ax.Labels = new List<string>();
            while (RDR.Read())
            {
                
                col.Values.Add(Convert.ToDouble(RDR["revenue"]));
               
                i = Convert.ToInt32(RDR["month"].ToString());
                mon = GetMonthName(i); //int month converted to string month name
                myear=mon+"-"+ Convert.ToString(RDR["yearh"].ToString());
                ax.Labels.Add(myear);
            }

            cartesianChart1.Series.Add(col);
            cartesianChart1.AxisX.Add(ax);
            cartesianChart1.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString(),
                Separator = new Separator()
            });
            lblchart.Text = "YoY Gold Sales Analysis";
            RDR.Close();
            CMD.Dispose();
        }

        private void VolumeChart()
        {
            cmbometal.SelectedIndex = 0;
            string sMonth = DateTime.Now.ToString("MMMM");
            int index = cmbomonth.Items.IndexOf(sMonth);
            cmbomonth.SelectedValue = sMonth;
            cmboyear.SelectedIndex = 0;
            string mon = null;
            string myear = null;
            string chartsql = null;
            int i;

            chartsql = "select yearh,month,Purity,Netwt from TBLVolumeAnalysis where month= '" + index + "' and Catagory='" + cmbometal.Items[0] + "' and purity=22 group by month,yearh,NetWt,Purity order by yearh,month";
            SqlCommand CMD = new SqlCommand(chartsql, clsConnection.Conn);
            SqlDataReader RDR = CMD.ExecuteReader();
            ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<Double>(), LabelPoint = point => point.Y.ToString() };
            Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };

            ax.Labels = new List<string>();
            while (RDR.Read())
            {

                col.Values.Add(Convert.ToDouble(RDR["Netwt"]));

                i = Convert.ToInt32(RDR["month"].ToString());
                mon = GetMonthName(i); //int month converted to string month name
                myear = mon + "-" + Convert.ToString(RDR["yearh"].ToString());
                ax.Labels.Add(myear);
            }

            cartesianChart2.Series.Add(col);
            cartesianChart2.AxisX.Add(ax);
            cartesianChart2.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString(),
                Separator = new Separator()
            });
            lblchartvolume.Text = "YoY Gold Sales in Volume Analysis";
            RDR.Close();
            CMD.Dispose();
        }

        private void CashChart()
        {
            
            string sMonth = DateTime.Now.ToString("MMMM");
            int index = cmbomonth.Items.IndexOf(sMonth);
            cmbomonth.SelectedValue = sMonth;
            cmboyear.SelectedIndex = 0;
            string mon = null;
            string myear = null;
            string chartsql = null;
            int i;

            chartsql = "select[year],[month],dramt,cramt from View_CashBank where Brcode='" + Global.branch + "' and(([year]= left('" + Global.finyr + "', 4) AND [month] BETWEEN 4 and 12) " +
                        "OR ([year]= right('" + Global.finyr + "', 4) AND [month] BETWEEN 1 and 3)) order by year,month";
            SqlCommand CMD = new SqlCommand(chartsql, clsConnection.Conn);
            SqlDataReader RDR = CMD.ExecuteReader();
            ColumnSeries col1 = new ColumnSeries() { Title="Debit", Values = new ChartValues<Double>(), LabelPoint = point => point.Y.ToString() };
            ColumnSeries col2 = new ColumnSeries() { Title = "Credit", Values = new ChartValues<Double>(), LabelPoint = point => point.Y.ToString() };

            Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };

            ax.Labels = new List<string>();
            while (RDR.Read())
            {
                if(Convert.ToDouble(RDR["dramt"]) != 0)
                {
                    col1.Values.Add(Convert.ToDouble(RDR["dramt"]));
                    i = Convert.ToInt32(RDR["month"].ToString());
                    mon = GetMonthName(i); //int month converted to string month name
                    ax.Labels.Add(mon);
                }
                if (Convert.ToDouble(RDR["cramt"]) != 0)
                {
                    col2.Values.Add(Convert.ToDouble(RDR["cramt"]));
                }
                
            }

            cartesianChart3.Series.Add(col1);
            cartesianChart3.Series.Add(col2);
            cartesianChart3.AxisX.Add(ax);
            cartesianChart3.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString(),
                Separator = new Separator()
            });
            lblchartcash.Text = "Cash/Bank Flow";
            RDR.Close();
            CMD.Dispose();
        }
        private static string GetMonthName(int month)
        {
            // this sub has dependancy with chart, dont delete it.
            DateTime date = new DateTime(2010, month, 1);
            return date.ToString("MMM");

        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmVoucherAlt FVoucher = new FrmVoucherAlt();
            FVoucher.Show();
            FVoucher.BringToFront();
        }

        private void Change_Branch_Click(object sender, EventArgs e)
        {
            frm_Branch_Selection branchswitch = new frm_Branch_Selection();
            branchswitch.Show();
            branchswitch.BringToFront();
        }

        private void BackupRestore_Click(object sender, EventArgs e)
        {
            frmBackupRestore bkprestore = new frmBackupRestore();
            bkprestore.Show();
            bkprestore.BringToFront();
        }

        private void Change_Finyr_Click(object sender, EventArgs e)
        {
            frmFinyr finyrchange = new frmFinyr();
            finyrchange.Show();
            finyrchange.BringToFront();
        }

        private void VoucherPrint_Click(object sender, EventArgs e)
        {
            frmVoucherReport vr = new frmVoucherReport();
            vr.Show();
            vr.BringToFront();
        }

       
        private void mnuGL_Click(object sender, EventArgs e)
        {
            frmGLMast gl = new frmGLMast();
            gl.Show();
            gl.BringToFront();
        }

        private void mnuSL_Click(object sender, EventArgs e)
        {
            frmSLMast sl = new frmSLMast();
            sl.Show();
            sl.BringToFront();
        }

        private void mnucreditorInvoiceMaster_Click(object sender, EventArgs e)
        {
            FrmCreditorInvoice crdinv = new FrmCreditorInvoice();
            crdinv.Show();
            crdinv.BringToFront();
        }

        private void mnucreditorInvoiceCorrection_Click(object sender, EventArgs e)
        {
            frmInvCorrection invCorrection = new frmInvCorrection();
            invCorrection.Show();
            invCorrection.BringToFront();
        }

        private void mnuVPrint_Click(object sender, EventArgs e)
        {
            FrmVoucherPrint vchp = new FrmVoucherPrint();
            vchp.Show();
            vchp.BringToFront();
        }

        private void mnuReconbnkstmnt_Click(object sender, EventArgs e)
        {
            FrmStmntImport stmnt = new FrmStmntImport();
            stmnt.Show();
            stmnt.BringToFront();
        }

        private void mnuCashBankBook_Click(object sender, EventArgs e)
        {
            frmCashbook csh = new frmCashbook();
            csh.Show();
            csh.BringToFront();
        }

        private void mnuBooksOfAccount_Click(object sender, EventArgs e)
        {
            frm_Details_Ledger fdel = new frm_Details_Ledger();
            fdel.comboBox1.Text = "Only This Ledger";
            fdel.Show();
            fdel.BringToFront();
        }

        private void mnuTrialBalance_Click(object sender, EventArgs e)
        {
            frmTrial tr = new frmTrial();
            tr.Show();
            tr.BringToFront();
        }

        private void mnuCurrentBalance_Click(object sender, EventArgs e)
        {
            frmCollectionView clv = new frmCollectionView();
            clv.Show();
            clv.BringToFront();
        }

        private void mnuDayBook_Click(object sender, EventArgs e)
        {
            frmDaybook fdb = new frmDaybook();
            fdb.Show();
            fdb.BringToFront();
        }

        private void mnuLledgerDetails_Click(object sender, EventArgs e)
        {
            frmLedgerList ldgst = new frmLedgerList();
            ldgst.Show();
            ldgst.BringToFront();
        }

        private void mnuProffitAndLossStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPL pl = new frmPL();
            pl.Show();
            pl.BringToFront();
        }

        private void mnuBalanceSheet_Click(object sender, EventArgs e)
        {
            frmBalan bl = new frmBalan();
            bl.Show();
            bl.BringToFront();
        }

        private void TileSL_Click(object sender, EventArgs e)
        {
            frmSLMast sl = new frmSLMast();
            sl.Show();
            sl.BringToFront();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            FrmVoucherAlt FVoucher = new FrmVoucherAlt();
            FVoucher.Show();
            FVoucher.BringToFront();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            FrmVoucherPrint vchp = new FrmVoucherPrint();
            vchp.Show();
            vchp.BringToFront();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            frmDaybook fdb = new frmDaybook();
            fdb.Show();
            fdb.BringToFront();
        }

        private void metroTile4_Click(object sender, EventArgs e)
        {
            frmTrial tr = new frmTrial();
            tr.Show();
            tr.BringToFront();
        }

        private void metroTile5_Click(object sender, EventArgs e)
        {
            frmPL pl = new frmPL();
            pl.Show();
            pl.BringToFront();
        }

        private void metroTile6_Click(object sender, EventArgs e)
        {
            frmCashbook csh = new frmCashbook();
            csh.Show();
            csh.BringToFront();
        }

        private void metroTile7_Click(object sender, EventArgs e)
        {
            FrmStmntImport stmnt = new FrmStmntImport();
            stmnt.Show();
            stmnt.BringToFront();
        }

        private void daysCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCollectionView coll = new frmCollectionView();
            coll.Show();
            coll.BringToFront();
        }

        private void userManagement_Click(object sender, EventArgs e)
        {
            frmCreateUser usercreate = new frmCreateUser();
            usercreate.Show();
            usercreate.BringToFront();
        }

        private void metroPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged_1(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }

        private void cmbometal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
            string sMonth = cmbomonth.Text;
            int index = cmbomonth.Items.IndexOf(sMonth);
            string mon = null;
            string myear = null;
            string chartsql = null;
            string volumechartsql = null;
            int i;
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            cartesianChart2.Series.Clear();
            cartesianChart2.AxisX.Clear();
            cartesianChart2.AxisY.Clear();
            cartesianChart3.Series.Clear();
            cartesianChart3.AxisX.Clear();
            cartesianChart3.AxisY.Clear();


            if (cmboyear.SelectedIndex != 0 && cmbomonth.SelectedIndex != 0)
            {
                lblchart.Text = "Monthly '" + cmbometal.Text + "'  Sales Analysis [INR]";
                chartsql = "select yearh,month,revenue from TBLSALESANALYSIS where yearh='" + cmboyear.Text + "' and month= '" + index + "' and Category='" + cmbometal.Text + "' group by month,yearh,revenue order by yearh,month";
                lblchartvolume.Text = "Monthly '" + cmbometal.Text + "'  Volumetric Sales Analysis [Gm]";
                volumechartsql = "select yearh,month,Purity,Netwt from TBLVolumeAnalysis where yearh='" + cmboyear.Text + "' and month= '" + index + "' and Catagory='" + cmbometal.Text + "' and purity='"+ cmbopurity.Text +"' group by month,yearh,Purity,Netwt order by yearh,month";

            }
            else
            if (cmboyear.SelectedIndex != 0 && cmbomonth.SelectedIndex == 0)
            {
                lblchart.Text = "Yearly '" + cmbometal.Text + "'  Sales Analysis";
                chartsql = "select yearh,month,revenue from TBLSALESANALYSIS where yearh='" + cmboyear.Text + "' and Category='" + cmbometal.Text + "' group by month,yearh,revenue order by yearh,month";
                lblchartvolume.Text = "Yearly '" + cmbometal.Text + "'  Volumetric Sales Analysis [Gm]";
                volumechartsql = "select yearh,month,Purity,Netwt from TBLVolumeAnalysis where yearh='" + cmboyear.Text + "' and Catagory='" + cmbometal.Text + "' and purity='" + cmbopurity.Text + "' group by month,yearh,Purity,Netwt order by yearh,month";

            }
            else
            if (cmboyear.SelectedIndex == 0 && cmbomonth.SelectedIndex != 0)
            {
                lblchart.Text = "Comparative '" + cmbometal.Text + "'  Sales Analysis";
                chartsql = "select yearh,month,revenue from TBLSALESANALYSIS where month= '" + index + "' and Category='" + cmbometal.Text + "' group by month,yearh,revenue order by yearh,month";
                lblchartvolume.Text = "Comparative '" + cmbometal.Text + "'  Volumetric Sales Analysis [Gm]";
                volumechartsql = "select yearh,month,Purity,Netwt from TBLVolumeAnalysis where month= '" + index + "' and Catagory='" + cmbometal.Text + "' and purity='" + cmbopurity.Text + "' group by month,yearh,Purity,Netwt order by yearh,month";

            }
            else
            {
                lblchart.Text = "'" + cmbometal.Text + "'  Sales Analysis";
                chartsql = "select yearh,month,revenue from TBLSALESANALYSIS where Category='" + cmbometal.Text + "' group by month,yearh,revenue order by yearh,month";
                lblchartvolume.Text = "'" + cmbometal.Text + "'  Volumetric Sales Analysis [Gm]";
                volumechartsql = "select yearh,month,Purity,Netwt from TBLVolumeAnalysis where Catagory='" + cmbometal.Text + "' and purity='" + cmbopurity.Text + "' group by month,yearh,Purity,Netwt order by yearh,month";

            }
            //code for revenue chart
            SqlCommand CMD = new SqlCommand(chartsql, clsConnection.Conn);
            SqlDataReader RDR = CMD.ExecuteReader();
            ColumnSeries col = new ColumnSeries() { DataLabels = true, Values = new ChartValues<Double>(), LabelPoint = point => point.Y.ToString() };
            Axis ax = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };

            ax.Labels = new List<string>();
            while (RDR.Read())
            {

                col.Values.Add(Convert.ToDouble(RDR["revenue"]));

                i = Convert.ToInt32(RDR["month"].ToString());
                mon = GetMonthName(i); //int month converted to string month name
                myear = mon + "-" + Convert.ToString(RDR["yearh"].ToString());
                ax.Labels.Add(myear);
            }

            cartesianChart1.Series.Add(col);
            cartesianChart1.AxisX.Add(ax);
            cartesianChart1.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString(),
                Separator = new Separator()
            });
            RDR.Close();
            CMD.Dispose();
            //end code for revenue chart

            //code for volume chart
            SqlCommand CMD2 = new SqlCommand(volumechartsql, clsConnection.Conn);
            SqlDataReader RDR2 = CMD2.ExecuteReader();
            ColumnSeries col2 = new ColumnSeries() { DataLabels = true, Values = new ChartValues<Double>(), LabelPoint = point => point.Y.ToString() };
            Axis ax2 = new Axis() { Separator = new Separator() { Step = 1, IsEnabled = false } };

            ax2.Labels = new List<string>();
            while (RDR2.Read())
            {

                col2.Values.Add(Convert.ToDouble(RDR2["NetWt"]));

                i = Convert.ToInt32(RDR2["month"].ToString());
                mon = GetMonthName(i); //int month converted to string month name
                myear = mon + "-" + Convert.ToString(RDR2["yearh"].ToString());
                ax2.Labels.Add(myear);
            }

            cartesianChart2.Series.Add(col2);
            cartesianChart2.AxisX.Add(ax2);
            cartesianChart2.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString(),
                Separator = new Separator()
            });

            var tooltip = new DefaultTooltip
            {
                SelectionMode = TooltipSelectionMode.SharedYValues
            };

            cartesianChart1.DataTooltip = tooltip;
            CMD.Dispose();
            CMD2.Dispose();
            //end code for volume chart



        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle1.Checked == true)
            {
                timer1.Interval = 5000;
                timer1.Start();

            }
            else
            {
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshAnalysisChart();
            metroButton1_Click(null,null);
            PieChart();
            FillBalance();
            MapChart();
            VolumeChart();
            CashChart();

        }

        private void closeAllAndExit_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void StartMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
