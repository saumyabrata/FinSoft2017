using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FINNSOFT
{
    class clsSLBO
    {
        public String finyr = "";
        public String BrCode = "";
        public String GLID = "";
        public String SLID = "";
        public String SL_L_NAME = "";
        public String SLADD1 = "";
        public String SLADD2 = "";
        public String SLCITY = "";
        public String SLPIN = "";
        public String SLFAX = "";
        public String SLPHONE = "";
        public String SLCONT_PERS = "";
        public String REMARKS = "";       
        public Double op_bal = 0;
        public String STATUS = "";
        public String GSTIN = "";
        public void SetValue(string vfinyr, string vBrCode, string vGLID, string vSLID, string vSL_L_NAME, string vSLADD1, string vSLADD2, string vSLCITY, string vSLPIN, string vSLFAX, string vSLPHONE, string vSLCONT_PERS, string vREMARKS, Double vop_bal, string vSTATUS, string vgstin)
        {
            finyr = vfinyr;
            BrCode = vBrCode;
            GLID = vGLID;
            SLID = vSLID;
            SL_L_NAME = vSL_L_NAME;
            SLADD1 = vSLADD1;
            SLADD2 = vSLADD2;
            SLCITY = vSLCITY;
            SLPIN = vSLPIN;
            SLFAX = vSLFAX;
            SLPHONE = vSLPHONE;
            SLCONT_PERS = vSLCONT_PERS;
            REMARKS = vREMARKS;
            op_bal = vop_bal;
            STATUS = vSTATUS;
            GSTIN = vgstin;
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
        public String GetSLID()
        {
            return (SLID);
        }
        public String GetSL_L_NAME()
        {
            return (SL_L_NAME);
        }
        public String GetSLADD1()
        {
            return (SLADD1);
        }
        public String GetSLADD2()
        {
            return (SLADD2);
        }
        public String GetSLCITY()
        {
            return (SLCITY);
        }
        public String GetSLPIN()
        {
            return (SLPIN);
        }
        public String GetSLFAX()
        {
            return (SLFAX);
        }
        public String GetSLPHONE()
        {
            return (SLPHONE);
        }
        public String GetSLCONT_PERS()
        {
            return (SLCONT_PERS);
        }
        public String GetREMARKS()
        {
            return (REMARKS);
        }
        public Double Getop_bal()
        {
            return (op_bal);
        }
        public String GetSTATUS()
        {
            return (STATUS);
        }
        public String Getgstin()
        {
            return (GSTIN);
        }
    }
}
