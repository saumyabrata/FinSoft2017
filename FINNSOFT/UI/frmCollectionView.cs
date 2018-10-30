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

namespace FINNSOFT.UI
{
    public partial class frmCollectionView : MetroFramework.Forms.MetroForm
    {
        public frmCollectionView()
        {
            InitializeComponent();
        }

        private void filldata()
        {
            SqlCommand command1 = new SqlCommand("pCollectionView", clsConnection.Conn);
            command1.CommandType = CommandType.StoredProcedure;
            command1.Parameters.Add(new SqlParameter("@cash_curbal", SqlDbType.Decimal,0, ParameterDirection.Output, false,10,2, "@cash_curbal", DataRowVersion.Default, null));
            command1.Parameters.Add(new SqlParameter("@chq_curbal", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 2, "@chq_curbal", DataRowVersion.Default, null));
            command1.Parameters.Add(new SqlParameter("@card_curbal", SqlDbType.Decimal, 0, ParameterDirection.Output, false,10, 2, "@card_curbal", DataRowVersion.Default, null));
            command1.Parameters.Add(new SqlParameter("@cust_curbal", SqlDbType.Decimal, 0, ParameterDirection.Output, false, 10, 2, "@cust_curbal", DataRowVersion.Default, null));

            command1.Parameters.Add(new SqlParameter("@targetdate", SqlDbType.DateTime, 0, "VDT"));
            command1.Parameters.Add(new SqlParameter("@mbrcode", SqlDbType.Char, 0, "BrCode"));
            command1.Parameters["@targetdate"].Value = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            command1.Parameters["@mbrcode"].Value = Global.branch.Trim();
            command1.UpdatedRowSource = UpdateRowSource.OutputParameters;
            command1.ExecuteNonQuery();
            metroTile1.Text = command1.Parameters["@cash_curbal"].Value.ToString();
            metroTile2.Text = command1.Parameters["@chq_curbal"].Value.ToString();
            metroTile3.Text = command1.Parameters["@card_curbal"].Value.ToString();
            metroTile4.Text = command1.Parameters["@cust_curbal"].Value.ToString();

        }

        private void frmCollectionView_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Text = System.DateTime.Today.ToString("dd/MM/yyyy");

            //filldata();
            metroTile1.Text = "";
            metroTile2.Text = "";
            metroTile3.Text = "";
            metroTile4.Text = "";

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //filldata();
        }

        private void BttnShow_Click(object sender, EventArgs e)
        {
            filldata();
        }
    }
}
