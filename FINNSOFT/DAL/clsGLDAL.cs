using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FINNSOFT
{
    class clsGLDAL
    {
        string sql;
        public DataTable LoadValue(string Criteria, string Value)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd12 = new SqlCommand();
            SqlDataReader Sread;


            if (Value != "")
            {
                sql = "select finyr,BrCode,GLID,GL_L_Name,PL_BS_ID, ANYSL,op_bal from TblGLMast Where " + Criteria + " = '" + Value + "' and finyr= '" + clsVar.finYr + "' order by GL_L_Name";
            }
            else
            {
                sql = "select finyr,BrCode,GLID,GL_L_Name,PL_BS_ID, ANYSL,op_bal from TblGLMast Where " + Criteria + " Like '" + Value + "%' '" + clsVar.finYr + "' order by GL_L_Name";
            }


            cmd12.CommandText = sql;
            cmd12.Connection = clsConnection.ConnItem;
            Sread = cmd12.ExecuteReader();
            dt = (clsVar.ConvertToDataTable(Sread, 7));
            Sread.Close();
            cmd12.Dispose();
            return (dt);

        }
        public clsGLBO GetValue(string vGLID)
        {
            clsGLBO GlBO = new clsGLBO();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader Sread;
            cmd.CommandText = "select finyr,BrCode,GLID,GL_L_Name,PL_BS_ID, ANYSL,op_bal,Narration from TblGLMast Where GLID = '" + vGLID + "' and  finyr='" + Global.finyr + "' order by GL_L_Name";
            cmd.Connection = clsConnection.Conn;
            Sread = cmd.ExecuteReader();
            while (Sread.Read())
            {
                if (!Sread.IsDBNull(0))
                {
                    GlBO.SetValue(Convert.ToString(Sread.GetValue(0)), Convert.ToString(Sread.GetValue(1)), Convert.ToString(Sread.GetValue(2)), Convert.ToString(Sread.GetValue(3)), Convert.ToInt32(Sread.GetValue(4)), Convert.ToInt32(Sread.GetValue(5)), Convert.ToDouble(Sread.GetValue(6)), Convert.ToString(Sread.GetValue(7)));

                }
            }
            Sread.Close();
            cmd.Dispose();
            return (GlBO);
        }
        public String SaveData(clsGLBO GlBO)
        {
            SqlCommand cmd = new SqlCommand();
            String Result;
            cmd.CommandText = "Insert into TblGLMast(finyr,BrCode, GLID, GL_L_NAME,PL_BS_ID,ANYSL,op_bal,Narration)" +
                "Values(@finyr,@BrCode, @GLID, @GL_L_NAME,@PL_BS_ID,@ANYSL,@op_bal,@Narration)";
            cmd.Connection = clsConnection.Conn;
            try
            {
                cmd.Parameters.AddWithValue("@finyr", GlBO.Getfinyr());
                cmd.Parameters.AddWithValue("@BrCode", GlBO.GetBrCode());
                cmd.Parameters.AddWithValue("@GLID", GlBO.GetGLID());
                cmd.Parameters.AddWithValue("@GL_L_NAME", GlBO.GetGL_L_NAME());
                cmd.Parameters.AddWithValue("@PL_BS_ID", GlBO.GetPL_BS_ID());
                cmd.Parameters.AddWithValue("@ANYSL", GlBO.GetANYSL());
                cmd.Parameters.AddWithValue("@op_bal", GlBO.Getop_bal());
                cmd.Parameters.AddWithValue("@Narration", GlBO.GetNarration());

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
        public string UpdateData(clsGLBO GlBO)
        {
            SqlCommand cmd = new SqlCommand();
            String Result;
            cmd.CommandText = "Update TblGLMast set GL_L_NAME=@GL_L_NAME,PL_BS_ID=@PL_BS_ID,ANYSL=@ANYSL, op_bal=@op_bal where GLID = '" + GlBO.GetGLID() + "'";

            cmd.Connection = clsConnection.Conn;
            try
            {
                cmd.Parameters.AddWithValue("@finyr", GlBO.Getfinyr());
                cmd.Parameters.AddWithValue("@BrCode", GlBO.GetBrCode());
                cmd.Parameters.AddWithValue("@GLID", GlBO.GetGLID());
                cmd.Parameters.AddWithValue("@GL_L_NAME", GlBO.GetGL_L_NAME());
                cmd.Parameters.AddWithValue("@PL_BS_ID", GlBO.GetPL_BS_ID());
                cmd.Parameters.AddWithValue("@ANYSL", GlBO.GetANYSL());
                cmd.Parameters.AddWithValue("@op_bal", GlBO.Getop_bal());

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
