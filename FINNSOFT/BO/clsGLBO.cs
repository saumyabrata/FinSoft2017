using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FINNSOFT
{
    class clsGLBO
    {
        public String finyr = "";
        public String BrCode = "";
        public String GLID = "";
        public String GL_L_Name = "";
        public Int32 PL_BS_ID = 0;
        public Int32 ANYSL = 0;
        public Double op_bal = 0;
        public String Narration = "";
        public void SetValue(string vfinyr, string vBrCode, string vGLID, string vGL_L_NAME, Int32 vPL_BS_ID, Int32 vANYSL, Double vop_bal, string narr)
        {
            finyr = vfinyr;
            BrCode = vBrCode;
            GLID = vGLID;
            GL_L_Name = vGL_L_NAME;
            PL_BS_ID = vPL_BS_ID;
            ANYSL = vANYSL;
            op_bal = vop_bal;
            Narration = narr;
        }

        public String Getfinyr()
        {
            return (finyr);
        }
        public String GetBrCode()
        {
            return (BrCode);
        }
        public String GetGLID()
        {
            return (GLID);
        }
        public String GetGL_L_NAME()
        {
            return (GL_L_Name);
        }
        public Int32 GetPL_BS_ID()
        {
            return (PL_BS_ID);
        }
        public Int32 GetANYSL()
        {
            return (ANYSL);
        }
        public Double Getop_bal()
        {
            return (op_bal);
        }

        public string GetNarration()
        {
            return Narration;
        }

    }
}
