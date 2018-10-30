using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Data;


namespace FINNSOFT
{
    class clsConnection
    {
        public static SqlConnection Conn;
        public static SqlConnection Conn1;
        public static SqlConnection ConnItm;
        public static SqlConnection ConnUpdt;
       // public static SqlConnection Conn1;
        public static SqlConnection ConnStk;
        public static SqlConnection ConnItem;
        public static SqlConnection ConnCMNo;
        public static SqlConnection ConnExp;
        public static SqlConnection ConnRetStk;
        public static String Excp = string.Empty;
        public static String InsName = string.Empty;
        public static String AuthMode = string.Empty;
        public static String UID = string.Empty;
        public static String Pwd = string.Empty;
        public static String DbName = string.Empty;

        public static void GlobCon()
        {
            GetConParam();
            try
            {
                Conn = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    Conn.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;Asynchronous Processing=true;MultipleActiveResultSets=true";

                }
                else
                {
                    Conn.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + "; Asynchronous Processing=true;MultipleActiveResultSets=true";
                }
                Conn.Open();
                setDBDateFormat();

                Conn1 = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    Conn1.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    Conn1.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                Conn1.Open();

                ConnStk = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    ConnStk.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    ConnStk.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                ConnStk.Open();

                ConnItem = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    ConnItem.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    ConnItem.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                ConnItem.Open();

                ConnCMNo = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    ConnCMNo.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    ConnCMNo.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                ConnCMNo.Open();

                ConnExp = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    ConnExp.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    ConnExp.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                ConnExp.Open();

                ConnItm = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    ConnItm.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    ConnItm.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                ConnItm.Open();

                ConnUpdt = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    ConnUpdt.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    ConnUpdt.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                ConnUpdt.Open();

                ConnRetStk = new SqlConnection();
                if (AuthMode == "Windows Authentication")
                {
                    ConnRetStk.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";Integrated Security=True;";
                }
                else
                {
                    ConnRetStk.ConnectionString = "Data Source=" + InsName + ";Initial Catalog=" + DbName + ";UID=" + UID + ";password=" + Pwd + ";";
                }
                ConnRetStk.Open();
               
                Excp = "";
                setDBDateFormat();
            }
            catch (Exception ex)
            {
                Excp = ex.Message;
            }

        }
        public static String GetConParam()
        {
            try
            {
                XmlTextReader textReader = new XmlTextReader("Config.xml");
                String _ElementName = string.Empty;
                while (textReader.Read())
                {
                    XmlNodeType nType = textReader.NodeType;

                    if (nType == XmlNodeType.Element)
                    {
                        //MessageBox.Show("" + textReader.Name.ToString() + "-" + textReader.Value);
                        _ElementName = textReader.Name.ToString();

                    }
                    if (nType == XmlNodeType.Text)
                    {
                        // MessageBox.Show("" + textReader.Value);
                        //Console.WriteLine(textReader.Value);
                        if (_ElementName == "InstanceName")
                        {
                            InsName = textReader.Value;
                        }
                        if (_ElementName == "AuthenticationMode")
                        {
                            AuthMode = textReader.Value;
                        }
                        if (_ElementName == "UID")
                        {
                            UID = textReader.Value;
                        }
                        if (_ElementName == "PassWord")
                        {
                            Pwd = textReader.Value;
                        }
                        if (_ElementName == "DatabaseName")
                        {
                            DbName = textReader.Value;
                        }

                    }
                }
                textReader.Close();
                return ("");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public static void setDBDateFormat()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SET DATEFORMAT DMY";
            cmd.Connection = clsConnection.Conn;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
    }
}
