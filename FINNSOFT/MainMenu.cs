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
using System.Security.Cryptography;
using System.IO;

namespace FINNSOFT
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        static readonly string PasswordHash = "Pr@b1r";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";


        private void MainMenu_load(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void FrmGL_Click(object sender, EventArgs e)
        {
            frmGLMast f1 = new frmGLMast();
            f1.Show();
        }

        private void MainMenu_Load_1(object sender, EventArgs e)
        {
            clsConnection.GlobCon();
            panel1.Visible = true;
            toolStrip.Enabled = false;
            loadbranch();
            

        }

        private void MainMenu_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "F1")
            {
                frmCollectionView clv = new frmCollectionView();
                clv.Show();
            }
        }

        private void loadbranch()
        {
            string qry = "select rtrim(BrCode) as BrCode,rtrim(BrName) as BrName from TblBranch order by rtrim(BrName)";


            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["Branch"] != null)
                ds.Tables["Branch"].Clear();
            da.Fill(ds, "Branch");

            comboBox1.DataSource = null;
            comboBox1.DataSource = ds.Tables["Branch"];
            comboBox1.DisplayMember = "BrName";
            comboBox1.ValueMember = "BrCode";
            da.Dispose();
            ds.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void passthrough()
        {
            // StatusStrip usrname = MainMenu();
            // usrname.Text = "Branch: "+Global.branch;
            //this.Close();
            //frmMain frm = new frmMain();
            //frm.Show();

            SqlDataReader Sread = null;
            string qrydt = "select DtFrom,DtTo from TblControl where rtrim(BrCode)='" + Global.branch + "' and rtrim(FInYr)='" + Global.finyr + "'";
            SqlCommand cmd = new SqlCommand(qrydt, clsConnection.Conn);
            try
            {
                Sread = cmd.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Global.gFromDt = Convert.ToDateTime(Sread.GetValue(0));
                        Global.gToDt = Convert.ToDateTime(Sread.GetValue(1));
                    }
                }
            }
            
            finally
            {
                Sread.Close();
                cmd.Dispose();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Global.branch = comboBox1.SelectedValue.ToString();
            SqlDataReader rdr = null;
            string qry = "select password,role from TblPassword where username = '" + textBox1.Text.ToString() + "' and rtrim(BrCode)='" + Global.branch + "'";
            SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
            try
            {
                Global.username = textBox1.Text;
                rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(0))
                    {
                        string psswd = Convert.ToString(rdr.GetValue(0));
                        string userrole = Convert.ToString(rdr.GetValue(1));
                        Global.userrole = Convert.ToString(rdr.GetValue(1));

                        string MtchPsswd = Decrypt(psswd);
                        

                        if (textBox2.Text == MtchPsswd && userrole=="Admin")

                        {
                            panel1.Visible = false;
                            toolStrip.Enabled = true;
                            userManagementToolStripMenuItem.Enabled = true;
                            reindexingToolStripMenuItem.Enabled = true;
                            costCenterToolStripMenuItem.Enabled = true;
                            reindexingToolStripMenuItem.Enabled = true;
                            budgetoryControlToolStripMenuItem.Enabled = true;

                            FrmGL.Enabled = true;
                            FrmSL.Enabled = true;

                            currentBalanceToolStripMenuItem.Enabled = true;
                            FrmPL.Enabled = true;
                            toolStripButton1.Enabled = true;
                            Global.branch = comboBox1.SelectedValue.ToString();
                            Global.finyr = comboBox2.SelectedValue.ToString();
                            // clsConnection.Conn.Close();
                            // this.Close();

                            toolStripStatusLabel1.Text = "User Name: " + textBox1.Text + "     ";

                            toolStripStatusLabel2.Text = "Branch: " + comboBox1.Text + "     ";

                            toolStripStatusLabel3.Text = "Financial Year: " + Global.finyr + "     ";

                            toolStripStatusLabel4.Text = "    Built: 12th September 2017";

                        }

                        else if (textBox2.Text == MtchPsswd && userrole == "User")

                        {
                            panel1.Visible = false;
                            toolStrip.Enabled = true;

                            userManagementToolStripMenuItem.Enabled = false;
                            reindexingToolStripMenuItem.Enabled = false;
                            costCenterToolStripMenuItem.Enabled = false;
                            budgetoryControlToolStripMenuItem.Enabled = false;

                            FrmGL.Enabled = false;
                            //FrmSL.Enabled = false;

                            currentBalanceToolStripMenuItem.Enabled = false;
                            FrmPL.Enabled = false;
                            toolStripButton1.Enabled = false;

                            Global.branch = comboBox1.SelectedValue.ToString();
                            Global.finyr = comboBox2.SelectedValue.ToString();
                            // clsConnection.Conn.Close();
                            // this.Close();

                            toolStripStatusLabel1.Text = "User Name: " + textBox1.Text + "     ";

                            toolStripStatusLabel2.Text = "Branch: " + comboBox1.Text + "     ";

                            toolStripStatusLabel3.Text = "Financial Year: " + Global.finyr + "     ";

                            toolStripStatusLabel4.Text = "    Built: 12th September 2017";


                        }

                        else
                        {

                            const string message = "Username/Password does not match,retry?";
                            const string caption = "Invalid Login";
                            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            textBox1.Focus();


                        }


                    }

                    
                }
            }

            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                    com.Dispose();
                    passthrough();
                }

            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Global.branch = comboBox1.SelectedValue.ToString();
            //SqlCommand cmd = new SqlCommand();
            string qry = "select isnull(FinYr,'00') FinYr from TblControl where rtrim(BrCode)='" + Global.branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["FinYr"] != null)
                ds.Tables["FinYr"].Clear();
            da.Fill(ds, "FinYr");

            comboBox2.DataSource = null;
            comboBox2.DataSource = ds.Tables["FinYr"];
            comboBox2.DisplayMember = "FinYr";
            comboBox2.ValueMember = "FinYr";
            da.Dispose();
            ds.Dispose();
            //cmd.Dispose();
        }

        private void FrmSL_Click(object sender, EventArgs e)
        {
            frmSLMast SL = new frmSLMast();
            SL.Show();
        }

        private void cashBankBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCashbook csh = new frmCashbook();
            csh.Show();
        }

        private void booksOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // frmLedger led = new frmLedger();
            // led.Show();

            frm_Details_Ledger fdel = new frm_Details_Ledger();
            fdel.comboBox1.Text = "Only This Ledger";
            fdel.Show();
        }

        private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrial tr = new frmTrial();
            tr.Show();
        }

        private void FrmPL_Click(object sender, EventArgs e)
        {
            frmPL pl = new frmPL();
            pl.Show();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            frmBalan bl = new frmBalan();
            bl.Show();
        }

        private void printVouchersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVoucherReport vr = new frmVoucherReport();
            vr.Show();
        }

        //private void textBox1_TextChanged(object sender, EventArgs e)
        //{

        //}

        private void voucherPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVoucher vch = new frmVoucher();
            vch.Show();
        }

        private void voucherPrintToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmVoucherPrint vchp = new FrmVoucherPrint();
            vchp.Show();
        }

        private void currentBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCollectionView clv = new frmCollectionView();
            clv.Show();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateUser usr = new frmCreateUser();
            usr.Show();    

        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmDaybook fdb = new frmDaybook();

            fdb.Show();

        }

        private void companyInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            toolStrip.Enabled = false;
            Global.branch = "";
            Global.finyr = "";

            textBox1.Text = "";
            textBox2.Text = "";

            toolStripStatusLabel1.Text = "";

            toolStripStatusLabel2.Text = "";

            toolStripStatusLabel3.Text = "";

            toolStripStatusLabel4.Text = "";
        }

        private void backupRestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBackupRestore br = new frmBackupRestore();
            br.Show();
        }

        private void financialYearToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmFinyr fyr = new frmFinyr();
            fyr.Show();

        }

        private void ledgerDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLedgerList ldgst = new frmLedgerList();
            ldgst.Show();
        }
    }
}
