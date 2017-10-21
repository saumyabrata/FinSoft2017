using System;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace FINNSOFT.UI
{
    public partial class frmBackupRestore : Form
    {
        public frmBackupRestore()
        {
            InitializeComponent();
        }

        private void frmBackupRestore_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               string command = "Backup database [FINSOFT] TO DISK= '"+ saveFileDialog1.FileName + "' WITH FORMAT,  NAME = N'data-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
               SqlCommand  backup = new SqlCommand(command, clsConnection.Conn);
               backup.ExecuteNonQuery();
               backup.CommandTimeout = 10000;

                MessageBox.Show("Finsoft Backup done successfully");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            saveFileDialog1.Filter = "Text files (*.bak)|*.bak|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string command = "Restore database [FINSOFT] FROM DISK= '" + saveFileDialog1.FileName + "' WITH  FILE = 1,  NOUNLOAD,  REPLACE,  STATS = 10";
                SqlCommand restore = new SqlCommand(command, clsConnection.Conn);
                restore.ExecuteNonQuery();
                restore.CommandTimeout = 10000;

                MessageBox.Show("Finsoft Restored successfully");

            }

        }
    }
}
