using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace FINNSOFT
{
    class Connection
    {
        SqlConnection _con;

        /// <summary>
        /// Opens the CNN.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public SqlConnection OpenCnn()
        {
            // 1- initialise connection
            String cnnStr = ConfigurationManager.AppSettings["sqlcon"];
            _con = new SqlConnection(cnnStr);
            //2- open connection
            if (_con.State == ConnectionState.Open)
                _con.Close();
            _con.Open();
            return _con;
        }

        /// <summary>
        /// Returns Connection class object for sqlAdapter class.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public SqlConnection AdapterCon()
        {
            String cnnStr = ConfigurationManager.AppSettings["sqlcon"];
            _con=new SqlConnection(cnnStr);
            return _con;
        }

        /// <summary>
        /// Closes the CNN.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public SqlConnection CloseCnn()
        {
            // Closing an Open Connection.
            _con.Close();
            return _con;
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="err">The err.</param>
        /// <remarks></remarks>
        public void ShowErrorMessage(string err)
        {
            MessageBox.Show(@"Error Occurred. Reason:"+err,@"FINSOFT",MessageBoxButtons.OK,MessageBoxIcon.Stop);
        }

        /// <summary>
        /// Shows the ok message.
        /// </summary>
        /// <param name="err">The err.</param>
        /// <remarks></remarks>
        public void ShowOkMessage(string err)
        {
            MessageBox.Show(err, @"FINSOFT", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows the confirm message.
        /// </summary>
        /// <param name="err">The err.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DialogResult ShowConfirmMessage(string err)
        {
            DialogResult result = MessageBox.Show(err, @"FINSOFT", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            return result;
        }
    }
}
