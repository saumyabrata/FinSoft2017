﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FINNSOFT.UI;

namespace FINNSOFT.UI
{
    public partial class FrmCreditorInvoice : MetroFramework.Forms.MetroForm
    {
        public FrmCreditorInvoice()
        {
            InitializeComponent();
        }

        public static int rowindex;

        private void fillledgers()
        {
            try
            {
                string qry = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and rtrim(GLID) = 4";
                SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(qry, clsConnection.Conn);
                if (ds.Tables["sl"] != null)
                    ds.Tables["sl"].Clear();
                da.Fill(ds, "sl");
                DataTable dt = new DataTable();
                dt.TableName = "ledgers";
                dt.Columns.Clear();
                dt.Columns.Add("ledgname");
                dt.Columns.Add("glsl");
                for (int i = 0; i < ds.Tables["sl"].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables["sl"].Rows[i][0].ToString();
                    dr[1] = ds.Tables["sl"].Rows[i][1].ToString();
                    dt.Rows.Add(dr);
                }
                dt.DefaultView.Sort = "ledgname asc";
                CreditorList.DataSource = null;
                CreditorList.DataSource = dt;
                CreditorList.DisplayMember = "ledgname";
                CreditorList.ValueMember = "glsl";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmCreditorInvoice_Load(object sender, EventArgs e)
        {

            fillledgers();
            CreditorFind.Focus();
            //dataGridView1.Rows.Clear();
            TxtInv.Text = "";
            TxtJob.Text = "";
            txtinvdate.Text = "";
            TxtInvValue.Text = "0.00";
            txtothers.Text = "0.00";
            txtdiscount.Text = "0.00";
            txtsgst.Text = "0.00";
            txtcgst.Text = "0.00";
            txtigst.Text = "0.00";
            textnet.Text = "0.00";
            TxtInv.Focus();
        }
        private void calculatenet()
        {
            try
            {
                textnet.Text = Convert.ToDouble(Convert.ToDouble(TxtInvValue.Text) + Convert.ToDouble(txtothers.Text) - Convert.ToDouble(txtdiscount.Text) +
                        +Convert.ToDouble(txtsgst.Text) + Convert.ToDouble(txtcgst.Text) + Convert.ToDouble(txtigst.Text)).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreditorFind_TextChanged(object sender, EventArgs e)
        {
            string str = CreditorFind.Text;
            try
            {
                CreditorList.SelectedIndex = CreditorList.FindString(str, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreditorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CreditorList.Focused == true)
            {
                CreditorFind.Text = CreditorList.Text;
                CreditorList.Focus();
            }
        }

        private void CreditorList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                string str = CreditorList.SelectedValue.ToString();

                string glid = str.Substring(0, str.IndexOf("-"));
                string slid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));
                label8.Text = glid;
                label9.Text = slid;
                CreditorList.Visible = false;
                TxtInv.Focus();
            }
        }

        private void CreditorFind_KeyUp(object sender, KeyEventArgs e)
        {
            string str = e.KeyCode.ToString();
            if (str == "Down")
            {
                CreditorList.Focus();
                CreditorFind.Text = CreditorList.Text;

            }

            else if (str == "Up")

            {
                CreditorList.Focus();
                CreditorFind.Text = CreditorList.Text;
            }

            if (str == "Return")
            {
                CreditorList.SelectedIndex = CreditorList.FindString(CreditorFind.Text, 0);
                str = CreditorList.SelectedValue.ToString();

                string glid = str.Substring(0, str.IndexOf("-"));
                string slid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));

                label8.Text = glid;
                label9.Text = slid;

                CreditorFind.Text = CreditorList.Text;
                CreditorList.Visible = false;

                TxtInv.Focus();
            }
        }

        private void CreditorFind_Enter(object sender, EventArgs e)
        {
            CreditorList.Visible = true;
        }

        private void CreditorFind_Leave(object sender, EventArgs e)
        {
            CreditorList.Visible = false;
        }

        private void TxtInv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TxtJob.Focus();
            }
        }

        private void TxtJob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtinvdate.Focus();
            }
        }

        private void txtinvdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TxtInvValue.SelectAll();
                TxtInvValue.Focus();
            }

        }

        private void txtinvdate_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void TxtInv_TextChanged(object sender, EventArgs e)
        {

        }



        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 rowcnt = dataGridView1.Rows.Count;
            for (int a = 0; a < rowcnt; a = a+1)
            {
                if (dataGridView1.Rows[a].Cells[0].Value != null)
                {

                    string qry = "insert into TblCreditorInvMast (finyr,Brcode,GLID,SLID,Creditor_Name,InvNo,InvDt,Job,InvValue," +
                                "other_charge,discount,sgst,cgst,igst,net_value,PaidSum,Payment_Status) values " +
                        "(@finyr,@Brcode,@glid,@slid,@creditor,@invno,@invdt,@job,@invvalue,@othchg,@disc,@sgst,@cgst,@igst,@netval,0,'N')";
                    SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
                        com.Parameters.Clear();
                        com.Parameters.AddWithValue("@finyr", Global.finyr);
                        com.Parameters.AddWithValue("@Brcode", Global.branch);
                        com.Parameters.AddWithValue("@glid", label8.Text);
                        com.Parameters.AddWithValue("@slid", label9.Text);
                        com.Parameters.AddWithValue("@Creditor", CreditorFind.Text);
                        com.Parameters.AddWithValue("@InvNo", dataGridView1.Rows[a].Cells[0].Value);
                        com.Parameters.AddWithValue("@Job", dataGridView1.Rows[a].Cells[1].Value);
                        com.Parameters.AddWithValue("@InvDt", dataGridView1.Rows[a].Cells[2].Value);
                        com.Parameters.AddWithValue("@invvalue", Convert.ToDouble(dataGridView1.Rows[a].Cells[3].Value ?? 0));
                        com.Parameters.AddWithValue("@othchg", Convert.ToDouble(dataGridView1.Rows[a].Cells[4].Value ?? 0));
                        com.Parameters.AddWithValue("@disc", Convert.ToDouble(dataGridView1.Rows[a].Cells[5].Value ?? 0));
                        com.Parameters.AddWithValue("@sgst", Convert.ToDouble(dataGridView1.Rows[a].Cells[6].Value ?? 0));
                        com.Parameters.AddWithValue("@cgst", Convert.ToDouble(dataGridView1.Rows[a].Cells[7].Value ?? 0));
                        com.Parameters.AddWithValue("@igst", Convert.ToDouble(dataGridView1.Rows[a].Cells[8].Value ?? 0));
                        com.Parameters.AddWithValue("@netval", Convert.ToDouble(dataGridView1.Rows[a].Cells[9].Value ?? 0));
                        com.ExecuteNonQuery();

                }

            }
            //Insert Audit Tran

            string qry2 = "insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values " +
                  "(@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
            SqlCommand com2 = new SqlCommand(qry2, clsConnection.Conn);
            com2.Parameters.Clear();
            com2.Parameters.AddWithValue("@HostName", Global.FetchHost());
            com2.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
            com2.Parameters.AddWithValue("@menu", "Creditor Invoice Master");
            com2.Parameters.AddWithValue("@OperationType", "Insert");
            com2.Parameters.AddWithValue("@username", Global.username);
            com2.Parameters.AddWithValue("@Remarks", "New Creditor Invoice Entry of Creditor: " + CreditorFind.Text.ToString());
            com2.ExecuteNonQuery();
            MessageBox.Show("Record saved successfully");
                TxtInv.Text = "";
                TxtJob.Text = "";
                txtinvdate.Text = "";
                TxtInvValue.Text = "0.00";
                txtothers.Text = "0.00";
                txtdiscount.Text = "0.00";
                txtsgst.Text = "0.00";
                txtcgst.Text = "0.00";
                txtigst.Text = "0.00";
                textnet.Text = "0.00";
                TxtInv.Focus();
            dataGridView1.Rows.Clear();
            dataGridView1.RowCount = 1;
            CreditorFind.Focus();
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void TxtInvValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }

                calculatenet();
                txtothers.SelectAll();
                txtothers.Focus();
            }
        }

        private void TxtInvValue_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void txtigst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                calculatenet();
                if (String.IsNullOrEmpty(TxtInv.Text) || String.IsNullOrEmpty(TxtJob.Text) || String.IsNullOrEmpty(txtinvdate.Text) || 
                    String.IsNullOrEmpty(TxtInvValue.Text) || string.IsNullOrEmpty(txtsgst.Text) || (string.IsNullOrEmpty(txtcgst.Text) 
                    && string.IsNullOrEmpty(txtigst.Text)))
                {
                    MessageBox.Show(" Highlighted Fields can not be left blank.");
                    if (String.IsNullOrEmpty(TxtInv.Text))
                    {
                        TxtInv.Focus();
                    }
                    if (String.IsNullOrEmpty(TxtJob.Text))
                    {
                        TxtJob.Focus();
                    }
                    if (String.IsNullOrEmpty(txtinvdate.Text))
                    {
                        txtinvdate.Focus();
                    }
                    if (String.IsNullOrEmpty(TxtInvValue.Text))
                    {
                        TxtInvValue.Focus();
                    }
                    if (String.IsNullOrEmpty(txtsgst.Text))
                    {
                        txtsgst.Focus();
                    }
                }
                else
                {
                    try
                    {
                        SqlCommand check_Inv_No = new SqlCommand("SELECT COUNT(*) FROM TblCreditorInvMast WHERE InvNo = @inv and finyr=@finyear " +
                                                                 "and brcode=@branchcode and glid=@mglid and slid=@mslid", clsConnection.Conn);
                        check_Inv_No.Parameters.AddWithValue("@inv", TxtInv.Text);
                        check_Inv_No.Parameters.AddWithValue("@finyear", Global.finyr);
                        check_Inv_No.Parameters.AddWithValue("@Branchcode", Global.branch);
                        check_Inv_No.Parameters.AddWithValue("@mglid", label8.Text);
                        check_Inv_No.Parameters.AddWithValue("@mslid", label9.Text);
                        int InvExist = (int)check_Inv_No.ExecuteScalar();

                        if (InvExist > 0)
                        {
                            MessageBox.Show("Same Invoice No. already exist for this credior in this financial year.");
                            TxtInv.Text = "";
                            TxtJob.Text = "";
                            txtinvdate.Text = "";
                            TxtInvValue.Text = "0.00";
                            txtothers.Text = "0.00";
                            txtdiscount.Text = "0.00";
                            txtsgst.Text = "0.00";
                            txtcgst.Text = "0.00";
                            txtigst.Text = "0.00";
                            textnet.Text = "0.00";
                            TxtInv.Focus();
                        }
                        else
                        {
                            if (dataGridView1.Rows.Count == 0)
                            {
                                dataGridView1.Rows[0].Cells[0].Value = TxtInv.Text;
                                dataGridView1.Rows[0].Cells[1].Value = TxtJob.Text;
                                dataGridView1.Rows[0].Cells[2].Value = Convert.ToString(txtinvdate.Text);
                                dataGridView1.Rows[0].Cells[3].Value = TxtInvValue.Text;
                                dataGridView1.Rows[0].Cells[4].Value = txtothers.Text;
                                dataGridView1.Rows[0].Cells[5].Value = txtdiscount.Text;
                                dataGridView1.Rows[0].Cells[6].Value = txtsgst.Text;
                                dataGridView1.Rows[0].Cells[7].Value = txtcgst.Text;
                                dataGridView1.Rows[0].Cells[8].Value = txtigst.Text;
                                dataGridView1.Rows[0].Cells[9].Value = textnet.Text;
                                dataGridView1.Rows.Add();
                            }
                            else
                            {
                                Int32 i = dataGridView1.Rows.Count - 1;
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[i].Cells[0].Value = TxtInv.Text;
                                dataGridView1.Rows[i].Cells[1].Value = TxtJob.Text;
                                dataGridView1.Rows[i].Cells[2].Value = Convert.ToString(txtinvdate.Text);
                                dataGridView1.Rows[i].Cells[3].Value = TxtInvValue.Text;
                                dataGridView1.Rows[i].Cells[4].Value = txtothers.Text;
                                dataGridView1.Rows[i].Cells[5].Value = txtdiscount.Text;
                                dataGridView1.Rows[i].Cells[6].Value = txtsgst.Text;
                                dataGridView1.Rows[i].Cells[7].Value = txtcgst.Text;
                                dataGridView1.Rows[i].Cells[8].Value = txtigst.Text;
                                dataGridView1.Rows[i].Cells[9].Value = textnet.Text;

                            }
                            TxtInv.Text = "";
                            TxtJob.Text = "";
                            txtinvdate.Text = "";
                            TxtInvValue.Text = "0.00";
                            txtothers.Text = "0.00";
                            txtdiscount.Text = "0.00";
                            txtsgst.Text = "0.00";
                            txtcgst.Text = "0.00";
                            txtigst.Text = "0.00";
                            textnet.Text = "0.00";
                            TxtInv.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void txtothers_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtdiscount_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtsgst_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void txtcgst_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtigst_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtothers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                calculatenet();
                txtdiscount.SelectAll();
                txtdiscount.Focus();
            }
        }

        private void txtdiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                calculatenet();
                txtsgst.SelectAll();
                txtsgst.Focus();
            }
        }

        private void txtsgst_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtsgst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                calculatenet();
                txtcgst.SelectAll();
                txtcgst.Focus();
            }
        }

        private void txtcgst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == 13)
            {
                calculatenet();
                txtigst.SelectAll();
                txtigst.Focus();
            }
        }

        private void FrmCreditorInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
