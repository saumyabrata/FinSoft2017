using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace FINNSOFT
{
    class clsGLBAL
    {

        clsGLBO GLBO = new clsGLBO();
        clsGLDAL GLDAL = new clsGLDAL();

        public DataTable GetGL(string crt, string val)
        {
            return (GLDAL.LoadValue(crt, val));
        }
        public clsGLBO GetValue(string Code)
        {
            return (GLDAL.GetValue(Code));
        }
        public string SaveData(string vfinyr, string vBrCode, string vGlID, string vGL_L_Name, Int32 vPL_BS_ID, Int32 vANYSL, double vop_bal, string narr)
        {
            GLBO.SetValue(vfinyr, vBrCode, vGlID, vGL_L_Name, vPL_BS_ID, vANYSL, vop_bal, narr);
            return (GLDAL.SaveData(GLBO));
        }
        public string UpdateData(string vfinyr, string vBrCode, string vGlID, string vGL_L_Name, Int32 vPL_BS_ID, Int32 vANYSL, double vop_bal, string narr)
        {
            GLBO.SetValue(vfinyr, vBrCode, vGlID, vGL_L_Name, vPL_BS_ID, vANYSL, vop_bal, narr);
            return (GLDAL.UpdateData(GLBO));
        }
    }
}
