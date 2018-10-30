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
    public partial class frm_Branch_Selection : MetroFramework.Forms.MetroForm
    {
        public frm_Branch_Selection()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        

        private void frm_Branch_Selection_Load(object sender, EventArgs e)
        {
            loadbranch();
        }

        private void loadbranch()
        {
            string qry = "select BrCode,BrName from TblBranch order by BrName";
            

            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["Branch"] != null)
                ds.Tables["Branch"].Clear();
            da.Fill(ds, "Branch");

            comboBox1.DataSource = null;
            comboBox1.DataSource = ds.Tables["Branch"];
            comboBox1.DisplayMember = "BrName";
            comboBox1.ValueMember = "BrCode";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader Sread;
            string qry = "select isnull(FinYr,'00') FinYr from TblControl where BrCode='" + Global.branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["FinYr"] != null)
                ds.Tables["FinYr"].Clear();
            da.Fill(ds, "FinYr");

            comboBox2.DataSource = null;
            comboBox2.DataSource = ds.Tables["FinYr"];
            comboBox2.DisplayMember = "FinYr";
            comboBox2.ValueMember = "FinYr";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Global.branch = comboBox1.SelectedValue.ToString();
            Global.finyr = comboBox2.SelectedValue.ToString();
            // StatusStrip usrname = MainMenu();
            // usrname.Text = "Branch: "+Global.branch;
            this.Close();
            //frmMain frm = new frmMain();
            //frm.Show();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader Sread;
            string qry = "select DtFrom,DtTo from TblControl where BrCode='" + Global.branch + "' and FInYr='" + Global.finyr + "'";
            cmd.CommandText = qry;
            cmd.Connection = clsConnection.Conn;
            Sread = cmd.ExecuteReader();
            while (Sread.Read())
            {
                if (!Sread.IsDBNull(0))
                {
                    Global.gFromDt = Convert.ToDateTime(Sread.GetValue(0));
                    Global.gToDt = Convert.ToDateTime(Sread.GetValue(1));

                }
            }

            Sread.Close();
            cmd.Dispose();
        }

        private void bttnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
