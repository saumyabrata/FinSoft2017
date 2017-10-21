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
    public partial class frmGLMast : Form
    {
        public frmGLMast()
        {
            InitializeComponent();
        }

        private void frmGLMast_Load(object sender, EventArgs e)
        {
            fillGL();
            fillCombo();
            string mCode = "";
            mCode = "0000000" + Convert.ToString(clsVar.LastNo("TblGlMast", "Glid", 5) + 1);
            mCode = mCode.Substring(mCode.Length - 5);
            //txtCode.Text = Right("0000000" + mCode, 5);
            txtCode.Text = mCode;
            cmbSubL.Text = "Yes";
            cmbOpenType.Text = "Dr";
            btnSave.Text = "&Save";
        }

        private void fillGL()
        {
            try
            {
                string qry = "select GLID,GL_L_Name from TblGLMast Where finyr= '" + Global.finyr + "' and BrCode='" + Global.branch + "' order by GL_L_Name";
                SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds = new DataSet();
                if (ds.Tables["GL"] != null)
                    ds.Tables["GL"].Clear();

                da.Fill(ds, "GL");

                listGL.DataSource = null;
                listGL.DataSource = ds.Tables["GL"];
                listGL.DisplayMember = "GL_L_NAME";
                listGL.ValueMember = "GLID";
            }
            catch (Exception)
            {
            }

        }
        private void fillCombo()
        {
            try
            {
                DataSet data = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter("Select PL_BS_ID,Description from [TblPL_BS_MAST] order by Description", clsConnection.Conn);
                adapter.Fill(data, "PL_BS");
                if (cmbType.DataSource != null)
                {
                    cmbType.DataSource = null;
                }
                cmbType.DataSource = data.Tables["PL_BS"];
                cmbType.DisplayMember = "Description";
                cmbType.ValueMember = "PL_BS_ID";
            }
            catch (Exception)
            { }

        }

        private void listGL_Click(object sender, EventArgs e)
        {
            clsGLBAL GlBAL = new clsGLBAL();
            DataTable dt;
            clsGLBO GlBO = new clsGLBO();
            string crt = "GLID";


            GlBO = GlBAL.GetValue(Convert.ToString(listGL.SelectedValue));
            txtCode.Text = GlBO.GetGLID();
            cmbType.SelectedValue = GlBO.GetPL_BS_ID();
            txtName.Text = GlBO.GetGL_L_NAME();
            txtNarration.Text = GlBO.GetNarration();
            if (GlBO.GetANYSL() == 0)
            {
                cmbSubL.Text = "No";

            }
            else
            {
                cmbSubL.Text = "Yes";
            }
            txtOpen.Text = Convert.ToString(Math.Abs(GlBO.Getop_bal()));
            if (GlBO.Getop_bal() > 1)
            {
                cmbOpenType.Text = "Dr";
            }
            else
            {
                cmbOpenType.Text = "Cr";
            }
            if (GlBO.GetGL_L_NAME() != "")
            {
                btnSave.Text = "&Update";
            }
            else
            {
                btnSave.Text = "&Save";
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newform();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Code.", "FINNSOFT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCode.Focus();
                return;
            }
            if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Name.", "FINNSOFT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtName.Focus();
                return;
            }

            string result = "";
            clsGLBAL GlBAL = new clsGLBAL();
            Int32 subL = 0;
            Double Opening = 0;
            if (cmbSubL.Text == "Yes")
            {
                subL = 1;
            }
            else
            {
                subL = 0;
            }
            if (cmbOpenType.Text == "Cr")
            {
                Opening = 0 - Convert.ToDouble(txtOpen.Text);
            }
            else
            {
                Opening = Math.Abs(Convert.ToDouble(txtOpen.Text));
            }
            if (btnSave.Text == "&Save")
            {
                result = GlBAL.SaveData(Global.finyr, Global.branch, txtCode.Text.Trim(), txtName.Text.Trim(), Convert.ToInt32(cmbType.SelectedValue), subL, Opening, txtNarration.Text);
            }
            else
            {
                result = GlBAL.UpdateData(Global.finyr, Global.branch, txtCode.Text.Trim(), txtName.Text.Trim(), Convert.ToInt32(cmbType.SelectedValue), subL, Opening, txtNarration.Text);
            }
            //
            //txtCode.AutoCompleteCustomSource.Add(txtCode.Text);
            //txtName.AutoCompleteCustomSource.Add(txtName.Text);

            //clsVar.autoPatCode.Add(txtCode.Text);
            //clsVar.autoPatName.Add(txtName.Text);
            //
            MessageBox.Show(result, "FINNSOFT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            newform();
            fillGL();
        }

        public void newform()
        {
            string mCode = "";
            mCode = Convert.ToString(clsVar.LastNo("TblGlMast", "Glid", 5) + 1);  //"0000000" + 
            //mCode = mCode.Substring(mCode.Length - 5);
            //txtCode.Text = Right("0000000" + mCode, 5);
            txtCode.Text = mCode;
            txtCode.Text = mCode;
            cmbSubL.Text = "Yes";
            cmbOpenType.Text = "Dr";
            btnSave.Text = "&Save";
            txtName.Text = "";
            txtOpen.Text = "0";
        }

        private void listGL_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Set the search string:
            string myString = textBox1.Text;
            // Search starting from index -1:
            int index = listGL.FindString(myString, -1);
            if (index != -1)
            {
                // Select the found item:
                listGL.SetSelected(index, true);
            }
            else
                MessageBox.Show("Item not found.");

        }
    }
}
