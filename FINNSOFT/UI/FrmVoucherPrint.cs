using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;


namespace FINNSOFT.UI
{
    public partial class FrmVoucherPrint : Form
    {
        public FrmVoucherPrint()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string str = comboBox1.Text;
            string vtype = "";
            if (str == "Journal")
                vtype = "J";
            else if (str == "Payment")
                vtype = "Y";
            else if (str == "Receipt")
                vtype = "R";
            else if (str == "Contra")
                vtype = "T";

            DateTime dt = dateTimePicker2.Value;

            DateTime dt1 = dt;

            DateTime addt = dt1.AddDays(1);

            //addt = Convert.ToDateTime("dd/mm/yyyy");

           // DateTime todt = Convert.ToDateTime(mskTo.Text, "yyyy/mm/dd");

            string qry = "DELETE from tblprintvoucher";
            SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
            com = new SqlCommand(qry, clsConnection.Conn);
            com.ExecuteNonQuery();

            qry = "insert into tblprintvoucher (finyr,BrCode,GLID,SLID,GL_L_Name,SL_L_NAME,VNO,TRANTYPE,AMTTYPE,AMT,InventoryVNo,VDT,ANYSL,BrName) select finyr,BrCode,GLID,SLID,GL_L_Name,SL_L_NAME,VNO,TRANTYPE,AMTTYPE,AMT,NAR,VDT,ANYSL,BrName from viewvoucher where brcode='" + Global.branch + "' and trantype='" + vtype + "' and (VDT >=  '" + dateTimePicker1.Value + "' and VDT < '" + addt + "') order by vdt,vno,amttype desc";

            com = new SqlCommand(qry, clsConnection.Conn);
            com.ExecuteNonQuery();

            ReportDocument rp = new ReportDocument();
            string rPath = Application.StartupPath.ToString() + "\\prntvoucher.rpt";
            rp.Load(rPath);

            frmReportViewer rpt = new frmReportViewer();

                      
            rpt.crystalReportViewer1.ReportSource = rp;
            rpt.Text = "Voucher Report";
            rpt.ShowDialog();
            rpt.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }   
    }
}
