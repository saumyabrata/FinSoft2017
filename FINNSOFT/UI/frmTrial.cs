using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.InteropServices;
using Excel= Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using SQL = System.Data;
using FINNSOFT.UI;

namespace FINNSOFT
{
    public partial class frmTrial : MetroFramework.Forms.MetroForm
    {
        public frmTrial()
        {
            InitializeComponent();
        }

        private void frmTrial_Load(object sender, EventArgs e)
        {
            this.Width = 567;
            this.Height = 396;
            this.StartPosition = FormStartPosition.CenterScreen;
            fdt.Value = Global.gFromDt;
            tdt.Value = Global.gToDt;
            radioButton1.Checked = true;
            metroProgressSpinner1.Visible = false;
            //checkBox1.Checked = true;
            fillGL();
            // filltable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd_del = new SqlCommand();
            cmd_del.CommandText = "DELETE FROM TblrptLEDGER WHERE spid=@@spid";
            cmd_del.Connection = clsConnection.ConnItem;
            Int32 rownum2 = cmd_del.ExecuteNonQuery();
            this.Close();
        }

        private void filltable()
        {
            Double PreDrbal = 0;
            Double PreCrBal = 0;
            Double PreOpBal = 0;

            DateTime dt1 = fdt.Value;

            DateTime addt = dt1.AddDays(1);

            if (radioButton1.Checked == true) //for All Accounts
            {
                string qry = "";
                if (metroToggle1.Checked==true)
                {
                    qry = "select * from TblGlmast where finyr='" + Global.finyr + "' and anysl=0 order by brcode,glid ";
                }
                else
                {
                    qry = "select * from TblGlmast where finyr='" + Global.finyr + "' and anysl=0 and brcode='" + Global.branch + "' order by glid ";
                }

                SqlDataAdapter da_GL = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_GL = new DataSet();
                if (ds_GL.Tables["GL"] != null)
                    ds_GL.Tables["GL"].Clear();
                da_GL.Fill(ds_GL, "GL");
                DataTable dt_GL = ds_GL.Tables["GL"];
                if (dt_GL.Rows.Count == 0)
                {
                    MessageBox.Show("No data in TblGLMast");
                    return;
                }

                if (metroToggle1.Checked == true)
                {
                    qry = "select * from TblSlmast where finyr='" + Global.finyr + "' order by brcode,glid,slid ";
                }
                else
                {
                    qry = "select * from TblSlmast where finyr='" + Global.finyr + "' and BrCode='" + Global.branch + "' order by glid,slid ";
                }

                SqlDataAdapter da_SL = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_SL = new DataSet();
                if (ds_SL.Tables["SL"] != null)
                    ds_SL.Tables["SL"].Clear();
                da_SL.Fill(ds_SL, "SL");
                DataTable dt_SL = ds_SL.Tables["SL"];

                //FOR UPDATE BLANK SLID WITH NULL
                qry = "UPDATE TblLEDGER SET TblLEDGER.SLID = '00' WHERE TblLEDGER.SLID=''";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = qry;
                cmd.Connection = clsConnection.ConnItem;
                Int32 rownum = cmd.ExecuteNonQuery();

                //Records Retrieve from TblLEDGER table

                if (metroToggle1.Checked == true)
                {
                    qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                    qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                    qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                    qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "')  ";
                    qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE";
                    qry = qry + " ORDER BY TblLEDGER.BRCode,TblLEDGER.GLID,TblLEDGER.SLID ";
                }
                else
                {
                    qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                    qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                    qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                    qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') and (TblVoucher.BrCode='" + Global.branch + "')  ";
                    qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE";
                    qry = qry + " ORDER BY TblLEDGER.GLID,TblLEDGER.SLID ";
                }



                SqlDataAdapter da_rpt = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_rpt = new DataSet();
                if (ds_rpt.Tables["RPTLED"] != null)
                    ds_rpt.Tables["RPTLED"].Clear();
                da_rpt.Fill(ds_rpt, "RPTLED");
                DataTable dt_rpt = ds_rpt.Tables["RPTLED"];

                //select general TblLEDGER & sub TblLEDGER name from TblGlmast & TblSlmast table

                if (metroToggle1.Checked == true)
                {
                    qry = "select brcode,glid, gl_l_name, slid, sl_l_name From Qryled WHERE finyr='" + Global.finyr + "' order by brcode,glid,slid ";
                }
                else
                {
                    qry = "select brcode,glid, gl_l_name, slid, sl_l_name From Qryled WHERE finyr='" + Global.finyr + "' and brcode='" + Global.branch + "' order by glid,slid ";
                }

                SqlDataAdapter da_list = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_list = new DataSet();
                if (ds_list.Tables["List"] != null)
                    ds_list.Tables["List"].Clear();
                da_list.Fill(ds_list, "List");
                DataTable dt_list = ds_list.Tables["List"];

                //progress spinner
                
                metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Red;
                int row_num = 0;

                //delete rcord from TblrptLEDGER table
                SqlCommand cmd_del = new SqlCommand();
                cmd_del.CommandText = "DELETE FROM TblrptLEDGER WHERE spid=@@spid";
                cmd_del.Connection = clsConnection.ConnItem;
                Int32 rownum2 = cmd_del.ExecuteNonQuery();
                row_num = 1;
                //insert into temprary table named TblrptLEDGER
                foreach (DataRow row in dt_list.Rows)
                {
                    
                    qry = ("insert into TblrptLEDGER (brcode,glid, gl_l_name, slid, sl_l_name,spid) values('" + row["brcode"] + "','" + row["glid"] + "', '" + row["gl_l_name"] + "', '" + row["slid"] + "', '" + row["sl_l_name"] + "',@@spid)");
                    SqlCommand cmd_insert = new SqlCommand();
                    cmd_insert.CommandText = qry;
                    cmd_insert.Connection = clsConnection.ConnItem;
                    Int32 row_insert = cmd_insert.ExecuteNonQuery();
                    
                    metroProgressSpinner1.Maximum = dt_list.Rows.Count;
                    metroProgressSpinner1.Value = row_num;
                    row_num = row_num + 1;
                }

                //to fill up gl_opening balance
                row_num = 1;
                metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Blue;
                foreach (DataRow row in dt_GL.Rows)
                {
                    //qry="select * from TblrptLEDGER where glid ='" + row["glid"] + "'";
                    //SqlDataAdapter da_temp = new SqlDataAdapter(qry, clsConnection.Conn);
                    //DataSet ds_temp = new DataSet();
                    //if (ds_temp.Tables["Temp"] != null)
                    //    ds_temp.Tables["Temp"].Clear();
                    //da_temp.Fill(ds_temp, "Temp");
                    //DataTable dt_temp = ds_temp.Tables["Temp"];
                    PreDrbal = 0;
                    PreCrBal = 0;
                    PreOpBal = 0;

                    if (metroToggle1.Checked == true)
                    {
                        qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                        qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                        qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                        qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') ";
                        qry = qry + "and (TblLEDGER.GLID='" + row["glid"] + "') ";
                        qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.AMTTYPE";
                        qry = qry + " ORDER BY TblLEDGER.GLID";
                    }
                    else
                    {
                        qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                        qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                        qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                        qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') and (TblVoucher.BrCode='" + Global.branch + "') ";
                        qry = qry + "and (TblLEDGER.GLID='" + row["glid"] + "') ";
                        qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.AMTTYPE";
                        qry = qry + " ORDER BY TblLEDGER.GLID";
                    }



                    SqlDataAdapter da_l_open = new SqlDataAdapter(qry, clsConnection.Conn);
                    DataSet ds_l_open = new DataSet();
                    if (ds_l_open.Tables["Opening"] != null)
                        ds_l_open.Tables["Opening"].Clear();
                    da_l_open.Fill(ds_l_open, "Opening");
                    DataTable dt_l_open = ds_l_open.Tables["Opening"];
                    foreach (DataRow row1 in dt_l_open.Rows)
                    {
                        if (Convert.ToString(row1["AMTTYPE"]) == "D")
                        {
                            PreDrbal = Convert.ToDouble(row1["amt"]);
                        }
                        else
                        {
                            PreCrBal = Convert.ToDouble(row1["amt"]);
                        }
                    }
                    //PreOpBal = PreDrbal - PreCrBal;
                    double number;
                    if (row["op_bal"] == DBNull.Value)
                    {
                        number = 0;
                    }
                    else
                    {
                        number = Convert.ToDouble(row["op_bal"]);
                    }
                    if (number + PreOpBal >= 0)
                    {
                        qry = ("Update TblrptLEDGER set op_dr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row["glid"]).Trim() + "' and spid=@@spid");
                        SqlCommand cmd_insert1 = new SqlCommand();
                        cmd_insert1.CommandText = qry;
                        cmd_insert1.Connection = clsConnection.ConnItem;
                        Int32 row_update = cmd_insert1.ExecuteNonQuery();
                    }
                    else
                    {
                        qry = ("Update TblrptLEDGER set op_cr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row["glid"]).Trim() + "' and spid=@@spid");
                        SqlCommand cmd_insert2 = new SqlCommand();
                        cmd_insert2.CommandText = qry;
                        cmd_insert2.Connection = clsConnection.ConnItem;
                        Int32 row_update2 = cmd_insert2.ExecuteNonQuery();
                    }
                    metroProgressSpinner1.Maximum = dt_GL.Rows.Count;
                    metroProgressSpinner1.Value = row_num;
                    row_num = row_num + 1;

                }

                //to fill up sub LEDGER openning balance
                row_num = 1;
                metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Green;
                foreach (DataRow row_sl in dt_SL.Rows)
                {
                    //if (Convert.ToString(row_sl["slid"]) != "")
                    //{
                    //}
                    PreDrbal = 0;
                    PreCrBal = 0;
                    PreOpBal = 0;
                    if (metroToggle1.Checked == true)
                    {
                        qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                        qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                        qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                        qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') ";
                        qry = qry + "and TblLedger.glid ='" + row_sl["glid"] + "' and TblLedger.slid ='" + row_sl["slid"] + "' ";
                        qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE";
                        qry = qry + " ORDER BY TblLEDGER.BRCode,TblLEDGER.GLID,TblLEDGER.SLID ";
                    }
                    else
                    {
                        qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                        qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                        qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                        qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') and (TblVoucher.BrCode='" + Global.branch + "') ";
                        qry = qry + "and TblLedger.glid ='" + row_sl["glid"] + "' and TblLedger.slid ='" + row_sl["slid"] + "' ";
                        qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE";
                        qry = qry + " ORDER BY TblLEDGER.BRCode,TblLEDGER.GLID,TblLEDGER.SLID ";
                    }


                    SqlDataAdapter da_l_open = new SqlDataAdapter(qry, clsConnection.Conn);
                    DataSet ds_l_open = new DataSet();
                    if (ds_l_open.Tables["Opening"] != null)
                        ds_l_open.Tables["Opening"].Clear();
                    da_l_open.Fill(ds_l_open, "Opening");
                    DataTable dt_l_open = ds_l_open.Tables["Opening"];
                    foreach (DataRow row1 in dt_l_open.Rows)
                    {
                        if (Convert.ToString(row1["AMTTYPE"]) == "D")
                        {
                            PreDrbal = Convert.ToDouble(row1["amt"]);
                        }
                        else
                        {
                            PreCrBal = Convert.ToDouble(row1["amt"]);
                        }
                    }
                   // PreOpBal = PreDrbal - PreCrBal;
                    double number = 0;
                    if (row_sl["op_bal"] == DBNull.Value)
                    {
                        number = 0;
                    }
                    else
                    {
                        number = Convert.ToDouble(row_sl["op_bal"]);
                    }
                    if (number + PreOpBal >= 0)
                    {
                        qry = ("Update TblrptLEDGER set op_dr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row_sl["glid"]).Trim() + "' and slid ='" + Convert.ToString(row_sl["slid"]).Trim() + "' and spid=@@spid ");
                        SqlCommand cmd_insert1 = new SqlCommand();
                        cmd_insert1.CommandText = qry;
                        cmd_insert1.Connection = clsConnection.ConnItem;
                        Int32 row_update = cmd_insert1.ExecuteNonQuery();
                    }
                    else
                    {
                        qry = ("Update TblrptLEDGER set op_cr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row_sl["glid"]).Trim() + "' and slid ='" + Convert.ToString(row_sl["slid"]).Trim() + "' and spid=@@spid");
                        SqlCommand cmd_insert2 = new SqlCommand();
                        cmd_insert2.CommandText = qry;
                        cmd_insert2.Connection = clsConnection.ConnItem;
                        Int32 row_update2 = cmd_insert2.ExecuteNonQuery();
                    }
                    metroProgressSpinner1.Maximum = dt_SL.Rows.Count;
                    metroProgressSpinner1.Value = row_num;
                    row_num = row_num + 1;
                }

                //to fill up TblLEDGER Debit amt / Credit
                row_num = 1;
                metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Purple;
                foreach (DataRow row in dt_rpt.Rows)
                {
                    if (Convert.ToString(row["slid"]) != "00")
                    {
                        if (Convert.ToString(row["amttype"]) == "D")
                        {
                            qry = "Update TblrptLEDGER set dr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "'and slid ='" + row["slid"] + "' and spid=@@spid";
                        }
                        else
                        {
                            qry = "Update TblrptLEDGER set cr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "'and slid ='" + row["slid"] + "' and spid=@@spid";
                        }

                    }
                    else
                    {
                        if (Convert.ToString(row["amttype"]) == "D")
                        {
                            qry = "Update TblrptLEDGER set dr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "' and spid=@@spid";
                        }
                        else
                        {
                            qry = "Update TblrptLEDGER set cr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "' and spid=@@spid";
                        }
                    }
                    SqlCommand cmd_upd = new SqlCommand();
                    cmd_upd.CommandText = qry;
                    cmd_upd.Connection = clsConnection.ConnItem;
                    Int32 row_upd = cmd_upd.ExecuteNonQuery();
                    metroProgressSpinner1.Maximum = dt_rpt.Rows.Count;
                    metroProgressSpinner1.Value = row_num;
                    row_num = row_num + 1;
                }
            }
            //#####################################################################################
            else if (radioButton2.Checked == true) //for Group Accounts
            {
                metroToggle1.Checked = false;
                if (listGL.SelectedItems.Count <= 0)
                {
                    MessageBox.Show("Select Min. One Group of Accounts");
                    return;
                }

                string qrylid = "";
                string acheads = "";
                Int32 i = 0;
                foreach (Object selecteditem in listGL.SelectedItems)
                {

                    object[] objCollection = new object[listGL.SelectedItems.Count];

                    listGL.SelectedItems.CopyTo(objCollection, 0);

                    //MessageBox.Show((objCollection[i] as DataRowView)[0] as string);
                    if (qrylid == "")
                    {
                        acheads = acheads + (objCollection[i] as DataRowView)[0] as string + " ";
                        qrylid = "( glid = '" + (objCollection[i] as DataRowView)[0] as string + "'";
                    }
                    else
                    {
                        qrylid = qrylid + " or glid = '" + (objCollection[i] as DataRowView)[0] as string + "'";
                    }

                    i++;
                }

                qrylid = qrylid + " ) ";


                string qry = "";
                qry = "select * from TblGlmast where " + qrylid + " and finyr='" + Global.finyr + "' and anysl=0  and BrCode='" + Global.branch + "' order by glid ";
                SqlDataAdapter da_GL = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_GL = new DataSet();
                if (ds_GL.Tables["GL"] != null)
                    ds_GL.Tables["GL"].Clear();
                da_GL.Fill(ds_GL, "GL");
                DataTable dt_GL = ds_GL.Tables["GL"];

                qry = "select * from TblSlmast where " + qrylid + " and finyr='" + Global.finyr + "' and BrCode='" + Global.branch + "' order by glid,slid ";
                SqlDataAdapter da_SL = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_SL = new DataSet();
                if (ds_SL.Tables["SL"] != null)
                    ds_SL.Tables["SL"].Clear();
                da_SL.Fill(ds_SL, "SL");
                DataTable dt_SL = ds_SL.Tables["SL"];

                //Records Retrieve from TblLEDGER table
                qry = "SELECT TblLEDGER.BRCode,TblLEDGER.BRCODE,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') and (TblVoucher.BrCode='" + Global.branch + "')  ";
                qry = qry + "and " + qrylid;
                qry = qry + " GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE";
                qry = qry + " ORDER BY TblLEDGER.GLID,TblLEDGER.SLID ";
                SqlDataAdapter da_rpt = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_rpt = new DataSet();
                if (ds_rpt.Tables["RPTLED"] != null)
                    ds_rpt.Tables["RPTLED"].Clear();
                da_rpt.Fill(ds_rpt, "RPTLED");
                DataTable dt_rpt = ds_rpt.Tables["RPTLED"];

                //select general TblLEDGER & sub TblLEDGER name from TblGlmast & TblSlmast table
                qry = "select brcode,glid, gl_l_name, slid, sl_l_name From Qryled WHERE finyr='" + Global.finyr + "' and brcode='" + Global.branch + "' order by brcode,glid,slid ";
                SqlDataAdapter da_list = new SqlDataAdapter(qry, clsConnection.Conn);
                DataSet ds_list = new DataSet();
                if (ds_list.Tables["List"] != null)
                    ds_list.Tables["List"].Clear();
                da_list.Fill(ds_list, "List");
                DataTable dt_list = ds_list.Tables["List"];


                //delete rcord from TblrptLEDGER table
                SqlCommand cmd_del = new SqlCommand();
                cmd_del.CommandText = "delete from TblrptLEDGER where spid=@@spid";
                cmd_del.Connection = clsConnection.ConnItem;
                Int32 rownum2 = cmd_del.ExecuteNonQuery();

                //insert into temprary table named TblrptLEDGER
                foreach (DataRow row in dt_list.Rows)
                {
                    qry = ("insert into TblrptLEDGER (brcode,glid, gl_l_name, slid, sl_l_name,spid) values('" + row["brcode"] + "','" + row["glid"] + "', '" + row["gl_l_name"] + "', '" + row["slid"] + "', '" + row["sl_l_name"] + "',@@spid)");
                    SqlCommand cmd_insert = new SqlCommand();
                    cmd_insert.CommandText = qry;
                    cmd_insert.Connection = clsConnection.ConnItem;
                    Int32 row_insert = cmd_insert.ExecuteNonQuery();
                }

                //to fill up gl_opening balance
                foreach (DataRow row in dt_GL.Rows)
                {
                    //qry="select * from TblrptLEDGER where glid ='" + row["glid"] + "'";
                    //SqlDataAdapter da_temp = new SqlDataAdapter(qry, clsConnection.Conn);
                    //DataSet ds_temp = new DataSet();
                    //if (ds_temp.Tables["Temp"] != null)
                    //    ds_temp.Tables["Temp"].Clear();
                    //da_temp.Fill(ds_temp, "Temp");
                    //DataTable dt_temp = ds_temp.Tables["Temp"];
                    PreDrbal = 0;
                    PreCrBal = 0;
                    PreOpBal = 0;

                    qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                    qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                    qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                    qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') and (TblVoucher.BrCode='" + Global.branch + "') ";
                    qry = qry + "and (TblLEDGER.GLID='" + row["glid"] + "') ";
                    qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.AMTTYPE";
                    qry = qry + " ORDER BY TblLEDGER.GLID";
                    SqlDataAdapter da_l_open = new SqlDataAdapter(qry, clsConnection.Conn);
                    DataSet ds_l_open = new DataSet();
                    if (ds_l_open.Tables["Opening"] != null)
                        ds_l_open.Tables["Opening"].Clear();
                    da_l_open.Fill(ds_l_open, "Opening");
                    DataTable dt_l_open = ds_l_open.Tables["Opening"];
                    foreach (DataRow row1 in dt_l_open.Rows)
                    {
                        if (Convert.ToString(row1["AMTTYPE"]) == "D")
                        {
                            PreDrbal = Convert.ToDouble(row1["amt"]);
                        }
                        else
                        {
                            PreCrBal = Convert.ToDouble(row1["amt"]);
                        }
                    }
                    //PreOpBal = PreDrbal - PreCrBal;
                    double number;
                    if (row["op_bal"] == DBNull.Value)
                    {
                        number = 0;
                    }
                    else
                    {
                        number = Convert.ToDouble(row["op_bal"]);
                    }
                    if (number + PreOpBal >= 0)
                    {
                        qry = ("Update TblrptLEDGER set op_dr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row["glid"]).Trim() + "' and spid=@@spid");
                        SqlCommand cmd_insert1 = new SqlCommand();
                        cmd_insert1.CommandText = qry;
                        cmd_insert1.Connection = clsConnection.ConnItem;
                        Int32 row_update = cmd_insert1.ExecuteNonQuery();
                    }
                    else
                    {
                        qry = ("Update TblrptLEDGER set op_cr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row["glid"]).Trim() + "'  and spid=@@spid");
                        SqlCommand cmd_insert2 = new SqlCommand();
                        cmd_insert2.CommandText = qry;
                        cmd_insert2.Connection = clsConnection.ConnItem;
                        Int32 row_update2 = cmd_insert2.ExecuteNonQuery();
                    }
                }

                //to fill up sub LEDGER openning balance
                foreach (DataRow row_sl in dt_SL.Rows)
                {
                    //if (Convert.ToString(row_sl["slid"]) != "")
                    //{
                    //}                   
                    PreDrbal = 0;
                    PreCrBal = 0;
                    PreOpBal = 0;
                    qry = "SELECT TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) as amt ";
                    qry = qry + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.VNO = TblLEDGER.VNO) AND ";
                    qry = qry + "(TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) AND (TblVOUCHER.FINYR = TblLEDGER.FINYR) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode)  ";
                    qry = qry + "where (TblVOUCHER.vdt >='" + fdt.Value.ToString("yyyy-MM-dd") + "' and TblVOUCHER.vdt < '" + addt.ToString("yyyy-MM-dd") + "') and (TblVoucher.BrCode='" + Global.branch + "') ";
                    qry = qry + "and TblLedger.glid ='" + row_sl["glid"] + "' and TblLedger.slid ='" + row_sl["slid"] + "' ";
                    qry = qry + "GROUP BY TblLEDGER.BRCode,TblLEDGER.GLID, TblLEDGER.SLID, TblLEDGER.AMTTYPE";
                    qry = qry + " ORDER BY TblLEDGER.GLID,TblLEDGER.SLID ";

                    SqlDataAdapter da_l_open = new SqlDataAdapter(qry, clsConnection.Conn);
                    DataSet ds_l_open = new DataSet();
                    if (ds_l_open.Tables["Opening"] != null)
                        ds_l_open.Tables["Opening"].Clear();
                    da_l_open.Fill(ds_l_open, "Opening");
                    DataTable dt_l_open = ds_l_open.Tables["Opening"];
                    foreach (DataRow row1 in dt_l_open.Rows)
                    {
                        if (Convert.ToString(row1["AMTTYPE"]) == "D")
                        {
                            PreDrbal = Convert.ToDouble(row1["amt"]);
                        }
                        else
                        {
                            PreCrBal = Convert.ToDouble(row1["amt"]);
                        }
                    }
                   // PreOpBal = PreDrbal - PreCrBal;
                    double number = 0;
                    if (row_sl["op_bal"] == DBNull.Value)
                    {
                        number = 0;
                    }
                    else
                    {
                        number = Convert.ToDouble(row_sl["op_bal"]);
                    }
                    if (number + PreOpBal >= 0)
                    {
                        qry = ("Update TblrptLEDGER set op_dr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row_sl["glid"]).Trim() + "' and slid ='" + Convert.ToString(row_sl["slid"]).Trim() + "' and spid=@@spid ");
                        SqlCommand cmd_insert1 = new SqlCommand();
                        cmd_insert1.CommandText = qry;
                        cmd_insert1.Connection = clsConnection.ConnItem;
                        Int32 row_update = cmd_insert1.ExecuteNonQuery();
                    }
                    else
                    {
                        qry = ("Update TblrptLEDGER set op_cr_bal= " + Math.Abs(number + PreOpBal) + " where glid ='" + Convert.ToString(row_sl["glid"]).Trim() + "' and slid ='" + Convert.ToString(row_sl["slid"]).Trim() + "' and spid=@@spid");
                        SqlCommand cmd_insert2 = new SqlCommand();
                        cmd_insert2.CommandText = qry;
                        cmd_insert2.Connection = clsConnection.ConnItem;
                        Int32 row_update2 = cmd_insert2.ExecuteNonQuery();
                    }
                }

                //to fill up TblLEDGER Debit amt / Credit
                foreach (DataRow row in dt_rpt.Rows)
                {
                    if (Convert.ToString(row["slid"]) != "00")
                    {
                        if (Convert.ToString(row["amttype"]) == "D")
                        {
                            qry = "Update TblrptLEDGER set dr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "'and slid ='" + row["slid"] + "' and spid=@@spid";
                        }
                        else
                        {
                            qry = "Update TblrptLEDGER set cr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "'and slid ='" + row["slid"] + "' and spid=@@spid";
                        }

                    }
                    else
                    {
                        if (Convert.ToString(row["amttype"]) == "D")
                        {
                            qry = "Update TblrptLEDGER set dr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "' and spid=@@spid";
                        }
                        else
                        {
                            qry = "Update TblrptLEDGER set cr_bal= " + row["amt"] + " where glid ='" + row["glid"] + "' and spid=@@spid";
                        }
                    }
                    SqlCommand cmd_upd = new SqlCommand();
                    cmd_upd.CommandText = qry;
                    cmd_upd.Connection = clsConnection.ConnItem;
                    Int32 row_upd = cmd_upd.ExecuteNonQuery();

                }

            }


            // ' to set dr/cr op bal for ledgers having pl_bs_id = 3,4 in tblrptledger

            // 'qry = "update tblrptledger set op_dr_bal=0,op_cr_bal=0 where glid in (select glid from tblglmast where pl_bs_id in(3,4))"
            // 'conn.Execute qry
            string qry2 = "";
            Int32 rowupd = 0;
            SqlCommand cmdupd = new SqlCommand();

            qry2 = "update tblrptledger set op_dr_bal=0 where op_dr_bal is NULL  and spid=@@spid";
            cmdupd.CommandText = qry2;
            cmdupd.Connection = clsConnection.ConnItem;
            rowupd = cmdupd.ExecuteNonQuery();
            cmdupd.Cancel();
            qry2 = "update tblrptledger set op_cr_bal=0 where op_cr_bal is NULL  and spid=@@spid";
            cmdupd.CommandText = qry2;
            cmdupd.Connection = clsConnection.ConnItem;
            rowupd = cmdupd.ExecuteNonQuery();
            cmdupd.Cancel();
            qry2 = "update tblrptledger set dr_bal=0 where dr_bal is NULL  and spid=@@spid";
            cmdupd.CommandText = qry2;
            cmdupd.Connection = clsConnection.ConnItem;
            rowupd = cmdupd.ExecuteNonQuery();
            cmdupd.Cancel();
            qry2 = "update tblrptledger set cr_bal=0 where cr_bal is NULL  and spid=@@spid";
            cmdupd.CommandText = qry2;
            cmdupd.Connection = clsConnection.ConnItem;
            rowupd = cmdupd.ExecuteNonQuery();
            cmdupd.Cancel();

            if (checkBox1.Checked == true)
            {
                qry2 = "DELETE From TblrptLEDGER ";
                qry2 = qry2 + "Where (op_dr_bal =0 or op_dr_bal =NULL)  And (op_cr_bal = 0 or op_cr_bal=NULL) And (DR_BAL = 0 or DR_BAL=NULL) And (CR_BAL = 0 or CR_BAL=NULL)  and spid=@@spid";
                cmdupd.CommandText = qry2;
                cmdupd.Connection = clsConnection.ConnItem;
                rowupd = cmdupd.ExecuteNonQuery();
                cmdupd.Cancel();
            }


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                this.Width = 836;
                this.Height = 396;

            }
            else
            {
                this.Width = 567;
                this.Height = 396;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                this.Width = 836;
                this.Height = 396;

            }
            else
            {
                this.Width = 567;
                this.Height = 396;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            metroProgressSpinner1.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            filltable();
            Cursor.Current = Cursors.Default;
            metroProgressSpinner1.Visible = false;

            Global.trlfrmdt = fdt.Value;
            Global.trltodt = tdt.Value;

            frmPrint F = new frmPrint();
            F.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            metroProgressSpinner1.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            filltable();
            Cursor.Current = Cursors.Default;
            metroProgressSpinner1.Visible = false;
            SqlCommand cmd;
            SqlDataAdapter da;
            DataSet ds;

            string qry = "select brcode,gl_l_name as 'General Ledger Name',sl_l_name as 'Sub Ledger Name','opening_balance'= " +
                 "CASE WHEN op_dr_bal > 0 THEN op_dr_bal " +
                 "WHEN op_cr_bal> 0 THEN op_cr_bal*-1 " +
                 "ELSE 0 " +
                 "END," +
                 "DR_BAL as 'Debit',CR_BAL as 'Credit',(op_dr_bal + DR_BAL - op_cr_bal - CR_BAL) as 'Closing Balance',spid " +
                 "from TblRPTLEDGER ORDER BY brcode,gl_l_name";
            cmd = new SqlCommand(qry, clsConnection.Conn);

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();

            da.Fill(ds);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML File|*.xml";
            saveFileDialog1.Title = "Save an XML File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                
                   ds.WriteXml(@saveFileDialog1.FileName);
            }

            MessageBox.Show("XML file created");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            metroProgressSpinner1.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            filltable();
            int row_num = 1;
            metroProgressSpinner1.Style = MetroFramework.MetroColorStyle.Yellow;

            string qry = "select BrCode,gl_l_name as 'General Ledger Name',sl_l_name as 'Sub Ledger Name','opening_balance'= " +
                 "CASE WHEN op_dr_bal > 0 THEN op_dr_bal " +
                 "WHEN op_cr_bal> 0 THEN op_cr_bal*-1 " +
                 "ELSE 0 " +
                 "END," +
                 "DR_BAL as 'Debit',CR_BAL as 'Credit',(op_dr_bal + DR_BAL - op_cr_bal - CR_BAL) as 'Closing Balance',spid " +
                 "from TblRPTLEDGER ORDER BY brcode,gl_l_name";

            SQL.DataTable dtTrial = new SQL.DataTable();
            using (SqlDataAdapter da = new SqlDataAdapter(qry, clsConnection.Conn))
            {
                da.Fill(dtTrial);
            }

            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            oXL = new Excel.Application();
            oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            oSheet = (Excel._Worksheet)oWB.ActiveSheet;


            try
            {
                SQL.DataTable dtbranch =
                        dtTrial.DefaultView.ToTable(true, "BrCode");

                foreach (SQL.DataRow x in dtbranch.Rows)
                {
                    metroProgressSpinner1.Maximum = dtbranch.Rows.Count;
                    oSheet = (Excel._Worksheet)oXL.Worksheets.Add();
                    oSheet.Name = x[0].ToString().Replace(" ", "").
                        Replace("  ", "").Replace("/", "").
                            Replace("\\", "").Replace("*", "");

                    string[] colNames = new string[dtTrial.Columns.Count];
                    int col = 0;

                    foreach (SQL.DataColumn dc in dtTrial.Columns)
                        colNames[col++] = dc.ColumnName;

                    char lastColumn = (char)(65 + dtTrial.Columns.Count - 1);

                    oSheet.get_Range("A1", lastColumn + "1").Value2 = colNames;
                    oSheet.get_Range("A1", lastColumn + "1").Font.Bold = true;
                    oSheet.get_Range("A1", lastColumn + "1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    oSheet.Columns.AutoFit();

                    SQL.DataRow[] dr = dtTrial.Select(string.Format("BrCode='{0}'", x[0].ToString()));

                    string[,] rowData = new string[dr.Count<SQL.DataRow>(), dtTrial.Columns.Count];

                    int rowCnt = 0;
                    int redRows = 2;
                    foreach (SQL.DataRow row in dr)
                    {
                        for (col = 0; col < dtTrial.Columns.Count; col++)
                        {
                            rowData[rowCnt, col] = row[col].ToString();
                        }

                        if (double.Parse(row["Opening_Balance"].ToString()) == 0)
                        {
                            Range range = oSheet.get_Range("A" + redRows.ToString(), "H" + redRows.ToString());
                            range.Cells.Interior.Color = System.Drawing.Color.LightYellow;
                        }
                        redRows++;
                        rowCnt++;
                    }
                Range range1 = oSheet.get_Range("A" + rowCnt.ToString(), "H" + rowCnt.ToString());
                range1.Borders.Color = System.Drawing.Color.Black.ToArgb();

                oSheet.get_Range("A2", lastColumn + rowCnt.ToString()).Value2 = rowData;
                metroProgressSpinner1.Value = row_num;
                row_num = row_num + 1;

                }

                //Cursor gets default - wait ends
                Cursor.Current = Cursors.Default;
                metroProgressSpinner1.Visible = false;

                //Filesave pops up
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel File|*.xlsx";
                saveFileDialog1.Title = "Save an Excel File";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.  
                if (saveFileDialog1.FileName != "")
                {
                    object misValue = System.Reflection.Missing.Value;
                    oWB.SaveAs(saveFileDialog1.FileName, AccessMode: Excel.XlSaveAsAccessMode.xlShared);
                    oWB.Close(saveFileDialog1.FileName);
                    oXL.Quit();


                    MessageBox.Show("Excel file created");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Occured while creating Excel file " + ex.ToString());
            }
            finally
            {
                Marshal.ReleaseComObject(oWB);
            }
        }

        private void bttngridview_Click(object sender, EventArgs e)
        {
            metroProgressSpinner1.Visible = true;
            Cursor.Current = Cursors.WaitCursor;
            filltable();
            Cursor.Current = Cursors.Default;
            metroProgressSpinner1.Visible = false;
            FrmGridTrial x = new FrmGridTrial(fdt.Value,tdt.Value);
            x.ShowDialog();
        }
    }
}
