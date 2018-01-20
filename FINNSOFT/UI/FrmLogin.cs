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

namespace FINNSOFT.UI
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        static readonly string PasswordHash = "Pr@b1r";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            clsConnection.GlobCon();
            loadbranch();
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

                        //MessageBox.Show(MtchPsswd);
                        if (textBox2.Text == MtchPsswd && userrole.Trim() == "Admin")

                        {
                            Global.branch = comboBox1.SelectedValue.ToString();
                            Global.finyr = comboBox2.SelectedValue.ToString();
                            new DashBoardMenu().Show();
                            this.Close();
                        }

                        else if (textBox2.Text == MtchPsswd && userrole.Trim() == "User")

                        {
                            Global.branch = comboBox1.SelectedValue.ToString();
                            Global.finyr = comboBox2.SelectedValue.ToString();
                            new DashBoardMenu().Show();
                            this.Close();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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
    }
 }

