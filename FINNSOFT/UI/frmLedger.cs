using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using System.Windows.Forms.DataVisualization.Charting;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions;
using CrystalDecisions.Shared;



namespace FINNSOFT
{
    public partial class frmLedger : MetroFramework.Forms.MetroForm
    {
        public static string mSlid = "";
        public static string mGlid = "00001";
        public static string mLedger = "";
        List<string> dates = new List<string>();
        List<double> val = new List<double>();


        DataTable dtchart = new DataTable();
        DataTable dtchart1 = new DataTable();


        public frmLedger()
        {
            InitializeComponent();
        }


        //private void setchart()
        //{
        //    mscDailySales.DataSource = dtchart;
        //    mscDailySales.Series[0].XValueMember = "mon";
        //    mscDailySales.Series[0].YValueMembers = "value";

        //    mscDailySales.Series[1].XValueMember = "mon";
        //    mscDailySales.Series[1].YValueMembers = "value";

        //    mscDailySales.DataBind();
        //    double dblMean = 0.0;
        //    try
        //    {
        //        dblMean = mscDailySales.DataManipulator.Statistics.Mean("Series1");
        //    }
        //    catch (Exception)
        //    {
        //        dblMean = 1;
        //    }
        //    dates.Clear();
        //    val.Clear();

        //    for (int i = 0; i < dtchart.Rows.Count; i++)
        //    {
        //        dates.Add(dtchart.Rows[i][0].ToString());
        //        try
        //        {
        //            val.Add(Convert.ToDouble(dtchart.Rows[i][1]));
        //        }
        //        catch (Exception)
        //        {
        //            val.Add(0);
        //        }
        //    }


        //    mscDailySales.Series[0].Points.DataBindXY(dates.ToArray(), val.ToArray());

        //    dates.Clear();
        //    val.Clear();

        //    for (int i = 0; i < dtchart1.Rows.Count; i++)
        //    {
        //        dates.Add(dtchart1.Rows[i][0].ToString());
        //        try
        //        {
        //            val.Add(Convert.ToDouble(dtchart1.Rows[i][1]));
        //        }
        //        catch (Exception)
        //        {
        //            val.Add(0);
        //        }
        //    }


        //    mscDailySales.Series[1].Points.DataBindXY(dates.ToArray(), val.ToArray());


        //}

        private void frmLedger_Load(object sender, EventArgs e)
        {
            dtchart.Columns.Add("mon");
            dtchart.Columns.Add("value");

            dtchart1.Columns.Add("mon");
            dtchart1.Columns.Add("value");
            
            if (Global.ledgmon == 0 && Global.ledgyear == 0)
            {
                
                FILL_GRID_HEAD();
                fill_grid_detail();
                //setchart();
            }
            else
            {
                fill_grid_head_daily();
                fill_grid_detail();
                //setchart();
            }
        }

        private void fill_grid_head_daily()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "Opening Balance";
            int days = DateTime.DaysInMonth(Global.ledgyear, Global.ledgmon);

            dates.Clear();

            for (int i = 0; i < days; i++)
            {
                dataGridView1.Rows.Add();
                string day = (i + 1).ToString();
                string mon = Global.ledgmon.ToString();

                if (day.Length == 1)
                    day = "0" + day;
                if (mon.Length == 1)
                    mon = "0" + mon;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = day + "/" + mon + "/" + Global.ledgyear.ToString();
                dates.Add(day + "/" + mon + "/" + Global.ledgyear.ToString());
            }
        }

        private void FILL_GRID_HEAD()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "Opening Balance";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "April";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "May";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "June";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "July";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "August";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "September";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "October";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "November";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "December";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "January";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "February";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "March";
            
            //Add list dates

            dates.Clear();

            dates.Add("April");
            dates.Add("May");
            dates.Add("June");
            dates.Add("July");
            dates.Add("August");
            dates.Add("September");
            dates.Add("October");
            dates.Add("November");
            dates.Add("December");
            dates.Add("January");
            dates.Add("February");
            dates.Add("March");

        }
        private void fill_grid_detail()
        {
            double OpBal = 0;
            string QRY = "";


            if (mSlid == "")
            {
                QRY = "select GL_L_Name from TblGLmast where GLID = " + mGlid;
                SqlCommand comgl = new SqlCommand(QRY, clsConnection.Conn);
                mLedger = Convert.ToString(comgl.ExecuteScalar());

                if (Global.ledgyear == 0 && Global.ledgmon == 0)
                {
                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, Month([VDT]) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.glid = '" + mGlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "')  and tblvoucher.vdt<'" + Global.gFromDt.ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, Month(TblVOUCHER.VDT) ";
                    QRY = QRY + "ORDER BY Month(TblVOUCHER.VDT)";
                }
                else
                {
                    string stdt = "", enddt = "";

                    stdt = "01/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();
                    int day = DateTime.DaysInMonth(Global.ledgyear, Global.ledgmon);
                    enddt = day.ToString() + "/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();

                    DateTime stdt1 = Convert.ToDateTime(stdt);
                    DateTime enddt1 = Convert.ToDateTime(enddt);

                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, CONVERT(varchar,TblVOUCHER.VDT,103) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.glid = '" + mGlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "')  and tblvoucher.vdt<'" + stdt1.ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, CONVERT(varchar,TblVOUCHER.VDT,103) ";
                    QRY = QRY + "ORDER BY CONVERT(varchar,TblVOUCHER.VDT,103)";

                }

                SqlDataAdapter da = new SqlDataAdapter(QRY, clsConnection.Conn);
                DataSet ds = new DataSet();
                if (ds.Tables["Opening"] != null)
                    ds.Tables["Opening"].Clear();
                da.Fill(ds, "Opening");
                DataTable dt = ds.Tables["Opening"];
                foreach (DataRow row in dt.Rows)
                {
                    if (row["AMTTYPE"].ToString() == "D")
                    {
                        OpBal = OpBal + Convert.ToDouble(row["SumAmt"]);
                    }
                    else
                    {
                        OpBal = OpBal - Convert.ToDouble(row["SumAmt"]);
                    }

                }

                if (Global.ledgyear == 0 && Global.ledgmon == 0)
                {
                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, Month([VDT]) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.glid = '" + mGlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "')  and tblvoucher.vdt>='" + Global.gFromDt.ToString("dd/MM/yyyy") + "' and tblvoucher.vdt<'" + Global.gToDt.AddDays(1).ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, Month(TblVOUCHER.VDT) ";
                    QRY = QRY + "ORDER BY Month(TblVOUCHER.VDT)";
                }
                else
                {
                    string stdt = "", enddt = "";

                    stdt = "01/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();
                    int day = DateTime.DaysInMonth(Global.ledgyear, Global.ledgmon);
                    enddt = day.ToString() + "/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();

                    DateTime stdt1 = Convert.ToDateTime(stdt);
                    DateTime enddt1 = Convert.ToDateTime(enddt);

                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, CONVERT(varchar,TblVOUCHER.VDT,103) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.glid = '" + mGlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "')  and tblvoucher.vdt>='" + stdt1.ToString("dd/MM/yyyy") + "' and tblvoucher.vdt<'" + enddt1.AddDays(1).ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, CONVERT(varchar,TblVOUCHER.VDT,103) ";
                    QRY = QRY + "ORDER BY CONVERT(varchar,TblVOUCHER.VDT,103)";

                }
                string QRY2 = "";
                QRY2 = "Select * from TblGlMast where Glid='" + mGlid + "' and BrCode='" + Global.branch + "' and finyr='" + Global.finyr + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(QRY2, clsConnection.Conn);
                DataSet ds2 = new DataSet();
                if (ds2.Tables["Opening"] != null)
                    ds2.Tables["Opening"].Clear();
                da2.Fill(ds2, "Opening");
                DataTable dt2 = ds2.Tables["Opening"];
                foreach (DataRow row2 in dt2.Rows)
                {
                    OpBal = OpBal - Convert.ToDouble(row2["op_bal"]);

                }
                if (OpBal > 0)
                {
                    dataGridView1.Rows[1].Cells["Debit"].Value = Math.Round(OpBal, 2);
                    //val.Add(Math.Round(OpBal, 2));
                }
                else if (OpBal < 0)
                {
                    dataGridView1.Rows[1].Cells["Credit"].Value = Math.Round(Math.Abs(OpBal), 2);
                    //val.Add(Math.Round(Math.Abs(OpBal), 2));
                }
                else
                {
                    //val.Add(0);
                }

            }
            else
            {

                QRY = "select SL_L_NAME from TblSLmast where GLID = " + mGlid + " and SLID = " + mSlid;
                SqlCommand comgl = new SqlCommand(QRY, clsConnection.Conn);
                mLedger = Convert.ToString(comgl.ExecuteScalar());

                if (Global.ledgmon == 0 && Global.ledgyear == 0)
                {
                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, Month([VDT]) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.Slid = '" + mSlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "') and tblvoucher.vdt<'" + Global.gFromDt.ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, Month(TblVOUCHER.VDT) ";
                    QRY = QRY + "ORDER BY Month(TblVOUCHER.VDT)";
                }
                else
                {
                    string stdt = "", enddt = "";

                    stdt = "01/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();
                    int day = DateTime.DaysInMonth(Global.ledgyear, Global.ledgmon);
                    enddt = day.ToString() + "/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();

                    DateTime stdt1 = Convert.ToDateTime(stdt);
                    DateTime enddt1 = Convert.ToDateTime(enddt);

                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, CONVERT(varchar,TblVOUCHER.VDT,103) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.Slid = '" + mSlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "') and tblvoucher.vdt<'" + stdt1.ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, CONVERT(varchar,TblVOUCHER.VDT,103) ";
                    QRY = QRY + "ORDER BY CONVERT(varchar,TblVOUCHER.VDT,103)";

                }
                
                SqlDataAdapter da = new SqlDataAdapter(QRY, clsConnection.Conn);
                DataSet ds = new DataSet();
                if (ds.Tables["Opening"] != null)
                    ds.Tables["Opening"].Clear();
                da.Fill(ds, "Opening");
                DataTable dt = ds.Tables["Opening"];
                foreach (DataRow row in dt.Rows)
                {
                    if (row["AMTTYPE"].ToString() == "D")
                    {
                        OpBal = OpBal + Convert.ToDouble(row["SumAmt"]);
                    }
                    else
                    {
                        OpBal = OpBal - Convert.ToDouble(row["SumAmt"]);
                    }

                }

                if (Global.ledgyear == 0 && Global.ledgmon == 0)
                {
                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, Month([VDT]) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.Slid = '" + mSlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "') and tblvoucher.vdt>='" + Global.gFromDt.ToString("dd/MM/yyyy") + "'  and tblvoucher.vdt<'" + Global.gToDt.AddDays(1).ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, Month(TblVOUCHER.VDT) ";
                    QRY = QRY + "ORDER BY Month(TblVOUCHER.VDT)";
                }
                else
                {
                    string stdt = "", enddt = "";

                    stdt = "01/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();
                    int day = DateTime.DaysInMonth(Global.ledgyear, Global.ledgmon);
                    enddt = day.ToString() + "/" + Global.ledgmon.ToString() + "/" + Global.ledgyear.ToString();

                    DateTime stdt1 = Convert.ToDateTime(stdt);
                    DateTime enddt1 = Convert.ToDateTime(enddt);


                    QRY = "SELECT TblLEDGER.AMTTYPE, Sum(TblLEDGER.AMT) AS SumAMT, CONVERT(varchar,TblVOUCHER.VDT,103) AS mon ";
                    QRY = QRY + "FROM TblVOUCHER INNER JOIN TblLEDGER ON (TblVOUCHER.TRANTYPE = TblLEDGER.TRANTYPE) ";
                    QRY = QRY + "AND (TblVOUCHER.VNO = TblLEDGER.VNO) AND (TblVOUCHER.Finyr = TblLEDGER.Finyr) AND (TblVOUCHER.BrCode = TblLEDGER.BrCode) Where (TblLEDGER.Slid = '" + mSlid + "') ";
                    QRY = QRY + "AND (TblVOUCHER.BrCode = '" + Global.branch + "') and tblvoucher.vdt>='" + stdt1.ToString("dd/MM/yyyy") + "'  and tblvoucher.vdt<='" + enddt1.AddDays(1).ToString("dd/MM/yyyy") + "' ";
                    QRY = QRY + "GROUP BY TblLEDGER.AMTTYPE, CONVERT(varchar,TblVOUCHER.VDT,103) ";
                    QRY = QRY + "ORDER BY CONVERT(varchar,TblVOUCHER.VDT,103)";

                }


                string QRY2 = "";
                QRY2 = "Select * from TblSlMast where Slid='" + mSlid + "' and BrCode='" + Global.branch + "' and finyr='" + Global.finyr + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(QRY2, clsConnection.Conn);
                DataSet ds2 = new DataSet();
                if (ds2.Tables["Opening"] != null)
                    ds2.Tables["Opening"].Clear();
                da2.Fill(ds2, "Opening");
                DataTable dt2 = ds2.Tables["Opening"];
                foreach (DataRow row2 in dt2.Rows)
                {
                    OpBal = OpBal - Convert.ToDouble(row2["op_bal"]);

                }
                if (OpBal > 0)
                {
                    dataGridView1.Rows[1].Cells["Debit"].Value = Math.Round(OpBal, 2);
                    //val.Add(Math.Round(OpBal, 2));
                }
                else if (OpBal < 0)
                {
                    dataGridView1.Rows[1].Cells["Credit"].Value = Math.Round(Math.Abs(OpBal), 2);
                    //val.Add(Math.Round(Math.Abs(OpBal), 2));
                }
                else
                {
                    //dataGridView1.Rows[1].Cells["Credit"].Value = "0";
                    //val.Add(0);
                }
            }
            SqlDataAdapter da4 = new SqlDataAdapter(QRY, clsConnection.Conn);
            DataSet ds4 = new DataSet();
            if (ds4.Tables["Tran"] != null)
                ds4.Tables["Tran"].Clear();
            da4.Fill(ds4, "Tran");
            DataTable dt4 = ds4.Tables["Tran"];

            if (Global.ledgmon == 0 && Global.ledgyear == 0)
            {

                foreach (DataRow row4 in dt4.Rows)
                {
                    DataRow dr = dtchart.NewRow();
                    DataRow dr1 = dtchart1.NewRow();
                    if (row4["mon"].ToString() == "4")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[1].Cells["Debit"].Value = row4["SumAMT"];
                            
                        }
                        else
                        {
                            dataGridView1.Rows[1].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }

                        dr[0] = "April";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[1].Cells["Debit"].Value));

                        dr1[0] = "April";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[1].Cells["Credit"].Value));
                        
                            
                    }
                    else if (row4["mon"].ToString() == "5")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[2].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[2].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }

                        dr[0] = "May";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[2].Cells["Debit"].Value));

                        dr1[0] = "May";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[2].Cells["Credit"].Value));
                        

                    }
                    else if (row4["mon"].ToString() == "6")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[3].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[3].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "June";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[3].Cells["Debit"].Value));

                        dr1[0] = "June";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[3].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "7")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[4].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[4].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "July";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[4].Cells["Debit"].Value));

                        dr1[0] = "July";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[4].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "8")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[5].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[5].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }

                        dr[0] = "August";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[5].Cells["Debit"].Value));

                        dr1[0] = "August";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[5].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "9")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[6].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[6].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "September";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[6].Cells["Debit"].Value));

                        dr1[0] = "September";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[6].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "10")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[7].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[7].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "October";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[7].Cells["Debit"].Value));

                        dr1[0] = "October";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[7].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "11")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[8].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[8].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "November";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[8].Cells["Debit"].Value));

                        dr1[0] = "November";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[8].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "12")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[9].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[9].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "December";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[9].Cells["Debit"].Value));

                        dr1[0] = "December";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[9].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "1")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[10].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[10].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "January";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[10].Cells["Debit"].Value));

                        dr1[0] = "January";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[10].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "2")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[11].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[11].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "February";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[11].Cells["Debit"].Value));

                        dr1[0] = "February";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[11].Cells["Credit"].Value));
                        
                    }
                    else if (row4["mon"].ToString() == "3")
                    {
                        if (row4["AmtType"].ToString() == "D")
                        {
                            dataGridView1.Rows[12].Cells["Debit"].Value = row4["SumAMT"];
                        }
                        else
                        {
                            dataGridView1.Rows[12].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                        }
                        dr[0] = "March";
                        dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[12].Cells["Debit"].Value));

                        dr1[0] = "March";
                        dr1[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[12].Cells["Credit"].Value));
                        
                    }

                    int chk = 0;
                   
                    for (int i = 0; i < dtchart.Rows.Count; i++)
                    {
                        if (dtchart.Rows[i][0].ToString() == dr[0].ToString())
                        {
                            dtchart.Rows.RemoveAt(i);
                            dtchart.Rows.InsertAt(dr, i);
                            chk = 1;
                        }
                        
                    }

                    if (chk == 0)
                    {
                        dtchart.Rows.Add(dr);
                    }

                    chk = 0;
                    for (int i = 0; i < dtchart1.Rows.Count; i++)
                    {
                        if (dtchart1.Rows[i][0].ToString() == dr1[0].ToString())
                        {
                            dtchart1.Rows.RemoveAt(i);
                            dtchart1.Rows.InsertAt(dr1, i);
                            chk = 1;
                        }

                    }

                    if (chk == 0)
                    {
                        dtchart1.Rows.Add(dr1);
                    }

                }
            }
            else
            {
                foreach (DataRow row4 in dt4.Rows)
                {
                    DataRow dr = dtchart.NewRow();
                    DataRow dr1 = dtchart1.NewRow();
                    for (int i = 1; i < dataGridView1.Rows.Count; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["Month"].Value.ToString() == row4["mon"].ToString())
                        {
                            if (row4["AmtType"].ToString() == "D")
                            {
                                dataGridView1.Rows[i].Cells["Debit"].Value = row4["SumAMT"];
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells["Credit"].Value = Math.Round(Math.Abs(Convert.ToDouble(row4["SumAMT"])), 2);
                            }

                            dr[0] = row4["mon"].ToString();
                            dr[1] = Math.Abs(Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value));

                            dr1[0] = row4["mon"].ToString();
                            dr1[1] = Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);

                            int chk = 0;

                            for (int i1 = 0; i1 < dtchart.Rows.Count; i1++)
                            {
                                if (dtchart.Rows[i1][0].ToString() == dr[0].ToString())
                                {
                                    dtchart.Rows.RemoveAt(i1);
                                    dtchart.Rows.InsertAt(dr, i1);
                                    chk = 1;
                                }

                            }

                            if (chk == 0)
                            {
                                dtchart.Rows.Add(dr);
                            }

                            chk = 0;

                            for (int i1 = 0; i1 < dtchart1.Rows.Count; i1++)
                            {
                                if (dtchart1.Rows[i1][0].ToString() == dr1[0].ToString())
                                {
                                    dtchart1.Rows.RemoveAt(i1);
                                    dtchart1.Rows.InsertAt(dr1, i1);
                                    chk = 1;
                                }

                            }

                            if (chk == 0)
                            {
                                dtchart1.Rows.Add(dr1);
                            }
                        }

                        
                    }
                }
            }

            calculatetoatal();
        }

        private void calculatetoatal()
        {
            double totdebit = 0.0;
            double totcredit = 0.0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                double debit = 0.0;
                double credit = 0.0;

                try
                {
                    debit = Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
                }
                catch (Exception)
                { }

                try
                {
                    credit = Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);
                }
                catch (Exception)
                { }

                totcredit = totcredit + credit;
                totdebit = totdebit + debit;

                
            }

            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Month"].Value = "Total";
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Debit"].Value = totdebit;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Credit"].Value = totcredit;
        }

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    int objCount = 0;

        //    for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
        //    {
        //        double debitamt = Convert.ToDouble(dataGridView1.Rows[j].Cells["Debit"].Value);
        //        double Creditamt = Convert.ToDouble(dataGridView1.Rows[j].Cells["Credit"].Value);
        //        if (debitamt > 0 || Creditamt > 0)
        //        {
        //            objCount++;
        //        }
        //    }

        //    //if (Global.ledgmon == 0 && Global.ledgyear == 0)
        //    //{
        //    //    objCount = 12;
        //    //}
        //    //else
        //    //{
        //    //    int day = DateTime.DaysInMonth(Global.ledgyear, Global.ledgmon);
        //    //    objCount = day;
        //    //}

        //    double maxamt = 0;

        //    for (int i = 1; i < dataGridView1.Rows.Count-1; i++)
        //    {
        //        double debitamt = Convert.ToDouble(dataGridView1.Rows[i].Cells["Debit"].Value);
        //        double Creditamt = Convert.ToDouble(dataGridView1.Rows[i].Cells["Credit"].Value);

        //        double amt = debitamt - Creditamt;
        //        amt = Math.Abs(amt);

        //        if (amt > maxamt)
        //            maxamt = amt;
        //    }


        //    for (int n = 0; n < objCount; n++)
        //    {
        //        double debitamt = Convert.ToDouble(dataGridView1.Rows[n+1].Cells["Debit"].Value);
        //        double Creditamt = Convert.ToDouble(dataGridView1.Rows[n+1].Cells["Credit"].Value);
        //        if (debitamt > 0 || Creditamt > 0)
        //        {

        //            double amt = debitamt - Creditamt;
        //            amt = Math.Abs(amt);
        //            double maxwidth = panel1.Width / (n + 1);
        //            double width = 0;

        //            if (amt == maxamt)
        //                width = maxwidth;
        //            else
        //            {
        //                double per = (amt * 100) / maxamt;

        //                width = (per * maxwidth) / 100;
        //            }



        //            g.FillRectangle(Brushes.AliceBlue, 0, n * (panel1.Height / objCount),
        //                            (float)width, panel1.Height / objCount);
        //            g.DrawRectangle(new Pen(Color.Black), 0, n * (panel1.Height / objCount),
        //                            (float)width, panel1.Height / objCount);
        //            g.DrawString(amt.ToString(), new Font("Arial", 8f), Brushes.Black,
        //                         2, 2 + n * (panel1.Height / objCount));
        //        }
        //    }
        //}


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            DataTable dt = new DataTable();
            dt.TableName = "ledgprint";
            if (dt.Columns.Count > 0)
                dt.Columns.Clear();
            //System.String
            //System.Double
                
            dt.Columns.Add("LedgName",Type.GetType("System.String"));
            dt.Columns.Add("Debit", Type.GetType("System.Double"));
            dt.Columns.Add("Credit", Type.GetType("System.Double"));
            dt.Columns.Add("Closing", Type.GetType("System.Double"));

            
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                DataRow dr = dt.NewRow();
                try
                {
                    dr[0] = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                }
                catch (Exception)
                {
                    dr[0] = "";
                }
                try
                {
                    dr[1] = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                }
                catch (Exception)
                {
                    dr[1] = "0";
                }
                try
                {
                    dr[2] = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                }
                catch (Exception)
                {
                    dr[2] = "0";
                }
                try
                {
                    dr[3] = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                }
                catch (Exception)
                {
                    dr[3] = "0";
                }

                dt.Rows.Add(dr);
            }

            ReportDocument rp = new ReportDocument();
            string rPath = Application.StartupPath.ToString() + "\\rptLedgPrint.rpt";
            rp.Load(rPath);
            rp.SetDataSource(dt);

            frmReportViewer rpt = new frmReportViewer();

            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            string companyname = Global.company;
            
            crParameterDiscreteValue.Value = companyname;
            crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["Company"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;
                        
            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            crParameterDiscreteValue.Value = mLedger;
            crParameterFieldDefinitions = rp.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["Ledg"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);

            rpt.crystalReportViewer1.ReportSource = rp;
            rpt.Text = "Legder Report";
            rpt.ShowDialog();
            rpt.WindowState = FormWindowState.Maximized;
            this.Cursor = Cursors.Default;



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (e.RowIndex != 0 || e.RowIndex != dataGridView1.Rows.Count - 1)
            {
                string date = dataGridView1.Rows[e.RowIndex].Cells["Month"].Value.ToString();
                DateTime dt = DateTime.Today;

                string slgl = "";
                if (mSlid == "")
                {
                    slgl = mGlid.ToString() + "-00";
                }
                else
                {
                    slgl = mGlid.ToString() + "-" + mSlid.ToString();
                }


                try
                {
                    dt = Convert.ToDateTime(date);
                    frm_Details_Ledger f = new frm_Details_Ledger();
                    f.frmDt.Value = dt;
                    f.toDt.Value = dt;
                    f.fillledgers();
                    f.cmbLedger.SelectedValue = slgl;
                    //f.comboBox1.Text = "Total Voucher";
                    f.comboBox1.Text = "Only This Ledger";
                    f.button1_Click(null, null);
                    f.Show();

                }
                catch (Exception)
                {
                    string stdt = "", enddt = "";
                    string mon = "";
                    int currentyear = Global.gFromDt.Year;

                    if (date == "January")
                    {
                        mon = "01";
                        currentyear++;
                    }
                    else if (date == "February")
                    {
                        mon = "02";
                        currentyear++;
                    }
                    else if (date == "March")
                    {
                        mon = "03";
                        currentyear++;
                    }
                    else if (date == "April")
                        mon = "04";
                    else if (date == "May")
                        mon = "05";
                    else if (date == "June")
                        mon = "06";
                    else if (date == "July")
                        mon = "07";
                    else if (date == "August")
                        mon = "08";
                    else if (date == "September")
                        mon = "09";
                    else if (date == "October")
                        mon = "10";
                    else if (date == "November")
                        mon = "11";
                    else if (date == "December")
                        mon = "12";

                    stdt = "01/" + mon + "/" + currentyear.ToString();

                    int day = DateTime.DaysInMonth(currentyear, Convert.ToInt32(mon));
                    enddt = day.ToString() + "/" + mon + "/" + currentyear.ToString();

                    frm_Details_Ledger f = new frm_Details_Ledger();
                    f.Show();
                    f.frmDt.Value = Convert.ToDateTime(stdt);
                    f.toDt.Value = Convert.ToDateTime(enddt);
                    f.fillledgers();
                    f.cmbLedger.SelectedValue = slgl;
                    //f.comboBox1.Text = "Total Voucher";
                    f.comboBox1.Text = "Only This Ledger";
                    f.button1_Click(null, null);

                }


            }

            this.Cursor = Cursors.Default;
        }
    }
}
