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
using CrystalDecisions.Shared;


namespace FINNSOFT
{
    public partial class frmPrint : Form
    {
        public frmPrint()
        {
            InitializeComponent();
        }

        private void ShowReport()
        {
            ReportDocument rp = new ReportDocument();
            string rPath = "";
            DataSet ds = new DataSet();
            SqlCommand Cmd = new SqlCommand();
            Cmd.Connection = clsConnection.Conn;
            Cmd.CommandText = "select * from TblRPTLEDGER order by GLID";
            //Cmd.CommandText = "Select * from View_Bill where CashMemo='" + memono.Trim() + "'order by convert(datetime,IndentDate,103)";
            //   if (mode == "Cash")
            //   {
            rPath = Application.StartupPath + "\\Trial.rpt";
            //rPath="D:\\Personal\\Project\\FINNSOFT\\FINNSOFT\\Report\\Trial.rpt";
            //   }
            //   else
            //   {

            //   }

            frmTrial ftrl = new frmTrial();

            rp.Load(rPath);
            rp.DataDefinition.FormulaFields["compname"].Text = "\"" + Global.branch + "\"";
            rp.DataDefinition.FormulaFields["rptperiod"].Text = "\"" + Global.trlfrmdt.ToString("dd/MM/yyyy") + " - " + Global.trltodt.ToString("dd/MM/yyyy") + "\"";
            rp.DataDefinition.FormulaFields["fin_period"].Text = "\"" + Global.gFromDt.ToString("dd/MM/yyyy") + " - " + Global.gToDt.ToString("dd/MM/yyyy") + "\"";
            //rp.SetParameterValue("cmpname", Global.branch);
            //rp.SetParameterValue("dt_period",Global.gFromDt + " - "+ Global.gToDt );
            SqlDataAdapter sDAP = new SqlDataAdapter();
            sDAP.SelectCommand = Cmd;
            sDAP.Fill(ds, "Trial");

            //Data.Pres pr = new AppointmentMaster.Data.Pres();


            rp.SetDataSource(ds.Tables["Trial"]);
            //btnEXCEL.Enabled = false;
            crystalReportViewer1.ReportSource = rp;

            crystalReportViewer1.RefreshReport();
            this.crystalReportViewer1.Show();
            //rptViewer.ShowDialog();
            Cmd.Dispose();
        }



        private void frmPrint_Load(object sender, EventArgs e)
        {
            ShowReport();
        }
    }
}
