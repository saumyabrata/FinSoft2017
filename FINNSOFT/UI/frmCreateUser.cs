using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient; 

namespace FINNSOFT
{
    public partial class frmCreateUser : MetroFramework.Forms.MetroForm
    {
        public frmCreateUser()
        {
            InitializeComponent();
        }

        static readonly string PasswordHash= "Pr@b1r";
        static readonly string SaltKey= "S@LT&KEY";
        static readonly string VIKey= "@1B2c3D4e5F6g7H8";

        private void button1_Click(object sender, EventArgs e)
        {
            /* txtusrname.Text =  Encrypt("admin");
               txtpasswd.Text = Convert.ToString(Decrypt("gqNvtCuri6n9nLVH8CRakA==")); */

            if (txtusrname.Text == "" || txtpasswd.Text == "" || cmbRole.Text == "")
            {

                MessageBox.Show("Please fillup all the required fields", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                String Result;


                cmd.CommandText = "Insert into TblPassword(brcode,username,password,role)" +
                    "Values(@BrCode,@UsrName, @Psswd,@Role)";
                cmd.Connection = clsConnection.Conn;
                try
                {
                    cmd.Parameters.AddWithValue("@BrCode", Global.branch);
                    cmd.Parameters.AddWithValue("@UsrName", txtusrname.Text);
                    cmd.Parameters.AddWithValue("@Psswd", Encrypt(txtpasswd.Text));
                    cmd.Parameters.AddWithValue("@Role", cmbRole.Text);

                    cmd.ExecuteNonQuery();
                    Result = "UserID Created Successfully";

                    MessageBox.Show(Result);

                    button2_Click(null, null);

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);

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

        private void button3_Click(object sender, EventArgs e)
        {

            if (txtusrname.Text == "" || txtpasswd.Text == "" || cmbRole.Text == "")
            {

                MessageBox.Show("Field(s) cannot be blank", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
            else
            {

                SqlCommand cmd = new SqlCommand();
                String Result;

                string NewPsswd = txtpasswd.Text;
                NewPsswd = Encrypt(NewPsswd);

                cmd.CommandText = "update tblpassword set password='" + NewPsswd + "',role='" + cmbRole.Text + "' where username='" + txtusrname.Text + "' and brcode='" + Global.branch + "'";
                cmd.Connection = clsConnection.Conn;
                try
                {
                    cmd.ExecuteNonQuery();
                    Result = "Password Updated Successfully";

                    MessageBox.Show(Result);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {

            if (txtusrname.Text == "" || txtpasswd.Text == "" || cmbRole.Text == "")
            {

                MessageBox.Show("Field(s) cannot be blank", "Alert Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                String Result;

                string NewPsswd = txtpasswd.Text;
                NewPsswd = Encrypt(NewPsswd);

                cmd.CommandText = "delete from tblpassword where username='" + txtusrname.Text + "' and brcode='" + Global.branch + "'";
                cmd.Connection = clsConnection.Conn;
                try
                {
                    cmd.ExecuteNonQuery();
                    Result = "UserID Deleted Successfully";

                    MessageBox.Show(Result);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            txtusrname.Text = "";
            txtpasswd.Text = "";
            cmbRole.Text = "";
        }

        private void frmCreateUser_Load(object sender, EventArgs e)
        {

        }
    }
    }
