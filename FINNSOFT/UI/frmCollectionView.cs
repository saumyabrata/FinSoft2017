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
    public partial class frmCollectionView : Form
    {
        public frmCollectionView()
        {
            InitializeComponent();
        }

        private void filldata()
        {

            int Cashdrbal = 0;
            int Cashcrbal = 0;

            int Cqhdrbal = 0;
            int cqhcrbal = 0;

            int CChdrbal = 0;
            int cChcrbal = 0;

            int accdrbal = 0;
            int acccrbal = 0;

            int cashOPBal = 0;
            int ChqQPBal = 0;
            int CCHOPBal = 0;
            int AdvcOPBal = 0;

            //cash in hand.

            SqlDataReader Sread = null;
            string qry7 = "select op_bal from tblslmast where glid='00001' and slid='173' and brcode='"+ Global.branch +"'";

            SqlCommand cmd7 = new SqlCommand(qry7, clsConnection.Conn);
            try
            {
                Sread = cmd7.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        cashOPBal = Convert.ToInt32(Sread.GetValue(0));

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
                cmd7.Dispose();
            }

            //SqlDataReader Sread = null;
            string qry = "select sum(b.amt) as CashDrBl from tblvoucher a, tblledger b where a.BrCode=b.brcode";
            qry = qry + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") +"' and b.brcode = '" + Global.branch + "' and(b.glid = '00001' and b.slid = '173') and b.AMTTYPE = 'D'";
           
            SqlCommand cmd = new SqlCommand(qry, clsConnection.Conn);
            try
            {
                Sread = cmd.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Cashdrbal = Convert.ToInt32(Sread.GetValue(0));
                        
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

            string qry1 ="select sum(b.amt) as CashCrBl from tblvoucher a, tblledger b where a.BrCode = b.brcode";
            qry1 = qry1 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' and b.brcode ='" + Global.branch + "' and(b.glid = '00001' and b.slid = '173') and b.AMTTYPE = 'C'";

            SqlCommand cmd1 = new SqlCommand(qry1, clsConnection.Conn);
            try
            {
                Sread = cmd1.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Cashcrbal = Convert.ToInt32(Sread.GetValue(0));
                      
                    }
                }

               int curbal = cashOPBal + (Cashdrbal - Cashcrbal);

                label6.Text = "Current Cash Balance Rs:  " + Convert.ToString(curbal);
            }

   

            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
               
            }
            finally
            {
                Sread.Close();
                cmd1.Dispose();
            }

            // Cheques in hand

            string qry8 = "select op_bal from tblslmast where glid='00001' and slid='191' and brcode='" + Global.branch + "'";

            SqlCommand cmd8 = new SqlCommand(qry8, clsConnection.Conn);
            try
            {
                Sread = cmd8.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        ChqQPBal = Convert.ToInt32(Sread.GetValue(0));

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
                cmd8.Dispose();
            }


            string qry2 = "select sum(b.amt) as CChqDrBl from tblvoucher a, tblledger b where a.BrCode=b.brcode";
            qry2 = qry2 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' and b.brcode = '" + Global.branch + "' and(b.glid = '00001' and b.slid = '191') and b.AMTTYPE = 'D'";
           
            SqlCommand cmd2 = new SqlCommand(qry2, clsConnection.Conn);
            try
            {
                Sread = cmd2.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        Cqhdrbal = Convert.ToInt32(Sread.GetValue(0));
                        
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
                cmd2.Dispose();
            }

            string qry3 ="select sum(b.amt) as CCHqCrBl from tblvoucher a, tblledger b where a.BrCode = b.brcode";
            qry3 = qry3 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'  and b.brcode = '" + Global.branch + "' and(b.glid = '00001' and b.slid = '191') and b.AMTTYPE = 'C'";

            SqlCommand cmd3 = new SqlCommand(qry3, clsConnection.Conn);
            try
            {
                Sread = cmd3.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        cqhcrbal = Convert.ToInt32(Sread.GetValue(0));
                      
                    }
                }

               int curbal = ChqQPBal + (Cqhdrbal - cqhcrbal);

                label7.Text = "Current Cheques in Hand Rs:  " + Convert.ToString(curbal);
            }

   

            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
               
            }
            finally
            {
                Sread.Close();
                cmd3.Dispose();
            }

            // Credit card in hand.

            string qry9 = "select op_bal from tblslmast where glid='00001' and slid='192' and brcode='" + Global.branch + "'";

            SqlCommand cmd9 = new SqlCommand(qry9, clsConnection.Conn);
            try
            {
                Sread = cmd9.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        CCHOPBal = Convert.ToInt32(Sread.GetValue(0));

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
                cmd9.Dispose();
            }


            string qry4 = "select sum(b.amt) as CCardDrBl from tblvoucher a, tblledger b where a.BrCode=b.brcode";
            qry4 = qry4 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'  and b.brcode = '" + Global.branch + "' and(b.glid = '00001' and b.slid = '192') and b.AMTTYPE = 'D'";
           
            SqlCommand cmd4 = new SqlCommand(qry4, clsConnection.Conn);
            try
            {
                Sread = cmd4.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        CChdrbal = Convert.ToInt32(Sread.GetValue(0));
                        
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
                cmd4.Dispose();
            }

            string qry6 = "select sum(b.amt) as CCardCrBl from tblvoucher a, tblledger b where a.BrCode = b.brcode";
            qry6 = qry6 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'  and b.brcode = '" + Global.branch + "' and(b.glid = '00001' and b.slid = '192') and b.AMTTYPE = 'C'";

            SqlCommand cmd6 = new SqlCommand(qry6, clsConnection.Conn);
            try
            {
                Sread = cmd6.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        cChcrbal = Convert.ToInt32(Sread.GetValue(0));
                      
                    }
                }

               int curbal = CCHOPBal + (CChdrbal - cChcrbal);

                label8.Text = "Current Credit Card in Hand Rs:  " + Convert.ToString(curbal);
            }

   

            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
               
            }
            finally
            {
                Sread.Close();
                cmd6.Dispose();
            }

            // Advance from customer

            string qry10 = "select op_bal from tblslmast where glid='6' and slid='139' and brcode='" + Global.branch + "'";

            SqlCommand cmd10 = new SqlCommand(qry10, clsConnection.Conn);
            try
            {
                Sread = cmd10.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        AdvcOPBal = Convert.ToInt32(Sread.GetValue(0));

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
                cmd9.Dispose();
            }


            string qry11 = "select sum(b.amt) as CCardDrBl from tblvoucher a, tblledger b where a.BrCode=b.brcode";
            qry11 = qry11 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'  and b.brcode = '" + Global.branch + "' and(b.glid = '6' and b.slid = '139') and b.AMTTYPE = 'D'";

            SqlCommand cmd11 = new SqlCommand(qry11, clsConnection.Conn);
            try
            {
                Sread = cmd10.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        accdrbal = Convert.ToInt32(Sread.GetValue(0));

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
                cmd10.Dispose();
            }

            string qry12 = "select sum(b.amt) as CCardCrBl from tblvoucher a, tblledger b where a.BrCode = b.brcode";
            qry12 = qry12 + " and a.TRANTYPE = b.TRANTYPE and a.vno = b.vno and a.vdt = '" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "'  and b.brcode = '" + Global.branch + "' and(b.glid = '6' and b.slid = '139') and b.AMTTYPE = 'C'";

            SqlCommand cmd12 = new SqlCommand(qry12, clsConnection.Conn);
            try
            {
                Sread = cmd12.ExecuteReader();
                while (Sread.Read())
                {
                    if (!Sread.IsDBNull(0))
                    {
                        acccrbal = Convert.ToInt32(Sread.GetValue(0));

                    }
                }

                int curbal = AdvcOPBal + (accdrbal - acccrbal);

                label9.Text = "Current Advance from Customer Balance Rs:  " + Convert.ToString(curbal);
            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 

            }
            finally
            {
                Sread.Close();
                cmd12.Dispose();
            }
        }

        private void frmCollectionView_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Text = System.DateTime.Today.ToString("dd/MM/yyyy");

            filldata();

        }
    }
}
