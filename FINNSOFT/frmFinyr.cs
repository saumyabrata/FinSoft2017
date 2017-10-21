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
    public partial class frmFinyr : Form
    {
        public frmFinyr()
        {
            InitializeComponent();
        }

        string MaxFinYr;
        int slid;
        int OpBal;
        int drBal;
        int crBal;
        double curBal;

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt;
            DateTime dt1;



            try
            {

                dt = Convert.ToDateTime(maskedTextBox1.Text);
                dt1 = Convert.ToDateTime(maskedTextBox1.Text);
                dt1 = dt1.AddYears(1);
                dt1 = dt1.AddDays(-1);

                string fromYr = dt.Year.ToString();
                string ToYr = dt1.Year.ToString();
                string finyr = fromYr + "-" + ToYr;

                string qry = "insert into tblcontrol (Brcode,finyr,DtFrom,DtTo,LastUsed) values " +
                                   "(@Brcode,@Finyr,@DtFrom,@dtTo,@lused)";
                SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@Brcode", Global.branch);
                com.Parameters.AddWithValue("@finyr", finyr);
                com.Parameters.AddWithValue("@DtFrom", maskedTextBox1.Text);
                com.Parameters.AddWithValue("@DtTo", dt1);
                com.Parameters.AddWithValue("@lused", "1");

                com.ExecuteNonQuery();

                dataGridView1.Rows.Clear();

                frmFinyr_Load(null, null);

                MessageBox.Show("Financial Year created Successfully", "Message");

            }

            catch (Exception)
            {

                MessageBox.Show("Financial Year already created", "Error Message");
            }

        }

        private void frmFinyr_Load(object sender, EventArgs e)
        {

            string qry1 = "select max(finyr) from tblCOntrol where brcode='" + Global.branch + "'";

            SqlDataReader sread = null;
            SqlCommand cmd1 = new SqlCommand(qry1, clsConnection.Conn);

            sread = cmd1.ExecuteReader();

            sread.Read();

            MaxFinYr = Convert.ToString(sread.GetValue(0));

            sread.Close();

            string qry = "select finyr,dtfrom,dtto from tblcontrol where brcode='" + Global.branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);


            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            if (ds.Tables["finyr"] != null)
                ds.Tables["finyr"].Clear();
            da.Fill(ds, "finyr");

            dt.TableName = "finyr";
            dt.Columns.Clear();
            dt.Columns.Add("finyr");
            dt.Columns.Add("FromDt");
            dt.Columns.Add("ToDt");

            dt.Rows.Clear();

            for (int i = 0; i < ds.Tables["finyr"].Rows.Count; i++)
            {

                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = ds.Tables["finyr"].Rows[i]["finyr"].ToString();
                dataGridView1.Rows[i].Cells[1].Value = ds.Tables["finyr"].Rows[i]["dtfrom"].ToString();
                dataGridView1.Rows[i].Cells[2].Value = ds.Tables["finyr"].Rows[i]["dtto"].ToString();


            }
        }

        private void button2_Click(object sender, EventArgs e)

        {


            var result = MessageBox.Show("Are you sure to proceed?", "Alert", MessageBoxButtons.YesNo);

            if (Convert.ToString(result) == "Yes")
            {
                CreateLedgers();

                UpdateLedgerBalance();

            }
        }

        private void CreateLedgers()
        {

            SqlCommand cmd = new SqlCommand("SP_Create_Ledgers", clsConnection.Conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PrvFinYr", SqlDbType.NVarChar).Value = Global.finyr;
            cmd.Parameters.AddWithValue("@NewFinYr", SqlDbType.NVarChar).Value = MaxFinYr;

            cmd.ExecuteNonQuery();

        
        }

        private void UpdateLedgerBalance()
        {

            DateTime frmdt;

            frmdt = Global.gToDt;
            frmdt = frmdt.AddDays(1);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();            

            string qry1 = "select distinct(a.slid) as slid from tblledger a, tblslmast b, tblvoucher c where a.brcode=b.brcode and b.brcode=c.brcode and a.vno=c.vno and a.TRANTYPE=c.TRANTYPE and b.BrCode=c.BrCode and c.vdt>='" + Global.gFromDt + "' and c.vdt<'" + frmdt + "' and a.brcode='" + Global.branch + "' and a.glid=00001 and a.amt>0";
            SqlDataAdapter da = new SqlDataAdapter(qry1, clsConnection.Conn);

            if (ds.Tables["SlBal"] != null)
                ds.Tables["SlBal"].Clear();
            da.Fill(ds, "SlBal");

            dt.TableName = "SlBal";
            dt.Columns.Clear();
            dt.Columns.Add("slid");

            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();



            for (int i = 0; i < ds.Tables["SlBal"].Rows.Count; i++)
            {

                slid = Convert.ToInt32(ds.Tables["SlBal"].Rows[i]["slid"]);                

                SqlDataReader Sread = null;
                string qry3 = "select op_bal from tblslmast where glid=00001 and slid='" + slid + "' and brcode='" + Global.branch + "' and finyr='" + Global.finyr + "'";

                SqlCommand cmd2 = new SqlCommand(qry3, clsConnection.Conn);

                Sread = cmd2.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        OpBal = Convert.ToInt32(Sread.GetValue(0));


                    }
                }

                Sread.Close();

                string qry = "select sum(b.amt) as CashDrBl from tblvoucher a, tblledger b where a.BrCode=b.brcode";
                qry = qry + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt>='" + Global.gFromDt + "' and a.vdt < '" + frmdt + "' and b.brcode = '" + Global.branch + "' and(b.glid = '00001' and b.slid = '" + slid + "') and b.AMTTYPE = 'D'";

                SqlCommand cmd = new SqlCommand(qry, clsConnection.Conn);

                Sread = cmd.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        drBal = Convert.ToInt32(Sread.GetValue(0));

                    }
                }

                Sread.Close();

                string qry4 = "select sum(b.amt) as CashCrBl from tblvoucher a, tblledger b where a.BrCode = b.brcode";
                qry4 = qry4 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt>='" + Global.gFromDt + "' and a.vdt < '" + frmdt + "' and b.brcode ='" + Global.branch + "' and(b.glid = '00001' and b.slid = '" + slid + "') and b.AMTTYPE = 'C'";

                SqlCommand cmd4 = new SqlCommand(qry4, clsConnection.Conn);

                Sread = cmd4.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        crBal = Convert.ToInt32(Sread.GetValue(0));

                    }
                }

                curBal = OpBal + (drBal - crBal);

                curBal = Math.Round(curBal, 2);

                    string curBal1 = Convert.ToString(curBal);

                                                  
                ds1.Dispose();
                dt1.Dispose();
                Sread.Close();

                string UpdtQry = "update tblslmast set op_bal=" + curBal + " where glid='00001' and slid='" + slid + "' and brcode='" + Global.branch + "' and finyr='" + MaxFinYr + "'";
                SqlCommand cmd5 = new SqlCommand(UpdtQry, clsConnection.Conn);
                cmd5.ExecuteNonQuery();

                curBal = 0;
                OpBal = 0;
                drBal = 0;
                crBal = 0;

            }

            MessageBox.Show("GL and SL created for new Financial Year and Opening Balance updated, You can now log in with new Financial Year");
        }

        
    }
}
