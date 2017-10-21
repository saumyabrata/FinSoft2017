using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FINNSOFT
{
    class clsSLDAL
    {
        string sql;
        public DataTable LoadValue(string Criteria, string Value)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd12 = new SqlCommand();
            SqlDataReader Sread;


            if (Value != "")
            {
                sql = "select Finyr,BrCode,GLID,SLID,SL_L_NAME,SLADD1,SLADD2,SLCITY,SLPIN,SLFAX,SLPHONE,SLCONT_PERS,REMARKS,OP_BAL,STATUS,GSTIN from TblSLMast Where " + Criteria + " = '" + Value + "' and finyr= '" + clsVar.finYr + "' order by SL_L_Name";
            }
            else
            {
                sql = "select Finyr,BrCode,GLID,SLID,SL_L_NAME,SLADD1,SLADD2,SLCITY,SLPIN,SLFAX,SLPHONE,SLCONT_PERS,REMARKS,OP_BAL,STATUS,GSTIN from TblSLMast Where " + Criteria + " Like '" + Value + "%' '" + clsVar.finYr + "' order by SL_L_Name";
            }


            cmd12.CommandText = sql;
            cmd12.Connection = clsConnection.ConnItem;
            Sread = cmd12.ExecuteReader();
            dt = (clsVar.ConvertToDataTable(Sread, 15));
            Sread.Close();
            cmd12.Dispose();
            return (dt);

        }
        public clsSLBO GetValue(string vSLID)
        {
            clsSLBO SlBO = new clsSLBO();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader Sread;
            //Double tmp_op_bal = 0;
            cmd.CommandText = "select Finyr,BrCode,GLID,SLID,SL_L_NAME,SLADD1,SLADD2,SLCITY,SLPIN,SLFAX,SLPHONE,SLCONT_PERS,REMARKS,OP_BAL,STATUS,GSTIN from TblSLMast Where SLID = '" + vSLID + "' and  finyr='" + Global.finyr + "'  and BrCode='"+ Global.branch +"' order by SL_L_Name";
            cmd.Connection = clsConnection.Conn;
            Sread = cmd.ExecuteReader();
            while (Sread.Read())
            {
                if (!Sread.IsDBNull(0))
                {
                    //tmp_op_bal = Convert.ToDouble(string.IsNullOrEmpty(Sread.GetValue(13).ToString()) ? 0 : Sread.GetValue(13));
                    SlBO.SetValue(Convert.ToString(Sread.GetValue(0)), Convert.ToString(Sread.GetValue(1)), Convert.ToString(Sread.GetValue(2)), Convert.ToString(Sread.GetValue(3)), Convert.ToString(Sread.GetValue(4)), Convert.ToString(Sread.GetValue(5)), Convert.ToString(Sread.GetValue(6)), Convert.ToString(Sread.GetValue(7)), Convert.ToString(Sread.GetValue(8)), Convert.ToString(Sread.GetValue(9)), Convert.ToString(Sread.GetValue(10)), Convert.ToString(Sread.GetValue(11)), Convert.ToString(Sread.GetValue(12)), Convert.ToDouble(string.IsNullOrEmpty(Sread.GetValue(13).ToString()) ? 0 : Sread.GetValue(13)), Convert.ToString(Sread.GetValue(14)), Convert.ToString(Sread.GetValue(15)));

                }
            }
            Sread.Close();
            cmd.Dispose();
            return (SlBO);
        }
        public DataTable GetSL(string vGLID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader Sread;
            cmd.CommandText = "select SLID,SL_L_NAME from TblSLMast Where GLID = '" + vGLID + "' and  finyr='" + Global.finyr + "' and BrCode='"+  Global.branch +"' order by GL_L_Name";
            cmd.Connection = clsConnection.Conn;
            Sread = cmd.ExecuteReader();
            dt = (clsVar.ConvertToDataTable(Sread, 2));
            Sread.Close();
            cmd.Dispose();
            return (dt);
        }
        public String SaveData(clsSLBO SlBO)
        {
            SqlCommand cmd = new SqlCommand();
            String Result;
            cmd.CommandText = "Insert into TblSLMast(Finyr,BrCode,GLID,SLID,SL_L_NAME,SLADD1,SLADD2,SLCITY,SLPIN,SLFAX,SLPHONE,SLCONT_PERS,REMARKS,OP_BAL,STATUS,GSTIN)" +
                "Values(@Finyr,@BrCode,@GLID,@SLID,@SL_L_NAME,@SLADD1,@SLADD2,@SLCITY,@SLPIN,@SLFAX,@SLPHONE,@SLCONT_PERS,@REMARKS,@OP_BAL,@STATUS,@GSTIN)";
            cmd.Connection = clsConnection.Conn;
            try
            {
                cmd.Parameters.AddWithValue("@Finyr", SlBO.Getfinyr());
                cmd.Parameters.AddWithValue("@BrCode", SlBO.GetBrCode());
                cmd.Parameters.AddWithValue("@GLID", SlBO.GetGLID());
                cmd.Parameters.AddWithValue("@SLID", SlBO.GetSLID());
                cmd.Parameters.AddWithValue("@SL_L_NAME", SlBO.GetSL_L_NAME());
                cmd.Parameters.AddWithValue("@SLADD1", SlBO.GetSLADD1());
                cmd.Parameters.AddWithValue("@SLADD2", SlBO.GetSLADD2());
                cmd.Parameters.AddWithValue("@SLCITY", SlBO.GetSLCITY());
                cmd.Parameters.AddWithValue("@SLPIN", SlBO.GetSLPIN());
                cmd.Parameters.AddWithValue("@SLFAX", SlBO.GetSLFAX());
                cmd.Parameters.AddWithValue("@SLPHONE", SlBO.GetSLPHONE());
                cmd.Parameters.AddWithValue("@SLCONT_PERS", SlBO.GetSLCONT_PERS());
                cmd.Parameters.AddWithValue("@REMARKS", SlBO.GetREMARKS());
                cmd.Parameters.AddWithValue("@op_bal", SlBO.Getop_bal());
                cmd.Parameters.AddWithValue("@STATUS", SlBO.GetSTATUS());
                cmd.Parameters.AddWithValue("@GSTIN", SlBO.Getgstin());
                cmd.ExecuteNonQuery();
                Result = "Record Inserted Successfully";
            }
            catch (Exception)
            {
                //Logger.LogInfo(ex);
                //Logger.LogInfo("Patient Registration failed");
                Result = "Please check all Inputs";
            }
            cmd.Dispose();
            return (Result);
        }
        public string UpdateData(clsSLBO SlBO)
        {
            SqlCommand cmd = new SqlCommand();
            String Result;
            cmd.CommandText = "Update TblSLMast set SL_L_NAME=@SL_L_NAME,SLADD1=@SLADD1,SLADD2=@SLADD2,SLCITY=@SLCITY,SLPIN=@SLPIN,SLFAX=@SLFAX,SLPHONE=@SLPHONE,SLCONT_PERS=@SLCONT_PERS,REMARKS=@REMARKS,OP_BAL=@OP_BAL,STATUS=@STATUS,GSTIN=@GSTIN where SLID = '" + SlBO.GetSLID() + "' and BrCode='"+ Global.branch +"' and finyr='"+ Global.finyr +"'";

            cmd.Connection = clsConnection.Conn;
            try
            {
                cmd.Parameters.AddWithValue("@Finyr", SlBO.Getfinyr());
                cmd.Parameters.AddWithValue("@BrCode", SlBO.GetBrCode());
                cmd.Parameters.AddWithValue("@GLID", SlBO.GetGLID());
                cmd.Parameters.AddWithValue("@SLID", SlBO.GetSLID());
                cmd.Parameters.AddWithValue("@SL_L_NAME", SlBO.GetSL_L_NAME());
                cmd.Parameters.AddWithValue("@SLADD1", SlBO.GetSLADD1());
                cmd.Parameters.AddWithValue("@SLADD2", SlBO.GetSLADD2());
                cmd.Parameters.AddWithValue("@SLCITY", SlBO.GetSLCITY());
                cmd.Parameters.AddWithValue("@SLPIN", SlBO.GetSLPIN());
                cmd.Parameters.AddWithValue("@SLFAX", SlBO.GetSLFAX());
                cmd.Parameters.AddWithValue("@SLPHONE", SlBO.GetSLPHONE());
                cmd.Parameters.AddWithValue("@SLCONT_PERS", SlBO.GetSLCONT_PERS());
                cmd.Parameters.AddWithValue("@REMARKS", SlBO.GetREMARKS());
                cmd.Parameters.AddWithValue("@op_bal", SlBO.Getop_bal());
                cmd.Parameters.AddWithValue("@STATUS", SlBO.GetSTATUS());
                cmd.Parameters.AddWithValue("@GSTIN", SlBO.Getgstin());
                cmd.ExecuteNonQuery();
                Result = "Record Updated Successfully";
            }

            catch (Exception ex)
            {
                Result = ex.Message;
            }
            cmd.Dispose();
            return (Result);
        }
    }
}
