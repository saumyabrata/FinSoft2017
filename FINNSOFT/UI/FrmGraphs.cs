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
using System.Security.Cryptography;
using System.IO;

namespace FINNSOFT.UI
{
    public partial class FrmGraphs : Form
    {
        public FrmGraphs()
        {
            InitializeComponent();
        }

        private void FrmGraphs_Load(object sender, EventArgs e)
        {
            showchart();
        }

        private void showchart()
        {
            try
            {

                string qry_asset_dr = "SELECT TOP (100) PERCENT dbo.TblPL_BS_MAST.Description, SUM(dbo.TblLEDGER.AMT) AS Amt, dbo.TblLEDGER.AMTTYPE " +
                    "FROM dbo.TblGLmast INNER JOIN " +
                    "dbo.TblPL_BS_MAST ON dbo.TblGLmast.PL_BS_ID = dbo.TblPL_BS_MAST.PL_BS_ID INNER JOIN " +
                    "dbo.TblSLmast ON dbo.TblGLmast.finyr = dbo.TblSLmast.finyr AND dbo.TblGLmast.BrCode = dbo.TblSLmast.BrCode AND " +
                    "dbo.TblGLmast.GLID = dbo.TblSLmast.GLID INNER JOIN dbo.TblLEDGER ON dbo.TblSLmast.GLID = dbo.TblLEDGER.GLID AND " +
                    "dbo.TblSLmast.SLID = dbo.TblLEDGER.SLID AND dbo.TblSLmast.finyr = dbo.TblLEDGER.FinYr AND dbo.TblSLmast.BrCode = dbo.TblLEDGER.BrCode " +
                    "INNER JOIN dbo.TblVOUCHER ON dbo.TblLEDGER.FinYr = dbo.TblVOUCHER.FinYr AND dbo.TblLEDGER.BrCode = dbo.TblVOUCHER.BrCode " +
                    "AND dbo.TblLEDGER.VNO = dbo.TblVOUCHER.VNO AND dbo.TblLEDGER.TRANTYPE = dbo.TblVOUCHER.TRANTYPE " +
                    "WHERE dbo.TblGLmast.finyr = '" + Global.finyr + "' and dbo.TblGLmast.BrCode = '" + Global.branch + "' " +
                    "and(dbo.TblVOUCHER.VDT >= '" + fdt.Value.ToString("dd/MM/yyyy") + "' " +
                    "and dbo.TblVOUCHER.VDT < '" + tdt.Value.AddDays(1).ToString("dd/MM/yyyy") + "') " +
                    "GROUP BY dbo.TblPL_BS_MAST.Description, dbo.TblLEDGER.AMTTYPE " +
                    "HAVING(dbo.TblPL_BS_MAST.Description = N'Assets') OR (dbo.TblPL_BS_MAST.Description = N'Liabilities') " +
                    "ORDER BY dbo.TblPL_BS_MAST.Description";

                SqlCommand cmdqry = new SqlCommand(qry_asset_dr, clsConnection.Conn);
                SqlDataReader myreader;
                myreader = cmdqry.ExecuteReader();
                foreach (var series in chart1.Series)
                {
                    series.Points.Clear();
                }
                while (myreader.Read())
                {
                    this.chart1.Series["Assets"].Points.AddXY(myreader["AMTTYPE"].ToString(), Convert.ToInt32(myreader["Amt"]));
                    this.chart1.Series["Liabilities"].Points.AddXY(myreader["AMTTYPE"].ToString(), Convert.ToInt32(myreader["Amt"]));

                }
                myreader.Dispose();
                cmdqry.Dispose();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            showchart();
        }
    }
}