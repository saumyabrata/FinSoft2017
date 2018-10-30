using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.IO;
using System.Data.SqlClient;


namespace FINNSOFT.UI
{
    public partial class FrmStmntImport : MetroFramework.Forms.MetroForm
    {
        public FrmStmntImport()
        {
            InitializeComponent();
        }

        private void FrmStmntImport_Load(object sender, EventArgs e)
        {
            loadbranch();
            banklist.Focus();
        }
        private void loadbranch()
        {
            string qry = "select SLID,SL_L_NAME from TblSLmast where glid='00001' and BrCode='" + Global.branch + "' and FInYr='" + Global.finyr + "' and SL_L_NAME<>'Cash' order by SL_L_NAME";
            SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn);
            DataSet ds = new DataSet();
            if (ds.Tables["SLEDGER"] != null)
                ds.Tables["SLEDGER"].Clear();
            da.Fill(ds, "SLEDGER");

            banklist.DataSource = null;
            banklist.DataSource = ds.Tables["SLEDGER"];
            banklist.ValueMember = "SLID";
            banklist.DisplayMember = "SL_L_NAME";            
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dg.Rows.Clear();
                dg.Refresh();
                Cursor.Current = Cursors.WaitCursor;
                label2.Text = banklist.Text;
                label3.Text = banklist.SelectedValue.ToString();
                string sql = "select vno,trantype,amttype from tblledger where glid = '00001' and slid ='" + label3.Text + "' and BrCode='" + Global.branch + "' and FInYr='" + Global.finyr + "'";
                using (var cmd2 = new SqlCommand(sql, clsConnection.Conn))
                {
                    SqlDataReader dr;
                    dr = cmd2.ExecuteReader();

                    while (dr.Read())
                    {
                        int mvno = dr.GetInt32(0);
                        String mtrantype = dr.GetString(1);
                        String mamttype = dr.GetString(2);
                        //now place values in grid

                        string gsql = "SELECT a.vno,a.vdt, a.narration, vchtype = " +
                            "CASE a.trantype " +
                            "WHEN 'J' THEN 'Journal' " +
                            "WHEN 'Y' THEN 'Payment' " +
                            "WHEN 'T' THEN 'Contra' " +
                            "WHEN 'R' THEN 'Receipt' " +
                            "ELSE 'NA' " +
                            "END,  " +
                            "b.Amttype,b.AMT,a.instrument_no,a.instrument_date FROM TblVOUCHER a, TblLEDGER b " +
                            "where a.BrCode='" + Global.branch + "' and a.Finyr='" + Global.finyr + "' and a.trantype = '" + mtrantype + "' " +
                            "and a.vno = " + mvno + " and b.GLID <> '00001' and b.slid <>'" + label3.Text + "' " +
                            "AND (a.vdt >= '" + fdt.Text + "' and a.vdt <= '" + tdt.Text + "') and a.RECONDT is NULL " +
                            "and a.Finyr=b.Finyr and a.BrCode=b.BrCode and a.vno=b.vno and a.trantype=b.TRANTYPE " +
                            "ORDER BY VDT;";

                        SqlDataAdapter da2 = new SqlDataAdapter(gsql, clsConnection.Conn);
                        DataSet ds2 = new DataSet();
                        if (ds2.Tables["Recon"] != null)
                            ds2.Tables["Recon"].Clear();
                        da2.Fill(ds2, "Recon");
                        DataTable dt2 = ds2.Tables["Recon"];
                        foreach (DataRow row2 in dt2.Rows)
                        {
                            dg.Rows.Add();
                            dg.Rows[dg.Rows.Count - 1].Cells["VchNo"].Value = row2["vno"];
                            dg.Rows[dg.Rows.Count - 1].Cells["Date"].Value = row2["vdt"];
                            dg.Rows[dg.Rows.Count - 1].Cells["Particulars"].Value = row2["narration"];
                            dg.Rows[dg.Rows.Count - 1].Cells["vtype"].Value = row2["vchtype"];
                            dg.Rows[dg.Rows.Count - 1].Cells["ChqNo"].Value = row2["instrument_no"];
                            dg.Rows[dg.Rows.Count - 1].Cells["InstrDate"].Value = row2["instrument_date"];

                            if (row2["Amttype"].ToString() == "D")
                            {
                                dg.Rows[dg.Rows.Count - 1].Cells["Debit"].Value = row2["Amt"];
                            }
                            else
                            {
                                dg.Rows[dg.Rows.Count - 1].Cells["Credit"].Value = row2["Amt"];
                            }
                        }
                    }
                    dr.Close();
                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchValue = txtchq.Text;

            dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if (row.Cells["ChqNo"].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void savebtn_Click(object sender, EventArgs e)
        {

            int mvno;
            string mtrantype;
            SqlTransaction transaction;

            try
            {
                for (int i = 0; i < dg.Rows.Count; i++)
            {
                
                if (dg.Rows[i].Cells["BankDate"].Value != null)
                {

                            if (dg.Rows[i].Cells["vtype"].Value.ToString() == "Journal")
                            {
                                mtrantype = "J";
                            }
                            else if(dg.Rows[i].Cells["vtype"].Value.ToString() == "Payment")
                            {
                                mtrantype = "Y";
                            }
                            else if (dg.Rows[i].Cells["vtype"].Value.ToString() == "Contra")
                            {
                                mtrantype = "T";
                            }
                            else if (dg.Rows[i].Cells["vtype"].Value.ToString() == "Receipt")
                            {
                                mtrantype = "R";
                            }
                            else
                            {
                                MessageBox.Show("Wrong Voucher Type");
                                break;
                            }


                        transaction = clsConnection.Conn.BeginTransaction();
                        mvno = Convert.ToInt32(dg.Rows[i].Cells["VchNo"].Value);
                        var updtqry = @"UPDATE TBLVOUCHER SET recondt = @bankdt Where finyr = @finyr and BrCode=@BrCode 
                                        and trantype=@mvtype and vno = @mvno";

                        SqlCommand updtcom = new SqlCommand(updtqry, clsConnection.Conn, transaction);
                        updtcom.Parameters.Clear();
                        updtcom.Parameters.AddWithValue("@finyr", Global.finyr);
                        updtcom.Parameters.AddWithValue("@BrCode", Global.branch);
                        updtcom.Parameters.AddWithValue("@mvtype", mtrantype);
                        updtcom.Parameters.AddWithValue("@mvno", mvno);
                        updtcom.Parameters.AddWithValue("@bankdt", dg.Rows[i].Cells["BankDate"].Value.ToString());
                        updtcom.ExecuteNonQuery();
                        transaction.Commit();
                        updtqry = "";
                    MessageBox.Show("Reconciled date saved successfully");
                }
                }
            dg.Rows.Clear();
            dg.Refresh();
            banklist.Focus();

        }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
}
    }
}
