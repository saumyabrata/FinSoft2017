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
    public partial class frmSLMast : Form
    {
        public frmSLMast()
        {
            InitializeComponent();
        }

        private void frmSLMast_Load(object sender, EventArgs e)
        {
            fillGL();
            listGL.SelectedIndex = 1;
            //fillSL(Convert.ToString(listGL.SelectedValue));
            listGL_Click(null, null);
            listSub_Click(null, null);

            if (Global.userrole=="User")

            {

                btnNew.Enabled = false;
                txtID.Enabled = false;
                txtAdd1.Enabled = false;
                txtAdd2.Enabled = false;
                txtDesc.Enabled = false;
                txtPin.Enabled = false;

            }
        }
        private void fillGL()
        {
            try
            {
                string qry = "select GLID,GL_L_Name from TblGLMast Where anysl <> 0 and finyr= '" + Global.finyr + "' and BrCode='" + Global.branch + "' order by GL_L_Name";
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
        private void fillSL( string vGLID)
        {
            try
            {
                string qry = "select SLID,SL_L_Name from TblSLMast Where GLID='"+ vGLID +"'  and finyr= '" + Global.finyr + "' and BrCode='" + Global.branch + "' order by SL_L_Name";
                SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds = new DataSet();
                if (ds.Tables["SL"] != null)
                    ds.Tables["SL"].Clear();

                da.Fill(ds, "SL");

                listSub.DataSource = null;
                listSub.DataSource = ds.Tables["SL"];
                listSub.DisplayMember = "SL_L_NAME";
                listSub.ValueMember = "SLID";
            }
            catch (Exception)
            {
            }

        }

        private void listGL_Click(object sender, EventArgs e)
        {
            
            if (Convert.ToString(listGL.SelectedValue) == "4" )
            {
                cmbType.Visible = true;
                label13.Visible = true;
                cmbType.Items.Clear();
                cmbType.Items.Add("General");
                cmbType.Items.Add("Supplier");
                cmbType.Text = "Supplier";


            }
            else if (Convert.ToString(listGL.SelectedValue) == "3")
            {
                cmbType.Visible = true;
                label13.Visible = true;
                cmbType.Items.Clear();
                cmbType.Items.Add("General");
                cmbType.Items.Add("Customer");
                cmbType.Text = "Customer";
            }
            else
            {
                cmbType.Visible = false;
                label13.Visible = false;
                cmbType.Items.Clear();
            }
            fillSL(Convert.ToString(listGL.SelectedValue));
            listSub_Click(null, null);

        }

        private void listSub_Click(object sender, EventArgs e)
        {
            clsSLBAL SlBAL = new clsSLBAL();
            clsSLBO SlBO = new clsSLBO();
            //string crt = "GLID";


            SlBO = SlBAL.GetValue(Convert.ToString(listSub.SelectedValue));
            txtID.Text = SlBO.GetSLID();            
            txtDesc.Text = SlBO.GetSL_L_NAME();
            txtAdd1.Text = SlBO.GetSLADD1();
            txtAdd2.Text = SlBO.GetSLADD2();
            txtCity.Text = SlBO.GetSLCITY();
            txtPin.Text = SlBO.GetSLPIN();            
            txtPhone.Text = SlBO.GetSLPHONE();
            txtFax.Text = SlBO.GetSLFAX();
            txtContact.Text = SlBO.GetSLCONT_PERS();
            txtRem.Text = SlBO.GetREMARKS();
            txtOpen.Text = Convert.ToString(Math.Abs(SlBO.Getop_bal())); 
            textBox1.Text = SlBO.Getgstin();
            if (SlBO.Getop_bal() >=0 )
            {
                radioButton1.Checked = true;
                //cmbOpenType.Text = "Dr";
            }
            else
            {
                radioButton2.Checked = true;
                //cmbOpenType.Text = "Cr";
            }
            if (Convert.ToString(SlBO.GetGLID()) == "00004" || Convert.ToString(SlBO.GetGLID()) == "00003")
            {
                if (Convert.ToString(SlBO.GetSTATUS()) == "G")
                {
                    cmbType.Text = "General";
                }
                else if (Convert.ToString(SlBO.GetSTATUS()) == "C")
                {
                    cmbType.Text = "Customer";
                }
                else if (Convert.ToString(SlBO.GetSTATUS()) == "S")
                {
                    cmbType.Text = "Supplier";
                }
            }
            
            if (SlBO.GetSL_L_NAME() != "")
            {
                btnSave.Text = "&Update";
            }
            else
            {
                btnSave.Text = "&Save";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newform();
        }
        public void newform()
        {
            string mCode = "";
            mCode = Convert.ToString(clsVar.LastNo("TblSlMast", "Slid", 5) + 1); //"0000000" + 
            //mCode = mCode.Substring(mCode.Length - 5);
            //txtCode.Text = Right("0000000" + mCode, 5);
            txtID.Text = mCode;
            txtDesc.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtCity.Text = "";
            txtPin.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtContact.Text = "";
            txtRem.Text = "";
            txtOpen.Text = "0";
            textBox1.Text = "";
            btnSave.Text = "&Save";
            radioButton1.Checked = true;
            if (Convert.ToString(listGL.SelectedValue) == "4")
            {
                cmbType.Visible = true;
                label13.Visible = true;
                cmbType.Items.Clear();
                cmbType.Items.Add("General");
                cmbType.Items.Add("Supplier");
                cmbType.Text = "Supplier";


            }
            else if (Convert.ToString(listGL.SelectedValue) == "3")
            {
                cmbType.Visible = true;
                label13.Visible = true;
                cmbType.Items.Clear();
                cmbType.Items.Add("General");
                cmbType.Items.Add("Customer");
                cmbType.Text = "Customer";
            }
            else
            {
                cmbType.Visible = false;
                label13.Visible = false;
                cmbType.Items.Clear();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Code.", "FINNSOFT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtID.Focus();
                return;
            }
            if (txtDesc.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Name.", "FINNSOFT", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDesc.Focus();
                return;
            }
                       
            string result = "";
            clsSLBAL SlBAL = new clsSLBAL();
            string SlType="";
            Double Opening = 0;
            if ( radioButton2.Checked==true )
            {
                Opening = 0-Convert.ToDouble(txtOpen.Text);
            }
            else
            {
                Opening =Math.Abs( Convert.ToDouble(txtOpen.Text));
            }

            if (Convert.ToString(listGL.SelectedValue) == "00004" || Convert.ToString(listGL.SelectedValue) == "00003")
            {
                SlType= cmbType.Text.Substring(0,1);

            }
            
            else
            {
                SlType= "G";
            }
            if (btnSave.Text == "&Save")
            {
                result = SlBAL.SaveData(Global.finyr,Global.branch,Convert.ToString(listGL.SelectedValue),txtID.Text.Trim(), txtDesc.Text.Trim(),txtAdd1.Text,txtAdd2.Text,txtCity.Text,txtPin.Text,txtFax.Text,txtPhone.Text,txtContact.Text,txtRem.Text,Opening, SlType,textBox1.Text);
            }
            else
            {
                result = SlBAL.UpdateData(Global.finyr, Global.branch, Convert.ToString(listGL.SelectedValue), txtID.Text.Trim(), txtDesc.Text.Trim(), txtAdd1.Text, txtAdd2.Text, txtCity.Text, txtPin.Text, txtFax.Text, txtPhone.Text, txtContact.Text, txtRem.Text, Opening, SlType,textBox1.Text);
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
            listGL_Click(null, null);
        }

        private void listGL_SelectedIndexChanged(object sender, EventArgs e)
        {
            listGL_Click(null, null);
        }

        private void listSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            listSub_Click(null, null);
        }

        private void txtOpen_TextChanged(object sender, EventArgs e)
        {
            if(txtOpen.Text=="-" || txtOpen.Text=="+")

            {

                MessageBox.Show("Minus or plus signs not allowed, Please select credit for negative or Debit for positive values","Alert",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            }
        }
    }
}
