using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace FINNSOFT
{
    class clsVar
    {
        public static int UID = 0;
        public static string SelectedIC = "";
        public static string ModuleName = "";
        public static string CashMemoRet = "";
        public static string finYr = Global.finyr;//"2012-2013";
        public static string[] consCode;
        public static AutoCompleteStringCollection autoConsName;
        public static AutoCompleteStringCollection autoConsCode;
        public static AutoCompleteStringCollection autoGroupCode;

        public static AutoCompleteStringCollection autoGroupName;
        public static AutoCompleteStringCollection autoItemCode;
        public static AutoCompleteStringCollection autoItemName;
        public static AutoCompleteStringCollection autoUOMCode;
        public static AutoCompleteStringCollection autoUOMName;
        public static AutoCompleteStringCollection autoMfgCode;
        public static AutoCompleteStringCollection autoMfgName;
        public static AutoCompleteStringCollection autoMfgShortName;
        public static AutoCompleteStringCollection autoPatName;
        public static AutoCompleteStringCollection autoPatCode;
        public static AutoCompleteStringCollection autoCMNO;
        public static AutoCompleteStringCollection autoSuppName;
        public static AutoCompleteStringCollection autoSuppCode;
        public static AutoCompleteStringCollection autobatchcode;




        public static void txt_key_event(ref TextBox txt, ref TextBox prev, ref TextBox next, string keyCode, double type, ref Button pb, ref Button nb, double prevCtrl, double nxtCtrl)
        {
            //txt.Text = txt.Text.Trim();

            if (txt.Text.Trim() == "")
            {
                txt.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                txt.BackColor = System.Drawing.Color.White;
            }

            if (prevCtrl == 1 && nxtCtrl == 1) // Textbox,Textbox
            {
                if (keyCode == "Return")
                {
                    Validation(ref txt, type);
                    next.Focus();
                }
                //else if (keyCode == "Down")
                //{
                //    Validation(ref txt, type);
                //    next.Focus();
                //}
                //else if (keyCode == "Up")
                //{
                //    Validation(ref txt, type);
                //    prev.Focus();
                //}

            }
            else if (prevCtrl == 1 && nxtCtrl == 2)//Textbox,Button
            {
                if (keyCode == "Return")
                {
                    Validation(ref txt, type);
                    nb.Focus();
                }
                //else if (keyCode == "Down")
                //{
                //    Validation(ref txt, type);
                //    nb.Focus();
                //}
                //else if (keyCode == "Up")
                //{
                //    Validation(ref txt, type);
                //    prev.Focus();
                //}
            }
            else if (prevCtrl == 2 && nxtCtrl == 1)//Button,Textbox
            {
                if (keyCode == "Return")
                {
                    Validation(ref txt, type);
                    next.Focus();
                }
                //else if (keyCode == "Down")
                //{
                //    Validation(ref txt, type);
                //    next.Focus();
                //}
                //else if (keyCode == "Up")
                //{
                //    Validation(ref txt, type);
                //    pb.Focus();
                //}
            }
            else if (prevCtrl == 2 && nxtCtrl == 2)//Button,Button
            {
                if (keyCode == "Return")
                {
                    Validation(ref txt, type);
                    nb.Focus();
                }
                //else if (keyCode == "Down")
                //{
                //    Validation(ref txt, type);
                //    nb.Focus();
                //}
                //else if (keyCode == "Up")
                //{
                //    Validation(ref txt, type);
                //    pb.Focus();
                //}
            }
            else if (prevCtrl == 1 && nxtCtrl == 3)//Textbox,Combobox
            {
            }
            else if (prevCtrl == 3 && nxtCtrl == 1)//Combobox,Textbox
            {
            }
            else if (prevCtrl == 2 && nxtCtrl == 3)//Button,Combobox
            {
            }
            else if (prevCtrl == 3 && nxtCtrl == 2)//Combobox,Button
            {
            }
            else if (prevCtrl == 3 && nxtCtrl == 3)//Combobox,Combobox
            {
            }
        }
        public static void Validation(ref TextBox txt, double inp)
        {
            if (inp == 1) // String
            {

            }
            else if (inp == 2) //Neumeric
            {
                try
                {
                    Convert.ToDouble(txt.Text);
                }
                catch (Exception)
                {
                    txt.BackColor = System.Drawing.Color.Red;
                    txt.Text = "0.00";
                }
            }

        }
        public static DataTable ConvertToDataTable(SqlDataReader Sread, int cols)
        {
            DataTable dt = new DataTable("DataTable1");
            for (int i = 0; i < cols; i++)
            {
                dt.Columns.Add("Column" + i);
            }
            while (Sread.Read())
            {
                if (!Sread.IsDBNull(0))
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < cols; i++)
                    {
                        dr[i] = (Sread.GetValue(i));
                    }
                    dt.Rows.Add(dr);
                }
            }
            Sread.Close();
            return (dt);
        }
        public static void txt_key_event_Combobox(ref TextBox txt, ref ComboBox cmb, string keyCode, double type)
        {
            if (txt.Text.Trim() == "")
            {
                txt.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                txt.BackColor = System.Drawing.Color.White;
            }

            if (keyCode == "Return")
            {
                Validation(ref txt, type);
                cmb.Focus();
            }

        }
        public static void cmb_key_event_TextBox(ref ComboBox cmb, ref TextBox txt, string keyCode)
        {
            if (keyCode == "Return")
            {
                txt.Focus();
            }

        }
        public static void cmb_key_event_cmb(ref ComboBox cmb, ref ComboBox cmbNext, string keyCode)
        {
            if (keyCode == "Return")
            {
                cmbNext.Focus();
            }

        }
        public static void cmb_key_event_btn(ref ComboBox cmb, ref Button btnNext, string keyCode)
        {
            if (keyCode == "Return")
            {
                btnNext.Focus();
            }

        }
        public static void txt_key_event_Mask(ref TextBox cmb, ref MaskedTextBox btnMsk, string keyCode, double type)
        {
            if (cmb.Text.Trim() == "")
            {
                cmb.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                cmb.BackColor = System.Drawing.Color.White;
            }

            if (keyCode == "Return")
            {
                Validation(ref cmb, type);
                btnMsk.Focus();
            }
        }

        public static void Msk_key_event_cmb(ref MaskedTextBox cmb, ref ComboBox btnNext, string keyCode)
        {
            if (keyCode == "Return")
            {
                btnNext.Focus();
            }
        }

        public static void Msk_key_event_Txt(ref MaskedTextBox cmb, ref TextBox btnNext, string keyCode)
        {
            if (keyCode == "Return")
            {
                btnNext.Focus();
            }
        }

        public static void txt_key_event_Mask_txt(ref TextBox cmb, ref TextBox btnMsk, string keyCode, double type)
        {
            if (cmb.Text.Trim() == "")
            {
                cmb.BackColor = System.Drawing.Color.Yellow;
            }
            else
            {
                cmb.BackColor = System.Drawing.Color.White;
            }

            if (keyCode == "Return")
            {
                Validation(ref cmb, type);
                btnMsk.Focus();
            }
        }

        public static Boolean DateValidation(string value)
        {
            try
            {
                Convert.ToDateTime(value);
                return (true);
            }
            catch (Exception)
            {
                return (false);
            }
        }
        public static Boolean NumberValidation(string value)
        {
            try
            {
                Convert.ToDouble(value);
                return (true);
            }
            catch (Exception)
            {
                return (false);
            }
        }

        
        

        public static string ChangeDt(string dt)
        {
            string newDt;
            string[] dm = dt.Split('/');
            newDt = dm[0] + "/" + dm[1] + "/" + dm[2];

            return newDt;
        }

        public static string ChangeDt2(string dt)
        {
            string newDt;
            string[] dm = dt.Split('-');
            newDt = dm[1] + "/" + dm[0] + "/" + dm[2];

            return newDt;
        }

        public static string ValidateString(string str)
        {
            str = str.Replace("'", "''");
            return str;
        }
        
        public static double LastNo(string mtable , string mfield ,Int32 mTrim  )
        {
            double mem=0;
            SqlCommand cmd = new SqlCommand();       
            cmd.CommandText =  "Select max(abs(right(" + mfield + " , " + mTrim + " ))) from  "+ mtable + " ";
            cmd.Connection = clsConnection.Conn;

            mem = Convert.ToDouble(cmd.ExecuteScalar());
            return mem;

        }

    }
}
