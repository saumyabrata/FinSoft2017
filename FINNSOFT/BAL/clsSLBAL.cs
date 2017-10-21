using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FINNSOFT
{
    class clsSLBAL
    {
        clsSLBO SLBO = new clsSLBO();
        clsSLDAL SLDAL = new clsSLDAL();
        public DataTable LoadSLValue(string crt, string val)
        {
            return (SLDAL.LoadValue(crt, val));
        }
        public clsSLBO GetValue(string Code)
        {
            return (SLDAL.GetValue(Code));
        }
        public DataTable GetSL(string vGLID)
        {
            return (SLDAL.GetSL(vGLID));
        }
        public string SaveData(string vfinyr, string vBrCode, string vGLID, string vSLID, string vSL_L_NAME, string vSLADD1, string vSLADD2, string vSLCITY, string vSLPIN, string vSLFAX, string vSLPHONE, string vSLCONT_PERS, string vREMARKS, Double vop_bal, string vSTATUS, string gstin)
        {
            SLBO.SetValue( vfinyr,  vBrCode,  vGLID,  vSLID,  vSL_L_NAME,  vSLADD1,  vSLADD2,  vSLCITY,  vSLPIN,  vSLFAX,  vSLPHONE,  vSLCONT_PERS,  vREMARKS,  vop_bal,  vSTATUS, gstin);
            return (SLDAL.SaveData(SLBO));
        }
        public string UpdateData(string vfinyr, string vBrCode, string vGLID, string vSLID, string vSL_L_NAME, string vSLADD1, string vSLADD2, string vSLCITY, string vSLPIN, string vSLFAX, string vSLPHONE, string vSLCONT_PERS, string vREMARKS, Double vop_bal, string vSTATUS, string gstin)
        {
            SLBO.SetValue( vfinyr,  vBrCode,  vGLID,  vSLID,  vSL_L_NAME,  vSLADD1,  vSLADD2,  vSLCITY,  vSLPIN,  vSLFAX,  vSLPHONE,  vSLCONT_PERS,  vREMARKS,  vop_bal,  vSTATUS, gstin);
            return (SLDAL.UpdateData(SLBO));
        }
    }
}
