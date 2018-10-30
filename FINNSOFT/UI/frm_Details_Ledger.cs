using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;



namespace FINNSOFT
{
    public partial class frm_Details_Ledger : MetroFramework.Forms.MetroForm
    {
        public frm_Details_Ledger()
        {
            InitializeComponent();
        }

        string glid;
        string slid;
        string glid1;
        string slid1;
        double cashOPBal;
        decimal clbal;
        double curbal;
        double Cashdrbal;
        double Cashcrbal;
        double totdr=0.00;
        double totcr=0.00;

        SqlDataAdapter da = new SqlDataAdapter();

       DataTable dt = new DataTable();
       

        private void frm_Details_Ledger_Load(object sender, EventArgs e)
        {
            
            toDt.Value = DateTime.Now;
            frmDt.Value = DateTime.Now;
            fillledgers();
        }

        public void fillledgers()
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

            cmbLedger.DataSource = null;
            cmbLedger.DataSource = dt;
            cmbLedger.DisplayMember = "ledgname";
            cmbLedger.ValueMember = "glsl";



            //listBox1.Items.Add("z");
            //listBox1.Items.Add("a");
            //listBox1.Items.Add("d");
            //listBox1.Items.Add("b");
            //listBox1.Sorted = true;

            //ds.Dispose();
            //da.Dispose();
        }

        public void button1_Click(object sender, EventArgs e)
        {

         

            LblDr.Text = "0.00";
            LblCr.Text = "0.00";

            if (Convert.ToDateTime(frmDt.Value).Date < Convert.ToDateTime(Global.gFromDt)|| Convert.ToDateTime(toDt.Value).Date > Convert.ToDateTime(Global.gToDt))

            {
                label7.Visible = true;
                label7.Text = "Cannot view ledger details, as it is not from current finacial period";
                cmbLedger.Select();
                button3.Enabled = false;
                dataGridView1.Rows.Clear();
              
                
            }

            else
            {

                try
                {
                    string str = cmbLedger.SelectedValue.ToString();
                    button3.Enabled = true;
                    label7.Visible = false;
                    button3.Enabled = true;


                    glid = str.Substring(0, str.IndexOf("-"));
                    slid = str.Substring(str.IndexOf("-") + 1, str.Length - (str.IndexOf("-") + 1));
                    string qry = "";

                    if (str == "")
                    {
                        qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID,a.nar  from TblLEDGER a, TblVOUCHER b " +
                           "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and (CONVERT(varchar, b.vno)+b.TRANTYPE) in (" +
                           "select (CONVERT(varchar,vno)+TRANTYPE) from TblLEDGER where " +
                           " vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                            toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "' and a.brcode='" + Global.branch + "' and a.finyr='" + Global.finyr + "' order by b.vdt,b.vno";
                    }

                    if (slid == "00" || slid == "0" || slid == "")
                    {
                        if (comboBox1.Text == "Only This Ledger")
                        {
                            qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID,a.nar,a.chqno,a.Issuedate  from TblLEDGER a, TblVOUCHER b " +
                                "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and a.glid = " + glid +
                                " and vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                                toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "' and a.brcode='" + Global.branch + "' and a.finyr='" + Global.finyr + "' order by b.vdt,b.vno";
                        }
                        else if (comboBox1.Text == "Total Voucher")
                        {
                            qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID,a.nar   from TblLEDGER a, TblVOUCHER b " +
                               "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and (CONVERT(varchar, b.vno)+b.TRANTYPE) in (" +
                               "select (CONVERT(varchar,vno)+TRANTYPE) from TblLEDGER where glid = " + glid + ")" +
                               " and vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                                toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "' and a.brcode='" + Global.branch + "' and a.finyr='" + Global.finyr + "' order by b.vdt,b.vno";
                        }
                    }
                    else
                    {
                        if (comboBox1.Text == "Only This Ledger")
                        {
                            qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID,a.nar,a.chqno,a.Issuedate   from TblLEDGER a, TblVOUCHER b " +
                                "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and a.glid = " + glid +
                                " and a.slid = " + slid +
                                " and vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                                toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "' and a.brcode='" + Global.branch + "' and a.finyr='" + Global.finyr + "' order by b.vdt,b.vno";
                        }
                        else if (comboBox1.Text == "Total Voucher")
                        {
                            qry = "select b.vno,b.vdt,b.TRANTYPE,b.InventoryVNo,a.AMT,a.AMTTYPE,a.SLID,a.GLID,a.nar   from TblLEDGER a, TblVOUCHER b " +
                               "where a.BrCode = b.BrCode and a.VNO = b.VNO and a.TRANTYPE = b.TRANTYPE and a.finyr=b.finyr and (CONVERT(varchar, b.vno)+b.TRANTYPE) in (" +
                               "select (CONVERT(varchar,vno)+TRANTYPE) from TblLEDGER where " +
                               " vdt >='" + frmDt.Value.ToString("dd/MM/yyyy") + "' and vdt < '" +
                                toDt.Value.AddDays(1).ToString("dd/MM/yyyy") + "' and a.brcode='" + Global.branch + "' and a.finyr='" + Global.finyr + "' order by b.vdt,b.vno";
                        }


                    }


                    this.Cursor = Cursors.WaitCursor;
                    string companyname = Global.company;

                    SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);


                    DataSet ds = new DataSet();

                    if (ds.Tables["voucher1"] != null)
                        ds.Tables["voucher1"].Clear();
                    da.Fill(ds, "voucher1");

                    //   dataGridView1.Rows

                    //  dt delclartion
                    dt.TableName = "voucher";
                    dt.Columns.Clear();
                    dt.Columns.Add("Vno");
                    dt.Columns.Add("vdt");
                    dt.Columns.Add("TRANTYPE");
                    dt.Columns.Add("InventoryVNo");
                    dt.Columns.Add("CAMT");
                    dt.Columns.Add("AMTTYPE");
                    dt.Columns.Add("VName");
                    dt.Columns.Add("DAMT");
                    dt.Columns.Add("nar");
                    dt.Columns.Add("ChqNo");
                    dt.Columns.Add("IssueDate");

                    dt.Rows.Clear();

                    

                    for (int i = 0; i < ds.Tables["voucher1"].Rows.Count; i++)
                    {
                       // string glid1 = ds.Tables["voucher1"].Rows[i]["GLID"].ToString();
                      //  string slid1 = ds.Tables["voucher1"].Rows[i]["SLID"].ToString();

                       string qryLed = "select glid,slid from tblledger where amt = (select max(amt) from tblledger where vno='"+ ds.Tables["voucher1"].Rows[i]["vno"].ToString() + "' and trantype='"+ ds.Tables["voucher1"].Rows[i]["TRANTYPE"].ToString() + "' " +
                                       "and brcode = '"+ Global.branch +"' and AMTTYPE = 'D' and finyr='"+ Global.finyr + "') and vno = '" + ds.Tables["voucher1"].Rows[i]["vno"].ToString() + "' and trantype = '" + ds.Tables["voucher1"].Rows[i]["TRANTYPE"].ToString() + "' and brcode ='"+ Global.branch + "' and AMTTYPE = 'D' and finyr='" + Global.finyr +"'";

                        SqlDataAdapter com1 = new SqlDataAdapter(qryLed, clsConnection.Conn);
                        com1.SelectCommand.CommandTimeout = 100000;
                        DataSet ds1 = new DataSet();
                       

                        if (ds1.Tables["Ledname"] != null)
                            ds1.Tables["Ledname"].Clear();
                        com1.Fill(ds1, "Ledname");

                        glid1 = ds1.Tables["Ledname"].Rows[0]["GLID"].ToString();
                        slid1 = ds1.Tables["Ledname"].Rows[0]["SLID"].ToString();

                        string tablename = "TblSLmast";
                        string fieldname = "SL_L_NAME";
                        string whereclause = " SLID = " + slid1 + " and GLID = " + glid1 + " and brcode='" + Global.branch + "' and finyr='" + Global.finyr + "'";

                        if (slid1 == "00" || slid1 == "" || slid1 == "0")
                        {
                            tablename = "TblGLmast";
                            fieldname = "GL_L_Name";
                            whereclause = " GLID = " + glid1;
                        }

                        qry = "select " + fieldname + " from " + tablename + " where " + whereclause;
                        SqlCommand com = new SqlCommand(qry, clsConnection.Conn);
                        string name = Convert.ToString(com.ExecuteScalar());

                      
                    DataRow dr = dt.NewRow();
                        dr[0] = ds.Tables["voucher1"].Rows[i]["vno"].ToString();
                        dr[1] = ds.Tables["voucher1"].Rows[i]["vdt"].ToString();
                        

                        string trantype = ds.Tables["voucher1"].Rows[i]["TRANTYPE"].ToString();
                        if (trantype == "J")
                        {
                            dr[2] = "Journal";
                            dr[3] = "J";
                        }
                        else if (trantype == "Y")
                        {
                            dr[2] = "Payment";
                            dr[3] = "Y";
                        }
                        else if (trantype == "R")
                        {
                            dr[2] = "Receipt";
                            dr[3] = "R";
                        }
                        else if (trantype == "T")
                        {
                            dr[2] = "Contra";
                            dr[3] = "T";
                        }

                        //dr[2] = ds.Tables["voucher1"].Rows[i]["TRANTYPE"].ToString();

                        string amttype = ds.Tables["voucher1"].Rows[i]["AMTTYPE"].ToString();
                        if (amttype == "D")
                        {
                            dr[6] = Math.Round(Convert.ToDouble(ds.Tables["voucher1"].Rows[i]["AMT"].ToString()), 2);
                            dr[7] = 0;
                        }
                        else
                        {
                            dr[6] = 0;
                            dr[7] = Math.Round(Convert.ToDouble(ds.Tables["voucher1"].Rows[i]["AMT"].ToString()), 2);
                        }

                        dr[5] = name;

                        dr[4] = ds.Tables["voucher1"].Rows[i]["InventoryVNo"].ToString();

                        dr[8] = ds.Tables["voucher1"].Rows[i]["nar"].ToString();

                        dr[9] = ds.Tables["voucher1"].Rows[i]["ChqNo"].ToString();

                        dr[10] = ds.Tables["voucher1"].Rows[i]["IssueDate"].ToString();

                        dt.Rows.Add(dr);
                    }

                    //Display Report

                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0];
                        //dtr = dt.Rows[i][1].ToString();
                        //dttme = DateTime.ParseExact(dtr, "dd/mmm/yyyy 00:00:00", null);
                        //dataGridView1.Rows[i].Cells[1].Value = dttme;
                        dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1];
                        dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2];
                        dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3];
                        dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4];
                        dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5];
                        dataGridView1.Rows[i].Cells[6].Value = dt.Rows[i][6];
                        dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7];
                        dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i][8];
                        dataGridView1.Rows[i].Cells[9].Value = dt.Rows[i][9];
                        dataGridView1.Rows[i].Cells[10].Value = dt.Rows[i][10];

                        totdr = totdr + Convert.ToDouble(dt.Rows[i][6]);
                        totcr = totcr + Convert.ToDouble(dt.Rows[i][7]);

                    }

                    LblDr.Text = Convert.ToString(totdr);
                    LblCr.Text = Convert.ToString(totcr);

                    totdr = 0.00;
                    totcr = 0.00;

                    da.Dispose();


                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                         MessageBox.Show(ex.Message);
                }

            }

            calbalance();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string vno = dataGridView1.Rows[e.RowIndex].Cells["VNO"].Value.ToString();
                string vtype = dataGridView1.Rows[e.RowIndex].Cells["VType"].Value.ToString();

                frmVoucher frm = new frmVoucher();
                frm.Show();
                frm.txtvno.Text = vno;


                string str = vtype;
                if (str == "J")
                    vtype = "Journal";
                else if (str == "Y")
                    vtype = "Payment";
                else if (str == "R")
                    vtype = "Receipt";
                else if (str == "T")
                    vtype = "Contra";

                frm.lblbtype.Text = vtype;

                frm.txtvno_TextChanged(null, null);
                


            }
            catch (Exception)
            { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            DateTime dttme;
            DateTime dtr;


            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            


            int i = 0;
            int j = 0;

            DateTime dt1 = Convert.ToDateTime(frmDt.Text);        
        
            SqlDataReader Sread = null;
            string qry1 = "select op_bal from tblslmast where glid='" + glid + "' and slid='" + slid + "' and brcode='" + Global.branch + "' and finyr='"+ Global.finyr +"'";

            SqlCommand cmd1 = new SqlCommand(qry1, clsConnection.Conn);

            Sread = cmd1.ExecuteReader();
            while (Sread.Read())
            {
                if (!Sread.IsDBNull(0))
                {
                    cashOPBal = Convert.ToDouble(Sread.GetValue(0));

                 //  cashOPBal = Math.Round(curbal, 2, MidpointRounding.AwayFromZero);


                }
            }

           Sread.Close(); 

            string qry = "select sum(b.amt) as CashDrBl from tblvoucher a, tblledger b where a.BrCode=b.brcode";
            qry = qry + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.finyr=b.finyr and a.finyr='"+ Global.finyr +"' and a.vdt>='" + Global.gFromDt + "' and a.vdt < '" + dt1 + "' and b.brcode = '" + Global.branch + "' and(b.glid = '"+ glid +"' and b.slid = '"+ slid +"') and b.AMTTYPE = 'D'";

            SqlCommand cmd = new SqlCommand(qry, clsConnection.Conn);
            try
            {
                Sread = cmd.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Cashdrbal = Convert.ToDouble(Sread.GetValue(0));
                        Cashdrbal = Math.Round(Cashdrbal, 2, MidpointRounding.AwayFromZero);

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            finally
            {
                Sread.Close();
                cmd.Dispose();
            }

            string qry2 = "select sum(b.amt) as CashCrBl from tblvoucher a, tblledger b where a.BrCode = b.brcode";
            qry2 = qry2 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.finyr=b.finyr and a.finyr='" + Global.finyr + "' and  a.vdt>='" + Global.gFromDt +"' and a.vdt < '" + dt1 + "' and b.brcode ='" + Global.branch + "' and(b.glid = '"+ glid +"' and b.slid = '"+ slid +"') and b.AMTTYPE = 'C'";

            SqlCommand cmd2 = new SqlCommand(qry2, clsConnection.Conn);
            try
            {
                Sread = cmd2.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Cashcrbal = Convert.ToDouble(Sread.GetValue(0));
                        Cashcrbal = Math.Round(Cashcrbal, 2, MidpointRounding.AwayFromZero);

                    }
                }

               curbal = cashOPBal + (Cashdrbal - Cashcrbal);

                curbal = Convert.ToDouble(curbal);


                //curbal = curbal;


            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                Sread.Close();
                cmd2.Dispose();
            }

            xlWorkSheet.Range[xlWorkSheet.Cells[i + 1, j + 5], xlWorkSheet.Cells[i + 1, j + 5]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 3, j + 5], xlWorkSheet.Cells[i + 3, j + 5]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 6, j + 1], xlWorkSheet.Cells[i + 6, j + 6]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 5, j + 7], xlWorkSheet.Cells[i + 5, j + 7]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 5, j + 9], xlWorkSheet.Cells[i + 5, j + 9]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 5, j + 10], xlWorkSheet.Cells[i + 5, j + 10]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 5, j + 11], xlWorkSheet.Cells[i + 5, j + 11]].Font.Bold = true;

            xlWorkSheet.Range[xlWorkSheet.Cells[i + 5, j + 7], xlWorkSheet.Cells[i + 5, j + 8]].MergeCells = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 1, j + 5], xlWorkSheet.Cells[i + 1, j + 8]].MergeCells = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 3, j + 5], xlWorkSheet.Cells[i + 3, j + 6]].MergeCells = true;


            xlWorkSheet.Range[xlWorkSheet.Cells[i + 5, j + 7], xlWorkSheet.Cells[i + 5, j + 8]].HorizontalAlignment = true;

            xlWorkSheet.Cells[i + 1, j + 5] =  "Company : " + Global.company;
            xlWorkSheet.Cells[i + 3, j + 5] = "Branch: " + Global.branch;
            xlWorkSheet.Cells[i + 6, j + 1] = "VNO";
            xlWorkSheet.Cells[i + 6, j + 2] = "DATE";
            xlWorkSheet.Cells[i + 6, j + 3] = "VOUCHER TYPE";
            xlWorkSheet.Cells[i + 6, j + 4] = "TRANTYPE";
            xlWorkSheet.Cells[i + 6, j + 5] = "INV NO";
            xlWorkSheet.Cells[i + 6, j + 6] = "LEDGER";
            xlWorkSheet.Cells[i + 5, j + 7] = "Opening Balance";
           

            if (curbal > 0)

            {
                xlWorkSheet.Range[xlWorkSheet.Cells[i + 6, j + 7], xlWorkSheet.Cells[i + 6, j + 7]].Font.Bold = true;
                xlWorkSheet.Cells[i + 6, j + 7] = curbal;

            }
                  else
            {
                xlWorkSheet.Range[xlWorkSheet.Cells[i + 6, j + 8], xlWorkSheet.Cells[i + 6, j + 8]].Font.Bold = true;
                xlWorkSheet.Cells[i + 6, j + 8] = curbal;

            }
            
            xlWorkSheet.Cells[i + 5, j + 9] = "Reccuring Balance";
            xlWorkSheet.Cells[i + 5, j + 10] = "Chq No";
            xlWorkSheet.Cells[i + 5, j + 11] = "Issue Date";
            //xlWorkSheet.Cells[i + 5, j + 12] = "Issue Date";


            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                //  for (j = 0; j <= dataGridView1.ColumnCount - 2; j++)
                for (j = 0; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    DataGridViewCell cell = dataGridView1[j, i];
                    xlWorkSheet.Cells[i + 7, j + 1] = cell.Value;
               //    xlWorkSheet.Cells[i + 7, j + 2] = cell.Value;
                    if (j == 1)
                    {
                        dtr =  Convert.ToDateTime(cell.Value);
                    
                        TreeNode tn = new TreeNode(string.Format("{0:dd-MMM-yyyy}", dtr));

                        String dtformat = tn.ToString();

                        dtformat = dtformat.Replace("TreeNode:", "");

                        xlWorkSheet.Cells[i + 7, j + 1] = dtformat;
                        
                    }

                    if (j == 10)
                    {
                        if (cell.Value.ToString() != "")
                        { 
                            dtr = Convert.ToDateTime(cell.Value);

                        TreeNode tn = new TreeNode(string.Format("{0:dd-MMM-yyyy}", dtr));

                        String dtformat = tn.ToString();

                        dtformat = dtformat.Replace("TreeNode:", "");

                        xlWorkSheet.Cells[i + 7, j + 1] = dtformat;
                    }
                    }

                    //  String test= Convert.ToString(xlWorkSheet.Cells[i + 7, j + 1] = cell.Value);
                    //  MessageBox.Show(test);


                }


                if (curbal >= 0)
                {

                    // curbal = (curbal + Math.Round(Convert.ToDouble(dt.Rows[i][6]),0)) - Math.Round(Convert.ToDouble(dt.Rows[i][7]),0);

                    curbal = (curbal + (Convert.ToDouble(dt.Rows[i][6])) - (Convert.ToDouble(dt.Rows[i][7])));

                    xlWorkSheet.Cells[i + 7, 9] = Convert.ToString(curbal);


                }

                else if (curbal < 0)

                {
                    curbal = (curbal - (Convert.ToDouble(dt.Rows[i][7])) + (Convert.ToDouble(dt.Rows[i][6]))); 

                    xlWorkSheet.Cells[i + 7, 9] = Convert.ToString(curbal);

                }

            }

            xlWorkSheet.Range[xlWorkSheet.Cells[i + 9, 5], xlWorkSheet.Cells[i + 9, 7]].Font.Bold = true;
            xlWorkSheet.Range[xlWorkSheet.Cells[i + 9, 5], xlWorkSheet.Cells[i + 9, 6]].MergeCells = true;

            xlWorkSheet.Cells[i + 9, 5] = "Closing Balance: ";
            xlWorkSheet.Cells[i + 9, 7] = Convert.ToString(curbal);


            curbal = 0;
            cashOPBal = 0;
            Cashdrbal = 0;
            Cashcrbal = 0;

            xlWorkBook.SaveAs("C:\\mpjsql\\registers\\LedgerDetails.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);         
            

            string workbookPath = "C:\\mpjsql\\registers\\LedgerDetails.xls";
            Excel.Workbook excelWorkbook = xlApp.Workbooks.Open(workbookPath,
                    0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "",
                    true, false, 0, true, false, false);

            xlApp.Visible = true;

            //xlApp.Quit();
            releaseObject(xlWorkSheet); 
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            

        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();  
            }
        }

        public void calbalance()
        {

            int i = 0;
            int j = 0;

            DateTime dt1 = Convert.ToDateTime(frmDt.Text);

            SqlDataReader Sread = null;
            string qry1 = "select op_bal from tblslmast where glid='" + glid + "' and slid='" + slid + "' and brcode='" + Global.branch + "' and finyr='" + Global.finyr + "'";

            SqlCommand cmd1 = new SqlCommand(qry1, clsConnection.Conn);

            Sread = cmd1.ExecuteReader();
            while (Sread.Read())
            {
                if (!Sread.IsDBNull(0))
                {
                    cashOPBal = Convert.ToDouble(Sread.GetValue(0));

                    //  cashOPBal = Math.Round(curbal, 2, MidpointRounding.AwayFromZero);


                }
            }

            Sread.Close();

            string qry = "select sum(b.amt) as CashDrBl from tblvoucher a, tblledger b where a.BrCode=b.brcode";
            qry = qry + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.finyr=b.finyr and a.finyr='" + Global.finyr + "' and a.vdt>='" + Global.gFromDt + "' and a.vdt < '" + dt1 + "' and b.brcode = '" + Global.branch + "' and(b.glid = '" + glid + "' and b.slid = '" + slid + "') and b.AMTTYPE = 'D'";

            SqlCommand cmd = new SqlCommand(qry, clsConnection.Conn);
            try
            {
                Sread = cmd.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Cashdrbal = Convert.ToDouble(Sread.GetValue(0));
                        Cashdrbal = Math.Round(Cashdrbal, 2, MidpointRounding.AwayFromZero);

                    }
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

            finally
            {
                Sread.Close();
                cmd.Dispose();
            }

            string qry2 = "select sum(b.amt) as CashCrBl from tblvoucher a, tblledger b where a.BrCode = b.brcode";
            qry2 = qry2 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.finyr=b.finyr and a.finyr='" + Global.finyr + "' and  a.vdt>='" + Global.gFromDt + "' and a.vdt < '" + dt1 + "' and b.brcode ='" + Global.branch + "' and(b.glid = '" + glid + "' and b.slid = '" + slid + "') and b.AMTTYPE = 'C'";

            SqlCommand cmd2 = new SqlCommand(qry2, clsConnection.Conn);
            try
            {
                Sread = cmd2.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Cashcrbal = Convert.ToDouble(Sread.GetValue(0));
                        Cashcrbal = Math.Round(Cashcrbal, 2, MidpointRounding.AwayFromZero);

                    }
                }

                curbal = cashOPBal + (Cashdrbal - Cashcrbal);

                curbal = Convert.ToDouble(curbal);


               label6.Text = "Opening Balance: " + Convert.ToString(curbal);

            }
            

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                Sread.Close();
                cmd2.Dispose();
            }
                           

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
               

                if (curbal >= 0)
                {

                    curbal = (curbal + (Convert.ToDouble(dt.Rows[i][6])) - (Convert.ToDouble(dt.Rows[i][7])));

                    label8.Text = "Closing Balance: " + Convert.ToString(curbal);


                }

                else if (curbal < 0)

                {
                    curbal = (curbal - (Convert.ToDouble(dt.Rows[i][7])) + (Convert.ToDouble(dt.Rows[i][6])));

                    label8.Text = "Closing Balance: " + Convert.ToString(curbal);

                }

            }

    

           
          //  xlWorkSheet.Cells[i + 9, 7] = Convert.ToString(curbal);


            curbal = 0;
            cashOPBal = 0;
            Cashdrbal = 0;
            Cashcrbal = 0;

        }

        private void cmbLedger_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
