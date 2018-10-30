using System;
using System.Collections.Generic;
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
using System.Globalization;

namespace FINNSOFT.UI
{
    public partial class FrmVoucherAlt : MetroFramework.Forms.MetroForm
    {
        DataSet ds = new DataSet();
        public FrmVoucherAlt()
        {
            InitializeComponent();
        }

        private DataTable dt;
        double totdebit=0.00;
        double totcredit=0.00;
        string vtype = "";
        Boolean isbank = false;
        string mglid = "";
        string mslid = "";
        DateTime vchdate;
        string chkdate = "", minventoryno = "";
        

        private void FrmVoucherAlt_Load(object sender, EventArgs e)
        {
            txtvchtype.Text = "Journal";
            vtype = "J";
            getvoucherno("J");
            isbank = false;
            lbltotdebit.Text = "";
            lbltotcredit.Text = "";
            object_visible();
            txtamttype.Text = "Dr.";
            txtamttype.Focus();
            dateTimePicker1.Value = DateTime.Today;

        }

        private void LoadData()
        {
            try
            {

                string qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "'";
                SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds = new DataSet();
                if (ds.Tables["gl"] != null)
                    ds.Tables["gl"].Clear();
                da.Fill(ds, "gl");

                qry = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "'";
                da = new SqlDataAdapter(qry, clsConnection.Conn);
                if (ds.Tables["sl"] != null)
                    ds.Tables["sl"].Clear();

                da.Fill(ds, "sl");

                DataTable dt = new DataTable();
                dt.TableName = "ledgers";

                dt.Columns.Clear();
                dt.Columns.Add("ledgname");
                dt.Columns.Add("glsl");

                for (int i = 0; i < ds.Tables["gl"].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables["gl"].Rows[i][0].ToString();
                    dr[1] = ds.Tables["gl"].Rows[i][1].ToString();

                    dt.Rows.Add(dr);
                }

                for (int i = 0; i < ds.Tables["sl"].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables["sl"].Rows[i][0].ToString();
                    dr[1] = ds.Tables["sl"].Rows[i][1].ToString();

                    dt.Rows.Add(dr);
                }


                dt.DefaultView.Sort = "ledgname asc";
                comboBox2.DataSource = null;
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "ledgname";
                comboBox2.ValueMember = "glsl";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ledgerbalance()
        {
            try
            {
                string balance_qry = "";
                lblbalance.Text = "";
                balance_qry = "select op_bal+dramt-cramt as balance from [LedgerBalance] where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID ='" + mglid + "' and slid='" + mslid + "'"; //Cash not present
                SqlDataAdapter da = new SqlDataAdapter(balance_qry, clsConnection.Conn);

                if (ds.Tables["lbal"] != null)
                    ds.Tables["lbal"].Clear();
                da.Fill(ds, "lbal");

                DataTable dt = new DataTable();
                dt.TableName = "ledger_bal";

                dt.Columns.Clear();
                dt.Columns.Add("cl_balance");

                for (int i = 0; i < ds.Tables["lbal"].Rows.Count; i++)
                {
                    lblbalance.Text = "Ledger Balance:"+ ds.Tables["lbal"].Rows[i][0].ToString();
                    //lblbalance.Text = string.Format("{0:#,##0.00}", double.Parse(lblbalance.Text));
                }
                da.Dispose();
                dt.Dispose();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (comboBox2.Text != "")
                    {
                        string str = comboBox2.SelectedValue.ToString();
                        if (str != "")
                        {
                            mglid = str.Substring(0, str.IndexOf("-"));
                            mslid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));
                            lblglslid.Text = str;
                            textBox2.Focus();
                        }
                    }
                    else
                    {
                        if (Math.Abs(totdebit - totcredit) == 0)
                        {
                            if (isbank == true)
                            {
                                instrno.Focus();
                            }
                            else
                            {
                                txtnarration.Focus();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                    (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    insert_data_into_grid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bttnadd_Click(object sender, EventArgs e)
        {
            insert_data_into_grid();
        }

        private void insert_data_into_grid()
            {
            if (textBox2.Text != "" && Convert.ToDouble(textBox2.Text) != 0)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = txtamttype.Text;
                dataGridView1.Rows[index].Cells[1].Value = mglid;
                dataGridView1.Rows[index].Cells[2].Value = mslid;
                dataGridView1.Rows[index].Cells[3].Value = comboBox2.Text;
                if (txtamttype.Text == "Dr.")
                {
                    totdebit += Convert.ToDouble(textBox2.Text);
                    lbltotdebit.Text = Convert.ToString(Math.Round(totdebit, 2));
                    //dataGridView1.Rows[index].Cells[4].Value = textBox2.Text;
                    dataGridView1.Rows[index].Cells[4].Value = string.Format("{0:#,##0.00}", double.Parse(textBox2.Text));
                }
                else
                {
                    totcredit += Convert.ToDouble(textBox2.Text);
                    lbltotcredit.Text = Convert.ToString(Math.Round(totcredit, 2));
                    //dataGridView1.Rows[index].Cells[5].Value = textBox2.Text;
                    dataGridView1.Rows[index].Cells[5].Value = string.Format("{0:#,##0.00}", double.Parse(textBox2.Text));
                }
                lblglslid.Text = "";
                comboBox2.Text = "";
                check_amttype();
                txtamttype.Focus();
            }
            else
            {
                if (Math.Abs(totdebit - totcredit) == 0)
                {
                    txtbillnum.Focus();
                }
                else
                {
                    check_amttype();
                }
            }
        }
        private void check_amttype()
        {
            if (totdebit > totcredit)
            {
                txtamttype.Text = "Cr.";
                textBox2.Text = Math.Round(totdebit - totcredit,2).ToString();
            }
            else if (totcredit > totdebit)
            {
                txtamttype.Text = "Dr.";
                textBox2.Text = Math.Round(totcredit - totdebit,2).ToString();
            }
            else
            {
                txtamttype.Text = "Dr.";
                textBox2.Text = Math.Round(totdebit - totcredit,2).ToString(); //that means 0
                if (isbank == true)
                {
                    instrno.Focus();
                }
                else
                {
                    txtnarration.Focus();
                }

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if click is on new row or header row
            if (e.RowIndex == dataGridView1.NewRowIndex || e.RowIndex < 0)
                return;

            //Check if click is on specific column 
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                //Put some logic here, for example to remove row from your binding list.
                int row = dataGridView1.CurrentCell.RowIndex;
                if (dataGridView1.Rows[row].Cells[0].Value.ToString() == "Dr.")
                {
                    totdebit -= Convert.ToDouble(dataGridView1.Rows[row].Cells["Debit"].Value);
                    lbltotdebit.Text = Convert.ToString(Math.Round(totdebit, 2));
                    check_amttype();
                }
                else
                {
                    totcredit -= Convert.ToDouble(dataGridView1.Rows[row].Cells["Credit"].Value);
                    lbltotcredit.Text = Convert.ToString(Math.Round(totcredit, 2));
                    check_amttype();
                }
                dataGridView1.Rows.RemoveAt(row);
            }
        }

        private void bttnjournal_Click(object sender, EventArgs e)
        {
            bttnsave.Text = "Save";
            dateTimePicker1.Value = DateTime.Today;
            txtvchtype.Text = "Journal";
            vtype = "J";
            getvoucherno("J");
            isbank = false;
            lbltotdebit.Text = "";
            lbltotcredit.Text = "";
            object_visible();
            txtamttype.Text = "Dr.";
            txtamttype.Focus();
            txtamttype.SelectionLength = 3;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void bttncontra_Click(object sender, EventArgs e)
        {
            bttnsave.Text = "Save";
            dateTimePicker1.Value = DateTime.Today;
            txtvchtype.Text = "Contra";
            vtype = "T";
            getvoucherno("T");
            isbank = true;
            lbltotdebit.Text = "";
            lbltotcredit.Text = "";
            object_visible();
            txtamttype.Text = "Dr.";
            txtamttype.Focus();
            txtamttype.SelectionLength = 3;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void bttnpayment_Click(object sender, EventArgs e)
        {
            bttnsave.Text = "Save";
            dateTimePicker1.Value = DateTime.Today;
            txtvchtype.Text = "Payment";
            vtype = "Y";
            getvoucherno("Y");
            isbank = true;
            lbltotdebit.Text = "";
            lbltotcredit.Text = "";
            object_visible();
            txtamttype.Text = "Dr.";
            txtamttype.Focus();
            txtamttype.SelectionLength = 3;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void bttnreceipt_Click(object sender, EventArgs e)
        {
            bttnsave.Text = "Save";
            dateTimePicker1.Value = DateTime.Today;
            txtvchtype.Text = "Receipt";
            vtype = "R";
            getvoucherno("R");
            isbank = true;
            lbltotdebit.Text = "";
            lbltotcredit.Text = "";
            object_visible();
            txtamttype.Text = "Dr.";
            txtamttype.Focus();
            txtamttype.SelectionLength = 3;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }

        private void getvoucherno(string type)
        {
            try
            {
                string qry = "select isnull(MAX(vno),0) from TblVOUCHER where TRANTYPE = '" + type + "' and brcode = '" + Global.branch + "' and finyr='" + Global.finyr + "'";
                SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
                string vno = Convert.ToString(com.ExecuteScalar());
                int vno1 = Convert.ToInt32(vno) + 1;
                txtvchno.Text = vno1.ToString();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmVoucherAlt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode.ToString() == "F2")
                {
                    txtvchtype.Text = "Journal";
                    vtype = "J";
                    getvoucherno("J");
                    isbank = false;
                    object_visible();
                    txtamttype.Text = "Dr.";
                    txtamttype.Focus();
                }
                else if(e.KeyCode.ToString() == "F3")
                {
                    txtvchtype.Text = "Contra";
                    vtype = "T";
                    getvoucherno("T");
                    isbank = true;
                    object_visible();
                    txtamttype.Focus();
                }
                else if (e.KeyCode.ToString() == "F4")
                {
                    txtvchtype.Text = "Payment";
                    vtype = "Y";
                    getvoucherno("Y");
                    isbank = true;
                    object_visible();
                    txtamttype.Focus();
                }
                else if (e.KeyCode.ToString() == "F5")
                {
                    txtvchtype.Text = "Receipt";
                    vtype = "R";
                    getvoucherno("R");
                    isbank = true;
                    object_visible();
                    txtamttype.Focus();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void FrmVoucherAlt_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void instrno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (instrno.Text != "")
                    {
                        issuedt.Select();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void object_visible()
        {
            if (isbank == false)
            {
                lblchqno.Visible = false;
                instrno.Visible = false;
                lblissuedt.Visible = false;
                issuedt.Visible = false;
            }
            else
            {
                lblchqno.Visible = true;
                instrno.Visible = true;
                lblissuedt.Visible = true;
                issuedt.Visible = true;
            }
        }

        private void issuedt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (instrno.Text != "")
                    {
                        txtnarration.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.SelectionStart = 0;
                textBox2.SelectionLength = textBox2.Text.Length;
            }
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtnarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtnarration.Text != "")
                    {
                        bttnsave.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void bttnsave_Click(object sender, EventArgs e)
        {
            if ((bttnsave.Text == "Save") && (lbltotdebit.Text == lbltotcredit.Text) && totdebit > 0 && totcredit > 0 && totdebit==totcredit)
            {
                SqlTransaction transaction;
                transaction = clsConnection.Conn.BeginTransaction();
                try
                {
                    String qry = $"select isnull(MAX(vno),0) as maxvno from TblVOUCHER where TRANTYPE = '{vtype}' and brcode='{Global.branch}' and finyr='{Global.finyr}'";
                    SqlCommand cmd = new SqlCommand(qry, clsConnection.Conn,transaction);
                    string vno1 = Convert.ToString(cmd.ExecuteScalar());
                    int vno = Convert.ToInt32(vno1) + 1;
                    
                    qry = @"insert into tblvoucher (finyr,BrCode,VDT,VNO,trantype,iscontra,bill_num,cash_bank,inventoryvno,instrument_no,instrument_date,narration) values 
                          (@Finyr,@BrCode,@VDT,@VNO,@trantype,@iscontra,@billnum,@cshbank,@inventoryvno,@chqno,@issuedate,@narration)";
                    SqlCommand com = new SqlCommand(qry, clsConnection.Conn, transaction);
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@finyr", Global.finyr);
                    com.Parameters.AddWithValue("@BrCode", Global.branch);
                    com.Parameters.AddWithValue("@VDT", dateTimePicker1.Value.ToString("dd/MM/yyyy"));
                    com.Parameters.AddWithValue("@VNO", vno);
                    com.Parameters.AddWithValue("@trantype", vtype);
                    if (vtype == "T")
                    {
                        com.Parameters.AddWithValue("@iscontra", 1);
                        com.Parameters.AddWithValue("@cshbank", "C");
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@iscontra", 0);
                        com.Parameters.AddWithValue("@cshbank", "");
                    }
                    com.Parameters.AddWithValue("@inventoryvno", "Normal Voucher");
                    if (isbank==true)
                    {
                        com.Parameters.AddWithValue("@chqno", instrno.Text);
                        com.Parameters.AddWithValue("@issuedate", issuedt.Value.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@chqno", "");
                        com.Parameters.AddWithValue("@issuedate", "");
                    }
                    com.Parameters.AddWithValue("@billnum", txtbillnum.Text);
                    com.Parameters.AddWithValue("@narration", txtnarration.Text);
                    com.ExecuteNonQuery();
                    qry = "";
                    
                    //insert Ledger records
                    SqlCommand com2 = new SqlCommand(qry, clsConnection.Conn, transaction);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (((dataGridView1.Rows[i].Cells["Debit"].Value != null) || (dataGridView1.Rows[i].Cells["Credit"].Value != null)) && (Convert.ToInt32(lbltotdebit.Text)==Convert.ToInt32(lbltotcredit.Text)))
                        {
                            qry = "insert into tblledger (finyr,Brcode,Trantype,vno,glid,slid,amt,amttype) values(@Finyr,@Brcode,@Trantype,@vno,@glid,@slid,@amt,@amttype)";
                            
                            com2 = new SqlCommand(qry, clsConnection.Conn, transaction);
                            com2.Parameters.Clear();
                            com2.Parameters.AddWithValue("@Finyr", Global.finyr);
                            com2.Parameters.AddWithValue("@Brcode", Global.branch);
                            com2.Parameters.AddWithValue("@Trantype", vtype);
                            com2.Parameters.AddWithValue("@vno", vno);
                            com2.Parameters.AddWithValue("@glid", dataGridView1.Rows[i].Cells["glid"].Value);
                            com2.Parameters.AddWithValue("@slid", dataGridView1.Rows[i].Cells["slid"].Value);
                            string ptype = dataGridView1.Rows[i].Cells["pmttype"].Value.ToString();
                            string mtype = "";
                            if (ptype == "Dr.")
                            {
                                mtype = "D";
                                com2.Parameters.AddWithValue("@amt", Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value));
                            }
                            else
                            {
                                mtype = "C";
                                com2.Parameters.AddWithValue("@amt", Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value));
                            }
                            com2.Parameters.AddWithValue("@amttype", mtype);
                            com2.ExecuteNonQuery();
                        }
                    }

                    //Insert Audit Tran
                    qry = "";
                    qry = @"insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values 
                          (@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
                    SqlCommand com3 = new SqlCommand(qry, clsConnection.Conn, transaction);
                    com3.Parameters.Clear();
                    com3.Parameters.AddWithValue("@HostName", Global.FetchHost());
                    com3.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
                    com3.Parameters.AddWithValue("@menu", "New Voucher Entry");
                    com3.Parameters.AddWithValue("@OperationType", "Insert");
                    com3.Parameters.AddWithValue("@username", Global.username);
                    com3.Parameters.AddWithValue("@Remarks", "New Voucher Entry of VNo. " + vno.ToString() + " and VType = " + vtype);

                    com3.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("'"+ txtvchtype.Text +" ' Voucher :" + vno + " saved successfully.");
                    cmd.Dispose();
                    com.Dispose();
                    com2.Dispose();
                    com3.Dispose();
                    txtbillnum.Text = "";
                    instrno.Text = "";
                    txtnarration.Text = "";
                    dataGridView1.Rows.Clear();
                    getvoucherno(vtype);
                    totdebit = 0;
                    totcredit = 0;
                    txtamttype.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Commit Exception Type:" + ex.GetType());
                    MessageBox.Show("Message:" + ex.Message);

                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        MessageBox.Show("Rollback Exception Type:" + ex2.GetType());
                        MessageBox.Show("  Message:" + ex2.Message);
                    }
                }
            }
            else if ((bttnsave.Text == "Update") && (lbltotdebit.Text == lbltotcredit.Text) && totdebit > 0 && totcredit > 0 && totdebit == totcredit)
            {
                int mvno = Convert.ToInt32(txtvchno.Text);
                SqlTransaction transaction;
                transaction = clsConnection.Conn.BeginTransaction();
                try
                {
                    //update tblvoucher
                    var updtqry = @"UPDATE TBLVOUCHER SET vdt = @vdt, trantype = @trantype,iscontra=@iscontra,cash_bank=@cshbank, 
                    bill_num=@billnum,inventoryvno=@inventoryvno,instrument_no=@chqno,instrument_date=@issuedate,narration=@narration
                    Where finyr = @finyr and BrCode=@BrCode and trantype=@trantype and vno = @mvno";

                    SqlCommand updtcom = new SqlCommand(updtqry, clsConnection.Conn, transaction);
                    updtcom.Parameters.Clear();
                    updtcom.Parameters.AddWithValue("@finyr", Global.finyr);
                    updtcom.Parameters.AddWithValue("@BrCode", Global.branch);
                    updtcom.Parameters.AddWithValue("@trantype", vtype);
                    updtcom.Parameters.AddWithValue("@mvno", mvno);
                    updtcom.Parameters.AddWithValue("@VDT", dateTimePicker1.Value.ToString("dd/MM/yyyy"));

                    if (vtype == "T")
                    {
                        updtcom.Parameters.AddWithValue("@iscontra", 1);
                        updtcom.Parameters.AddWithValue("@cshbank", "C");
                    }
                    else
                    {
                        updtcom.Parameters.AddWithValue("@iscontra", 0);
                        updtcom.Parameters.AddWithValue("@cshbank", "");
                    }
                    updtcom.Parameters.AddWithValue("@inventoryVno", minventoryno);
                    if (isbank == true)
                    {
                        updtcom.Parameters.AddWithValue("@chqno", instrno.Text);
                        updtcom.Parameters.AddWithValue("@issuedate", issuedt.Value.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        updtcom.Parameters.AddWithValue("@chqno", "");
                        updtcom.Parameters.AddWithValue("@issuedate", "");
                    }
                        updtcom.Parameters.AddWithValue("@billnum", txtbillnum.Text);
                        updtcom.Parameters.AddWithValue("@narration", txtnarration.Text);
                    updtcom.ExecuteNonQuery();

                    updtqry = "";
                    //delete relevant records in tblledger table
                    string qry = $"DELETE FROM TBLLEDGER where VNO='{mvno}' and TRANTYPE = '{vtype}' and brcode='{Global.branch}' and finyr='{Global.finyr}'";
                    SqlCommand cmd = new SqlCommand(qry, clsConnection.Conn, transaction);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    string insertqry = "";
                    SqlCommand lcom = new SqlCommand(insertqry, clsConnection.Conn, transaction);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (((dataGridView1.Rows[i].Cells["Debit"].Value != null) || (dataGridView1.Rows[i].Cells["Credit"].Value != null)) && (Convert.ToInt32(lbltotdebit.Text) == Convert.ToInt32(lbltotcredit.Text)))
                    {

                        insertqry = @"insert into tblledger (finyr,Brcode,Trantype,vno,glid,slid,amt,amttype) 
                                        values(@Finyr,@Brcode,@Trantype,@mvno,@glid,@slid,@amt,@amttype)";

                        lcom = new SqlCommand(insertqry, clsConnection.Conn, transaction);
                        lcom.Parameters.Clear();
                        lcom.Parameters.AddWithValue("@Finyr", Global.finyr);
                        lcom.Parameters.AddWithValue("@Brcode", Global.branch);
                        lcom.Parameters.AddWithValue("@Trantype", vtype);
                        lcom.Parameters.AddWithValue("@mvno", mvno);
                        lcom.Parameters.AddWithValue("@glid", dataGridView1.Rows[i].Cells["glid"].Value);
                        lcom.Parameters.AddWithValue("@slid", dataGridView1.Rows[i].Cells["slid"].Value);
                        string ptype = dataGridView1.Rows[i].Cells["pmttype"].Value.ToString();
                        string mtype = "";
                        if (ptype == "Dr.")
                        {
                            mtype = "D";
                            lcom.Parameters.AddWithValue("@amt", Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value));
                        }
                        else
                        {
                            mtype = "C";
                            lcom.Parameters.AddWithValue("@amt", Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value));
                        }
                        lcom.Parameters.AddWithValue("@amttype", mtype);
                        lcom.ExecuteNonQuery();
                    }

                }
                    //Audittrail
                    //Insert Audit Tran
                    qry = "";
                    qry = @"insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values 
                          (@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
                    SqlCommand com3 = new SqlCommand(qry, clsConnection.Conn, transaction);
                    com3.Parameters.Clear();
                    com3.Parameters.AddWithValue("@HostName", Global.FetchHost());
                    com3.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
                    com3.Parameters.AddWithValue("@menu", "New Voucher Entry");
                    com3.Parameters.AddWithValue("@OperationType", "Insert");
                    com3.Parameters.AddWithValue("@username", Global.username);
                    com3.Parameters.AddWithValue("@Remarks", "New Voucher Entry of VNo. " + mvno.ToString() + " and VType = " + vtype);

                    com3.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("'" + txtvchtype.Text + " ' Voucher :" + mvno + " updated successfully.");
                    updtcom.Dispose();
                    lcom.Dispose();
                    com3.Dispose();
                    dataGridView1.Rows.Clear();
                    bttnsave.Text = "Save";
                    dateTimePicker1.Value = DateTime.Today;
                    getvoucherno(vtype);
                    totdebit = 0;
                    totcredit = 0;
                    txtbillnum.Text = "";
                    instrno.Text = "";
                    txtnarration.Text = "";
                    txtamttype.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Commit Exception Type:" + ex.GetType());
                    MessageBox.Show("Message:" + ex.Message);

                    // Attempt to roll back the transaction. 
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred 
                        // on the server that would cause the rollback to fail, such as 
                        // a closed connection.
                        MessageBox.Show("Rollback Exception Type:" + ex2.GetType());
                        MessageBox.Show("  Message:" + ex2.Message);
                    }
                }

            }
            else
            {
                MessageBox.Show("Empty Voucher can not be saved!");
            }
        }

        private void txtamttype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (txtamttype.Text == "")
                    {
                        check_amttype();
                        comboBox2.Focus();
                    }
                    else if (txtamttype.Text.ToUpper() == "D" || txtamttype.Text.ToUpper() == "DR" || txtamttype.Text.ToUpper() == "DR.") 
                    {
                        txtamttype.Text = "Dr.";
                        comboBox2.Focus();
                    }
                    else if (txtamttype.Text.ToUpper() == "C" || txtamttype.Text.ToUpper() == "CR" || txtamttype.Text.ToUpper() == "CR.")
                    {
                        txtamttype.Text = "Cr.";
                        comboBox2.Focus();
                    }
                    else if (txtamttype.Text.ToUpper() != "CR." || txtamttype.Text.ToUpper() != "DR.")
                    {
                        MessageBox.Show("Only C or D allowed");
                        check_amttype();
                        txtamttype.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtamttype_Enter(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtamttype.Text))
            {
                txtamttype.SelectionStart = 0;
                txtamttype.SelectionLength = txtamttype.Text.Length;
            }
        }

        private void txtamttype_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtamttype.Text))
            {
                txtamttype.SelectionStart = 0;
                txtamttype.SelectionLength = txtamttype.Text.Length;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = string.Format("{0:#,##0.00}", double.Parse(textBox2.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            ledgerbalance();
        }

        private void FrmVoucherAlt_Activated(object sender, EventArgs e)
        {
            txtvchtype.Text = "Journal";
            vtype = "J";
            getvoucherno("J");
            isbank = false;
            lbltotdebit.Text = "";
            lbltotcredit.Text = "";
            object_visible();
            txtamttype.Text = "Dr.";
            txtamttype.Focus();
            txtamttype.SelectionLength = 3;
        }

        private void bttnsearch_Click(object sender, EventArgs e)
        {
            bttnsave.Text = "Update";
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            searchvoucher();
        }

        private void txtvchno_Click(object sender, EventArgs e)
        {
            txtvchno.SelectionLength = txtvchno.TextLength;
            txtvchno.Focus();
        }

        private void txtbillnum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (isbank == true)
                    {
                        instrno.Focus();
                    }
                    else
                    {
                        txtnarration.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtvchno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    bttnsave.Text = "Update";
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                    searchvoucher();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comboBox2_Enter(object sender, EventArgs e)
        {
            ledger_refresh();
        }

        private void ledger_refresh()
        {
            try
            {
                string qry = "", qry1 = "";

                if (txtamttype.Text == "Cr.")
                {
                    if (vtype == "J")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Cash not present

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <>1"; //No Cash and cash Eqivalent
                    }
                    else if (vtype == "R")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Cash not present

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <>1"; //No Cash and cash Eqivalent

                    }
                    else if (vtype == "Y")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash 

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID in(1,6) or slid=256 or slid=255"; //Only Cash and cash Eqivalent

                    }
                    else if (vtype == "T")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash 

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash and cash Eqivalent

                    }
                }
                else if (txtamttype.Text == "Dr.")
                {
                    if (vtype == "J")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Cash not present

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <>1"; //No Cash and cash Eqivalent
                    }
                    else if (vtype == "R")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash present

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1 or slid=255 or slid=139 or slid=241 or slid=256 or slid=136"; //Only Cash and cash Eqivalent

                    }
                    else if (vtype == "Y")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Only Cash not present

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 00001 or slid=256 or slid=255"; //No Cash and cash Eqivalent

                    }
                    else if (vtype == "T")
                    {
                        qry = "select distinct GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash 

                        qry1 = "select distinct SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 00001"; //Only Bank present

                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds = new DataSet();
                if (ds.Tables["gl"] != null)
                    ds.Tables["gl"].Clear();
                da.Fill(ds, "gl");


                da = new SqlDataAdapter(qry1, clsConnection.Conn);
                if (ds.Tables["sl"] != null)
                    ds.Tables["sl"].Clear();

                da.Fill(ds, "sl");

                DataTable dt = new DataTable();
                dt.TableName = "ledgers";

                dt.Columns.Clear();
                dt.Columns.Add("ledgname");
                dt.Columns.Add("glsl");

                for (int i = 0; i < ds.Tables["gl"].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables["gl"].Rows[i][0].ToString();
                    dr[1] = ds.Tables["gl"].Rows[i][1].ToString();

                    dt.Rows.Add(dr);
                }

                for (int i = 0; i < ds.Tables["sl"].Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables["sl"].Rows[i][0].ToString();
                    dr[1] = ds.Tables["sl"].Rows[i][1].ToString();

                    dt.Rows.Add(dr);
                }


                dt.DefaultView.Sort = "ledgname asc";

                comboBox2.DataSource = null;
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "ledgname";
                comboBox2.ValueMember = "glsl";
                comboBox2.SelectionStart = 0;
                comboBox2.SelectionLength = comboBox2.Text.Length;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchvoucher()
        {
            if (txtvchno.Text != "")
            {
                try
                {
                    string qry = "select * from tblvoucher where vno = " + txtvchno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and finyr='" + Global.finyr + "'";
                    SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                    DataSet ds = new DataSet();

                    if (ds.Tables["voucher"] != null)
                        ds.Tables["voucher"].Clear();

                    da.Fill(ds, "voucher");
                    dataGridView1.Rows.Clear();

                    if (ds.Tables["voucher"].Rows.Count > 0)
                    {
                        vchdate = Convert.ToDateTime(ds.Tables["voucher"].Rows[0]["vdt"]);
                        if (vchdate < Convert.ToDateTime(Global.gFromDt) || vchdate > Convert.ToDateTime(Global.gToDt))
                        {
                            MessageBox.Show("Voucher + txtvchno.Text + of '+ vtype +' is of date:+vchdate+ & vbCrLf & and not in + Global.finyr + financial year, can not continue!");
                            txtvchno.Select();
                        }
                        else
                        {
                            string chkdate = "",minventoryno="";
                            totdebit = 0;
                            totcredit = 0;
                            dateTimePicker1.Value = Convert.ToDateTime(Convert.ToDateTime(ds.Tables["voucher"].Rows[0]["vdt"]).ToString("dd/MM/yyyy"));
                            txtbillnum.Text = ds.Tables["voucher"].Rows[0]["bill_num"].ToString();
                            txtnarration.Text = ds.Tables["voucher"].Rows[0]["Narration"].ToString();
                            instrno.Text = ds.Tables["voucher"].Rows[0]["Instrument_No"].ToString();
                            chkdate = ds.Tables["voucher"].Rows[0]["Instrument_Date"].ToString();
                            minventoryno = ds.Tables["voucher"].Rows[0]["inventoryVno"].ToString();
                            if (chkdate != "")
                            {
                                issuedt.Value = Convert.ToDateTime(Convert.ToDateTime(ds.Tables["voucher"].Rows[0]["Instrument_Date"]).ToString());
                            }

                            qry = "select * from tblledger where vno = " + txtvchno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and finyr='" + Global.finyr + "'";
                            if (ds.Tables["ledger"] != null)
                                ds.Tables["ledger"].Clear();

                            da = new SqlDataAdapter(qry, clsConnection.Conn);
                            da.Fill(ds, "ledger");
                            for (int i = 0; i < ds.Tables["ledger"].Rows.Count; i++)
                            {
                                string glid = ds.Tables["ledger"].Rows[i]["GLID"].ToString();
                                string slid = ds.Tables["ledger"].Rows[i]["SLID"].ToString();
                                string tablename = "TblSLmast";
                                string fieldname = "SL_L_NAME";
                                string whereclause = " SLID = " + slid + " and GLID = " + glid + " and brcode='" + Global.branch + "' and finyr='" + Global.finyr + "'";

                                if (slid == "00" || slid == "" || slid == "0")
                                {
                                    tablename = "TblGLmast";
                                    fieldname = "GL_L_Name";
                                    whereclause = " GLID = " + glid;
                                }

                                qry = "select " + fieldname + " from " + tablename + " where " + whereclause;
                                SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
                                string lname = Convert.ToString(com.ExecuteScalar());

                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["glid"].Value = glid;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["slid"].Value = slid;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Particulars"].Value = lname;
                                if (ds.Tables["ledger"].Rows[i]["AMTTYPE"].ToString() == "D")
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Pmttype"].Value = "Dr.";
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Debit"].Value = ds.Tables["ledger"].Rows[i]["AMT"].ToString();
                                    totdebit += Convert.ToDouble(ds.Tables["ledger"].Rows[i]["AMT"]);
                                }
                                else
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Pmttype"].Value = "Cr.";
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Credit"].Value = ds.Tables["ledger"].Rows[i]["AMT"].ToString();
                                    totcredit += Convert.ToDouble(ds.Tables["ledger"].Rows[i]["AMT"]);
                                }
                                //txtnarration.Text = ds.Tables["ledger"].Rows[i]["NAR"].ToString(); should be from tblvoucher
                                lbltotdebit.Text = Convert.ToString(Math.Round(totdebit, 2));
                                lbltotcredit.Text = Convert.ToString(Math.Round(totcredit, 2));
                            }
                            txtvchno.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
