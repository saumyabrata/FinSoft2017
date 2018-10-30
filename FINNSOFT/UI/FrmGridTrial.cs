using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FINNSOFT.UI
{
    public partial class FrmGridTrial : MetroFramework.Forms.MetroForm
    {
        public FrmGridTrial()
        {
            InitializeComponent();
        }

        private void FrmGridTrial_Load(object sender, EventArgs e)
        {
            String strConnection = Properties.Settings.Default.BooksConnectionString;
            SqlConnection con = new SqlConnection(strConnection);

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = con;
            sqlCmd.CommandType = CommandType.Text;
            string qry = "select BrCode,glid,gl_l_name as 'General Ledger Name',slid,sl_l_name as 'Sub Ledger Name','opening_balance'= " +
                 "CASE WHEN op_dr_bal > 0 THEN op_dr_bal " +
                 "WHEN op_cr_bal> 0 THEN op_cr_bal*-1 " +
                 "ELSE 0 " +
                 "END," +
                 "DR_BAL as 'Debit',CR_BAL as 'Credit',(op_dr_bal + DR_BAL - op_cr_bal - CR_BAL) as 'Closing Balance',spid " +
                 "from TblRPTLEDGER ORDER BY brcode,gl_l_name";
            sqlCmd.CommandText = "Select * from titles";
            SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

            DataTable dtRecord = new DataTable();
            sqlDataAdap.Fill(dtRecord);
            dgTrial.DataSource = dtRecord;
        }
    }
}
