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

namespace FINNSOFT
{
    public partial class frmVoucher : Form
    {
        public static int rowindex;

        double dramt = 0.0, cramt = 0.0;
        DateTime vchdt;
        int cntr;
        string vtype1;
        int cnt1 = 0;

        public frmVoucher()
        {
            InitializeComponent();
        }

        DateTime MaxDt;
        DateTime VchDate;

        private void txtvno_KeyUp(object sender, KeyEventArgs e)
        {

            
            if (e.KeyValue == 13)
            {

                string str = lblbtype.Text;
                string vtype = "";
                if (str == "Journal")
                    vtype = "J";
                else if (str == "Payment")
                    vtype = "Y";
                else if (str == "Receipt")
                    vtype = "R";
                else if (str == "Contra")
                    vtype = "T";
                    

                int checkvno = Convert.ToInt32(txtvno.Text);

               string Vnoqry = "select isnull(MAX(vno),0) from TblVOUCHER where TRANTYPE = '" + vtype + "' and brcode='" + Global.branch + "' and finyr='"+ Global.finyr +"'";
               SqlCommand  VnoCom = new SqlCommand(Vnoqry, clsConnection.Conn);
               string vno1 = Convert.ToString(VnoCom.ExecuteScalar());
               int vno2 = Convert.ToInt32(vno1) + 1;
                

                if (checkvno > vno2)

                {

                    MessageBox.Show("Voucher Number not matching with current sequence");
                    txtvno.Select();
                    btnSave.Enabled = false;

                }

                else 
                {

                    dateTimePicker1.Select();
                    btnSave.Enabled = true;
                }

            }
        }

        private void dateTimePicker1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {

                string frdt = dateTimePicker1.Value.ToString("dd/mm/yyyy");

                string gblToDt = Global.gToDt.ToString();
                
                if (Convert.ToDateTime(dateTimePicker1.Text) < Global.gFromDt || Convert.ToDateTime(dateTimePicker1.Text) > Global.gToDt)
                {
                    MessageBox.Show("Date not valid for current financial year.");
                                       
                }

                else
                { 
                    if (dataGridView1.Rows.Count == 0)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Focus();
                        dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                        dataGridView1.Rows[0].Cells[0].Value = "By";
                        dataGridView1.BeginEdit(true);
                    }
                }
                string qry1 = "select max(vdt) from tblvoucher where brcode='" + Global.branch + "' and finyr='"+ Global.finyr +"'";

                SqlDataReader sread = null;
                SqlCommand cmd1 = new SqlCommand(qry1, clsConnection.Conn);

                sread = cmd1.ExecuteReader();

                sread.Read();                  
                                                

               //MaxDt = Convert.ToDateTime(sread.GetValue(0));

                sread.Close();

                if (Convert.ToDateTime(dateTimePicker1.Text) < Convert.ToDateTime(MaxDt) && vtype1!="T" && Global.userrole!="Admin")
                {

                    MessageBox.Show("Back date entry cannot be done. Press escape to continue.");
                    dataGridView1.Rows.Clear();
                    dateTimePicker1.Select();
                    
                }

               
            }
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            

            if (e.ColumnIndex == 0)
            {
                try
                {
                    
                    string str = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                    if (str.ToUpper() == "B" || str.ToUpper() == "BY")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = "By";
                        dataGridView1.RefreshEdit();

                        double totby = 0.0, totto = 0.0;

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                            {
                                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "By")
                                {
                                    totby += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                                    lbltotdr.Text = Convert.ToString(Math.Round(totby, 2));
                                    dataGridView1.Rows[i + 1].Cells[0].Value = "To";
                                }
                                else if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                                {
                                    totto += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                                    lbltotcr.Text = Convert.ToString(Math.Round(totto, 2));
                                }
                            }
                        }



                        double totamt = totto - totby;
                        if (totamt > 0)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["Debit"].Value = totamt;
                            dataGridView1.RefreshEdit();
                        }
                    }
                    else if (str.ToUpper() == "T" || str.ToUpper() == "TO")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = "To";
                        dataGridView1.RefreshEdit();


                        double totby = 0.0, totto = 0.0;

                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                            {
                                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "By")
                                {
                                    totby += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                                    lbltotdr.Text = Convert.ToString(Math.Round(totby, 2));
                                    dataGridView1.Rows[i + 1].Cells[0].Value = "To";
                                }

                                if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                                {
                                    totto += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                                    lbltotcr.Text = Convert.ToString(Math.Round(totto, 2));
                                }
                            }
                        }


                        double totamt = totby - totto;
                        if (totamt > 0)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["Credit"].Value = totamt;
                            lbltotcr.Text = Convert.ToString(Math.Round(totto, 2));
                            dataGridView1.RefreshEdit();

                        }


                    }
                    else
                    {
                        MessageBox.Show("You are not allowed to enter any value except 'TO' and 'BY'");
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value = "By";
                        dataGridView1.RefreshEdit();
                    }

                }


                catch (Exception)
                { }
            }
            else if (e.ColumnIndex == 3) //Ledger Name
            {
                try
                {
                    panel1.Visible = true;
                    string str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    rowindex = e.RowIndex;
                    textBox1.Text = str;
                    textBox1.Focus();
                    textBox1.SelectionStart = textBox1.Text.Length;
                    
                }
                catch (Exception)
                { }
            }

            else if (e.ColumnIndex == 4) //Amount
            {
                double totby = 0.0, totto = 0.0;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "By")
                        {
                            totby += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                            lbltotdr.Text = Convert.ToString(Math.Round(totby, 2));
                        }

                    }

                        if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                        {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "To")
                        {
                            totto += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                            lbltotcr.Text = Convert.ToString(Math.Round(totto, 2));
                        }
                    }

                }
            }

            }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                int row = dataGridView1.CurrentCell.RowIndex;
                int col = dataGridView1.CurrentCell.ColumnIndex;
                
                if (col == 3)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells["Credit"];
                    dataGridView1.BeginEdit(true);

                }
            }

            if (e.KeyCode.ToString() == "Escape")
            {

                double totby = 0.0, totto = 0.0;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "By")
                        {
                            totby += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                            lbltotdr.Text = Convert.ToString(Math.Round(totby, 2));
                        }

                    }

                    if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "To")
                        {
                            totto += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                            lbltotcr.Text = Convert.ToString(Math.Round(totto, 2));
                        }
                    }

                }

                txtnarr.Select();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                int row = dataGridView1.CurrentCell.RowIndex;
                int col = dataGridView1.CurrentCell.ColumnIndex;
                MessageBox.Show(col.ToString());


                if (col == 3)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells["Credit"];
                    dataGridView1.BeginEdit(true);

                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void getmaxjid(string type)
        {
            string qry = "select isnull(MAX(vno),0) from TblVOUCHER where TRANTYPE = '" + type + "' and brcode = '" + Global.branch + "' and finyr='"+ Global.finyr +"'";
            SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
            string vno = Convert.ToString(com.ExecuteScalar());
            int vno1 = Convert.ToInt32(vno) + 1;

            txtvno.Text = vno1.ToString();
            string str = "";
            if (type == "J")
                str = "Journal";
            else if (type == "Y")
                str = "Payment";
            else if (type == "R")
                str = "Receipt";
            else if (type == "T")
                str = "Contra";

            lblbtype.Text = str;
        }

        private void frmVoucher_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            fillledgers();
            getmaxjid("J");
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Focus();
            issuedt.Value = DateTime.Now;
            
        }

        private void fillledgers()
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

            listBox1.DataSource = null;
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "ledgname";
            listBox1.ValueMember = "glsl";

            

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            try
            {
                dataGridView1.Rows[rowindex].Cells[3].Value = str;
                dataGridView1.RefreshEdit();

                listBox1.SelectedIndex = listBox1.FindString(str, 0);
                // dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                // dataGridView1.Rows[0].Cells[0].Value = "By";
                
            }
            catch (Exception)
            { }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string str = e.KeyCode.ToString();
			if (str == "Down")
			{
				listBox1.Focus();
				textBox1.Text = listBox1.Text;

			}

			else if (str == "Up")

			{
				listBox1.Focus();
				textBox1.Text = listBox1.Text;
			}

            if (str == "Return")
            {
                listBox1.SelectedIndex = listBox1.FindString(textBox1.Text, 0);
               str = listBox1.SelectedValue.ToString();

                string glid = str.Substring(0, str.IndexOf("-"));
                string slid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));

                dataGridView1.Rows[rowindex].Cells[1].Value = glid;
                dataGridView1.Rows[rowindex].Cells[2].Value = slid;

				textBox1.Text = listBox1.Text;
				listBox1.Focus();


				panel1.Visible = false;

                dataGridView1.BeginEdit(true);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Focused == true)
            {				
                textBox1.Text = listBox1.Text;


                listBox1.Focus();
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                string str = listBox1.SelectedValue.ToString();

                string glid = str.Substring(0, str.IndexOf("-"));
                string slid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));

                dataGridView1.Rows[rowindex].Cells[1].Value = glid;
                dataGridView1.Rows[rowindex].Cells[2].Value = slid;

				
				panel1.Visible = false;

                dataGridView1.BeginEdit(true);
            }
        }

        private void btnJ_Click(object sender, EventArgs e)
        {
            
            //SqlDataReader Sread = null;
            //string qry1 = "select max(vdt) from tblvoucher where brcode='"+ Global.branch +"' and trantype='J'";

            //SqlCommand cmd1 = new SqlCommand(qry1, clsConnection.Conn);
           
            //    Sread = cmd1.ExecuteReader();

            //Sread.Read();
               
            //           string ldate = Convert.ToString(Sread.GetValue(0));
            //           dateTimePicker1.Text = ldate;

            //Sread.Close();
                                            

            this.Text = "Journal";
            lblbtype.Text = "Journal";
            lbltotcr.Text = "0";
            lbltotdr.Text = "0";
            txtnarr.Text = "Narration Here";
            dateTimePicker1.Value = DateTime.Today;
            issuedt.Value = DateTime.Today;
            chqno.Text = "";
            vtype1 = "J";
            getmaxjid("J");
            dataGridView1.Rows.Clear();
            dateTimePicker1.Focus();
                        

        }

        private void BtnR_Click(object sender, EventArgs e)
        {
            this.Text = "Receipt";
            lblbtype.Text = "Receipt";
            lbltotcr.Text = "0";
            lbltotdr.Text = "0";
            txtnarr.Text = "Narration Here";
            vtype1 = "R";
            getmaxjid("R");
            dataGridView1.Rows.Clear();
            dateTimePicker1.Value = DateTime.Today;
            issuedt.Value = DateTime.Today;
            chqno.Text = "";
            dateTimePicker1.Focus();
        }

        private void btnP_Click(object sender, EventArgs e)
        {
            this.Text = "Payment";
            lblbtype.Text = "Payment";
            lbltotcr.Text = "0";
            lbltotdr.Text = "0";
            txtnarr.Text = "Narration Here";
            vtype1 = "P";
            getmaxjid("Y");
            dataGridView1.Rows.Clear();
            dateTimePicker1.Value = DateTime.Today;
            issuedt.Value = DateTime.Today;
            chqno.Text = "";
            dateTimePicker1.Focus();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            this.Text = "Contra";
            lblbtype.Text = "Contra";
            lbltotcr.Text = "0";
            lbltotdr.Text = "0";
            txtnarr.Text = "Narration Here";
            vtype1 = "T";
            getmaxjid("T");
            dataGridView1.Rows.Clear();
            dateTimePicker1.Value = DateTime.Today;
            issuedt.Value = DateTime.Today;
            chqno.Text = "";
            dateTimePicker1.Focus();
        }

        public void txtvno_TextChanged(object sender, EventArgs e)
        {
            string str = lblbtype.Text;
            string vtype = "";
            if (str == "Journal")
                vtype = "J";
            else if (str == "Payment")
                vtype = "Y";
            else if (str == "Receipt")
                vtype = "R";
            else if (str == "Contra")
                vtype = "T";



            if (txtvno.Text != "")
            {
                try
                {
                    string qry = "select * from tblvoucher where vno = " + txtvno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and finyr='"+ Global.finyr +"'";
                   // string qry = "select * from tblvoucher where vno = " + txtvno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and IsDeleted='NO'";
                    SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
                    DataSet ds = new DataSet();

                        if (ds.Tables["voucher"] != null)
                        ds.Tables["voucher"].Clear();

                    da.Fill(ds, "voucher");
                    dataGridView1.Rows.Clear();

                    
                        if (ds.Tables["voucher"].Rows.Count > 0)    

                        {

                        btnSave.Text = "Update";

                        vchdt = Convert.ToDateTime(ds.Tables["voucher"].Rows[0]["vdt"]);

                        if (vchdt < Convert.ToDateTime(Global.gFromDt) || vchdt > Convert.ToDateTime(Global.gToDt))

                        {

                            label7.Visible = true;
                            label7.Text = "Cannot view voucher, as it not from current finacial period OR has been modified";
                            txtvno.Select();
                            dateTimePicker1.Enabled = false;
                            btnSave.Enabled = false;
                            txtnarr.Text = "";
                            lbltotdr.Text = "0.00";
                            lbltotcr.Text = "0.00";

                            txtnarr.Enabled = false;
                               

                        }

                        else

                        {
                            label7.Visible = false;
                            dateTimePicker1.Enabled = true;
                            btnSave.Enabled = true;
                            txtnarr.Enabled = true;
                            string chkdate = "";
                            dateTimePicker1.Value = Convert.ToDateTime(Convert.ToDateTime(ds.Tables["voucher"].Rows[0]["vdt"]).ToString("dd/MM/yyyy"));
                            chqno.Text = ds.Tables["voucher"].Rows[0]["Instrument_No"].ToString();
                            chkdate = ds.Tables["voucher"].Rows[0]["Instrument_Date"].ToString();
                            if (chkdate != "")
                            {
                                issuedt.Value = Convert.ToDateTime(Convert.ToDateTime(ds.Tables["voucher"].Rows[0]["Instrument_Date"]).ToString());
                            }
                            
                                
                            

                            qry = "select * from tblledger where vno = " + txtvno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and finyr='"+ Global.finyr +"'";
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
                                string name = Convert.ToString(com.ExecuteScalar());
                                string toby = "";
                                
                                if (ds.Tables["ledger"].Rows[i]["AMTTYPE"].ToString() == "D")
                                {
                                    toby = "By";


                                    dataGridView1.Rows.Add();

                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["toby"].Value = toby;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["glid"].Value = glid;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["slid"].Value = slid;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["ledgername"].Value = name;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Debit"].Value = ds.Tables["ledger"].Rows[i]["AMT"].ToString();
                                    txtnarr.Text = ds.Tables["ledger"].Rows[i]["NAR"].ToString();
                                }
                                else

                                {
                                    toby = "To";
                                    dataGridView1.Rows.Add();
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["toby"].Value = toby;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["glid"].Value = glid;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["slid"].Value = slid;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["ledgername"].Value = name;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Credit"].Value = ds.Tables["ledger"].Rows[i]["AMT"].ToString();
                                    txtnarr.Text = ds.Tables["ledger"].Rows[i]["NAR"].ToString();
                                }
                                try
                                {
                                   // dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["ChqNo"].Value = ds.Tables["ledger"].Rows[i]["chqno"].ToString();
                                }
                                catch (Exception)
                                { }

                            }

                            panel1.Visible = false;
                            txtvno.Focus();
                        }
                    }

                        else

                    {

                        btnSave.Text = "Save";
                    }
                  //  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            finalcal();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = lblbtype.Text;
            string vtype = "";
            if (str == "Journal")
                vtype = "J";
            else if (str == "Payment")
                vtype = "Y";
            else if (str == "Receipt")
                vtype = "R";
            else if (str == "Contra")
                vtype = "T";
            //Check if the voucher already exists or not

           

            //if (InvRem != "Normal Voucher")
            //{
            //    MessageBox.Show("Cannot edit this voucher");

            if (btnSave.Text == "Update")
            {

                string qry1 = "select max(vdt) from tblvoucher where brcode='"+ Global.branch +"' and finyr='"+ Global.finyr +"'";

                SqlDataReader sread = null;
                SqlCommand cmd1 = new SqlCommand(qry1, clsConnection.Conn);

                sread = cmd1.ExecuteReader();

                sread.Read();

                            MaxDt = Convert.ToDateTime(sread.GetValue(0));
                          

                sread.Close();

                //if (DateTime.Today >= MaxDt)
                //{


                    string qry = "select count(*) from tblvoucher where vno = " + txtvno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and inventoryVNo='Normal Voucher' and finyr='"+ Global.finyr +"'";
                    SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
                    int cnt = Convert.ToInt32(com.ExecuteScalar());

                    //qry = "select isnull(MAX(vdt),0) from TblVOUCHER and brcode='" + Global.branch + "'";()
                    //com = new SqlCommand(qry, clsConnection.Conn);
                    //DateTime maxdt = Convert.ToDateTime(com.ExecuteReader());

                    //if(maxdt==DateTime.Today)

                    //Update
                    if (cnt > 0 && (MaxDt == dateTimePicker1.Value) || cnt > 0 && Global.userrole == "Admin")
                    {

                        com.CommandText ="Update tblvoucher set vdt='"+ dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' where vno = " + txtvno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and inventoryVNo='Normal Voucher' and finyr='" + Global.finyr + "'";
                        com.ExecuteNonQuery();

                        string chk = calculatedrcr();
                        if (chk == "0")
                        {
                            return;
                        }

                        qry = "delete from tblledger where vno = " + txtvno.Text + " and trantype = '" + vtype + "' and brcode = '" + Global.branch + "' and finyr='"+ Global.finyr +"'";
                   // com.BeginExecuteNonQuery();
                        com = new SqlCommand(qry, clsConnection.Conn);
                        com.ExecuteNonQuery();


                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                            {
                                qry = "insert into tblledger (finyr,Brcode,Trantype,vno,nar,glid,slid,amt,amttype) values " +
                                    "(@finyr,@Brcode,@Trantype,@vno,@nar,@glid,@slid,@amt,@amttype)";
                                com = new SqlCommand(qry, clsConnection.Conn);
                                com.Parameters.Clear();
                                 com.Parameters.AddWithValue("@finyr", Global.finyr);
                                com.Parameters.AddWithValue("@Brcode", Global.branch);
                                com.Parameters.AddWithValue("@Trantype", vtype);
                                com.Parameters.AddWithValue("@vno", txtvno.Text);
                                com.Parameters.AddWithValue("@nar", txtnarr.Text);
                                com.Parameters.AddWithValue("@glid", dataGridView1.Rows[i].Cells["glid"].Value);  
                                com.Parameters.AddWithValue("@slid", dataGridView1.Rows[i].Cells["slid"].Value);
                                com.Parameters.AddWithValue("@amt", dataGridView1.Rows[i].Cells["Debit"].Value);
                                string type = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                if (type == "By")
                                    type = "D";
                                else
                                    type = "C";

                                com.Parameters.AddWithValue("@amttype", type);
                                

                            com.ExecuteNonQuery();

                                //Insert Audit Tran

                                qry = "insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values " +
                                      "(@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
                                com = new SqlCommand(qry, clsConnection.Conn);
                                com.Parameters.Clear();
                                com.Parameters.AddWithValue("@HostName", Global.FetchHost());
                                com.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
                                com.Parameters.AddWithValue("@menu", "Update Voucher Entry");
                                com.Parameters.AddWithValue("@OperationType", "Update");
                                com.Parameters.AddWithValue("@username", Global.username);
                                com.Parameters.AddWithValue("@Remarks", "New Voucher Entry of VNo. " + txtvno.Text.ToString() + " and VType = " + vtype);

                                com.ExecuteNonQuery();
                            }

                            // Modified on 25th Apr 2017

                            else if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                            {
                                qry = "insert into tblledger (finyr,Brcode,Trantype,vno,nar,glid,slid,amt,amttype) values " +
                                    "(@finyr,@Brcode,@Trantype,@vno,@nar,@glid,@slid,@amt,@amttype)";
                                com = new SqlCommand(qry, clsConnection.Conn);
                                com.Parameters.Clear();
                                com.Parameters.AddWithValue("@finyr", Global.finyr);
                                com.Parameters.AddWithValue("@Brcode", Global.branch);
                                com.Parameters.AddWithValue("@Trantype", vtype);
                                com.Parameters.AddWithValue("@vno", txtvno.Text);
                                com.Parameters.AddWithValue("@nar", txtnarr.Text);
                                com.Parameters.AddWithValue("@glid", dataGridView1.Rows[i].Cells["glid"].Value);
                                com.Parameters.AddWithValue("@slid", dataGridView1.Rows[i].Cells["slid"].Value);
                                com.Parameters.AddWithValue("@amt", dataGridView1.Rows[i].Cells["Credit"].Value);
                                string type = dataGridView1.Rows[i].Cells[0].Value.ToString();

                                type = "C";

                                com.Parameters.AddWithValue("@amttype", type);
                                

                            com.ExecuteNonQuery();

                                //Insert Audit Tran

                                qry = "insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values " +
                                      "(@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
                                com = new SqlCommand(qry, clsConnection.Conn);
                                com.Parameters.Clear();
                                com.Parameters.AddWithValue("@HostName", Global.FetchHost());
                                com.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
                                com.Parameters.AddWithValue("@menu", "New Voucher Entry");
                                com.Parameters.AddWithValue("@OperationType", "Insert");
                                com.Parameters.AddWithValue("@username", Global.username);
                                com.Parameters.AddWithValue("@Remarks", "New Voucher Entry of VNo. " + txtvno.ToString() + " and VType = " + vtype);

                                com.ExecuteNonQuery();

                            


                            }
                        }

                 //   com.EndExecuteNonQuery(com.BeginExecuteNonQuery());
                }

                    else if (cnt > 0 && (dateTimePicker1.Value != DateTime.Today))
                    {

                        cntr = 1;

                        qry = "insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values " +
                                      "(@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
                        com = new SqlCommand(qry, clsConnection.Conn);
                        com.Parameters.Clear();
                        com.Parameters.AddWithValue("@HostName", Global.FetchHost());
                        com.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
                        com.Parameters.AddWithValue("@menu", "Attemt for Updte of Voucher");
                        com.Parameters.AddWithValue("@OperationType", "Update");
                        com.Parameters.AddWithValue("@username", Global.username);
                        com.Parameters.AddWithValue("@Remarks", "Voucher not updated as it is not in current date and user is not having Admin priviledge");

                        com.ExecuteNonQuery();

                        MessageBox.Show("Cannot edit voucher other than voucher creation date");

                    }

                //}

                //else
                //{

                //    MessageBox.Show("You current system date has been changed. Cannot edit bill");
                //}
            }

        

            //Save
            else if(btnSave.Text == "Save")
            {
                  String  qry = "select isnull(MAX(vno),0) from TblVOUCHER where TRANTYPE = '" + vtype + "' and brcode='" + Global.branch + "' and finyr='"+ Global.finyr +"'";
                  SqlCommand  com = new SqlCommand(qry, clsConnection.Conn);
                    string vno = Convert.ToString(com.ExecuteScalar());
                    int vno1 = Convert.ToInt32(vno) + 1;


                    string chk = calculatedrcr();
                    if (chk == "0")
                    {
                        return;
                    }
                    string chqno = "";
                    string isdate = "";
                    chqno = this.chqno.Text;
                    if (chqno != "")
                    {
                        isdate = issuedt.Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        isdate = "";
                    }
                    qry = "insert into tblvoucher (finyr,BrCode,VDT,VNO,trantype,iscontra,inventoryvno,instrument_no,instrument_date) values " +
                        "(@Finyr,@BrCode,@VDT,@VNO,@trantype,@iscontra,@inventoryvno,@chqno,@issuedate)";
                    com = new SqlCommand(qry, clsConnection.Conn);
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("@finyr", Global.finyr);
                    com.Parameters.AddWithValue("@BrCode", Global.branch);
                    string dt = dateTimePicker1.Value.ToString("dd/MM/yyyy"); //+ " " + System.DateTime.Now.ToString("HH:mm:ss");
                    com.Parameters.AddWithValue("@VDT", dt);
                    com.Parameters.AddWithValue("@VNO", vno1);
                    com.Parameters.AddWithValue("@trantype", vtype);
                    if (vtype=="T")
                        {
                            com.Parameters.AddWithValue("@iscontra", 1);
                        }
                    else
                        {
                            com.Parameters.AddWithValue("@iscontra", 0);
                        }
                    com.Parameters.AddWithValue("@inventoryvno", "Normal Voucher");
                    com.Parameters.AddWithValue("@chqno", chqno);
                    com.Parameters.AddWithValue("@issuedate", isdate);
                //  com.Parameters.AddWithValue("@IsDltd", "NO");
                //  com.BeginExecuteNonQuery();
                    com.ExecuteNonQuery();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                        {
                            qry = "insert into tblledger (finyr,Brcode,Trantype,vno,nar,glid,slid,amt,amttype) values " +
                                "(@Finyr,@Brcode,@Trantype,@vno,@nar,@glid,@slid,@amt,@amttype)";
                            com = new SqlCommand(qry, clsConnection.Conn);
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@Finyr", Global.finyr);
                            com.Parameters.AddWithValue("@Brcode", Global.branch);
                            com.Parameters.AddWithValue("@Trantype", vtype);
                            com.Parameters.AddWithValue("@vno", vno1);
                            com.Parameters.AddWithValue("@nar", txtnarr.Text);
                            com.Parameters.AddWithValue("@glid", dataGridView1.Rows[i].Cells["glid"].Value);
                            com.Parameters.AddWithValue("@slid", dataGridView1.Rows[i].Cells["slid"].Value);
                            com.Parameters.AddWithValue("@amt", dataGridView1.Rows[i].Cells["Debit"].Value);
                            string type = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            if (type == "By")
                            { 
                                type = "D";
                            }
                            else
                            {
                                type = "C";
                            }
                            com.Parameters.AddWithValue("@amttype", type);
                            com.ExecuteNonQuery();

                            //Insert Audit Tran

                            qry = "insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values " +
                                  "(@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
                            com = new SqlCommand(qry, clsConnection.Conn);
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@HostName", Global.FetchHost());
                            com.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
                            com.Parameters.AddWithValue("@menu", "New Voucher Entry");
                            com.Parameters.AddWithValue("@OperationType", "Insert");
                            com.Parameters.AddWithValue("@username", Global.username);
                            com.Parameters.AddWithValue("@Remarks", "New Voucher Entry of VNo. " + vno1.ToString() + " and VType = " + vtype);

                            com.ExecuteNonQuery();

                        }

                    // Code modified on 24-04-2017
                    
                        if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                        {
                            qry = "insert into tblledger (finyr,Brcode,Trantype,vno,nar,glid,slid,amt,amttype) values " +
                                "(@Finyr,@Brcode,@Trantype,@vno,@nar,@glid,@slid,@amt,@amttype)";
                            com = new SqlCommand(qry, clsConnection.Conn);
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@Finyr", Global.finyr);
                            com.Parameters.AddWithValue("@Brcode", Global.branch);
                            com.Parameters.AddWithValue("@Trantype", vtype);
                            com.Parameters.AddWithValue("@vno", vno1);
                            com.Parameters.AddWithValue("@nar", txtnarr.Text);
                            com.Parameters.AddWithValue("@glid", dataGridView1.Rows[i].Cells["glid"].Value);
                            com.Parameters.AddWithValue("@slid", dataGridView1.Rows[i].Cells["slid"].Value);
                            com.Parameters.AddWithValue("@amt", dataGridView1.Rows[i].Cells["Credit"].Value);
                            string type = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            type = "C";
                            com.Parameters.AddWithValue("@amttype", type);
                            com.ExecuteNonQuery();
                            
                            //Insert Audit Tran
                            qry = "insert into TblAuditTrn (HostName,ipaddress,menu,OperationType,username,Remarks) values " +
                                  "(@HostName,@ipaddress,@menu,@OperationType,@username,@Remarks)";
                            com = new SqlCommand(qry, clsConnection.Conn);
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@HostName", Global.FetchHost());
                            com.Parameters.AddWithValue("@ipaddress", Global.FetchIP());
                            com.Parameters.AddWithValue("@menu", "New Voucher Entry");
                            com.Parameters.AddWithValue("@OperationType", "Insert");
                            com.Parameters.AddWithValue("@username", Global.username);
                            com.Parameters.AddWithValue("@Remarks", "New Voucher Entry of VNo. " + vno1.ToString() + " and VType = " + vtype);

                            com.ExecuteNonQuery();


                       

                      //  com.EndExecuteNonQuery(com.BeginExecuteNonQuery());

                    }
                    
                }

                    txtvno.Text = vno1.ToString();

                    //btnclear_Click(null, null);
                    //txtvno.Select();
                }


                if (cntr == 1)

                {
                    MessageBox.Show("Process Aborted");
                    dateTimePicker1.Value = DateTime.Today;
                    btnclear_Click(null, null);
                    txtvno.Select();
                }
                else
                {

                    MessageBox.Show("Process Complete");
                    dateTimePicker1.Value = DateTime.Today;
                    btnclear_Click(null, null);
                    //checkBox1.Checked = false;
                    txtvno.Select();
                }

            
        }

    
        private string calculatedrcr()
        {
            string check = "0";
            dramt = 0;
            cramt = 0;

            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["Debit"].Value != null && dataGridView1.Rows[i].Cells["ledgername"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "By")
                            {
                                dramt += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                            }
                            //else
                            //{
                            //    cramt += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                            //}
                        }

                        if (dataGridView1.Rows[i].Cells["Credit"].Value != null && dataGridView1.Rows[i].Cells["ledgername"].Value != null)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "To")
                            {

                                cramt += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);

                            }

                        }
                    }
                    if (Math.Round(dramt,2) != Math.Round(cramt,2))
                    {
                        MessageBox.Show("Debit and Credit Amount are not same.");
                        check = "0";
                    }
                    else
                    {
                        if (dramt != 0 && cramt != 0)
                            check = "1";
                        else
                            check = "0";
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
            }

            return check;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtnarr_Enter(object sender, EventArgs e)
        {
            if (txtnarr.Text == "Narration Here")
                txtnarr.Text = "";
        }

        private void txtnarr_Leave(object sender, EventArgs e)
        {
            if (txtnarr.Text == "")
                txtnarr.Text = "Narration Here";
        }

        private void txtnarr_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                chqno.Focus();
                
            }
        }
        
        

        private void btnclear_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string str = lblbtype.Text;
            string vtype = "";
            if (str == "Journal")
                vtype = "J";
            else if (str == "Payment")
                vtype = "Y";
            else if (str == "Receipt")
                vtype = "R";
            else if (str == "Contra")
                vtype = "T";

            lbltotdr.Text = "0";
            lbltotcr.Text = "0";

            getmaxjid(vtype);

            txtnarr.Text = "Narration Here";
        }

        private void a(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string vtype = "";
            if (lblbtype.Text == "Journal")
                vtype = "J";
            else if (lblbtype.Text == "Payment")
                vtype = "Y";
            else if (lblbtype.Text == "Receipt")
                vtype = "R";
            else if (lblbtype.Text == "Contra")
                vtype = "T";

            string qry = "", qry1 = "";

            if (dataGridView1.Rows[e.RowIndex].Cells["toby"].Value.ToString() == "To")
            {
                if (vtype == "J")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Cash not present

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <>1"; //No Cash and cash Eqivalent
                }
                else if (vtype == "R")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Cash not present

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <>1"; //No Cash and cash Eqivalent

                }
                else if (vtype == "Y")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash 

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID in(1,6) or slid=256 or slid=255"; //Only Cash and cash Eqivalent
               
                }
                else if (vtype == "T")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash 

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash and cash Eqivalent

                }
            }
            else if (dataGridView1.Rows[e.RowIndex].Cells["toby"].Value.ToString() == "By")
            {
                if (vtype == "J")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Cash not present

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <>1"; //No Cash and cash Eqivalent
                }
                else if (vtype == "R")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash present

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1 or slid=255 or slid=139 or slid=241 or slid=256 or slid=136"; //Only Cash and cash Eqivalent

                }
                else if (vtype == "Y")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 1"; //Only Cash not present

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID <> 00001 or slid=256 or slid=255"; //No Cash and cash Eqivalent

                }
                else if (vtype == "T")
                {
                    qry = "select GL_L_Name,GLID+'-00' from TblGLmast where ANYSL = 0 and finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 1"; //Only Cash 

                    qry1 = "select SL_L_NAME,GLID + '-' + SLID  from TblSLmast where finyr = '" + Global.finyr + "' and Brcode = '" + Global.branch + "' and GLID = 00001"; //Only Bank present

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

            listBox1.DataSource = null;
            listBox1.DataSource = dt;
            listBox1.DisplayMember = "ledgname";
            listBox1.ValueMember = "glsl";

            

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmVoucherPrint vchp = new FrmVoucherPrint();
            vchp.Show();
        }

        private void finalcal()
        {

            double totby = 0.0, totto = 0.0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells["Debit"].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "By")
                    {
                        totby += Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                        lbltotdr.Text = Convert.ToString(Math.Round(totby, 2));
                    }

                }

                if (dataGridView1.Rows[i].Cells["Credit"].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "To")
                    {
                        totto += Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                        lbltotcr.Text = Convert.ToString(Math.Round(totto,2));
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string GLID1;
            string SLID1;
            string GLID2;
            string SLID2;
            string GLID3;
            string SLID3;
            string GLID4;
            string SLID4;

            if (dataGridView1.RowCount > 0 && checkBox1.Checked == false)
            {
                

                string str = lblbtype.Text;
                string vtype = "";
                if (str == "Journal")
                    vtype = "J";
                else if (str == "Payment")
                    vtype = "Y";
                else if (str == "Receipt")
                    vtype = "R";
                else if (str == "Contra")
                    vtype = "T";
                

                int checkvno = Convert.ToInt32(txtvno.Text);

                string Vnoqry = "select isnull(MAX(vno),0) from TblVOUCHER where TRANTYPE = '" + vtype + "' and brcode='" + Global.branch + "'";
                SqlCommand VnoCom = new SqlCommand(Vnoqry, clsConnection.Conn);
                string vno1 = Convert.ToString(VnoCom.ExecuteScalar());
                int vno2 = Convert.ToInt32(vno1) + 1;
                txtvno.Text = Convert.ToString(vno2);

                dataGridView1.Rows.Clear();

                GLID1 = "";
                SLID1 = "";
                lbltotcr.Text = "0.00";
                lbltotdr.Text = "0.00";


            }
            else
            {

                dataGridView1.Rows.Add();
                dataGridView1.Focus();
              //  dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.Rows[0].Cells[0].Value = "By";
                dataGridView1.Rows[0].Cells[3].Value = "RCM Input CGST";
                dataGridView1.BeginEdit(true);
                String qry = "select GLID,SLID from Tblslmast where brcode='" + Global.branch + "'and sl_l_name='RCM Input CGST'";
                SqlDataReader sread = null;
                SqlCommand cmd1 = new SqlCommand(qry, clsConnection.Conn);

                sread = cmd1.ExecuteReader();

                sread.Read();

                GLID1 = Convert.ToString(sread.GetValue(0));
                SLID1 = Convert.ToString(sread.GetValue(1));

                sread.Close();

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["glid"].Value = GLID1;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["slid"].Value = SLID1;

               // cmd1.Parameters.AddWithValue("@glid", dataGridView1.Rows[0].Cells["glid"].Value);
               // cmd1.Parameters.AddWithValue("@slid", dataGridView1.Rows[0].Cells["slid"].Value);
          
                dataGridView1.Rows.Add();
                dataGridView1.Focus();
             //   dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.Rows[1].Cells[0].Value = "By";
                dataGridView1.Rows[1].Cells[3].Value = "RCM Input SGST";
                dataGridView1.BeginEdit(true);
                String qry1 = "select GLID,SLID from Tblslmast where brcode='" + Global.branch + "'and sl_l_name='RCM Input SGST'";
                SqlDataReader sread1 = null;
                SqlCommand cmd2 = new SqlCommand(qry1, clsConnection.Conn);

                sread1 = cmd2.ExecuteReader();

                sread1.Read();

                GLID2 = Convert.ToString(sread1.GetValue(0));
                SLID2 = Convert.ToString(sread1.GetValue(1));

                sread1.Close();

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["glid"].Value = GLID2;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["slid"].Value = SLID2;

                //cmd2.Parameters.AddWithValue("@glid", dataGridView1.Rows[1].Cells["glid"].Value);
                //cmd2.Parameters.AddWithValue("@slid", dataGridView1.Rows[1].Cells["slid"].Value);
                
                dataGridView1.Rows.Add();
                dataGridView1.Focus();
               // dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.Rows[2].Cells[0].Value = "To";
                dataGridView1.Rows[2].Cells[3].Value = "CGST Payable";

                dataGridView1.BeginEdit(true);
                String qry2 = "select GLID,SLID from Tblslmast where brcode='" + Global.branch + "'and sl_l_name='CGST Payable'";
                SqlDataReader sread2 = null;
                SqlCommand cmd3 = new SqlCommand(qry2, clsConnection.Conn);

                sread2 = cmd3.ExecuteReader();

                sread2.Read();

                GLID3 = Convert.ToString(sread2.GetValue(0));
                SLID3= Convert.ToString(sread2.GetValue(1));

                sread2.Close();

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["glid"].Value = GLID3;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["slid"].Value = SLID3;

                //cmd3.Parameters.AddWithValue("@glid", dataGridView1.Rows[2].Cells["glid"].Value);
                //cmd3.Parameters.AddWithValue("@slid", dataGridView1.Rows[2].Cells["slid"].Value);

                dataGridView1.Rows.Add();
                dataGridView1.Focus();
              //  dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridView1.Rows[3].Cells[0].Value = "To";
                dataGridView1.Rows[3].Cells[3].Value = "SGST Payable";

                dataGridView1.BeginEdit(true);

                String qry3 = "select GLID,SLID from Tblslmast where brcode='" + Global.branch + "'and sl_l_name='SGST Payable'";

                SqlDataReader sread3 = null;
                SqlCommand cmd4 = new SqlCommand(qry3, clsConnection.Conn);

                sread3 = cmd4.ExecuteReader();

                sread3.Read();

                GLID4 = Convert.ToString(sread3.GetValue(0));
                SLID4 = Convert.ToString(sread3.GetValue(1));

                sread3.Close();

                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["glid"].Value = GLID4;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["slid"].Value = SLID4;
                
                //cmd3.Parameters.AddWithValue("@glid", dataGridView1.Rows[3].Cells["glid"].Value);
                //cmd3.Parameters.AddWithValue("@slid", dataGridView1.Rows[3].Cells["slid"].Value);
        
                panel1.Visible = false;
                txtvno.Focus();

            }
        }

        private void chqno_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                issuedt.Focus();

            }
        }

        private void issuedt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                btnSave.Focus();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

